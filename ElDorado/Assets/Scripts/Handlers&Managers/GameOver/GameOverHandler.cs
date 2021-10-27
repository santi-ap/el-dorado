using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    public bool allPlayersDead = false;
    public GameOverNetcode gameOverNetcode;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Color gameOverColor;

    public void CheckPlayersDead(PartyModel party)
    {
        // int amountOfDeadPlayers = 0;
        
        foreach(PlayerModel player in party.playerList)
        {
            if(player.isDead)
            {
                // amountOfDeadPlayers++;
                allPlayersDead = true;
            }
            else{
                allPlayersDead = false;
                return;
            }
        }

        if(allPlayersDead)
        {
            gameOverNetcode.InvokeGameOverAsHostEvent();
        }
    }

    public void GameOver()
    {
        Debug.Log("Everyone died");
        gameOverPanel.SetActive(true);
        gameOverText.text = "GAME OVER! EVERYONE IN THE PARTY DIED!";
        gameOverText.color = gameOverColor;
    }
}
