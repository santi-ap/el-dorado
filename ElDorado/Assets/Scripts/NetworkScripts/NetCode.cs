using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class NetCode : MonoBehaviour
{
    RoomPropertyAgent roomPropertyAgent;
    RoomRemoteEventAgent roomRemoteEventAgent;
    public ShowOutcome showOutcome;
    public ExplorationResult explorationResult;
    public ReturnToLobby returnToLobby;

    private void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomPropertyAgent = FindObjectOfType<RoomPropertyAgent>();
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    public void InvokeShowOutcomeEvent(string resultOutcome)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.PushUTF8ShortString(resultOutcome);
        //* calls HandleShowOutcomeEvent method
        roomRemoteEventAgent.Invoke("ShowOutcomeEvent", msg);
    }

    public void HandleShowOutcomeEvent(SWNetworkMessage msg) {
        string resultOutcome = msg.PopUTF8ShortString();
        showOutcome.HandleSWNEventTest(resultOutcome);
    }

    public void InvokeShowOutcomeEventClient(string playerId, int actionId)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        Debug.Log("this is the player id in the InvokeShowOutcomeEventClient " + playerId);
        Debug.Log("this is the action id in the InvokeShowOutcomeEventClient " + actionId);

        msg.Push(actionId);
        msg.PushUTF8ShortString(playerId);
        
        //* calls HandleShowOutcomeEventClient method
        roomRemoteEventAgent.Invoke("ShowOutcomeEventClient", msg);
    }

    public void HandleShowOutcomeEventClient(SWNetworkMessage msg){
        int actionId = msg.PopInt32();
        string playerId =msg.PopUTF8ShortString();
        

        Debug.Log("this is the player id in the HandleShowOutcomeEventClient " + playerId);
        Debug.Log("this is the action id in the HandleShowOutcomeEventClient " + actionId);

        showOutcome.CalculateOutcomeForClient(playerId, actionId);
    }

    public void InvokeReturnResultToClientEvent(string resultOutcome, string playerId){
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.PushUTF8ShortString(resultOutcome);
        msg.PushUTF8ShortString(playerId);
        //* calls HandleReturnResultToClientEvent method
        roomRemoteEventAgent.Invoke("ReturnResultToClientEvent", msg);
    }

    public void HandleReturnResultToClientEvent(SWNetworkMessage msg) {
        string resultOutcome = msg.PopUTF8ShortString();
        string playerId = msg.PopUTF8ShortString();
        Debug.Log("playerIds: " + NetworkClient.Instance.PlayerId + " - " + playerId);
        if (NetworkClient.Instance.PlayerId == playerId){
            showOutcome.HandleOutComeUIChange(resultOutcome);
        }
    }

    public void OnChangeToMovementState(SWNetworkMessage msg) {
        showOutcome.ChangeToVotingState();
    }

    public void InvokeStateChangedEvent(){
        roomRemoteEventAgent.Invoke("ChangeToMovementStateHandler");
    }

    public void InvokeShowSecretTrailToGuestEvent(string nodeName){
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.PushUTF8ShortString(nodeName);
        //* calls HandleReturnResultToClientEvent method
        roomRemoteEventAgent.Invoke("ShowSecretTrailToGuestEvent", msg);
    }

    public void HandleShowSecretTrailToGuestEvent(SWNetworkMessage msg) {
        string nodeName = msg.PopUTF8ShortString();
        explorationResult.ShowSecretPath(nodeName);
    }

    public void InvokePlayerDeathEventClient(string playerId)
    {
        SWNetworkMessage msg = new SWNetworkMessage();

        msg.PushUTF8ShortString(playerId);
        
        //* calls HandlePlayerDeathEventClient method
        roomRemoteEventAgent.Invoke("PlayerDeathEventClient", msg);
    }

    public void HandlePlayerDeathEventClient(SWNetworkMessage msg){
        string playerId =msg.PopUTF8ShortString();
        

        returnToLobby.HandlePLayerDeathForClient(playerId);
    }
}