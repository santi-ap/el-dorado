using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class RiversVisibility : MonoBehaviour
{
    public GameObject partyGameObject;
    public RoleSO localGuide;

    void Start()
    {
        StateHandler stateHandler = StateHandler.StateHandlerInstance;
        stateHandler.partyHasBeenInitializedEvent += HandleRiversVisibility;
    }


    void HandleRiversVisibility(){
        foreach (PlayerModel p in partyGameObject.GetComponent<PartyModel>().playerList)
        {
            if (p.GetPlayerId() == NetworkClient.Instance.PlayerId)
                this.gameObject.SetActive(p.GetPlayerRole().roleName == localGuide.roleName);
        }
    }
}
