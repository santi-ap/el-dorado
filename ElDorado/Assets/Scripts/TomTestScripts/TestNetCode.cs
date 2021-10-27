using System;
using UnityEngine;
using UnityEngine.Events;
using SWNetwork;

[Serializable]
public class GameDataEvent : UnityEvent<int>
{

}

public class TestNetCode : MonoBehaviour
{
    public GameDataEvent OnGameDataReadyEvent = new GameDataEvent();
    public GameDataEvent OnGameDataChangedEvent = new GameDataEvent();

    public UnityEvent OnGameStateChangedEvent = new UnityEvent();
    
    RoomPropertyAgent roomPropertyAgent;
    RoomRemoteEventAgent roomRemoteEventAgent;

    const string TEST_VALUE = "TestValue";
    const string TEST_VALUE_EVENT = "TestValueChangedEvent";

    private void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomPropertyAgent = FindObjectOfType<RoomPropertyAgent>();
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }
    ///<summary>
    /// DO NOT MODIFY GAME DATA IF YOU ARE NOT THE HOST.
    ///
    /// This methods should be called when the host is changing a value in
    /// gamedata. This will trigger a chain of events that will make sure
    /// the entire party has the same gamedata object. 
    ///</summary>
    public void ModifyGameData(int testValueData)
    {
        roomPropertyAgent.Modify(TEST_VALUE, testValueData);
    }

    ///<summary>
    /// Triggers a network event present in the RoomRemoteEventAgent.
    /// this makes sure everybody, including the host, will update their gamedata.
    ///<summary/>
    public void NotifyOtherPlayersTestValueChanged()
    {
        roomRemoteEventAgent.Invoke(TEST_VALUE_EVENT);
    }
    ///<summary>
    /// initializes the room property agent.
    ///</summary>
    public void EnableRoomPropertyAgent()
    {
        roomPropertyAgent.Initialize();
    }

    ///<summary>
    /// When updating the gamedata over the network, the game data will be unavailable
    /// until ready, which is when this method will run.
    ///</summary>
    public void OnTestDataReady()
    {
        Debug.Log("OnTestDataReady");
        int testValueData = roomPropertyAgent.GetPropertyWithName(TEST_VALUE).GetIntValue();
        OnGameDataReadyEvent.Invoke(testValueData);
    }

    ///<summary>
    /// When the host broadcasts the change of the value, this method will be run on 
    /// every local machine.
    ///</summary>
    public void OnTestDataChanged()
    {
        Debug.Log("OnTestDataChanged");
        int testValueData = roomPropertyAgent.GetPropertyWithName(TEST_VALUE).GetIntValue();
        OnGameDataChangedEvent.Invoke(testValueData);

    }

    ///<summary>
    /// when the client wants to change the value
    ///</summary>
    public void tellHostToChangeTestValue(int testValue)
    {
        Debug.Log("Telling host to change value.");
        SWNetworkMessage msg = new SWNetworkMessage();
        msg.Push(testValue);
        roomRemoteEventAgent.Invoke("ClientChangedTestValue", msg);
    }

    ///<summary>
    /// this method is run when the host is told by other players
    /// that they need to change the testValue.
    ///</summary>
    public void HostChangeTestValue(SWNetworkMessage msg){
        Debug.Log("Somebody is asking to change value.");
        int testValue = msg.PopInt32();
        this.ModifyGameData(testValue);
    }
    
}
