using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class LobbyUI : MonoBehaviour
{
    public GameObject roomRowPrefab;
    public GameObject roomList;
    public GameObject playerRowPrefab;
    public GameObject playerList;

    //This is for the UI reacting to lobby registration and room selection
    public GameObject roomTablePanel;
    public GameObject playerTablePanel;
    public GameObject nameInput;
    public GameObject startButton;

    public Text usernameText;
    public Text roomsText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void RemoveAllChildren(Transform parent)
    {
        foreach (Transform childTransform in parent)
        {
            Destroy(childTransform.gameObject);
        }
    }

    public void handleRegistrationUI()
    {
        //Hide the NameInput 
        nameInput.SetActive(false);
        //Show RoomList
        roomTablePanel.SetActive(true);
    }

    public void handlePlayersUI()
    {
        //Hide the RoomList 
        roomTablePanel.SetActive(false);
        //Show PlayerList
        playerTablePanel.SetActive(true);
    }

    public void revertPlayersUI()
    {
        //Show the RoomList 
        roomTablePanel.SetActive(true);
        //Hides PlayerList
        playerTablePanel.SetActive(false);
    }

    public void handleUsernameText(String username)
    {
        usernameText.text += username;
    }

    public void handleRoomsText(String roomname)
    {
        roomsText.text = roomname;
    }

    /// <summary>
    /// Remove all the rows in the Room list.
    /// </summary>
    public void ClearRoomList()
    {
        Debug.Log("39: " + roomList.name);
        RemoveAllChildren(roomList.transform);
    }

    /// <summary>
    /// Add a row to the Room list.
    /// </summary>
    public void AddRowForRoom(string title, string objectId, TableRow.SelectedHandler callback)
    {
        AddRowToTable(roomList.transform, roomRowPrefab, title, objectId, callback);
    }

    // Helper methods
    void AddRowToTable(Transform table, GameObject rowPrefab, string title, string objectId)
    {
        GameObject newRow = Instantiate(rowPrefab, table);
        TableRow tableRow = newRow.GetComponent<TableRow>();
        tableRow.SetTitle(title);
        tableRow.SetObjectId(objectId);
    }

    void AddRowToTable(Transform table, GameObject rowPrefab, string title, string objectId, TableRow.SelectedHandler callback)
    {
        GameObject newRow = Instantiate(rowPrefab, table);
        TableRow tableRow = newRow.GetComponent<TableRow>();
        tableRow.OnSelected += callback;
        tableRow.SetTitle(title);
        tableRow.SetObjectId(objectId);
    }


    /// <summary>
    /// Add a row to the Player list.
    /// </summary>
    public void AddRowForPlayer(string title, string objectId)
    {
        AddRowToTable(playerList.transform, playerRowPrefab, title, objectId);
    }

    /// <summary>
    /// Remove all the rows in the Player list.
    /// </summary>
    public void ClearPlayerList()
    {
        RemoveAllChildren(playerList.transform);
    }

}