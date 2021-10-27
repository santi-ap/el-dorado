using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SWNetwork;

public class StateHandler : MonoBehaviour
{
    // region to intanciate the StateHandler instance
    #region Singleton

    //create an variable to be the only instance of the StateHandler class in the whole game
    private static StateHandler _stateHandlerInstance;

    //method to set and get the actual instance
    public static StateHandler StateHandlerInstance
    {
        get
        {   //sets the the instance to be a new instnace if one doesn't already exist
            if (_stateHandlerInstance == null)
            {
                _stateHandlerInstance = GameObject.FindObjectOfType<StateHandler>();
            }
            //returns tha actual instance
            return _stateHandlerInstance;
        }
    }
    #endregion

    //the possible states of the game
    public enum GameState
    {
        StartGameState,
        StartTurnState,
        EatingState,
        ChoiceState,
        MovementState,
        KickVoteState
    }

    //the actual game state variable
    private GameState gameState;

    //the game state events
    public Action gameHasStartedStateEvent;//event when the game starts
    public Action turnHasStartedStateEvent;//event when a turn starts
    public Action eatingStateHasStartedEvent;//event when the eating state starts
    public Action choiceStateHasStartedEvent;//event when the choice state starts
    public Action movementStateHasStartedEvent;//event when the moving state starts
    public Action kickVoteStateHasStartedEvent;//event when the player kick state starts
    public Action partyHasBeenInitializedEvent;//event when the player kick state starts

    public StateNetCode stateNetCode;

    void Awake()
    {
        //Protects the instance of this class from being destroyed while changing scenes
        DontDestroyOnLoad(gameObject);
        stateNetCode = gameObject.AddComponent<StateNetCode>();
        // gameHasStartedStateEvent += () => Debug.Log("Game has started");
        // turnHasStartedStateEvent += () => Debug.Log("Turn has started");
        // eatingStateHasStartedEvent += () => Debug.Log("Eating face has started");
        // choiceStateHasStartedEvent += () => Debug.Log("Choice event has started");
    }

    public GameState GetGameState()
    {
        return this.gameState;
    }


    //method that other classes call to set the state of the game
    public void SetState(GameState gameState)
    {
        //makes the gameState variable in this class equal the one that is sent through the method
        this.gameState = gameState;
        // Debug.Log("The game state is: " + this.gameState.ToString());
        // Debug.Log("The game state number is: " + ((int)this.gameState));

        //depending on the game state that was brought in, it will call the respective event to start
        //this will then call all the methods that are subscribed to the respectice event
        switch (this.gameState)
        {
            case GameState.StartGameState:
                gameHasStartedStateEvent();
                break;
            case GameState.StartTurnState:
                turnHasStartedStateEvent();
                break;
            case GameState.EatingState:
                eatingStateHasStartedEvent();
                break;
            case GameState.ChoiceState:
                choiceStateHasStartedEvent();
                break;
            case GameState.MovementState:
                movementStateHasStartedEvent();
                break;
            case GameState.KickVoteState:
                kickVoteStateHasStartedEvent();
                break;
            default:
                Debug.Log("Not a defined game state");
                break;
        }
    }


    /// <summary>
    /// Method that gets called from netcode to set the state for all players.
    /// </summary>
    /// <param name="gameStateInt">The int value of the game state.</param>
    public void SetStateFromInt(int gameStateInt)
    {
        //makes the gameState variable in this class equal the one that is sent through the method
        // this.gameState = gameStateInt;

        //depending on the game state that was brought in, it will call the respective event to start
        //this will then call all the methods that are subscribed to the respectice event
        switch (gameStateInt)
        {
            case ((int)GameState.StartGameState):
                this.gameState = GameState.StartGameState;
                gameHasStartedStateEvent();
                break;
            case ((int)GameState.StartTurnState):
                this.gameState = GameState.StartTurnState;
                turnHasStartedStateEvent();
                break;
            case ((int)GameState.EatingState):
                this.gameState = GameState.EatingState;
                eatingStateHasStartedEvent();
                break;
            case ((int)GameState.ChoiceState):
                this.gameState = GameState.ChoiceState;
                choiceStateHasStartedEvent();
                break;
            case ((int)GameState.MovementState):
                this.gameState = GameState.MovementState;
                movementStateHasStartedEvent();
                break;
            case ((int)GameState.KickVoteState):
                this.gameState = GameState.KickVoteState;
                kickVoteStateHasStartedEvent();
                break;
            default:
                Debug.Log("Not a defined game state");
                break;
        }
    }

    /// <summary>
    /// Calls the method to change the state as a host to be broadcasted to every player.
    /// This will be called from the StateNetCode class.
    /// </summary>
    /// <param name="stateNumber">The int value of the game state. This comes from the StateNetCode class</param>
    public void SendStateNumberToHost(int stateNumber)
    {
        stateNetCode.InvokeChangeStateAsHostEvent(stateNumber);
    }
}
