using UnityEngine;

public class MountainCollider : MonoBehaviour
{
    public ConsumeFood consumeFood;
    public PartyModel party;
    public PartyNetCode netCode;
    public NotificationUIHandler notificationUIHandler;

    //Method to check if there's a collision between the partys and any river, 
    //then every player loses health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "VisibleArea") && (collision.gameObject.tag != "FinalTempleParent"))
        {

            notificationUIHandler.StartNotificationFlow("The party crossed through a mountain and everybody lost 10 health");
            foreach (PlayerModel player in party.playerList)
            {
                if (!player.isDead)
                {
                    consumeFood.LoseHealth(player);
                }
            }
            netCode.ModifyParty(this.party);
        }
    }
}