using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndofGameHandler : MonoBehaviour
{
    public GameObject endOfGameUI;
    public GameObject gameUI;
    public LeaderBoardUIHandler leaderBoardUIHandler;
    
    public void ShowEndOfGame(){
        endOfGameUI.SetActive(true);
        gameUI.SetActive(false);
        leaderBoardUIHandler.HandleEndOfGameUI();
    }


}
