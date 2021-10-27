using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanelBehavior : MonoBehaviour
{
    public GameObject introPanel;

    public void CloseIntroPanel()
    {
        introPanel.SetActive(false);
    }
}
