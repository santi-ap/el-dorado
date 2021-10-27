using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVotingHandler : MonoBehaviour
{
    public PartyModel party;
    public List<NodeModel> adjacentNodes = new List<NodeModel>();
    public Color defaultNodeColor = new Color32(255, 255, 255, 118);//the default color of the nodes
    public List<Color> possibleNodeChoiceColors = new List<Color>();//the list of possible colors nodes can take during voting

    private StateHandler stateHandler;

    void Awake()
    {
        //make this list of nodes equal to the list of adjacent nodes in the current node
        adjacentNodes = party.currentNode.adjacientNodesList;
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        //subscribe SetDefaultNodeColor to next state
        stateHandler.turnHasStartedStateEvent += SetDefaultNodeColor;
    }

    //sets the color of a node during voting
    public void GiveChoiceColorToNodes()
    {
        // Debug.Log("Setting colors");
        //index to go through the list of possible colors
        int i = 0;
        foreach (NodeModel node in adjacentNodes)
        {
            if (node.secretPath == null || node.secretPath != party.currentNode.secretPath || !node.secretPath.GetComponent<TrailHandler>().isSecretPath)
            {
                //sets the color that will be shown in the UI
                node.nodeChoiceColor = possibleNodeChoiceColors[i];
                //sets the color of the acutal ndoe sprite in the game
                node.gameObject.GetComponentInChildren<SpriteRenderer>().color = possibleNodeChoiceColors[i];
                //to go the next index in the list
                i++;
            }

        }
    }

    //sets the color back to the default
    public void SetDefaultNodeColor()
    {
        Debug.Log("Resetting colors");
        foreach (NodeModel node in adjacentNodes)
        {
            node.gameObject.GetComponentInChildren<SpriteRenderer>().color = defaultNodeColor;
        }
    }
}
