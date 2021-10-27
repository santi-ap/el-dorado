using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SWNetwork;


public class ShowOutcome : MonoBehaviour
{
    private StateHandler stateHandler;

    public GameObject Panel;
    public Text outcomeText;
    public string resultOutcome;
    public PartyModel party;
    public PlayerModel thisPlayer;
    public RoleSO hunterRole;
    public NodeModel currentNode;
    public OutcomeUIHandler outcomeUIHandler;
    public OutcomeHandler outcomeHandler;
    public NetCode netCode;
    public InventoryNetCode inventoryNetCode;
    public int playerActionCount;
    public PartyNetCode partyNetCode;
    public ExplorationResult explorationResult;


    private int actionId; //* 1:hunt 2:forage 3:explore 4: excavate

    EventModel[,] huntOutcomeArray;
    EventModel[,] forageOutcomeArray;
    EventModel[,] excavateOutcomeArray;
    EventModel[,] exploreOutcomeArray;

    void Awake()
    {
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        // explorationResult = gameObject.AddComponent<ExplorationResult>();
        stateHandler.turnHasStartedStateEvent += SetCurrentNode;
    }

    int HuntingLifeLossRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 10);
            // return;
            case 2:
                return UnityEngine.Random.Range(10, 20);
            case 3:
                return UnityEngine.Random.Range(30, 40);
            case 4:
                return UnityEngine.Random.Range(40, 50);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }

    int HuntingResourceGainRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 10);
            case 2:
                return UnityEngine.Random.Range(10, 20);
            case 3:
                return UnityEngine.Random.Range(30, 40);
            case 4:
                return UnityEngine.Random.Range(40, 50);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }

    int ForageLifeLossRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 10);
            case 2:
                return UnityEngine.Random.Range(10, 20);
            case 3:
                return UnityEngine.Random.Range(30, 40);
            case 4:
                return UnityEngine.Random.Range(40, 50);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }

    int ForageResourceGainRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 10);
            // return;
            case 2:
                return UnityEngine.Random.Range(10, 20);
            case 3:
                return UnityEngine.Random.Range(30, 40);
            case 4:
                return UnityEngine.Random.Range(40, 50);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }

    int ExcavateLifeLossRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 10);
            case 2:
                return UnityEngine.Random.Range(10, 20);
            case 3:
                return UnityEngine.Random.Range(30, 40);
            case 4:
                return UnityEngine.Random.Range(40, 50);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }

    int ExcavateResourceGainRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 10);
            // return;
            case 2:
                return UnityEngine.Random.Range(10, 20);
            case 3:
                return UnityEngine.Random.Range(30, 40);
            case 4:
                return UnityEngine.Random.Range(40, 50);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }

    int ExploreLifeLossRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 5);
            case 2:
                return UnityEngine.Random.Range(6, 10);
            case 3:
                return UnityEngine.Random.Range(11, 15);
            case 4:
                return UnityEngine.Random.Range(16, 20);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }

    int ExploreResourceGainRange(int range)
    {
        switch (range)
        {
            case 1:
                return UnityEngine.Random.Range(1, 3);
            // return;
            case 2:
                return UnityEngine.Random.Range(4, 7);
            case 3:
                return UnityEngine.Random.Range(8, 10);
            case 4:
                return UnityEngine.Random.Range(11, 13);
            default:
                Debug.Log("Not an acceptable range!");
                throw new InvalidOperationException("Not an acceptable range!");
        }
    }



    public void init()
    {
        //TODO change this to use the actual player info
        //player
        // hunterRole = ScriptableObject.CreateInstance("RoleSO") as RoleSO;
        // hunterRole.roleName = "Hunter";
        // hunterRole.rollStatForHunting = 1.2;
        // hunterRole.rollStatForExcavating = 1.1;
        // hunterRole.rollStatForExploring = 1.1;
        // hunterRole.rollStatForGathering = 1.1;
        // this.thisPlayer = new PlayerModel();
        // thisPlayer.SetPlayerRole(hunterRole);
        //

        //Node
        // currentNode = party.currentNode;

        //Definition of Choices Arrays

        //Hunt Outcomes Array          [Dimensions of the Array]
        //                                  ↓
        huntOutcomeArray = new EventModel[4, 4]
            {
            { new EventModel(HuntingLifeLossRange(1),HuntingResourceGainRange(1),"huntEvent 1,1"), new EventModel(HuntingLifeLossRange(1),HuntingResourceGainRange(2),"huntEvent 1,2") ,new EventModel(HuntingLifeLossRange(1),HuntingResourceGainRange(3),"huntEvent 1,3"), new EventModel(HuntingLifeLossRange(1),HuntingResourceGainRange(4),"huntEvent 1,4")},
            { new EventModel(HuntingLifeLossRange(2),HuntingResourceGainRange(1),"huntEvent 2,1"), new EventModel(HuntingLifeLossRange(2),HuntingResourceGainRange(2),"huntEvent 2,2") ,new EventModel(HuntingLifeLossRange(2),HuntingResourceGainRange(3),"huntEvent 2,3"), new EventModel(HuntingLifeLossRange(2),HuntingResourceGainRange(4),"huntEvent 2,4")},
            { new EventModel(HuntingLifeLossRange(3),HuntingResourceGainRange(1),"huntEvent 3,1"), new EventModel(HuntingLifeLossRange(3),HuntingResourceGainRange(2),"huntEvent 3,2") ,new EventModel(HuntingLifeLossRange(3),HuntingResourceGainRange(3),"huntEvent 3,3"), new EventModel(HuntingLifeLossRange(3),HuntingResourceGainRange(4),"huntEvent 3,4")},
            { new EventModel(HuntingLifeLossRange(4),HuntingResourceGainRange(1),"huntEvent 4,1"), new EventModel(HuntingLifeLossRange(4),HuntingResourceGainRange(2),"huntEvent 4,2") ,new EventModel(HuntingLifeLossRange(4),HuntingResourceGainRange(3),"huntEvent 4,3"), new EventModel(HuntingLifeLossRange(4),HuntingResourceGainRange(4),"huntEvent 4,4")}
            };

        //Forage Outcomes Array
        forageOutcomeArray = new EventModel[4, 4]
            {
            { new EventModel(ForageLifeLossRange(1),ForageResourceGainRange(1),"forageEvent 1,1"), new EventModel(ForageLifeLossRange(1),ForageResourceGainRange(2),"forageEvent 1,2") ,new EventModel(ForageLifeLossRange(1),ForageResourceGainRange(3),"forageEvent 1,3"), new EventModel(ForageLifeLossRange(1),ForageResourceGainRange(4),"forageEvent 1,4")},
            { new EventModel(ForageLifeLossRange(2),ForageResourceGainRange(1),"forageEvent 2,1"), new EventModel(ForageLifeLossRange(2),ForageResourceGainRange(2),"forageEvent 2,2") ,new EventModel(ForageLifeLossRange(2),ForageResourceGainRange(3),"forageEvent 2,3"), new EventModel(ForageLifeLossRange(2),ForageResourceGainRange(4),"forageEvent 2,4")},
            { new EventModel(ForageLifeLossRange(3),ForageResourceGainRange(1),"forageEvent 3,1"), new EventModel(ForageLifeLossRange(3),ForageResourceGainRange(2),"forageEvent 3,2") ,new EventModel(ForageLifeLossRange(3),ForageResourceGainRange(3),"forageEvent 3,3"), new EventModel(ForageLifeLossRange(3),ForageResourceGainRange(4),"forageEvent 3,4")},
            { new EventModel(ForageLifeLossRange(4),ForageResourceGainRange(1),"forageEvent 4,1"), new EventModel(ForageLifeLossRange(4),ForageResourceGainRange(2),"forageEvent 4,2") ,new EventModel(ForageLifeLossRange(4),ForageResourceGainRange(3),"forageEvent 4,3"), new EventModel(ForageLifeLossRange(4),HuntingResourceGainRange(4),"forageEvent 4,4")}
            };
        //Excavate Outcomes Array
        excavateOutcomeArray = new EventModel[4, 4]
            {
            { new EventModel(ExcavateLifeLossRange(1),ExcavateResourceGainRange(1),"excavateEvent 1,1"), new EventModel(ExcavateLifeLossRange(1),ExcavateResourceGainRange(2),"excavateEvent 1,2") ,new EventModel(ExcavateLifeLossRange(1),ExcavateResourceGainRange(3),"excavateEvent 1,3"), new EventModel(ExcavateLifeLossRange(1),ExcavateResourceGainRange(4),"excavateEvent 1,4")},
            { new EventModel(ExcavateLifeLossRange(2),ExcavateResourceGainRange(1),"excavateEvent 2,1"), new EventModel(ExcavateLifeLossRange(2),ExcavateResourceGainRange(2),"excavateEvent 2,2") ,new EventModel(ExcavateLifeLossRange(2),ExcavateResourceGainRange(3),"excavateEvent 2,3"), new EventModel(ExcavateLifeLossRange(2),ExcavateResourceGainRange(4),"excavateEvent 2,4")},
            { new EventModel(ExcavateLifeLossRange(3),ExcavateResourceGainRange(1),"excavateEvent 3,1"), new EventModel(ExcavateLifeLossRange(3),ExcavateResourceGainRange(2),"excavateEvent 3,2") ,new EventModel(ExcavateLifeLossRange(3),ExcavateResourceGainRange(3),"excavateEvent 3,3"), new EventModel(ExcavateLifeLossRange(3),ExcavateResourceGainRange(4),"excavateEvent 3,4")},
            { new EventModel(ExcavateLifeLossRange(4),ExcavateResourceGainRange(1),"excavateEvent 4,1"), new EventModel(ExcavateLifeLossRange(4),ExcavateResourceGainRange(2),"excavateEvent 4,2") ,new EventModel(ExcavateLifeLossRange(4),ExcavateResourceGainRange(3),"excavateEvent 4,3"), new EventModel(ExcavateLifeLossRange(4),ExcavateResourceGainRange(4),"huntEvent 4,4")}
            };
        //Explore Outcomes Array
        exploreOutcomeArray = new EventModel[4, 4]
            {
            { new EventModel(ExploreLifeLossRange(1),ExploreResourceGainRange(1),"exploreEvent 1,1"), new EventModel(ExploreLifeLossRange(1),ExploreResourceGainRange(2),"exploreEvent 1,2") ,new EventModel(ExploreLifeLossRange(1),ExploreResourceGainRange(3),"exploreEvent 1,3"), new EventModel(ExploreLifeLossRange(1),ExploreResourceGainRange(4),"exploreEvent 1,4")},
            { new EventModel(ExploreLifeLossRange(2),ExploreResourceGainRange(1),"exploreEvent 2,1"), new EventModel(ExploreLifeLossRange(2),ExploreResourceGainRange(2),"exploreEvent 2,2") ,new EventModel(ExploreLifeLossRange(2),ExploreResourceGainRange(3),"exploreEvent 2,3"), new EventModel(ExploreLifeLossRange(2),ExploreResourceGainRange(4),"exploreEvent 2,4")},
            { new EventModel(ExploreLifeLossRange(3),ExploreResourceGainRange(1),"exploreEvent 3,1"), new EventModel(ExploreLifeLossRange(3),ExploreResourceGainRange(2),"exploreEvent 3,2") ,new EventModel(ExploreLifeLossRange(3),ExploreResourceGainRange(3),"exploreEvent 3,3"), new EventModel(ExploreLifeLossRange(3),ExploreResourceGainRange(4),"exploreEvent 3,4")},
            { new EventModel(ExploreLifeLossRange(4),ExploreResourceGainRange(1),"exploreEvent 4,1"), new EventModel(ExploreLifeLossRange(4),ExploreResourceGainRange(2),"exploreEvent 4,2") ,new EventModel(ExploreLifeLossRange(4),ExploreResourceGainRange(3),"exploreEvent 4,3"), new EventModel(ExploreLifeLossRange(4),ExploreResourceGainRange(4),"exploreEvent 4,4")}
            };
    }

    void Start()
    {
        init();
    }

    public void OpenPanel()
    {
        if (Panel.activeSelf == false)
        {
            //calling the showOutcome mehtod with hardcoded variables CHANGE LATER
            //this.showOutcome(1,1,1,ActionEnumerator.hunt);
            Panel.SetActive(true);
        }
    }

    public void HandleOutcomeCalculationDuty(double playerAbility, double nodeSuccessDifficulty, double nodeLifeDifficulty, ActionEnumerator action, string playerId)
    {
        if (NetworkClient.Instance.IsHost)
        {
            Debug.Log("I'm the host and can do stuff");
            EventModel resultOutcomeEvent = this.CalculateOutcome(playerAbility, nodeSuccessDifficulty, nodeLifeDifficulty, action);
            foreach(PlayerModel p in party.playerList) {
                if (p.GetPlayerId() == playerId){
                    p.SetPlayerHealth(p.GetPlayerHealth() - resultOutcomeEvent.lifeLoss);
                }
            }
            partyNetCode.ModifyParty(party);

            outcomeUIHandler.ShowActionOutcome(resultOutcomeEvent.message);
            //invoking SWN Event for this event.         
            netCode.InvokeShowOutcomeEvent(resultOutcomeEvent.message);
            //lets update the inventory:
            this.UpdateInventory(action, resultOutcomeEvent.resourceGain);
            this.playerActionCount++; //Adds to the amount of actions done
            if (playerActionCount == party.GetPlayerCount())
            { // If the amount of actions is 4, state will change to voting
                this.playerActionCount = 0;
                netCode.InvokeStateChangedEvent();
            }
        }
        else
        {
            Debug.Log("I'm not the host");
            //Switch to set the actionID
            switch (action)
            {
                //HUNT case
                case ActionEnumerator.hunt:
                    actionId = 1;
                    break;
                //FORAGE case
                case ActionEnumerator.forage:
                    actionId = 2;
                    break;
                //EXPLORE case
                case ActionEnumerator.explore:
                    actionId = 3;
                    break;
                //EXCAVATE case
                case ActionEnumerator.excavate:
                    actionId = 4;
                    break;

            }
            netCode.InvokeShowOutcomeEventClient(NetworkClient.Instance.PlayerId, actionId);
        }
    }

    public EventModel CalculateOutcomeForClient(string playerId, int actionId)
    {
        double nodeSuccessDifficulty = 0;
        double nodeLifeDifficulty = 0;
        ActionEnumerator action = ActionEnumerator.hunt;
        PlayerModel player = new PlayerModel();
        double playerAbility = 0;


        Debug.Log("This is the playerId in the party model: " + playerId);

        
        foreach (PlayerModel p in party.playerList) {
            Debug.Log("this is the player id from the party: " + p.GetPlayerId());
            if (playerId.Equals(p.GetPlayerId())) {
                 player = p;
            }
        }

        //make the global player model of this class equal to the player model of the guest
        this.thisPlayer = player;
        

        switch (actionId)
        {
            case 1://hunting
                nodeSuccessDifficulty = this.currentNode.huntingNodeSuccessDificulty;
                nodeLifeDifficulty = this.currentNode.huntingNodeLifeLossDificulty;
                action = ActionEnumerator.hunt;
                playerAbility = player.GetPlayerRole().rollStatForHunting;
                break;
            case 2://foraging
                nodeSuccessDifficulty = this.currentNode.forageNodeSuccessDificulty;
                nodeLifeDifficulty = this.currentNode.forageNodeLifeLossDificulty;
                action = ActionEnumerator.forage;
                playerAbility = player.GetPlayerRole().rollStatForGathering;
                break;
            case 3://exploring
                nodeSuccessDifficulty = this.currentNode.exploreNodeSuccessDificulty;
                nodeLifeDifficulty = this.currentNode.exploreNodeLifeLossDificulty;
                action = ActionEnumerator.explore;
                playerAbility = player.GetPlayerRole().rollStatForExploring;
                break;
            case 4://excavating
                nodeSuccessDifficulty = this.currentNode.excavateNodeSuccessDificulty;
                nodeLifeDifficulty = this.currentNode.excavateNodeLifeLossDificulty;
                action = ActionEnumerator.excavate;
                playerAbility = player.GetPlayerRole().rollStatForExcavating;
                break;
        }


        EventModel outcomeResultEvent = this.CalculateOutcome(playerAbility, nodeSuccessDifficulty, nodeLifeDifficulty, action);
        //TODO get the life loss, remove life from correct player and update party.
        foreach(PlayerModel p in party.playerList) {
                if (p.GetPlayerId() == player.GetPlayerId()){
                    p.SetPlayerHealth(p.GetPlayerHealth() - outcomeResultEvent.lifeLoss);
                }
            }
            partyNetCode.ModifyParty(party);

        netCode.InvokeShowOutcomeEvent(outcomeResultEvent.message);
        netCode.InvokeReturnResultToClientEvent(outcomeResultEvent.message, playerId);
        this.UpdateInventory(action, outcomeResultEvent.resourceGain);
        this.playerActionCount++; //Adds to the amount of actions done
        if (playerActionCount == party.GetPlayerCount())
        { // If the amount of actions is , state will change to voting
            this.playerActionCount = 0;
            netCode.InvokeStateChangedEvent();
        }
        return outcomeResultEvent;
    }

    public EventModel CalculateOutcome(double playerAbility, double nodeSuccessDifficulty, double nodeLifeDifficulty, ActionEnumerator action)
    {
        EventModel result = new EventModel(0, 0, " ");
        // OutcomeHandler outcomeHandler = new OutcomeHandler();

        //THESE ARE THE GOOD DEFINITIONS
        int lifeposition = outcomeHandler.ActionCalculation(playerAbility, nodeLifeDifficulty);
        int resourcenumber = outcomeHandler.ActionCalculation(playerAbility, nodeSuccessDifficulty);

        // Debug.Log("lifepos: " + lifeposition);
        // Debug.Log("res: " + resourcenumber);

        //Switch to show a different text outcome depending on the Choice
        switch (action)
        {
            //HUNT case
            case ActionEnumerator.hunt:
                result = huntOutcomeArray[lifeposition - 1, resourcenumber - 1];
                resultOutcome = "You got " + result.resourceGain + " food and lost " + result.lifeLoss + " health!";
                actionId = 1;
                break;
            //FORAGE case
            case ActionEnumerator.forage:
                result = forageOutcomeArray[lifeposition - 1, resourcenumber - 1];
                resultOutcome = "You gathered " + result.resourceGain + " food and lost " + result.lifeLoss + " health!";
                actionId = 2;
                break;
            //EXPLORE case
            case ActionEnumerator.explore:
                result = exploreOutcomeArray[lifeposition - 1, resourcenumber - 1];
                // resultOutcome = "You explored " + result.resourceGain + " node(s) and lost " + result.lifeLoss + " health!";
                actionId = 3;
                //* calls method to return exploration result
                resultOutcome = explorationResult.ReturnExplorationResult(thisPlayer, currentNode, resourcenumber, lifeposition, result);
                break;
            //EXCAVATE case
            case ActionEnumerator.excavate:
                result = excavateOutcomeArray[lifeposition - 1, resourcenumber - 1];
                resultOutcome = "You found " + result.resourceGain + " artifact(s) and lost " + result.lifeLoss + " health!";
                actionId = 4;
                break;

        }

        // Debug.Log("Life number is: " + lifeposition);
        // Debug.Log("resource number is: " + resourcenumber);
        result.message = resultOutcome;
        return result;
    }

    public void HandleSWNEventTest(string resultOutcome)
    {
        Debug.Log(resultOutcome);
    }

    public void HandleOutComeUIChange(string resultOutcome)
    {
        outcomeUIHandler.ShowActionOutcome(resultOutcome);
    }

    public void huntAction()
    {
        SetCurrentPlayer();
        this.HandleOutcomeCalculationDuty(thisPlayer.GetPlayerRole().rollStatForHunting, currentNode.huntingNodeSuccessDificulty, currentNode.huntingNodeLifeLossDificulty, ActionEnumerator.hunt, NetworkClient.Instance.PlayerId);
    }

    public void forageAction()
    {
        SetCurrentPlayer();
        this.HandleOutcomeCalculationDuty(thisPlayer.GetPlayerRole().rollStatForGathering, currentNode.forageNodeSuccessDificulty, currentNode.forageNodeLifeLossDificulty, ActionEnumerator.forage, NetworkClient.Instance.PlayerId);
    }

    public void exploreAction()
    {
        SetCurrentPlayer();
        this.HandleOutcomeCalculationDuty(thisPlayer.GetPlayerRole().rollStatForExploring, currentNode.exploreNodeSuccessDificulty, currentNode.exploreNodeLifeLossDificulty, ActionEnumerator.explore, NetworkClient.Instance.PlayerId);
    }

    public void excavateAction()
    {
        SetCurrentPlayer();
        this.HandleOutcomeCalculationDuty(thisPlayer.GetPlayerRole().rollStatForExcavating, currentNode.excavateNodeSuccessDificulty, currentNode.excavateNodeLifeLossDificulty, ActionEnumerator.excavate, NetworkClient.Instance.PlayerId);
    }

    public void UpdateInventory(ActionEnumerator action, double resourceGain)
    {
        switch (action)
        {
            //HUNT and FORAGE case
            case ActionEnumerator.forage:
            case ActionEnumerator.hunt:
            case ActionEnumerator.explore:
                inventoryNetCode.ModifyFoodCount(Convert.ToSingle(party.inventory.foodCount + resourceGain));
                break;
            //EXCAVATE case
            case ActionEnumerator.excavate:
                //TODO modify artifactCount
                inventoryNetCode.ModifyArtifactCount(Convert.ToSingle(party.inventory.artifactCount + resourceGain));
                break;

        }
    }

    public void OnModifyFoodAmountEvent(double resourceAmount)
    {
        party.inventory.foodCount = resourceAmount; //Sets the amount of food in the local machine of the host and the clients
        outcomeUIHandler.UpdateFoodCountUI(party.inventory.foodCount);
    }

    public void OnModifyArtifactAmountEvent(double resourceAmount)
    {
        party.inventory.artifactCount = resourceAmount; //Sets the amount of food in the local machine of the host and the clients
        outcomeUIHandler.UpdateArtifactCountUI(party.inventory.artifactCount);
    }

    public void ChangeToVotingState()
    {
        Debug.Log("Now voting");
        stateHandler.SetState(StateHandler.GameState.MovementState);
    }

    public void SetCurrentPlayer()
    {
        foreach(PlayerModel player in party.playerList)
        {
            if(player.GetPlayerId() == NetworkClient.Instance.PlayerId)
            {
                thisPlayer = player;
            }
        }
    }

    public void SetCurrentNode()
    {
        currentNode = party.currentNode;
    }
}
