using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHanlder : MonoBehaviour
{
    private StateHandler stateHandler;//declare a variable of type StateHandler

    // Start is called before the first frame update
    void Start()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        // stateHandler.SetState(StateHandler.GameState.StartGameState);
        //TODO this is temporary.
        //TODO this is so that the choice state gets called after the turn state
        //TODO this should actually happen after consuming food and all that during the turn state
        stateHandler.turnHasStartedStateEvent += () => stateHandler.SetState(StateHandler.GameState.ChoiceState);

        stateHandler.SetState(StateHandler.GameState.StartTurnState);
        Debug.Log("Turn state has started!");

        //* Started the choice state
        stateHandler.SetState(StateHandler.GameState.ChoiceState);
        // Debug.Log("Choice state has started!");

        //* Starts movement voting and actual movement state
        // stateHandler.SetState(StateHandler.GameState.MovementState);
        Debug.Log("Movement state has started!");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
