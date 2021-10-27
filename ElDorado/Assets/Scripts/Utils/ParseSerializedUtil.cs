using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public static class ParseSerializedUtil
{
    //Extension class to provide serialize / deserialize methods to object.
    //src: http://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
    //NOTE: You need add [Serializable] attribute in your class to enable serialization


    public static byte[] SerializeToByteArray(this object obj)
    {
        if (obj == null)
        {
            return null;
        }
        var bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    public static T Deserialize<T>(this byte[] byteArray) where T : class
    {
        if (byteArray == null)
        {
            return null;
        }
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(byteArray, 0, byteArray.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = (T)binForm.Deserialize(memStream);
            return obj;
        }
    }

    public static PartySerializableData GetPartySerializableData(List<PlayerModel> playerModels)
    {
        PlayerSerializable[] playerList = new PlayerSerializable[4];
        PartySerializableData partyData = new PartySerializableData();
        int count = 0;
        foreach (PlayerModel p in playerModels)
        {
            playerList[++count] = ParseSerializedUtil.GetPlayerSerializable(p);
        }
        partyData.playerList = playerList;
        return partyData;
    }

    public static PlayerSerializable GetPlayerSerializable(PlayerModel p)
    {
        PlayerSerializable playerSerializable = new PlayerSerializable();
        playerSerializable.playerId = p.GetPlayerId();
        playerSerializable.playerHealth = p.GetPlayerHealth();

        playerSerializable.roleSerializable = ParseSerializedUtil.GetRoleSerializable(p.GetPlayerRole());

        return playerSerializable;
    }

    public static RoleSerializable GetRoleSerializable(RoleSO role)
    {
        if(role != null) {
        RoleSerializable roleSerializable = new RoleSerializable();
        roleSerializable.roleName = role.roleName;
        roleSerializable.rollStatForExcavating = role.rollStatForExcavating;
        roleSerializable.rollStatForExploring = role.rollStatForExploring;
        roleSerializable.rollStatForGathering = role.rollStatForGathering;
        roleSerializable.rollStatForHunting = role.rollStatForHunting;
        return roleSerializable;
        } else {
            return null;
        }
    }
}

