using System;
using System.Collections;
using System.Collections.Generic;
using SWNetwork;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeFood : MonoBehaviour
{
    public PartyModel party;
    public PartyNetCode netCode;
    public Text FoodCountText;
    private Boolean isFirstTurn;
    public OutcomeUIHandler outcomeUIHandler;
    public PlayerDeathHandler playerDeathHandler;
    public NotificationNetcode notificationNetcode;
    public RoleIconInPartyHandler roleIconInPartyHandler;
    public GameOverHandler gameOverHandler;
    public InventoryNetCode inventoryNetCode;

    private StateHandler stateHandler;//declare a variable of type StateHandler

    public readonly int HEALTH_CONSTANT = 5;

    void Awake()
    {
        isFirstTurn = true;
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        stateHandler.eatingStateHasStartedEvent += Eating;
        playerDeathHandler = new PlayerDeathHandler();
        playerDeathHandler.notificationNetcode = this.notificationNetcode;
        playerDeathHandler.roleIconInPartyHandler = this.roleIconInPartyHandler;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Eating()
    {
        if (isFirstTurn) {
            isFirstTurn = false;
            return;
        }

        //First: We get all players from the party and 
        //for each player:
        //  Check if the enough food in the inventory to eat 
        //      If so, Eat and automatically regenerate health
        //      If not, lose life
        if (NetworkClient.Instance.IsHost)
        {
            foreach (PlayerModel player in party.playerList)
            {
                if (!player.isDead)
                {
                    if (this.CheckFood())
                    {
                        Eat(1);
                        RegenerateHealth(player, HEALTH_CONSTANT);
                    }
                    else
                    {
                        Debug.Log("There's not enough food");
                        LoseHealth(player);
                        // player.SetPlayerHealth(player.GetPlayerHealth() - HEALTH_CONSTANT);
                    }
                }
            }
            netCode.ModifyParty(this.party);
            inventoryNetCode.ModifyFoodCount((float)(this.party.inventory.foodCount));
        }
    }

    public bool CheckFood()
    {
        return !(party.inventory.foodCount <= 1);
    }

    public void Eat(int amountOfFood)
    {
        //TODO make multiplayer stuff work
        Debug.Log("You ate 2 food");
        
        party.inventory.foodCount -= amountOfFood;
        Debug.Log(party.inventory.foodCount);
        outcomeUIHandler.UpdateFoodCountUI(party.inventory.foodCount);
    }

    public void RegenerateHealth(PlayerModel player, int amountOfHealth)
    {
        int health = player.GetPlayerHealth();
        health += amountOfHealth;
        if (health > 100) health = 100;
        player.SetPlayerHealth(health);
    }

    public void LoseHealth(PlayerModel player)
    {
        int health = player.GetPlayerHealth();
        health -= HEALTH_CONSTANT;
        player.SetPlayerHealth(health);
        PlayerDeath(player);
        gameOverHandler.CheckPlayersDead(this.party);
    }

    /// <summary>
    /// Checks id player health is under 0 |
    /// If true, isDead = true
    /// </summary>
    /// <param name="p"></param>
    public void PlayerDeath(PlayerModel p)
    {
        if (p.GetPlayerHealth() <= 0)
        {
            playerDeathHandler.PlayerDeath(p);
        }
    }

}
