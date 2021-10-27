using UnityEngine;
using SWNetwork;
using System.Collections.Generic;


public class PartyNetCode : MonoBehaviour
{
    RoomPropertyAgent roomPropertyAgent;
    RoomRemoteEventAgent roomRemoteEventAgent;
    public PartyInizialization partyInizialization;
    readonly string PARTY_MODEL = "partyModel";

    void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomPropertyAgent = FindObjectOfType<RoomPropertyAgent>();
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    ///<summary>
    /// DO NOT MODIFY GAME DATA IF YOU ARE NOT THE HOST.
    ///
    /// This methods should be called when the host is changing a value in
    /// inventory. This will trigger a chain of events that will make sure
    /// the entire party has the same amount of food. 
    ///</summary>
    public void ModifyParty(PartyModel party)
    {
        //Serializing the party data
        //@deprecated Byte[] value = ParseSerializedUtil.SerializeToByteArray(party.GetPartySerializableData());
        string value = NotAStringParser.PartyModelToPipedString(party);
        roomPropertyAgent.Modify(PARTY_MODEL, value);
    }

    public void OnModifyParty() {
        SWNetwork.SWSyncedProperty syncedProperty = roomPropertyAgent.GetPropertyWithName(PARTY_MODEL);
        // if (syncedProperty == null) Debug.Log("Synced Property is null damn");
        // I need to set the party model as a class that the deserialize method should interpret 
        // as the returning type (check those <> signs below)
        List<PlayerModel> value = NotAStringParser.PipedStringToPartyModel(syncedProperty.GetStringValue());
        partyInizialization.OnPartyModelModified(value);
    }

    ///<summary>
    /// when creating the party model over the network, the game data will be unavailable
    /// until ready, which is when this method will run.
    ///</summary>
    public void OnPartyModelReady()
    {
        Debug.Log("PartyModel Ready");
        this.partyInizialization.Initialize();
    }

}
