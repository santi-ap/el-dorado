using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconsList : MonoBehaviour
{
    public GameObject prefab;
    public GameObject prefabOnScene; //this is the prefab already instantiated on the scene
    public Stack<GameObject> playerIcons;

    public Sprite botanistProfile;
    public Sprite hunterProfile;
    public Sprite archeologistProfile;
    public Sprite localGuideProfile;

    public void AddIcon (PlayerModel playerModel) {
        this.SetRoleProfile(playerModel);
        if (playerIcons == null) {
            playerIcons = new Stack<GameObject>();
        }
        playerIcons.Push(Instantiate(prefab,this.transform));
        playerIcons.Peek().GetComponentInChildren<PlayerIconBehaviour>().Init(playerModel);
        this.prefabOnScene.SetActive(false);
    }

    public void UpdateUI (PlayerModel playerModel) {
        this.SetRoleProfile(playerModel);
        foreach(GameObject p in playerIcons) {
            PlayerIconBehaviour playerIconBehaviour = p.GetComponentInChildren<PlayerIconBehaviour>();
            if (playerIconBehaviour.nameText.text == playerModel.playerName) {
                playerIconBehaviour.UpdateUI(playerModel);
            }
        } 
    }

    public void SetRoleProfile(PlayerModel player)
    {
        switch(player.GetPlayerRole().roleName)
        {
            case "Archeologist":
                player.GetPlayerRole().roleAvatar = archeologistProfile;
                break;
            case "Botanist":
                player.GetPlayerRole().roleAvatar = botanistProfile;
                break;
            case "Local Guide":
                player.GetPlayerRole().roleAvatar = localGuideProfile;
                break;
            case "Hunter":
                player.GetPlayerRole().roleAvatar = hunterProfile;
                break;
            default:
                break;
        }
    }
}
