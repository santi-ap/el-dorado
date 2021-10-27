using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChoiceCalculationTest
{
    private OutcomeHandler outcomeHandler = new OutcomeHandler();
    // A Test behaves as an ordinary method
    [Test]
    public void ChoiceCalculationTestSimplePasses()
    {
        int outcome = outcomeHandler.ActionCalculation(1.1, 10);
        Assert.LessOrEqual(outcome,4);
        Assert.GreaterOrEqual(outcome,1);
    }

}
