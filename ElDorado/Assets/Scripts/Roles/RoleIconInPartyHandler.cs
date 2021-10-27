using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class RoleIconInPartyHandler : MonoBehaviour
{
    public SpriteRenderer archeologistIcon;
    public SpriteRenderer hunterIcon;
    public SpriteRenderer botanistIcon;
    public SpriteRenderer localGuideIcon;
    public KillIconNetcode killIconNetcode;

    public PartyModel party;

    public Color deadColor;


    public void KillIcon(RoleSO role)
    {
        if(NetworkClient.Instance.IsHost)
        {
            killIconNetcode.InvokeKillIconAsHostEvent(role.roleName);
        }else{
            killIconNetcode.InvokeKillIconAsGuestEvent(role.roleName);
        }
    }

    // public void TellHostToLeaveIcon(string roleName)
    // {
    //     killIconNetcode.InvokeKillIconAsHostEvent(roleName);
    // }

    public void LeaveIcon(string roleName)
    {
        switch(roleName){
            
            case "Archeologist":
                archeologistIcon.color = deadColor;
                archeologistIcon.transform.SetParent(party.currentNode.transform);
                break;
            case "Botanist":
                botanistIcon.color = deadColor;
                botanistIcon.transform.SetParent(party.currentNode.transform);
                break;
            case "Local Guide":
                localGuideIcon.color = deadColor;
                localGuideIcon.transform.SetParent(party.currentNode.transform);
                break;
            case "Hunter":
                hunterIcon.color = deadColor;
                hunterIcon.transform.SetParent(party.currentNode.transform);
                break;
            default:
                break;
        }
    }
}
