using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseStateHandlerExample : MonoBehaviour
{
    //this class is used as an example of how to subscribe and call events and states

    public int number;//declare a number that we'll use later to print it out
    private StateHandler stateHandler;//declare a variable of type StateHandler

    // Start is called before the first frame update
    void Start()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;

        //make the number equal to 1
        number = 1;

        //we subscribe the PrintNumber method to the gameHasStartedStateEvent event
        //this means that when that event starts, the PrintNumber method will be called automatically
        stateHandler.turnHasStartedStateEvent += PrintNumber;

        //changes the state of the game to StartGameState
        stateHandler.SetState(StateHandler.GameState.StartTurnState);
    }

    //prints a number
    void PrintNumber()
    {
        Debug.Log(number);
    }
}
