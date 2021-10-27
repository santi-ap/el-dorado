using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class KickPlayerHandler : MonoBehaviour
{
    public GameObject dontKickButton;
    public PartyModel party;
    public PlayerDeathHandler playerDeathHandler;
    public NotificationNetcode notificationNetcode;
    public RoleIconInPartyHandler roleIconInPartyHandler;
    public GameOverHandler gameOverHandler;

    private StateHandler stateHandler;//declare a variable of type StateHandler

    void Awake()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        playerDeathHandler = new PlayerDeathHandler();
        playerDeathHandler.notificationNetcode = this.notificationNetcode;
        playerDeathHandler.roleIconInPartyHandler = this.roleIconInPartyHandler;
    }

    public void KickPlayer(GameObject winningOption)
    {
        //if the choice was to not move
        if (winningOption == dontKickButton)
        {
            Debug.Log("No body will be kicked");
            //start new turn
            stateHandler.SetState(StateHandler.GameState.EatingState);
            stateHandler.SetState(StateHandler.GameState.StartTurnState);
        }//TODO kick the selected player
        else
        {
            Debug.Log("Somebody is gonna be kicked out and left to die!");
            string playerName = winningOption.GetComponent<KickPlayerVotingPrefabModel>().playerName.text;
            Debug.Log("Kicking: " + playerName);

            //TODO kill player
            if (NetworkClient.Instance.IsHost)
            {
                playerDeathHandler.PlayerDeath(winningOption.GetComponent<KickPlayerVotingPrefabModel>().player);
                gameOverHandler.CheckPlayersDead(this.party);
            }
            // foreach(PlayerModel player in party.playerList)
            // {
            //     if( player == winningOption.GetComponent<KickPlayerVotingPrefabModel>().player)
            //     {
            //         player.SetIsDead(true);
            //     }
            // }

            // //the position of the node that was selected
            // Vector3 selectedNodePosition = winningOption.GetComponent<NodeVotingPrefabModel>().node.transform.position;
            // //the instance of the node that was selected
            // NodeModel newCurrentNode = winningOption.GetComponent<NodeVotingPrefabModel>().node;
            // //calls method to set the new current node of the party
            // SetNewCurrentNode(newCurrentNode);
            // //calls the method to show the party moving
            // StartCoroutine("ShowPartyMoving", selectedNodePosition);
            stateHandler.SetState(StateHandler.GameState.EatingState);
            stateHandler.SetState(StateHandler.GameState.StartTurnState);
        }
    }
}
