using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pathfinding;

public class MovementHandler : MonoBehaviour
{
    public PartyModel party;
    public GameObject dontMoveButton;
    public Tween currentTween;
    public GameObject pathTarget;
    public EndofGameHandler endofGameHandler;


    private StateHandler stateHandler;//declare a variable of type StateHandler

    void Awake()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
    }


    public void MoveToNode(GameObject winningOption)
    {
        //if the choice was to not move
        if(winningOption == dontMoveButton)
        {
            Debug.Log("Party will not move");
            //TODO start new turn
            stateHandler.SetState(StateHandler.GameState.EatingState);
            stateHandler.SetState(StateHandler.GameState.StartTurnState);
        }//if a node was selected to move to
        else
        {
            Debug.Log("Party will move");
            //the position of the node that was selected
            Vector3 selectedNodePosition = winningOption.GetComponent<NodeVotingPrefabModel>().node.transform.position;
            //the instance of the node that was selected
            NodeModel newCurrentNode = winningOption.GetComponent<NodeVotingPrefabModel>().node;
            
            //calls method to set the new current node of the party
            SetNewCurrentNode(newCurrentNode);
            //calls the method to show the party moving
            StartCoroutine("ShowPartyMoving", selectedNodePosition);
            //if(newCurrentNode.isFinalNode){
                //Handle end of game 
                //endofGameHandler.ShowEndOfGame();
            //}
        }

    }

    //method to show the party moving
    IEnumerator ShowPartyMoving(Vector3 selectedNodePosition)
    {
        //Sets target for the pathfinder to be the selected node
        pathTarget.transform.position = selectedNodePosition;
        // party.GetComponent<AIDestinationSetter>().target = selectedNodePosition;
        Debug.Log("Party is moving");
        //shows the party moving and sets the current tween to allow it to finish before going to the next line
        // currentTween = party.transform.DOMove(selectedNodePosition, 1);
        // //Allows the current tween to finish before going to the next line
        // yield return currentTween.WaitForCompletion();
        yield return null;
        //start new turn
        stateHandler.SetState(StateHandler.GameState.EatingState);
        stateHandler.SetState(StateHandler.GameState.StartTurnState);
    }

    //method that sets the current node of the party to the selected node
    public void SetNewCurrentNode(NodeModel newCurrentNode)
    {
        party.currentNode = newCurrentNode;
    }
}
