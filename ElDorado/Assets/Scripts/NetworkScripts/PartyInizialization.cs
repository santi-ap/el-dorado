using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class PartyInizialization : MonoBehaviour
{
    private StateHandler stateHandler;
    public PartyModel partyModel;
    public PartyNetCode partyNetCode;
    public PlayerIconsList playerIconsList;
    public AssignRoleToPlayer assignRoleToPlayer;
    public GameObject fogOfWar;

    public List<RoleSO> playerRoleList;

    void Awake()
    {
        stateHandler = StateHandler.StateHandlerInstance;
        stateHandler.partyHasBeenInitializedEvent += OnPartyHasBeenInitialized;
    }

    public void Initialize()
    {
        if (NetworkClient.Instance.IsHost)
        {
            Debug.Log("getting players ");
            NetworkClient.Lobby.GetPlayersInRoom((successful, reply, error) =>
            {
                if (successful)
                {
                //Looping through players
                Debug.Log("Party initialization successful");
                    foreach (SWPlayer swPlayer in reply.players)
                    {
                        Debug.Log("Player: " + swPlayer.id + "\nCustomData: " + swPlayer.GetCustomDataString());
                        PlayerModel playerModel = new PlayerModel();
                        playerModel.setPlayerName(swPlayer.id.Split('-')[1]);
                        playerModel.SetPlayerId(swPlayer.id);
                        partyModel.playerList.Add(playerModel);
                    }
                //sets the roles to each player and returns the udpated player list
                partyModel.playerList = new AssignRoleToPlayer(this).assignRoleToNewPlayer();
                    partyNetCode.ModifyParty(partyModel);
                }
                else
                {
                    Debug.Log("Get Players was not successful.\n" + error);
                }
            });
        }

        
    }

    public void Test()
    {
        if (NetworkClient.Instance.IsHost)
        {
            partyModel.playerList[0].SetPlayerHealth(50);
            partyNetCode.ModifyParty(partyModel);
        }
    }

     public void OnPartyHasBeenInitialized() {
         //loop through players and if the player is a local guide, remove fog of war
        foreach (PlayerModel playerModel in partyModel.playerList) {
            if (playerModel.GetPlayerId() == NetworkClient.Instance.PlayerId && playerModel.GetPlayerRole().roleName == "Local Guide") {
                fogOfWar.SetActive(false);
            }
        }
     }   

    public void OnPartyModelModified(List<PlayerModel> party)
    {
        StateHandler stateHandler = StateHandler.StateHandlerInstance;

        Debug.Log("OnPartyModified");

        //If there are no player Icons init
        if (playerIconsList.playerIcons == null || playerIconsList.playerIcons.Count == 0)
        {
            foreach (PlayerModel p in party)
            {
                playerIconsList.AddIcon(p);
            }
            stateHandler.SetState(StateHandler.GameState.EatingState);
        }
        else
        {
            foreach (PlayerModel p in party)
            {
                playerIconsList.UpdateUI(p);
            }
        }
        this.partyModel.playerList = party;
        stateHandler.partyHasBeenInitializedEvent();
    }
}