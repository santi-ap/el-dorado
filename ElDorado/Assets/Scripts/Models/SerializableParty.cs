using System;

[Serializable]
public class PartySerializableData
{
    public PlayerSerializable[] playerList;
}

[Serializable]
public class PlayerSerializable
{
    public string playerId;
    public int playerHealth;
    public RoleSerializable roleSerializable;
}

[Serializable]
public class RoleSerializable
{
    public string roleName;
    public double rollStatForHunting;
    public double rollStatForGathering;
    public double rollStatForExploring;
    public double rollStatForExcavating;
}



