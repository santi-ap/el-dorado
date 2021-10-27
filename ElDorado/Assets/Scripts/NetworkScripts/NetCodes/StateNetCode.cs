using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class StateNetCode : MonoBehaviour
{
    private StateHandler stateHandler;//declare a variable of type StateHandler
    RoomRemoteEventAgent roomRemoteEventAgent;

     private void Awake()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        //We need to first get the objects we're going to interact with
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    public void InvokeChangeStateAsHostEvent(int stateNumber)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.Push(stateNumber);
        //* calls HandleChangeStateAsHostEvent method
        roomRemoteEventAgent.Invoke("ChangeStateAsHostEvent", msg);
    }

    public void HandleChangeStateAsHostEvent(SWNetworkMessage msg)
    {
        int stateNumber = msg.PopInt32();
        stateHandler.SetStateFromInt(stateNumber);
    }

    //* -------------------------------------------------------------------------------------
    public void InvokeChangeStateAsGuestEvent(int stateNumber)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.Push(stateNumber);
        //* calls HandleChangeStateAsGuestEvent method
        roomRemoteEventAgent.Invoke("ChangeStateAsGuestEvent", msg);
    }

    public void HandleChangeStateAsGuestEvent(SWNetworkMessage msg)
    {
        int stateNumber = msg.PopInt32();
        this.stateHandler.SendStateNumberToHost(stateNumber);
    }   
}
