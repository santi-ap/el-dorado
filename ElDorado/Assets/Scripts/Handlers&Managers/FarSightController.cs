using System.Collections;
using System.Collections.Generic;
using SWNetwork;
using UnityEngine;
using UnityEngine.UI;

public class FarSightController : MonoBehaviour
{
    public GameObject partyGameObject;
    public string farSightRelatedRoleName;

    private readonly float TRANSPARENCY_DISTANCE_THRESHOLD = 250;
    private readonly float FULL_TRANSPARENCY_DISTANCE_THRESHOLD = 100;
    private float distanceFromParty;

    private RoleSO localPlayerRole;

    void Start()
    {
        StateHandler stateHandler = StateHandler.StateHandlerInstance;
        stateHandler.partyHasBeenInitializedEvent += HandleRoleVisibility;
    }

    void FixedUpdate()
    {
        distanceFromParty = Vector2.Distance(Camera.main.WorldToScreenPoint(partyGameObject.transform.position), this.transform.position);
        this.HandleTransparancy();
    }

    void HandleTransparancy()
    {
        if (distanceFromParty < TRANSPARENCY_DISTANCE_THRESHOLD)
        {
            Image sprite = this.gameObject.GetComponent<Image>();
            if (sprite.color.a != 0 && distanceFromParty > FULL_TRANSPARENCY_DISTANCE_THRESHOLD)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, ((distanceFromParty) / TRANSPARENCY_DISTANCE_THRESHOLD));
            }
            else
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
            }

        }
    }

    void HandleRoleVisibility()
    {
        foreach (PlayerModel p in partyGameObject.GetComponent<PartyModel>().playerList)
        {
            if (p.GetPlayerId() == NetworkClient.Instance.PlayerId)
                this.gameObject.SetActive(p.GetPlayerRole().roleName == farSightRelatedRoleName);
        }
    }
}
