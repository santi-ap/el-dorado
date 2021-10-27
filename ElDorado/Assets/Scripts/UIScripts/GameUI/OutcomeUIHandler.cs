using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutcomeUIHandler : MonoBehaviour
{
    public GameObject Panel;
    public Text outcomeText;
    public Text FoodCountText;
    public Text ArtifactCountText;

    public void Start()
    {
        this.FoodCountText.text = "x 0";
        this.ArtifactCountText.text = "x 0";
    }

    public void ShowActionOutcome(string resultOutcome)
    {
        //Shows the switch result on the Panel
        outcomeText.text = resultOutcome;
        this.openPanel();
    }

    public void openPanel()
    {
        if (Panel.activeSelf == false)
        {
            //calling the showOutcome mehtod with hardcoded variables CHANGE LATER
            // this.showOutcome(1,1,1,ActionEnumerator.hunt);
            Panel.SetActive(true);
        }
    }

    public void UpdateFoodCountUI(double foodCount)
    {
        this.FoodCountText.text = "x " + foodCount;
    }

    public void UpdateArtifactCountUI(double artifactCount)
    {
        this.ArtifactCountText.text = "x " + artifactCount;
    }
}
