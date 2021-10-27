using UnityEngine;
using SWNetwork;

public class PlayerDeathHandler
{
    public NotificationNetcode notificationNetcode;
    public RoleIconInPartyHandler roleIconInPartyHandler;
    
    //public EndofGameHandler endofGameHandler;
    /// <summary>
    /// Checks id player health is under 0 |
    /// If true, isDead = true
    /// </summary>
    /// <param name="p"></param>
    public void PlayerDeath(PlayerModel p)
    {
        p.isDead = true;
        p.SetPlayerHealth(0);
        Debug.Log("PLAYER DIED");
        roleIconInPartyHandler.KillIcon(p.GetPlayerRole());
        if (NetworkClient.Instance.IsHost)
        {
            notificationNetcode.InvokeShowNotificationAsHostEvent(p.playerName +" DIED",p.GetPlayerId());
        }
        else{
            notificationNetcode.InvokeShowNotificationAsGuestEvent(p.playerName + " DIED",p.GetPlayerId());
        }
        
    }

}
