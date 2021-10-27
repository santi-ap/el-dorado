using System;
using System.Collections.Generic;
using UnityEngine;

public static class NotAStringParser
{

    private static string PlayerModelToPipedString(PlayerModel playerModel)
    {
        string piped = "";
        piped += playerModel.GetPlayerId();
        piped += "|";
        piped += playerModel.playerName;
        piped += "|";
        piped += playerModel.GetPlayerHealth();
        piped += "|";
        piped += playerModel.GetPlayerRole().roleName;
        piped += "|";
        piped += playerModel.GetIsDead()?"1":"0";


        return piped;
    }

    private static PlayerModel PipedStringToPlayerModel(string piped)
    {
        PlayerModel player = new PlayerModel();
        string[] unPiped = piped.Split('|');
        Debug.Log("nasp.26: " + piped);
        player.SetPlayerId(unPiped[0]);
        player.playerName = unPiped[1];
        player.SetPlayerHealth(int.Parse(unPiped[2]));
        player.SetPlayerRolFromName(unPiped[3]);
        player.SetIsDead(int.Parse(unPiped[4])==1);
        return player;
    }

    public static string PartyModelToPipedString(PartyModel partyModel)
    {
        string piped = "";
        foreach (PlayerModel p in partyModel.playerList)
        {
            piped += PlayerModelToPipedString(p);
            piped += '°';
        }
        return piped;
    }

    public static List<PlayerModel> PipedStringToPartyModel(string piped)
    {
        List<PlayerModel> partyModel = new List<PlayerModel>();
        // Debug.Log("nasp.48: " + piped);

        string[] unPiped = piped.Split('°');
        for (int i = 0; i < unPiped.Length; i++)
        {
            // Debug.Log("nasp.53: " + unPiped[i]);
            if (unPiped[i].Contains("|"))
            {
                partyModel.Add(PipedStringToPlayerModel(unPiped[i]));
            }
        }
        return partyModel;
    }
}
