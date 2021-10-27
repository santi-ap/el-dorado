using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StateHandlerTest
{
    //declare variables
    public StateHandler stateHandler;
    public int number;

    //initiliaze variables
    public void InitializeAttributes()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        // stateHandler = StateHandler.StateHandlerInstance;
        stateHandler = new StateHandler();
        number = 1;
    }

    //method to change the value of the number
    public void ChangeNumber()
    {
        number = 5;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void GameHasStartedEventTest()
    {   
        //calls the method to initialize all the values to start from scratch
        InitializeAttributes();
        //checks that the number is 1 at first
        Assert.AreEqual(1, number);
        //subscribes the ChangeNumber method to the event
        stateHandler.gameHasStartedStateEvent += ChangeNumber;
        //changes the state of the game
        stateHandler.SetState(StateHandler.GameState.StartGameState);
        //checks that the number is now equal to 5 since it should have changed when the event was started
        Assert.AreEqual(5, number);
    }

    [Test]
    public void TurnHasStartedEventTest()
    {
        InitializeAttributes();
        Assert.AreEqual(1, number);
        stateHandler.turnHasStartedStateEvent += ChangeNumber;
        stateHandler.SetState(StateHandler.GameState.StartTurnState);
        Assert.AreEqual(5, number);
    }

    [Test]
    public void EatingHasStartedEventTest()
    {
        InitializeAttributes();
        Assert.AreEqual(1, number);
        stateHandler.eatingStateHasStartedEvent += ChangeNumber;
        stateHandler.SetState(StateHandler.GameState.EatingState);
        Assert.AreEqual(5, number);
    }

    [Test]
    public void ChoicesHasStartedEventTest()
    {
        InitializeAttributes();
        Assert.AreEqual(1, number);
        stateHandler.choiceStateHasStartedEvent += ChangeNumber;
        stateHandler.SetState(StateHandler.GameState.ChoiceState);
        Assert.AreEqual(5, number);
    }

    // [Test]
    // public void OutcomeHasStartedEventTest()
    // {
    //     InitializeAttributes();
    //     Assert.AreEqual(1, number);
    //     stateHandler.outcomeStateHasStartedEvent += ChangeNumber;
    //     stateHandler.SetState(StateHandler.GameState.OutcomeState);
    //     Assert.AreEqual(5, number);
    // }

    [Test]
    public void MovementHasStartedEventTest()
    {
        InitializeAttributes();
        Assert.AreEqual(1, number);
        stateHandler.movementStateHasStartedEvent += ChangeNumber;
        stateHandler.SetState(StateHandler.GameState.MovementState);
        Assert.AreEqual(5, number);
    }

    [Test]
    public void KickVoteHasStartedEventTest()
    {
        InitializeAttributes();
        Assert.AreEqual(1, number);
        stateHandler.kickVoteStateHasStartedEvent += ChangeNumber;
        stateHandler.SetState(StateHandler.GameState.KickVoteState);
        Assert.AreEqual(5, number);
    }

}
