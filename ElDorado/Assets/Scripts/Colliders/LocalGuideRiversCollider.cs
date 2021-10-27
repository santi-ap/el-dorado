using UnityEngine;

public class LocalGuideRiversCollider : MonoBehaviour
{
    public ConsumeFood consumeFood;
    public PartyModel party;
    public PartyNetCode netCode;

    //Method to check if there's a collision between the partys and any river, 
    //then every player loses health
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "VisibleArea") && (collision.gameObject.tag != "FinalTempleParent"))
        {
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