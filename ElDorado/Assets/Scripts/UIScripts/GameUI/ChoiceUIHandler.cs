using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUIHandler : MonoBehaviour
{

    public Image huntDifficultyIndicator;
    public Image huntLifeDifficultyIndicator;
    public Image forageDifficultyIndicator;
    public Image forageLifeDifficultyIndicator;
    public Image exploreDifficultyIndicator;
    public Image exploreLifeDifficultyIndicator;
    public Image excavateDifficultyIndicator;
    public Image excavateLifeDifficultyIndicator;
    public PartyModel party;
    public NodeModel currentNode;
    private StateHandler stateHandler;//declare a variable of type StateHandler
    public GameObject choiceUI;

    void Awake()
    {
        currentNode = party.currentNode;
        //make the stateHandler variable equal to the only instance of the StateHandler class
        stateHandler = StateHandler.StateHandlerInstance;
        stateHandler.turnHasStartedStateEvent += UpdateColorDifficultyIndicator;
        stateHandler.choiceStateHasStartedEvent += ShowhoiceUI;
        stateHandler.movementStateHasStartedEvent += HideChoiceUI;
        stateHandler.kickVoteStateHasStartedEvent += HideChoiceUI;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateColorDifficultyIndicator()
    {
        currentNode = party.currentNode;
        HuntIndicatorColor(currentNode.huntingNodeSuccessDificulty);
        HuntLifeIndicatorColor(currentNode.huntingNodeLifeLossDificulty);

        ForageIndicatorColor(currentNode.forageNodeSuccessDificulty);
        ForageLifeIndicatorColor(currentNode.forageNodeLifeLossDificulty);

        ExploreIndicatorColor(currentNode.exploreNodeSuccessDificulty);
        ExploreLifeIndicatorColor(currentNode.exploreNodeLifeLossDificulty);

        ExcavateIndicatorColor(currentNode.excavateNodeSuccessDificulty);
        ExcavateLifeIndicatorColor(currentNode.excavateNodeLifeLossDificulty);
    }

    public Color ChoiceColorIndicator(double difficulty)
    {
        

        if(difficulty <= 10){
            // ? Debug.Log("10");
            return new Color32(58,230,58,255);
        }else
        if(difficulty <= 20){
            // ? Debug.Log("20");
            return new Color32(200,255,0,255);
        }else
        if(difficulty <= 30){
            // ? Debug.Log("30");
            return new Color32(255,255,69,255);
        }else
        if(difficulty <= 40){
            // ? Debug.Log("40");
            return new Color32(255,150,0,255);
        }else{
            // ? Debug.Log("50");
            return new Color32(255,56,56,255);
        }
    }

    public void HuntIndicatorColor(double difficulty){
        huntDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void HuntLifeIndicatorColor(double difficulty){
        huntLifeDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void ForageIndicatorColor(double difficulty){
        forageDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void ForageLifeIndicatorColor(double difficulty){
        forageLifeDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void ExploreIndicatorColor(double difficulty){
        exploreDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void ExploreLifeIndicatorColor(double difficulty){
        exploreLifeDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void ExcavateIndicatorColor(double difficulty){
        excavateDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void ExcavateLifeIndicatorColor(double difficulty){
        excavateLifeDifficultyIndicator.color = ChoiceColorIndicator(difficulty);
    }

    public void tester(){
        Debug.Log("this is  a test");
    }

    public void ShowhoiceUI(){ 
        choiceUI.SetActive(true);
    }

    public void HideChoiceUI(){ 
        choiceUI.SetActive(false);
    }

    

}
