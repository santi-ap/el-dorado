using System;
using System.Collections.Generic;


[Serializable]
public class RoomCustomData
{
    public string name;
    public List<PlayerModel> playerList;
    public List<RoleSO> playerRoleList;
    

    public RoomCustomData(List<RoleSO> playerRoleList)
    {
        this.playerList = new List<PlayerModel>();
        this.playerRoleList = playerRoleList;
    }

}
