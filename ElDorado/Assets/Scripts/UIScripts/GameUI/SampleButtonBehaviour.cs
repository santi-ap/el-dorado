using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWNetwork;

public class SampleButtonBehaviour : MonoBehaviour
{
    public SyncPropertyAgent sampleAgent;
    public Text sampleText;


    public void OnClick() {
        //Change value of sync property
        Debug.Log("Clicked the Button.");
        sampleAgent.Modify("text", sampleAgent.GetPropertyWithName("text") + NetworkClient.Instance.PlayerId + " Pressed the button!\n");
    }
    public void OnValueChanged() {
        Debug.Log("Value Has Been Changed.");
        //Set text component to new text value
        sampleText.text = sampleAgent.GetPropertyWithName("text").GetStringValue();
    }
}
