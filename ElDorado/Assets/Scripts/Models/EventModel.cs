using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventModel
{
    public int lifeLoss;//life amount lost after the outcome
    public double resourceGain; // resource amount gained after the outcome
    public bool isSpecialEvent;
    public string message;

    //Constructor
    public EventModel(int lifeLoss, double resourceGain, bool isSpecialEvent){
        this.lifeLoss = lifeLoss;
        this.resourceGain = resourceGain;
        this.isSpecialEvent = isSpecialEvent;
        this.message = "";
    }

}
