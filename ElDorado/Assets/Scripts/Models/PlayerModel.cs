using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    [SerializeField]
    private string playerId;

    [SerializeField]
    public string playerName;
    [SerializeField]
    private RoleSO playerRole; //! this is probably why the object comes as null
    [SerializeField]
    private int playerHealth;

    public bool isDead = false;

    public AssignRoleToPlayer assignRoleToPlayer = new AssignRoleToPlayer();


    public PlayerModel()
    {
        this.playerHealth = 100;
    }
    public PlayerModel(string playerId)
    {
        this.playerId = playerId;
        this.playerHealth = 100;
    }
    public PlayerModel(string playerId, string playerName)
    {
        this.playerId = playerId;
        this.playerName = playerName;
        this.playerHealth = 100;
    }
    public PlayerModel(PlayerSerializable p)
    {
        this.playerId = p.playerId;
        this.playerName = p.playerId.Split('-')[1];
        this.playerHealth = p.playerHealth;
        //sthis.playerRole = new RoleSO(p.playerRole);
    }


    public void SetPlayerId(string id)
    {
        this.playerId = id;
    }

    public string GetPlayerId()
    {
        return this.playerId;
    }

    public void SetPlayerRole(RoleSO role)
    {
        this.playerRole = role;
        
    }

    //sets the player role based on the name of the role
    public void SetPlayerRolFromName(string roleName)
    {
        assignRoleToPlayer.getRoleFromName(roleName);
        this.playerRole = assignRoleToPlayer.getRoleFromName(roleName);
    }

    public RoleSO GetPlayerRole()
    {
        return this.playerRole;
    }

    public int GetPlayerHealth()
    {
        return this.playerHealth;
    }

    public void SetPlayerHealth(int health)
    {
        this.playerHealth = health;
    }

    public void setPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }

    public bool GetIsDead()
    {
        return this.isDead;
    }

}
