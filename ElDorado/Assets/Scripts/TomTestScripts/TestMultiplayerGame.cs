using System.Collections;
using System.Collections.Generic;
using SWNetwork;
using UnityEngine;

public class TestMultiplayerGame : MonoBehaviour {
    protected TestGameData gameData;
    protected TestNetCode netCode;

    void Awake () {
        // netCode is found
        gameData = new TestGameData();
        netCode = FindObjectOfType<TestNetCode> ();

        NetworkClient.Lobby.GetPlayersInRoom ((successful, reply, error) => {
            if (successful) {
                //Looping through players
                foreach (SWPlayer swPlayer in reply.players) {
                    Debug.Log ("Player: " + swPlayer.id + "\nCustomData: " + swPlayer.GetCustomDataString ());

                    // TODO Assign players to their PlayerGameObject, which in the case of 
                    // the player will be the instance that makes the decisions

                    // Call the custom data for each to get the info:
                    // string playerName = swPlayer.GetCustomDataString();
                    // and the id:
                    // string playerId = swPlayer.id;

                    // This code defines, in a 2 player game, who is going to be the
                    // player that is local and who is the remote player.
                    // Keep in mind that this code is run in every single machine
                    // so players will be assigned to their local instance and everybody
                    // else is going to be set as the remote player.

                    // if (playerId.Equals(NetworkClient.Instance.PlayerId))
                    // {
                    //     localPlayer.PlayerId = playerId;
                    //     localPlayer.PlayerName = playerName;
                    // }
                    // else
                    // {
                    //     remotePlayer.PlayerId = playerId;
                    //     remotePlayer.PlayerName = playerName;
                    // }
                }

                // This is just a class that holds important information
                // the idea of encrypting the data is to prevent people from 
                // easily reading it and cheat.
                // gameDataManager = new GameDataManager(localPlayer, remotePlayer, NetworkClient.Lobby.RoomId);

                //Netcode Initialized
                netCode.EnableRoomPropertyAgent ();
            } else {
                Debug.Log ("Failed to get players in room.");
            }

        });
    }

    //****************** NetCode Events *********************//
    public void OnGameDataReady (int testValueData) {
        Debug.Log ("Game.OnGameDataReady says: this is the value " + testValueData);
        if (testValueData == 0) {
            Debug.Log ("OnGameDataReady says: game is new");
            if (NetworkClient.Instance.IsHost) {
                //this piece of code is initializing the testvalue
                Debug.Log ("OnGameDataReady says: game data will be set to 1 and broadcast.");
                netCode.ModifyGameData(1);
                netCode.NotifyOtherPlayersTestValueChanged();
            }
        } else { }
    }
    ///<summary>
    /// This method handles the OnGameDataChangedEvent
    /// which is invoked during NetCode.OnTestDataChanged()
    ///</summary>
    public void OnGameDataChanged (int testValue) {
        Debug.Log ("OnGameDataChanged says: game data was changed remotely and broadcast to us.\nValue: " + testValue);
        gameData.testValueData = testValue;
    }

    public void OnTestButtonPressed () {
        Debug.Log ("The Test Button is pressed");
        if (NetworkClient.Instance.IsHost) {
            Debug.Log ("Host Set Value to " + gameData.testValueData+ "+ 1");
            netCode.ModifyGameData(gameData.testValueData+1);
        } else {
            Debug.Log ("Tell the host to change the value to " + gameData.testValueData + "+ 1");
            netCode.tellHostToChangeTestValue(gameData.testValueData+1);
        }
    }

    

}