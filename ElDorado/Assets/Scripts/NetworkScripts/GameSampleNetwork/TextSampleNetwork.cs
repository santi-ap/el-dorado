using UnityEngine;
using UnityEngine.UI;
using SWNetwork;

public class TextSampleNetwork : MonoBehaviour
{
    public NetworkID networkId;
    public SyncPropertyAgent syncPropertyAgent;
    public string currentText;
    public Text sampleText;
    // Start is called before the first frame update
    void Start()
    {
        networkId = GetComponent<NetworkID>();
        syncPropertyAgent = gameObject.GetComponent<SyncPropertyAgent>();
        currentText = "Game Started.\n";
        Debug.Log("SampleNetwork started");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSampleTextReady()
    {
        Debug.Log("OnSampleTextPropertyReady");

        // Get the current value of the "text" SyncProperty.
        currentText = syncPropertyAgent.GetPropertyWithName("text").GetStringValue();

        // Check if the local player has ownership of the GameObject. 
        // Source GameObject can modify the "text" SyncProperty.
        // Remote duplicates should only be able to read the "text" SyncProperty.
        if (networkId.IsMine)
        {
            int version = syncPropertyAgent.GetPropertyWithName("text").version;

            if (version != 0)
            {
                // You can check the version of a SyncProperty to see if it has been initialized. 
                // If version is not 0, it means the SyncProperty has been modified before. 
                // Probably the player got disconnected from the game. 
                // Set hpSlider's value to currentHP to restore player's hp.
                sampleText.text = currentText;
            }
            else
            {
                // If version is 0, you can call the Modify() method on the SyncPropertyAgent to initialize player's hp to maxHp.
                syncPropertyAgent.Modify("text", currentText);
            }
        }
        else
        {
            sampleText.text = currentText;
        }
    }

    public void OnTextChanged()
    {
        // Update the sampletext when text changes
        currentText = syncPropertyAgent.GetPropertyWithName("text").GetStringValue();
        sampleText.text = currentText;

        //This be an event!!
        // if (currentHP == 0)
        // {
        //     // invoke the "killed" remote event when hp is 0. 
        //     if (networkId.IsMine)
        //     {
        //         remoteEventAgent.Invoke("killed");
        //     }
        // }
    }

    public void HandleTextChange()
    {
        if (networkId.IsMine)
        {
            Debug.Log("Changing current text!");

            currentText += NetworkClient.Instance.PlayerId + " pressed the button!\n";
            Debug.Log("Broadcasting new text.");
            syncPropertyAgent.Modify("text", currentText);
        }
    }
}
