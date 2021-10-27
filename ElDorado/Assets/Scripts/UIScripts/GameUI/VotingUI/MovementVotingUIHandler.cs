using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementVotingUIHandler : MonoBehaviour
{
    public PartyModel party;
    public List<NodeModel> adjacentNodes = new List<NodeModel>();
    public GameObject moveToNodeButtonPrefab;
    public GameObject stayOnNodeButton;
    public GameObject parentObject;
    public MovementVotingHandler movementVotingHandler;
    public int nextOptionId = 0;

    private StateHandler stateHandler;

    void Awake()
    {
        //make this list of nodes equal to the list of adjacent nodes in the current node
        adjacentNodes = party.currentNode.adjacientNodesList;
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        //subscribes ShowNodeVotingUI method to happen when the state event starts
        stateHandler.movementStateHasStartedEvent += ShowNodeVotingUI;
        //* subscribe HideNodeVotingUI to next state
        stateHandler.turnHasStartedStateEvent += HideNodeVotingUI;
    }

    //Will show the node movement voting UI
    public void ShowNodeVotingUI()
    {
        movementVotingHandler.adjacentNodes = party.currentNode.adjacientNodesList;
        adjacentNodes = party.currentNode.adjacientNodesList;
        movementVotingHandler.GiveChoiceColorToNodes();
        this.parentObject.SetActive(true);
        this.RefreshNodeList();
    }

    //Will hide the node movement voting UI
    public void HideNodeVotingUI()
    {
        this.parentObject.SetActive(false);
        this.ClearNodeList(parentObject.transform);
        movementVotingHandler.SetDefaultNodeColor();
    }

    //loads a refreshed list of buttotns of adjacent nodes to move to
    void RefreshNodeList()
    {
        //clears the list of nodes
        ClearNodeList(parentObject.transform);
        // Debug.Log("Cleared node list");
        foreach (NodeModel node in adjacentNodes)
        {
            if (node.secretPath == null || node.secretPath != party.currentNode.secretPath || !node.secretPath.GetComponent<TrailHandler>().isSecretPath)
            {
                nextOptionId++;
                //calls the method that adds the node buttons to the list that is displayed
                AddButtonForNode(node, nextOptionId);
                // Debug.Log("Adding node to list");
            }
        }
    }

    //method that adds and shows the button with the node 
    public void AddButtonForNode(NodeModel node, int optionId)
    {
        //spawns a new node voting button based on the prefab and inside the specified parent object
        GameObject newNodeVotingButton = Instantiate(moveToNodeButtonPrefab, parentObject.transform);
        //sets the new button to true so that it's visible
        newNodeVotingButton.SetActive(true);
        //this helps us set the values that are within the button
        NodeVotingPrefabModel nodeVotingPrefabModel = newNodeVotingButton.GetComponent<NodeVotingPrefabModel>();
        //set the actual node object so we can reference it later
        nodeVotingPrefabModel.node = node;
        //sets the unique color of the node while voting
        nodeVotingPrefabModel.nodeColorSprite.GetComponent<Image>().color = node.nodeChoiceColor;
        //adds 1 to the option id
        VoteCounter voteCounter = newNodeVotingButton.GetComponent<VoteCounter>();
        voteCounter.optionId = optionId;
    }

    /// <summary>
    /// Clears the list of nodes
    /// </summary>
    public void ClearNodeList(Transform parent)
    {
        nextOptionId = 0;
        //looks for all the transforms in the parent game object and destroys them
        foreach (Transform childTransform in parent)
        {
            //unless it's the prefab or the option to stay on the current node
            if (childTransform.gameObject != moveToNodeButtonPrefab && childTransform.gameObject != stayOnNodeButton)
            {
                Destroy(childTransform.gameObject);
            }
        }
    }

}
