// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;

// public class RoleAssignmentTest
// {
//     private List<PlayerModel> playerList;
//     private List<RoleSO> playerRoleList;

//     public RoleSO hunterRole;
//     public RoleSO botanistRole;
//     public RoleSO localGuideRole;
//     public RoleSO archeologistRole;

//     public AssignRoleToPlayer assignRoleToPlayer;
//     public RoomCustomData roomCustomData;

//     void initializeAttributes()
//     {
//         playerList = new List<PlayerModel>();
//         playerRoleList = new List<RoleSO>();
//         hunterRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
//         botanistRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
//         localGuideRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
//         archeologistRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;

//         roomCustomData = new RoomCustomData(playerRoleList);
//         roomCustomData.playerList = this.playerList;
//         // assignRoleToPlayer = new AssignRoleToPlayer(this.roomCustomData);
//     }

//     void addPlayers()
//     {
//         playerList.Add(new PlayerModel("1", "Santi"));
//         playerList.Add(new PlayerModel("2", "David"));
//         playerList.Add(new PlayerModel("3", "Tom"));
//         playerList.Add(new PlayerModel("4", "Wendell"));
//     }

//     void addRoleNames()
//     {
//         hunterRole.roleName = "Hunter";
//         botanistRole.roleName = "Botanist";
//         localGuideRole.roleName = "Local Guide";
//         archeologistRole.roleName = "Archeologist";
//     }

//     void addRolesToList()
//     {
//         playerRoleList.Add(hunterRole);
//         playerRoleList.Add(botanistRole);
//         playerRoleList.Add(localGuideRole);
//         playerRoleList.Add(archeologistRole);
//     }

//     void printRoles()
//     {
//         foreach (RoleSO role in playerRoleList)
//         {
//             Debug.Log(role.roleName);
//         }
//     }

//     void printPlayerRoles()
//     {
//         foreach (PlayerModel player in playerList)
//         {
//             Debug.Log("Player name: " + player.playerName + " Player role: " + player.GetPlayerRole().roleName);
//         }
//     }

//     void assignRoleToNewPlayer()
//     {
//         this.assignRoleToPlayer.assignRoleToNewPlayer();
//     }

//     // This is the test that checks that players have a role assigned
//     [Test]
//     public void CheckPlayerRoleNotNull()
//     {
//         //running all methods so that all values have been assigned
//         initializeAttributes();
//         this.addPlayers();
//         this.addRoleNames();
//         this.addRolesToList();
//         this.printRoles();
//         this.assignRoleToNewPlayer();

//         //goes through each player and checks if the player role is not null
//         foreach (PlayerModel player in this.roomCustomData.playerList)
//         {
//             Assert.IsNotNull(player.GetPlayerRole());
//         }
//     }

//     // This is the test that checks that all players have different roles assigned
//     [Test]
//     public void CheckPlayersHaveDifferentRoles()
//     {
//         //running all methods so that all values have been assigned
//         initializeAttributes();
//         this.addPlayers();
//         this.addRoleNames();
//         this.addRolesToList();
//         this.printRoles();
//         this.assignRoleToNewPlayer();
//         printPlayerRoles();

//         //nested for loop to make sure all players have different roles
//         foreach (PlayerModel player in this.roomCustomData.playerList)
//         {
//             foreach (PlayerModel player2 in this.roomCustomData.playerList)
//             {
//                 if (player != player2)
//                 {
//                     Assert.AreNotSame(player.GetPlayerRole().roleName, player2.GetPlayerRole().roleName);
//                 }
//             }
//         }
//     }

// }
