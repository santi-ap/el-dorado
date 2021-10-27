using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SWNetwork;
using System.Net.Http;
using UnityEngine.UI; // Required when Using UI elements.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class LobbyNetwork : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();
    public int NUMBER_OF_PLAYERS_TO_PLAY = 2;
    public Text playerNameInput;
    public LobbyUI lobbyUI;
    public RoleSO hunterRole;
    public RoleSO botanistRole;
    public RoleSO localGuideRole;
    public RoleSO archeologistRole;
    public Button registerPlayerButton;
    public Button createRoomButton;



    /// <summary>
    /// Current room's custom data.
    /// </summary>
    RoomCustomData roomData;

    /// <summary>
    /// Current page index of the room list.
    /// </summary>
    int currentRoomPageIndex = 0;

    /// <summary>
    /// the user that is currently playing in the local machine
    /// </summary>
    PlayerModel localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        createRoomButton.enabled = false;
        NetworkClient.Lobby.OnLobbyConnectedEvent += Lobby_OnLobbyConncetedEvent;

        NetworkClient.Lobby.OnNewPlayerJoinRoomEvent += Lobby_OnNewPlayerJoinRoomEvent;
        NetworkClient.Lobby.OnPlayerLeaveRoomEvent += Lobby_OnPlayerLeaveRoomEvent;
        NetworkClient.Lobby.OnRoomCustomDataChangeEvent += Lobby_OnRoomCustomDataChangeEvent;
        NetworkClient.Lobby.OnRoomReadyEvent += Lobby_OnStartGame;
    }

    void onDestroy()
    {
        NetworkClient.Lobby.OnLobbyConnectedEvent -= Lobby_OnLobbyConncetedEvent;
        NetworkClient.Lobby.OnNewPlayerJoinRoomEvent -= Lobby_OnNewPlayerJoinRoomEvent;
        NetworkClient.Lobby.OnPlayerLeaveRoomEvent -= Lobby_OnPlayerLeaveRoomEvent;
        NetworkClient.Lobby.OnRoomCustomDataChangeEvent -= Lobby_OnRoomCustomDataChangeEvent;
        NetworkClient.Lobby.OnRoomReadyEvent -= Lobby_OnStartGame;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterPlayer()
    {
        // store the playerName as id
        // player also used to register local player to the lobby server
        localPlayer = new PlayerModel();
        localPlayer.SetPlayerId(playerNameInput.text);
        localPlayer.playerName = playerNameInput.text;
        localPlayer.SetPlayerHealth(545450405);
        //The id is the player Name



        NetworkClient.Instance.CheckIn(localPlayer.GetPlayerId(), (bool successful, string error) =>
        {
            if (!successful)
            {
                Debug.LogError("Error while checking in player to SWLobby: \n" + error);
            }
            else
            {
                this.DisableRegsiterButton();
                lobbyUI.handleRegistrationUI();
                lobbyUI.handleUsernameText(localPlayer.playerName);
                Debug.Log("Player checked in successfully to SWLobby: " + localPlayer.GetPlayerId());
            }
        });


    }

    void Lobby_OnLobbyConncetedEvent()
    {
        Debug.Log("Lobby_OnLobbyConncetedEvent was triggered.");
        // Register the player using the entered player name.
        //*Loop through the available node regions and print the name of each one.
        // int count = 0;
        // foreach (NodeRegion nodeRegion in NetworkClient.Instance.AvailableNodeRegions)
        // {
        //     if (nodeRegion.name == "euro") {
        //         NetworkClient.Instance.NodeRegion = NetworkClient.Instance.AvailableNodeRegions[count].name;
        //         Debug.Log("NodeRegion changed to " + NetworkClient.Instance.NodeRegion);
        //     }
        //     Debug.Log("Node Region: #" + count);
        //     count++;
        //     Debug.Log("Node Region: " + nodeRegion.name);

        // }

        Debug.Log("NodeRegion: " + NetworkClient.Instance.NodeRegion);
        NetworkClient.Lobby.Register(localPlayer, (successful, reply, error) =>
        {
            if (successful)
            {
                Debug.Log("The local user has been successfully registered to SWLobby:\n" + reply);
                if (reply.started)
                {
                    // Player is in a room and the room has started.
                    // Call NetworkClient.Instance.ConnectToRoom to connect to the game servers of the room.
                }
                else if (reply.roomId != null)
                {
                    // Player is in a room.
                    GetRooms();
                    GetPlayersInCurrentRoom();
                }
                else
                {
                    // Player is not in a room.
                    GetRooms();
                }
            }
            else
            {
                Debug.Log("The local user failed to register to SWLobby:\n " + error);
            }
        });
    }

    public void CreateNewRoom()
    {
        List<RoleSO> playerRoleList = new List<RoleSO>() { hunterRole, botanistRole, localGuideRole, archeologistRole };
        //creating the room data object, adding a name and adding the player as the first player in lobby.
        roomData = new RoomCustomData(playerRoleList);
        //TODO There needs to be a check to see if the username is valid and registered
        roomData.name = localPlayer.GetPlayerId() + "'s Room";

        roomData.playerList.Add(localPlayer);

        // use the serializable roomData object as room's custom data.
        NetworkClient.Lobby.CreateRoom(roomData, false, NUMBER_OF_PLAYERS_TO_PLAY, (successful, reply, error) =>
        {
            if (successful)
            {
                Debug.Log("Room created successfully:\n" + reply);
                //connet
                // refresh the room list
                GetRooms();
                //makes the roomUI dissapear
                lobbyUI.handlePlayersUI();
                // refresh the player list
                GetPlayersInCurrentRoom();
            }
            else
            {
                Debug.LogError("Failed to create room:\n" + error);
            }
        });

    }


    public void GetRooms()
    {
        // Get the rooms for the current page.
        NetworkClient.Lobby.GetRooms(currentRoomPageIndex, 5, (successful, reply, error) =>
        {
            if (successful)
            {

                //checking if we actually got rooms and is not the first page 
                //(if the first page has no rooms theres no need to recall this method)
                if (currentRoomPageIndex != 0 && reply.currentPageCount == 0)
                {
                    currentRoomPageIndex = 0;
                    this.GetRooms();
                }
                else
                {
                    Debug.Log("Rooms received successfully: " + reply.currentPageCount);
                    // Remove rooms in the rooms list
                    lobbyUI.ClearRoomList();
                    foreach (SWRoom room in reply.rooms)
                    {

                        // Deserialize the room custom data.
                        RoomCustomData rData = room.GetCustomData<RoomCustomData>();
                        if (rData != null)
                        {
                            // Add rooms to the rooms list.
                            lobbyUI.AddRowForRoom(rData.name, room.id, OnRoomSelected);
                        }

                    }
                }
            }
            else
            {
                Debug.Log("Failed to get rooms:\n" + error);
            }
        });
    }

    public void OnRoomSelected(string roomId)
    {
        // Join the selected room
        NetworkClient.Lobby.JoinRoom(roomId, (successful, reply, error) =>
        {
            if (successful)
            {
                Debug.Log("Joined room:\n" + reply);

                // refresh the player list
                GetPlayersInCurrentRoom();
                //makes the roomlist disappear
                lobbyUI.handlePlayersUI();

            }
            else
            {
                Debug.Log("Failed to Join room:\n" + error);
            }
        });
    }

    public void GetPlayersInCurrentRoom()
    {
        NetworkClient.Lobby.GetPlayersInRoom((successful, reply, error) =>
        {
            if (successful)
            {
                // If its the owner and there are enough players
                // show the start game button.
                if (NetworkClient.Lobby.IsOwner && roomData.playerList.Count == NUMBER_OF_PLAYERS_TO_PLAY)
                {
                    lobbyUI.startButton.SetActive(true);
                }
                Debug.Log("Got players in current room:\n" + reply);

                foreach (SWPlayer player in reply.players)
                {
                    JObject json = JObject.Parse(player.data);
                    roomData.playerList.Add(new PlayerModel(player.id, (string)json["playerName"]));
                    Debug.Log(roomData.playerList);
                }
                // fetch the room custom data.
                GetRoomCustomData();
            }
            else
            {
                Debug.Log("Failed to get players in current room:\n" + error);
            }
        });
    }

    public void GetRoomCustomData()
    {
        NetworkClient.Lobby.GetRoomCustomData((successful, reply, error) =>
        {
            if (successful)
            {
                Debug.Log("RoomCustomData successfully acquired:\n" + reply);

                // Deserialize the room custom data.
                roomData = reply.GetCustomData<RoomCustomData>();
                if (roomData != null)
                {
                    RefreshPlayerList();
                }
                lobbyUI.handleRoomsText(roomData.name);
            }
            else
            {
                Debug.Log("Failed to get RoomCustomData:\n" + error);
            }
        });
    }

    void RefreshPlayerList()
    {
        // Use the room custom data, and the playerId and player Name dictionary to populate the player lsit

        lobbyUI.ClearPlayerList();
        foreach (PlayerModel p in roomData.playerList)
        {
            lobbyUI.AddRowForPlayer(p.playerName, p.GetPlayerId());
        }


    }


    void Lobby_OnNewPlayerJoinRoomEvent(SWJoinRoomEventData eventData)
    {
        Debug.Log("Player joined room.");

        if (NetworkClient.Lobby.IsOwner)
        {
            // Update the room custom data
            JObject json = JObject.Parse(eventData.data);
            roomData.playerList.Add(new PlayerModel((string)json["playerId"], (string)json["playerName"]));

            NetworkClient.Lobby.ChangeRoomCustomData(roomData, (bool successful, SWLobbyError error) =>
            {
                if (successful)
                {
                    Debug.Log("ChangeRoomCustomData successful");
                    GetPlayersInCurrentRoom();
                    RefreshPlayerList();
                }
                else
                {
                    Debug.Log("ChangeRoomCustomData failed:\n" + error);
                }
            });
            //Debug.Log("player name 0: " + roomData.playerList[0].playerName + " player role 0 : " + roomData.playerList[0].GetPlayerRole().roleName);
            //Debug.Log("player name 1: " + roomData.playerList[1].playerName + " player role 1 : " + roomData.playerList[1].GetPlayerRole().roleName);
            // Update the room custom data
            NetworkClient.Lobby.ChangeRoomCustomData(roomData, (bool successful, SWLobbyError error) =>
            {
                if (successful)
                {
                    Debug.Log("ChangeRoomCustomData successful.");
                    GetPlayersInCurrentRoom();
                    RefreshPlayerList();
                }
                else
                {
                    Debug.Log("ChangeRoomCustomData failed:\n" + error);
                }
            });
        }
    }

    void Lobby_OnPlayerLeaveRoomEvent(SWLeaveRoomEventData eventData)
    {
        Debug.Log("Player left room.");

        if (NetworkClient.Lobby.IsOwner)
        {
            // Remove the players from both team.
            // Debug.Log("This is the room data player list: " + roomData.playerList);

            PlayerModel toRemove = null;

            foreach (PlayerModel p in roomData.playerList)
            {
                foreach (string s in eventData.leavePlayerIds)
                {
                    if (s.Contains(p.GetPlayerId()))
                    {
                        toRemove = p;
                    }
                }
            }
            if (toRemove != null)
            {
                roomData.playerList.Remove(toRemove);
            }
            // Update the room custom data
            NetworkClient.Lobby.ChangeRoomCustomData(roomData, (bool successful, SWLobbyError error) =>
            {
                if (successful)
                {
                    Debug.Log("ChangeRoomCustomData successful.");
                    GetPlayersInCurrentRoom();
                    RefreshPlayerList();
                }
                else
                {
                    Debug.Log("ChangeRoomCustomData failed:\n" + error);
                }
            });
        }
    }

    void Lobby_OnRoomCustomDataChangeEvent(SWRoomCustomDataChangeEventData eventData)
    {
        Debug.Log("Room custom data changed:\n" + eventData);

        SWRoom room = NetworkClient.Lobby.RoomData;
        roomData = room.GetCustomData<RoomCustomData>();

        // Room custom data changed, refresh the player list.
        RefreshPlayerList();
    }

    public void LeaveRoom()
    {
        lobbyUI.startButton.SetActive(false);
        NetworkClient.Lobby.LeaveRoom((successful, error) =>
        {
            if (successful)
            {
                Debug.Log("Left room.");
                lobbyUI.ClearPlayerList();
                GetRooms();
                lobbyUI.revertPlayersUI();
            }
            else
            {
                Debug.Log("Failed to leave room." + error);
            }
        });
    }

    public void Lobby_StartGame()
    {
        if (NetworkClient.Lobby.IsOwner && roomData.playerList.Count == NUMBER_OF_PLAYERS_TO_PLAY)
        {
            NetworkClient.Lobby.StartRoom((successful, error) =>
            {
                if (successful)
                {
                    Debug.Log("Starting the room...");
                }
                else
                {
                    Debug.Log("Failed to start room:\n" + error);
                }
            });
        }
        else
        {
            Debug.Log("Not enough player in room.");
        }
    }

    public void Lobby_OnStartGame(SWRoomReadyEventData eventData)
    {

        NetworkClient.Instance.ConnectToRoom((successful) =>
        {
            if (successful)
            {
                Debug.Log("Room started.");
                //SceneManager.LoadScene("GameScene");
                SceneManager.LoadScene("DavidScene");
                // SceneManager.LoadScene("TomTestScene");
            }
            else
            {
                Debug.Log("The player could not be connected to game server.");
            }
        });

    }

    public void NextPage()
    {
        currentRoomPageIndex++;
        GetRooms();
    }


    public void PreviousPage()
    {
        currentRoomPageIndex--;
        if (currentRoomPageIndex < 0) currentRoomPageIndex = 0;
        GetRooms();
    }

    public void DisableRegsiterButton()
    {
        registerPlayerButton.enabled = false;
        createRoomButton.enabled = true;
    }

}
