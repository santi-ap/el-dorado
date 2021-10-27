using System.Collections.Generic;
using UnityEngine;

public class PartyModel : MonoBehaviour
{
    public NodeModel currentNode; //the node that the party is currently on
    public List<PlayerModel> playerList; //the list of players in the pary
    public InventoryModel inventory;
    private PartySerializableData partySerializableData;

    /// This awake cannot be made into a start() method cause I 
    /// need this to run before my PartyInizialization.Start() method
    void Awake()
    {
        playerList = new List<PlayerModel>();
        inventory = new InventoryModel();
    }

    public PartySerializableData GetPartySerializableData()
    {
        partySerializableData = ParseSerializedUtil.GetPartySerializableData(playerList);
        return this.partySerializableData;
    }

    public void SetFromPartySerializableData(PartySerializableData partySerializableData)
    {
        if (partySerializableData == null){Debug.Log("Party serializable is null!! party model 27");}
        this.playerList = new List<PlayerModel>();
        for (int i = 0; i<4; i++) {
            if (partySerializableData.playerList[i] != null) //! this be failing tho
            this.playerList.Add(new PlayerModel (partySerializableData.playerList[i]));
        }
    }

    public int GetPlayerCount() {
        int count = 0;
        foreach(PlayerModel p in this.playerList) {
            if (!p.isDead) count++;
        }
        return count;
    }
}