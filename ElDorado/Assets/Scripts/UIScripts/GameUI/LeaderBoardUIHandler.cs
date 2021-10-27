using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWNetwork;
using UnityEngine.SceneManagement;


public class LeaderBoardUIHandler : MonoBehaviour
{
    // The leaderboard UI
    public Text leaderboardName;
    public Text leaderboardScore;
    public Text leaderboardTeamPosition;
    public LeaderboardHandler lbHandler;
    RoomRemoteEventAgent roomRemoteEventAgent;

    void Start()
    {
        //check if the scene is the lobby
        if (!(SceneManager.GetActiveScene().name == "Lobby"))
        {
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
        } else {
            this.GetLeaderboard();
        }
    }

    public void HandleEndOfGameUI()
    {
        //check if the NetworkClient instance is the host
        if (NetworkClient.Instance.IsHost)
        {
            //but it should be done only when the host finished his process
            Debug.Log("LeaderboardUIHandler: HandleEndOfGameUI");
            lbHandler.CreateScore();
            Debug.Log("LeaderboardUIHandler: CreateScore success");
            //call the socketweaver event to get the leaderboard
            roomRemoteEventAgent.Invoke("HostPostedScore");
        }
    }

    public void OnHostPostedScore()
    {
       this.GetLeaderboard();
       this.GetTeamPosition();
    }

    public void GetLeaderboard()
    {
        //get the leaderboard
        List<LeaderboardModel> leaderBoardModels = lbHandler.leaderboardService.GetLeaderboard().data;
        
        leaderboardName.text = "";
        leaderboardScore.text = "";
        // loop through the leaderboard and update the UI with the data of name and score
        int count = 1;
        foreach (LeaderboardModel model in leaderBoardModels)
        {
            leaderboardName.text += count + ". " + model.team + "\n";
            leaderboardScore.text += model.score + "\n";
            count++;
        }
    }

    public void GetTeamPosition() {
        LeaderboardPosition lp = lbHandler.GetTeamPosition();
        leaderboardTeamPosition.text = "";
        leaderboardTeamPosition.text = "We scored " + lp.score + " and that placed our team in this position: #" + lp.position + "\n";
    }
}
