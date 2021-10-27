using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    public double foodCount; //amount of food in party inventory
    public double artifactCount; //amount of artifacts in player inventory
    
    public InventoryModel() 
    {
        artifactCount = 0;
        foodCount = 0;
    }

    public void AddFood(double amountOfFood)
    {
        this.foodCount += amountOfFood;
    }

    public void SubtractFood(double amountOfFood)
    {
        this.foodCount -= amountOfFood;
    }

    public void AddArtifacts(double amountOfArtifacts)
    {
        this.artifactCount += amountOfArtifacts;
    }
}
