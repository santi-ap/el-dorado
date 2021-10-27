using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class NotificationNetcode : MonoBehaviour
{
    RoomRemoteEventAgent roomRemoteEventAgent;
    public NotificationUIHandler notificationHandler;

    private void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    public void InvokeShowNotificationAsHostEvent(string notifactionMessage, string playerId)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.PushUTF8ShortString(notifactionMessage);
        msg.PushUTF8ShortString(playerId);
        //* calls HandleShowNotificationAsHostEvent method
        roomRemoteEventAgent.Invoke("ShowNotificationAsHostEvent", msg);

    }

    public void HandleShowNotificationAsHostEvent(SWNetworkMessage msg)
    {
        string notifactionMessage = msg.PopUTF8ShortString();
        string actingPlayerId = msg.PopUTF8ShortString();
        //TODO check if it's the same player as the one that performed the action
        if (NetworkClient.Instance.PlayerId != actingPlayerId){
            notificationHandler.StartNotificationFlow(notifactionMessage);
        }
    }
//* -------------------------------------------------------------------------------------
    public void InvokeShowNotificationAsGuestEvent(string notifactionMessage, string playerId)
    {
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.PushUTF8ShortString(notifactionMessage);
        msg.PushUTF8ShortString(playerId);
        //* calls HandleShowNotificationAsGuestEvent method
        roomRemoteEventAgent.Invoke("ShowNotificationAsGuestEvent", msg);

    }

    public void HandleShowNotificationAsGuestEvent(SWNetworkMessage msg)
    {
        string notifactionMessage = msg.PopUTF8ShortString();
        string actingPlayerId = msg.PopUTF8ShortString();
        notificationHandler.SendRequestToHost(notifactionMessage,actingPlayerId);
    }   
}
