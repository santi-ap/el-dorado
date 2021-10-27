using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutcomeHandler : MonoBehaviour
{
    public double rangeDefault1;
    public double rangeDefault2;
    public double rangeDefault3;

    private double range1;
    private double range2;
    private double range3;

    public double nodeDifficultyMultiplier1;
    public double nodeDifficultyMultiplier2;
    public double nodeDifficultyMultiplier3;

    private System.Random rnd;
    public void Awake() {
        rangeDefault1 = 20.0;
        rangeDefault2 = 60.0;
        rangeDefault3 = 90.0;
        nodeDifficultyMultiplier1 = .5;
        nodeDifficultyMultiplier2 = .3;
        nodeDifficultyMultiplier3 = .2;
    }
    public int ActionCalculation(double playerAbility, double nodeDifficulty)
    {
        //resets the ranges to the default values
        range1 = rangeDefault1;
        range2 = rangeDefault2;
        range3 = rangeDefault3;

        //sets the ranges based on the node diffiuclty
        range1 = rangeDefault1 + nodeDifficulty * nodeDifficultyMultiplier1;
        range2 = rangeDefault2 + nodeDifficulty * nodeDifficultyMultiplier2;
        range3 = rangeDefault3 + nodeDifficulty * nodeDifficultyMultiplier3;

        // rnd = new System.Random();
        double randomNumber = (Random.Range(1, 101)) * playerAbility;
        // Debug.Log("Range 1: "+ range1);
        // Debug.Log("Range 2: "+ range2);
        // Debug.Log("Range 3: "+ range3);
        // Debug.Log("Role: " + randomNumber);

        if (randomNumber < range1)
        {
            // Debug.Log("Outcome: 1");
            return 1;
        }
        else
        if (randomNumber < range2)
        {
            // Debug.Log("Outcome: 2");
            return 2;
        }
        else
        if (randomNumber < range3)
        {
            // Debug.Log("Outcome: 3");
            return 3;
        }
        else
        {
            // Debug.Log("Outcome: 4");
            return 4;
        }
    }
}