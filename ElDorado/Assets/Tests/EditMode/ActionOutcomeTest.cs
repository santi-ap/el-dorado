using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


public class ActionOutcomeTest
{
    GameObject gameObject;
    
    ShowOutcome showOutcome;
    
    EventModel result2;
    NodeModel node;
    public GameObject Panel;

    //variables initialization
    public void init(){
    
        showOutcome = new ShowOutcome();
        Panel = new GameObject();
        gameObject = new GameObject();
        showOutcome.Panel = Panel;
        showOutcome.outcomeText = gameObject.AddComponent<Text>();
        showOutcome.party = gameObject.AddComponent<PartyModel>();
        node = gameObject.AddComponent<NodeModel>();
        node.huntingNodeLifeLossDificulty = 1;
        node.huntingNodeSuccessDificulty = 3;
        showOutcome.party.currentNode = node;
        showOutcome.init();
        
    }
    [Test]
    public void ActionOutcomeTestSimplePasses()
    {
        init();
        //TODO this has changed and needs to be reworked
        /*EventModel result = showOutcome.showOutcome(1.2, 1, 1, ActionEnumerator.hunt);
        Debug.Log(result.lifeLoss);
        Debug.Log(result.resourceGain);
        Debug.Log(result.message);
        Assert.Greater(result.lifeLoss,0);
        Assert.Less(result.lifeLoss,100);
        Assert.Greater(result.resourceGain,0);
        Assert.Less(result.resourceGain,100);
        //Assert.AreEqual(result.message, result2.message);*/
    }

}
