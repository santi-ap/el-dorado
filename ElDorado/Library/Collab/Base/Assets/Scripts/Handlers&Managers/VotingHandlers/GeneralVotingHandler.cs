using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWNetwork;

public class GeneralVotingHandler : MonoBehaviour
{
    private StateHandler stateHandler;//declare a variable of type StateHandler

    private VoteCounter voteCounter;
    public bool hasVoted = false;
    public GameObject noActionVoteSlotHolder;
    public int numberOfOverallVotes = 0;
    public GameObject winningOption;
    public bool isDraw = false;
    public MovementHandler movementHandler;
    public int maxAmountOfVotes = 4; //TODO change depending on alive players in party
    public VotingNetCode votingNetCode;
    public GameObject movementVotingUI;
    public GameObject kickingPlayerVotingUI;
    public string votingPlayerId;
    public bool isRequestFromGuest = false;
    public bool hostHasVoted = false;
    public PartyModel party;
    public PlayerModel thisPlayer;

    void Awake()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        //subscribe ResetVoteSlots to next state
        stateHandler.turnHasStartedStateEvent += ResetVoteSlots;
        stateHandler.movementStateHasStartedEvent += ResetVotingValues;
    }

    // Start is called before the first frame update
    void Start()
    {
        hostHasVoted = false;
        hasVoted = false;
        numberOfOverallVotes = 0;
        // maxAmountOfVotes = party.GetPlayerCount();
    }

    //method to get the option id of the button that was chosen
    public void SetOptionToId(GameObject voteSlotHolder)
    {
        if (!hasVoted && !GetCurrentPlayer().isDead) //checks if the player has already voted
        {
            int optionId = voteSlotHolder.GetComponentInParent<VoteCounter>().optionId;
            //* if I am the host
            if (NetworkClient.Instance.IsHost)
            {
                Debug.Log("I'm the host and can do stuff");
                //invoking SWN Event for this event.         
                votingNetCode.InvokeAddVoteAsHostEvent(optionId, NetworkClient.Instance.PlayerId);
                hostHasVoted = true;
            }//* if I am NOT the host
            else
            {
                Debug.Log("I'm not the host");
                //TODO add vote as guest
                votingNetCode.InvokeAddVoteAsGuestEvent(optionId, NetworkClient.Instance.PlayerId);
            }
            //calls the add vote method and send the option ID of the button that was chosen
        }
        else
        {
            Debug.Log("You already voted!");
        }
    }

    //adds a vote to the option that was chosen
    public void AddVote(int optionId)
    {

        // if (!hasVoted) //checks if the player has already voted
        // {
        // Debug.Log("Pressed on " + voteSlotHolder.name);
        //gets the vote slot holder option based on the option id
        GameObject voteSlotHolder = GetSlotHolder(optionId);
        //gets the amount of votes on that option
        voteCounter = voteSlotHolder.GetComponentInParent<VoteCounter>();
        //adds 1 to the amount of votes for that option
        voteCounter.numberOfVotes++;
        //adds 1 to the amount of overall votes
        numberOfOverallVotes++;
        //* checks if it's the same player who performed the vote
        Debug.Log("You are: " + NetworkClient.Instance.PlayerId + " - Vote came from: " + votingPlayerId);
        if (votingPlayerId == NetworkClient.Instance.PlayerId || isRequestFromGuest)
        {
            if (NetworkClient.Instance.IsHost)
            {
                if (hostHasVoted)
                {
                    this.hasVoted = true;
                }
            }
            else
            {
                this.hasVoted = true;
            }

            isRequestFromGuest = false;

        }
        //calls the method to show the votes in the UI
        ShowVote(voteSlotHolder);//TODO this one has to happen locally for everyone
                                 //set the winning option to be used in HandleVoteResult
        SetWinningOption(voteSlotHolder.transform.parent.gameObject);
        if (numberOfOverallVotes == maxAmountOfVotes)
        {
            HandleVoteResult();
        }
        // }
        // else
        // {
        //     Debug.Log("You already voted!");
        // }
    }

    //shows the amount of votes in the UI
    public void ShowVote(GameObject voteSlotHolder)
    {
        //a place holder of the amount of votes for the loop
        int votePlaceHolder = voteCounter.numberOfVotes;
        //looks for all the child transforms in the voteSlotHolder game object

        //goes through each vote slot in the UI
        foreach (Transform childTransform in voteSlotHolder.transform)
        {
            //as long as there are still votes left
            if (votePlaceHolder > 0)
            {
                //just double checks that it's the vote slot game object
                if (childTransform.gameObject.tag == "VoteSlot")
                {
                    // Debug.Log("Child name " + childTransform.gameObject.name);
                    //sets the slot color to white to show a vote
                    childTransform.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    //removes 1 from the place holder
                    votePlaceHolder--;
                }
            }
            else
            {
                //this happens when there's no more votes to show on the same option
                break;
            }
        }
    }

    public void HandleVoteResult()
    {
        noActionVoteSlotHolder.GetComponentInParent<VoteCounter>().numberOfVotes = 0;
        //if there is a draw
        if (isDraw)
        {
            this.ResetVotingValues();
            Debug.Log("There is a draw");
            //TODO go to next turn
            stateHandler.SetState(StateHandler.GameState.EatingState);
            stateHandler.SetState(StateHandler.GameState.StartTurnState);
        }//if there is a winner
        else
        {
            //checks the current game state to determine which action to take
            switch (stateHandler.GetGameState())
            {
                //if it's for moving to node
                case StateHandler.GameState.MovementState:
                    Debug.Log("Winner is: " + winningOption.name);
                    //calls the method that takes care of performing the move
                    movementHandler.MoveToNode(winningOption);
                    this.ResetVotingValues();
                    break;
                //TODO if it's for kicking player
                case StateHandler.GameState.KickVoteState:
                    this.ResetVotingValues();
                    break;
            }
        }
    }

    public void SetWinningOption(GameObject optionToCheckAgainst)
    {
        //if there hans't been set a winning option yet
        if (winningOption == null)
        {
            //make the first option the winning option
            winningOption = optionToCheckAgainst;
        }
        else//if there is already a winning option
        {
            if (winningOption == optionToCheckAgainst)
            {
                isDraw = false;
            }
            //if the currently selected option has more votes than the current winning option
            else if (winningOption.GetComponent<VoteCounter>().numberOfVotes < optionToCheckAgainst.GetComponent<VoteCounter>().numberOfVotes)
            {
                //make the currently selected option the winning option
                winningOption = optionToCheckAgainst;
                //remove the draw
                isDraw = false;
            }//if the currently selected option has the same amount of votes as the current winning option
            else if (winningOption.GetComponent<VoteCounter>().numberOfVotes == optionToCheckAgainst.GetComponent<VoteCounter>().numberOfVotes)
            {
                //make a draw
                isDraw = true;
            }
        }
    }

    //Method change the color back of the voting slots
    public void ResetVoteSlots()
    {
        //goes through each vote slot in the UI
        foreach (Transform childTransform in noActionVoteSlotHolder.transform)
        {
            //just double checks that it's the vote slot game object
            if (childTransform.gameObject.tag == "VoteSlot")
            {
                // Debug.Log("Child name " + childTransform.gameObject.name);
                //sets the slot color to faded black to show a vote
                childTransform.gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 147);
            }
        }
    }

    //gets the game object that will be used in this class based on the option id
    public GameObject GetSlotHolder(int optionId)
    {
        //checks the current game state to determine which parent object to look through
        switch (stateHandler.GetGameState())
        {
            //if in the movement stae
            case StateHandler.GameState.MovementState:
                //goes through each option
                foreach (Transform childTransform in movementVotingUI.transform)
                {
                    //finds the option that has the option ID we're looking for
                    if (childTransform.gameObject.GetComponent<VoteCounter>().optionId == optionId)
                    {
                        //returns the game object that we're using in this class
                        return childTransform.Find("VoteSlotHolder").gameObject;
                    }
                }
                break;
            //TODO if it's for kicking player
            case StateHandler.GameState.KickVoteState:
                /* //TODO when we have the kicking player UI parent gameObject
                    //goes through each option
                    foreach (Transform childTransform in movementVotingUI.transform)
                    {
                        //finds the option that has the option ID we're looking for
                        if (childTransform.gameObject.GetComponent<VoteCounter>().optionId == optionId)
                        {
                            //returns the game object that we're using in this class
                            return childTransform.Find("VoteSlotHolder").gameObject;
                        }
                    }
                    */
                break;
        }
        return null;
    }

    public void SendRequestToHost(int incomingOptionId, string incomingPlayerId)
    {
        votingNetCode.InvokeAddVoteAsHostEvent(incomingOptionId, incomingPlayerId);
    }

    public void ResetVotingValues()
    {
        hostHasVoted = false;
        hasVoted = false;
        numberOfOverallVotes = 0;
        maxAmountOfVotes = party.GetPlayerCount();
        winningOption = null;
    }

    public PlayerModel GetCurrentPlayer()
    {
        foreach (PlayerModel player in party.playerList)
        {
            if (player.GetPlayerId() == NetworkClient.Instance.PlayerId)
            {
                return player;
            }
        }
        return null;
    }
}
