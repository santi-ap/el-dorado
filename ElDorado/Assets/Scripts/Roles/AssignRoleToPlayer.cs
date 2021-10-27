using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AssignRoleToPlayer
{
    public PartyInizialization partyInizialization;
    private System.Random rnd;

    private RoleSO hunterRole;
    private RoleSO botanistRole;
    private RoleSO localGuideRole;
    private RoleSO archeologistRole;

    // public RoleProfileHelper roleProfileHelper = (RoleProfileHelper)FindObjectOfType<RoleProfileHelper>();

    public AssignRoleToPlayer()
    {
        //empty constructor on purpose
    }

    public AssignRoleToPlayer(PartyInizialization partyInizialization ){
        this.partyInizialization = partyInizialization;
        rnd = new System.Random();
    }

    public List<PlayerModel> assignRoleToNewPlayer()
    {
        //goes through each player in the list of players
        foreach (PlayerModel player in partyInizialization.partyModel.playerList)
        {
            //if the player doesn't have a role yet
            if (player.GetPlayerRole() == null)
            {
                int randomNumber = this.getRandomNumber(partyInizialization.playerRoleList.Count);//gets a random number between 0 and the size of the list of roles minus 1
                player.SetPlayerRole(partyInizialization.playerRoleList[randomNumber]);//sets the role of the player to a random role from the list of roles
                partyInizialization.playerRoleList.RemoveAt(randomNumber);//removes the role that was assigned from the list of roles
                // Debug.Log("Hunter role: "+ roomCustomData.playerRoleList[randomNumber]);
            }
            Debug.Log("Player name after: "+ player.playerName);
            Debug.Log("Player role name after: "+player.GetPlayerRole().roleName);
        }
        return partyInizialization.partyModel.playerList;
    }

    public int getRandomNumber(int n)
    {
        return rnd.Next(0, n);//returns a random integer between 0 and n
    }

    //returns the RoleSO based on the name of the role
    public RoleSO getRoleFromName(string roleName)
    {
        // roleProfileHelper = find

        //first we need to instantiate each role and its values
        hunterRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
        hunterRole.name = "HunterRoleSO";
        hunterRole.roleName = "Hunter";
        hunterRole.rollStatForHunting = 1.2f;
        hunterRole.rollStatForGathering = 1f;
        hunterRole.rollStatForExploring = 1f;
        hunterRole.rollStatForExcavating = 1f;
        // hunterRole.roleAvatar = 
        //TODO assign the role avatar sprite

        botanistRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
        botanistRole.name = "BotanistRoleSO";
        botanistRole.roleName = "Botanist";
        botanistRole.rollStatForHunting = 1f;
        botanistRole.rollStatForGathering = 1.2f;
        botanistRole.rollStatForExploring = 1f;
        botanistRole.rollStatForExcavating = 1f;
        //TODO assign the role avatar sprite

        localGuideRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
        localGuideRole.name = "LocalGuideRoleSO";
        localGuideRole.roleName = "Local Guide";
        localGuideRole.rollStatForHunting = 1f;
        localGuideRole.rollStatForGathering = 1f;
        localGuideRole.rollStatForExploring = 1.2f;
        localGuideRole.rollStatForExcavating = 1f;
        //TODO assign the role avatar sprite

        archeologistRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
        archeologistRole.name = "ArcheologistRoleSO";
        archeologistRole.roleName = "Archeologist";
        archeologistRole.rollStatForHunting = 1f;
        archeologistRole.rollStatForGathering = 1f;
        archeologistRole.rollStatForExploring = 1f;
        archeologistRole.rollStatForExcavating = 1.2f;
        //TODO assign the role avatar sprite

        switch(roleName)
        {
            case "Archeologist":
                return archeologistRole;
            case "Botanist":
                return botanistRole;
            case "Local Guide":
                return localGuideRole;
            case "Hunter":
                return hunterRole;
            default:
                return null;
        }
    }
}
