using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;
using System;

public class InventoryNetCode : MonoBehaviour
{

    RoomPropertyAgent roomPropertyAgent;
    RoomRemoteEventAgent roomRemoteEventAgent;
    public ShowOutcome showOutcome;

    readonly string FOOD_COUNT = "foodCount";
    readonly string ARTIFACT_COUNT = "artifactCount";

    private void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomPropertyAgent = FindObjectOfType<RoomPropertyAgent>();
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    ///<summary>
    /// DO NOT MODIFY GAME DATA IF YOU ARE NOT THE HOST.
    ///
    /// This methods should be called when the host is changing a value in
    /// inventory. This will trigger a chain of events that will make sure
    /// the entire party has the same amount of food. 
    ///</summary>
    public void ModifyFoodCount(float foodCount)
    {
        roomPropertyAgent.Modify(FOOD_COUNT, foodCount.ToString());
    }

    ///<summary>
    /// When generating the food count synced property over the network, the game data will be unavailable
    /// until ready, which is when this method will run.
    ///</summary>
    public void OnFoodCountReady()
    {
        //Debug.Log("FoodCount Ready");
        if(NetworkClient.Instance.IsHost)
        {
            this.ModifyFoodCount(0f);
        }
    }

    public void OnFoodCountModified(){
        //Debug.Log("60");
        //Debug.Log("Food " +roomPropertyAgent.GetPropertyWithName(FOOD_COUNT).GetStringValue());
        double foodCount = Double.Parse(roomPropertyAgent.GetPropertyWithName(FOOD_COUNT).GetStringValue());
        this.showOutcome.OnModifyFoodAmountEvent(foodCount);
    }

    ///<summary>
    /// DO NOT MODIFY GAME DATA IF YOU ARE NOT THE HOST.
    ///
    /// This methods should be called when the host is changing a value in
    /// inventory. This will trigger a chain of events that will make sure
    /// the entire party has the same amount of food. 
    ///</summary>
    public void ModifyArtifactCount(float artifactCount)
    {
        roomPropertyAgent.Modify(ARTIFACT_COUNT, artifactCount.ToString());
    }

    ///<summary>
    /// When updating the food count over the network, the game data will be unavailable
    /// until ready, which is when this method will run.
    ///</summary>
    public void OnArtifactCountReady()
    {
        //Debug.Log("ArtifactCount Ready");
        if(NetworkClient.Instance.IsHost)
        {
            this.ModifyArtifactCount(0f);
        }
    }

    public void OnArtifactCountModified(){
        //Debug.Log("90 \nArtifacts: " + roomPropertyAgent.GetPropertyWithName(ARTIFACT_COUNT).GetStringValue());
        double artifactCount = Double.Parse(roomPropertyAgent.GetPropertyWithName(ARTIFACT_COUNT).GetStringValue());
        this.showOutcome.OnModifyArtifactAmountEvent(artifactCount);
    }
}