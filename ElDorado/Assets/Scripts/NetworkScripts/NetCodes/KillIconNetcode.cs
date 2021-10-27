using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class KillIconNetcode : MonoBehaviour
{
    RoomRemoteEventAgent roomRemoteEventAgent;
    public RoleIconInPartyHandler roleIconInPartyHandler;

    private void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    public void InvokeKillIconAsHostEvent(string roleName)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.PushUTF8ShortString(roleName);
        //* calls HandleKillIconAsHostEvent method
        roomRemoteEventAgent.Invoke("KillIconAsHostEvent", msg);

    }

    public void HandleKillIconAsHostEvent(SWNetworkMessage msg)
    {
        string roleName = msg.PopUTF8ShortString();
        //TODO check if it's the same player as the one that performed the action
        roleIconInPartyHandler.LeaveIcon(roleName);
    }
//* -------------------------------------------------------------------------------------
    public void InvokeKillIconAsGuestEvent(string roleName)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.PushUTF8ShortString(roleName);
        //* calls HandleKillIconAsGuestEvent method
        roomRemoteEventAgent.Invoke("KillIconAsGuestEvent", msg);

    }

    public void HandleKillIconAsGuestEvent(SWNetworkMessage msg)
    {
        string roleName = msg.PopUTF8ShortString();
        this.InvokeKillIconAsHostEvent(roleName);
    }   
}
