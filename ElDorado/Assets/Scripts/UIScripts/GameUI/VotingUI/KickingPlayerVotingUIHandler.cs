using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickingPlayerVotingUIHandler : MonoBehaviour
{
    public PartyModel party;
    public GameObject kickPlayerButtonPrefab;
    public GameObject dontKickButton;
    public GameObject parentObject;
    // public MovementVotingHandler movementVotingHandler;
    public int nextOptionId = 0;

    private StateHandler stateHandler;

    void Awake()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        //subscribes ShowNodeVotingUI method to happen when the state event starts
        stateHandler.kickVoteStateHasStartedEvent += () => Debug.Log("Kicking player UI called");
        stateHandler.kickVoteStateHasStartedEvent += ShowKickingPlayerVotingUI;
        //* subscribe HideKickingPlayerVotingUI to next state
        stateHandler.turnHasStartedStateEvent += HideKickingPlayerVotingUI;
    }

    //Will show the node movement voting UI
    public void ShowKickingPlayerVotingUI()
    {
        this.parentObject.SetActive(true);
        this.RefreshKickablePlayerList();
    }

    //Will hide the node movement voting UI
    public void HideKickingPlayerVotingUI()
    {
        this.parentObject.SetActive(false);
        this.ClearPlayerList(parentObject.transform);
        // movementVotingHandler.SetDefaultNodeColor();
    }

    //loads a refreshed list of buttotns of adjacent nodes to move to
    void RefreshKickablePlayerList()
    {
        //clears the list of nodes
        ClearPlayerList(parentObject.transform);
        Debug.Log("Cleared node list");
        foreach (PlayerModel player in party.playerList)
        {
            if (!player.isDead)
            {
                nextOptionId++;
                //calls the method that adds the node buttons to the list that is displayed
                AddButtonForPlayer(player, nextOptionId);
                Debug.Log("Adding node to list");
            }
        }
    }

    //method that adds and shows the button with the node 
    public void AddButtonForPlayer(PlayerModel player, int optionId)
    {
        //spawns a new node voting button based on the prefab and inside the specified parent object
        GameObject newKickingVotingButton = Instantiate(kickPlayerButtonPrefab, parentObject.transform);
        //sets the new button to true so that it's visible
        newKickingVotingButton.SetActive(true);
        //this helps us set the values that are within the button
        KickPlayerVotingPrefabModel kickPlayerVotingPrefabModel = newKickingVotingButton.GetComponent<KickPlayerVotingPrefabModel>();
        //sets the player of the option to the actual player
        kickPlayerVotingPrefabModel.player = player;
        //set the text to the name of the player
        kickPlayerVotingPrefabModel.playerName.text = player.playerName;
        //sets the role icon to be shown in the option
        kickPlayerVotingPrefabModel.playerRoleIcon.sprite = player.GetPlayerRole().roleAvatar;
        //adds 1 to the option id
        VoteCounter voteCounter = newKickingVotingButton.GetComponent<VoteCounter>();
        voteCounter.optionId = optionId;
    }

    /// <summary>
    /// Clears the list of nodes
    /// </summary>
    public void ClearPlayerList(Transform parent)
    {
        nextOptionId = 0;
        //looks for all the transforms in the parent game object and destroys them
        foreach (Transform childTransform in parent)
        {
            //unless it's the prefab or the option to stay on the current node
            if (childTransform.gameObject != kickPlayerButtonPrefab && childTransform.gameObject != dontKickButton)
            {
                Destroy(childTransform.gameObject);
            }
        }
    }
}
