using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconBehaviour : MonoBehaviour
{
    PlayerModel playerModel;
    public Text nameText;
    public Text roleText;
    public GameObject playerHealth;
    private float originalHealthXValue;
    public Image roleProfile;

    public void Init(PlayerModel playerModel) {
        this.playerModel = playerModel;
        this.nameText.text = playerModel.playerName;
        this.roleText.text = playerModel.GetPlayerRole().roleName;
        this.originalHealthXValue = playerHealth.transform.localScale.x;
        this.roleProfile.sprite = playerModel.GetPlayerRole().roleAvatar;
    }

    public void UpdateUI(PlayerModel p) {
        //TODO check if ded to make it greyed out
        playerHealth.transform.localScale = new Vector3(this.CalculateXDependingOnHealth(p.GetPlayerHealth()),playerHealth.transform.localScale.y,playerHealth.transform.localScale.z); 
    }

    private float CalculateXDependingOnHealth(int currentHealth){
        return ((((float)currentHealth)/100)*originalHealthXValue);
    }
}