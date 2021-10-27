using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ColorIndicatorTest
{
    private ChoiceUIHandler choiceUIHandler = new ChoiceUIHandler();

    Color colorRange20 = new Color32(200, 255, 0, 255);
    Color colorRange30 = new Color32(255, 255, 69, 255);
    Color colorRange40 = new Color32(255, 150, 0, 255);
    Color colorRange50 = new Color32(255, 56, 56, 255);
    // A Test behaves as an ordinary method
    [Test]
    public void ColorIndicatorTestRange10()
    {
        Color colorRange10 = new Color32(58, 230, 58, 255);
        Color indicatorColor = choiceUIHandler.ChoiceColorIndicator(9);
        Assert.AreEqual(indicatorColor, colorRange10);
    }

    [Test]
    public void ColorIndicatorTestRange20()
    {
        Color indicatorColor = choiceUIHandler.ChoiceColorIndicator(15);
        Assert.AreEqual(indicatorColor, colorRange20);
    }

    [Test]
    public void ColorIndicatorTestRange30()
    {
        Color indicatorColor = choiceUIHandler.ChoiceColorIndicator(25);
        Assert.AreEqual(indicatorColor, colorRange30);
    }

    [Test]
    public void ColorIndicatorTestRange40()
    {
        Color indicatorColor = choiceUIHandler.ChoiceColorIndicator(40);
        Assert.AreEqual(indicatorColor, colorRange40);
    }

    [Test]
    public void ColorIndicatorTestRange50()
    {
        Color indicatorColor = choiceUIHandler.ChoiceColorIndicator(49);
        Assert.AreEqual(indicatorColor, colorRange50);
    }
}
