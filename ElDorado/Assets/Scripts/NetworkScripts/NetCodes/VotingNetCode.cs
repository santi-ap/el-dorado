using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class VotingNetCode : MonoBehaviour
{
    RoomRemoteEventAgent roomRemoteEventAgent;
    public GeneralVotingHandler generalVotingHandler;

    private void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    public void InvokeAddVoteAsHostEvent(int optionId, string playerId)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.Push(optionId);
        msg.PushUTF8ShortString(playerId);
        //* calls HandleAddVoteAsHostEvent method
        roomRemoteEventAgent.Invoke("AddVoteAsHostEvent", msg);

    }

    public void HandleAddVoteAsHostEvent(SWNetworkMessage msg)
    {
        int optionId = msg.PopInt32();
        string votingPlayerId = msg.PopUTF8ShortString();
        generalVotingHandler.votingPlayerId = votingPlayerId;
        generalVotingHandler.AddVote(optionId);
    }
//* -------------------------------------------------------------------------------------
    public void InvokeAddVoteAsGuestEvent(int optionId, string playerId)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.Push(optionId);
        msg.PushUTF8ShortString(playerId);
        //* calls HandleAddVoteAsGuestEvent method
        roomRemoteEventAgent.Invoke("AddVoteAsGuestEvent", msg);

    }

    public void HandleAddVoteAsGuestEvent(SWNetworkMessage msg)
    {
        int optionId = msg.PopInt32();
        string votingPlayerId = msg.PopUTF8ShortString();
        generalVotingHandler.hasVoted = false;
        generalVotingHandler.isRequestFromGuest = true;
        // generalVotingHandler.votingPlayerId = votingPlayerId;
        // generalVotingHandler.AddVote(optionId);
        generalVotingHandler.SendRequestToHost(optionId, votingPlayerId);
    }   
}
