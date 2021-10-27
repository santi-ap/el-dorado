using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardHandler : MonoBehaviour
{
    public LeaderboardService leaderboardService;
    public PartyModel partyModel;

    // on awake instantiate leaderboard service
    void Awake()
    {
        leaderboardService = new LeaderboardService();
    }

    public void CreateScore() {
        //loop through all the playermodels in the partymodel and create a string concatenating the name of the player with a separator character
        Debug.Log("CreateScore");
        string partyString = this.CreateName();
        // call leaderboard service to create score using partymodel.inventory.artifactcount as the score 
        leaderboardService.AddScore(((int)partyModel.inventory.artifactCount), partyString);
    }

    public LeaderboardPosition GetTeamPosition() {
        return leaderboardService.GetTeamPosition(this.CreateName());
    }
    
    public string CreateName() {
        string partyString = "";
        foreach (PlayerModel player in partyModel.playerList)
        {
            partyString += player.playerName + ",";
        }
        //remove the last comma from the partyString
        partyString = partyString.Substring(0, partyString.Length - 1);
        return partyString;
    }
}
