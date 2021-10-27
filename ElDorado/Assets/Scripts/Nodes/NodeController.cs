using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

[ExecuteInEditMode]
public class NodeController : MonoBehaviour
{
    private StateHandler stateHandler;
    public NodeModel thisNode; //the node model of the current node
    private Transform adjacentNodeTransform; //the transform of an adjacent node. This is used for drawing gizmos for debugging
    public GameObject resourcePresenceImage;
    public GameObject highLifeLossImage;
    public PartyModel party;
    public PlayerModel thisPlayer;
    public NodeImageIndicator nodeImageIndicator;



    void Awake()
    {
        // nodeImageIndicator = gameObject.AddComponent<NodeImageIndicator>();
        stateHandler = StateHandler.StateHandlerInstance;
        //Debug.Log("Subs");
        stateHandler.partyHasBeenInitializedEvent += ShowHighResourcePresence;
        //automatically assignes the NodeModel script on this object to thisNode variable
        thisNode = gameObject.GetComponent<NodeModel>();
        party = gameObject.GetComponentInParent<NodeHelper>().party;

        //method that links adjacent nodes
        LinkNodes();
        // resourcePresenceImage.SetActive(true);
        // PrintRelations(); //prints the relations between nodes on the console

    }

    //method to link adjacent nodes to eachother
    public void LinkNodes()
    {
        //goes through each node in the list of nodes that were added in the editor
        foreach (NodeModel adjacentNode in thisNode.adjacientNodesList)
        {
            //if this node isn't already in the list of the adjacent node
            if (!adjacentNode.adjacientNodesList.Contains(thisNode))
            {
                //it adds this node to the list of adjacent nodes of the node that we're checking in the for loop
                adjacentNode.adjacientNodesList.Add(thisNode);
                // //Debug.Log("Linking " + adjacentNode.gameObject.name + " to " + thisNode.gameObject.name);
            }
        }
    }

    //prints the relations between nodes on the console
    void PrintRelations()
    {
        foreach (NodeModel nodeModel in thisNode.adjacientNodesList)
        {
            //Debug.Log(thisNode.name + " -> " + nodeModel.gameObject.name);
        }
    }

    //draws gizmos lines between connected notes in the editor
    void OnDrawGizmosSelected()
    {
        foreach (NodeModel adjacentNode in thisNode.adjacientNodesList)
        {
            adjacentNodeTransform = adjacentNode.gameObject.transform;
            if (adjacentNodeTransform != null)
            {
                // Draws a blue line from this transform to the target
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, adjacentNodeTransform.position);
            }
        }
    }

    public void ShowHighResourcePresence()
    {
        foreach (PlayerModel player in party.playerList)
        {
            if (player.GetPlayerId() == NetworkClient.Instance.PlayerId)
            {
                thisPlayer = player;
            }
        }


        switch (thisPlayer.GetPlayerRole().roleName)
        {
            case "Hunter":
                if (thisNode.huntingNodeSuccessDificulty <= 10f)
                {
                    nodeImageIndicator.SetHuntHighResourceImage();
                    resourcePresenceImage.SetActive(true);
                }
                if (thisNode.huntingNodeLifeLossDificulty >= 40)
                {
                    nodeImageIndicator.SetHuntHighLifeLossImage();
                    highLifeLossImage.SetActive(true);
                }
                return;
            case "Archeologist":
                if (thisNode.excavateNodeSuccessDificulty <= 10f)
                {
                    nodeImageIndicator.SetExcavateHighResourceImage();
                    resourcePresenceImage.SetActive(true);
                }
                if (thisNode.excavateNodeLifeLossDificulty >= 40)
                {
                    nodeImageIndicator.SetExcavateHighLifeLossImage();
                    highLifeLossImage.SetActive(true);
                }
                return;
            case "Botanist":
                if (thisNode.forageNodeSuccessDificulty <= 10f)
                {
                    nodeImageIndicator.SetForageHighResourceImage();
                    resourcePresenceImage.SetActive(true);
                }
                if (thisNode.forageNodeLifeLossDificulty >= 40)
                {
                    nodeImageIndicator.SetForageHighLifeLossImage();
                    highLifeLossImage.SetActive(true);
                }
                return;
            case "Local Guide":
                if (thisNode.exploreNodeSuccessDificulty <= 10f)
                {
                    // nodeImageIndicator.SetExploreHighResourceImage();
                    // resourcePresenceImage.SetActive(true);
                }
                if (thisNode.exploreNodeLifeLossDificulty >= 40)
                {
                    // nodeImageIndicator.SetExploreHighLifeLossImage();
                    // highLifeLossImage.SetActive(true);
                }
                return;
            default:
                //Debug.Log("No players");
                break;
        }
    }

}
