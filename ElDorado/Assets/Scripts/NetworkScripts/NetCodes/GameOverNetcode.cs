using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class GameOverNetcode : MonoBehaviour
{
    RoomRemoteEventAgent roomRemoteEventAgent;
    public GameOverHandler gameOverHandler;

    private void Awake()
    {
        //We need to first get the objects we're going to interact with
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    public void InvokeGameOverAsHostEvent()
    {
        //* calls HandleGameOverAsHostEvent method
        roomRemoteEventAgent.Invoke("GameOverAsHostEvent");

    }

    public void HandleGameOverAsHostEvent()
    {
        gameOverHandler.GameOver();
    }
}
