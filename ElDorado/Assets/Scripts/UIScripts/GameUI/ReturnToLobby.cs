using UnityEngine;
using UnityEngine.SceneManagement;
using SWNetwork;

public class ReturnToLobby : MonoBehaviour
{
    public PlayerDeathHandler playerDeathHandler;
    public PartyModel party;
    public PlayerModel thisPlayer;
    public NetCode netCode;
    public PartyNetCode partyNetCode;
    public NotificationNetcode notificationNetcode;
    public RoleIconInPartyHandler roleIconInPartyHandler;
    public GameOverHandler gameOverHandler;

    void Awake()
    {
        playerDeathHandler = new PlayerDeathHandler();
        playerDeathHandler.notificationNetcode = this.notificationNetcode;
        playerDeathHandler.roleIconInPartyHandler = this.roleIconInPartyHandler;
    }

    public void ExitToLobby()
    {
        SetCurrentPlayer();

        if (NetworkClient.Instance.IsHost)
        {
            playerDeathHandler.PlayerDeath(thisPlayer);
            gameOverHandler.CheckPlayersDead(this.party);
            partyNetCode.ModifyParty(party);
        }
        else
        {
            netCode.InvokePlayerDeathEventClient(thisPlayer.GetPlayerId());
        }
        LeaveRoom();
        // SceneManager.LoadScene("Lobby");
        Application.Quit();
    }

    public void SetCurrentPlayer()
    {
        foreach (PlayerModel player in party.playerList)
        {
            if (player.GetPlayerId() == NetworkClient.Instance.PlayerId)
            {
                thisPlayer = player;
            }
        }
    }

    public void HandlePLayerDeathForClient(string playerId)
    {
        foreach (PlayerModel p in party.playerList)
        {
            Debug.Log("this is the player id from the party: " + p.GetPlayerId());
            if (playerId.Equals(p.GetPlayerId()))
            {
                thisPlayer = p;
            }
        }

        playerDeathHandler.PlayerDeath(thisPlayer);
        gameOverHandler.CheckPlayersDead(this.party);
        partyNetCode.ModifyParty(party);
    }


    public void LeaveRoom()
    {
        NetworkClient.Lobby.LeaveRoom((successful, error) =>
        {
            if (successful)
            {
                Debug.Log("Left room.");
            }
            else
            {
                Debug.Log("Failed to leave room." + error);
            }
        });
    }
}
