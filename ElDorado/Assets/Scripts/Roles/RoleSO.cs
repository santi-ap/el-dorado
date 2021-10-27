using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Role", menuName = "PlayerRoles")]
public class RoleSO : ScriptableObject
{
    public string roleName;
    public double rollStatForHunting;
    public double rollStatForGathering;
    public double rollStatForExploring;
    public double rollStatForExcavating;
    public Sprite roleAvatar;

    public RoleSO(string roleName){
        this.roleName = roleName;
    }
    public RoleSO(RoleSerializable r) {
        this.roleName = r.roleName;
        this.rollStatForExcavating = r.rollStatForExcavating;
        this.rollStatForExploring = r.rollStatForExploring;
        this.rollStatForGathering = r.rollStatForGathering;
        this.rollStatForHunting = r.rollStatForHunting;
    }
}
