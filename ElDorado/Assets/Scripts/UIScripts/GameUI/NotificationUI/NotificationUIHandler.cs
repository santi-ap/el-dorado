using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NotificationUIHandler : MonoBehaviour
{
    public GameObject notification1;
    public Text notificationText1;

    public GameObject notification2;
    public Text notificationText2;

    public GameObject notification3;
    public Text notificationText3;

    public Vector3 startingPosition1;
    public Vector3 startingPosition2;
    public Vector3 startingPosition3;

    public bool oneAvailable = true;
    public bool twoAvailable = true;
    public bool threeAvailable = true;

    public Text nextAvailableText;
    public int nextAvailableIndex;

    public float waitTime;
    public NotificationNetcode notificationNetcode;


    void Awake()
    {
        startingPosition1 = notification1.transform.position;
        startingPosition2 = notification2.transform.position;
        startingPosition3 = notification3.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        // notification1.GetComponent<Image>().CrossFadeAlpha(255f,1f,true);
        // notification1.transform.DOMove(new Vector3(1648.15f, 969.0867f, 0f), 1f).SetEase(Ease.OutExpo);
        // notification2.transform.DOMove(new Vector3(1648.15f, 894.6866f, 0f), 1f).SetEase(Ease.OutExpo);
        // notification3.transform.DOMove(new Vector3(1648.15f, 821.0867f, 0f), 1f).SetEase(Ease.OutExpo);
        // StartCoroutine(ResetNotificationPosition());
        // this.StartNotificationFlow("This is a test message");
    }


    //TODO select next available notification
    public void SelectNextAvailableNotification()
    {
        if (oneAvailable)
        {
            nextAvailableText = notificationText1;
            nextAvailableIndex = 1;
            oneAvailable = false;
        }
        else if (twoAvailable)
        {
            nextAvailableText = notificationText2;
            nextAvailableIndex = 2;
            twoAvailable = false;
        }
        else
        {
            nextAvailableText = notificationText3;
            nextAvailableIndex = 3;
            threeAvailable = false;
        }
    }

    //TODO set values
    public void SetNotifcationMessage(string message)
    {
        nextAvailableText.text = message;
    }

    //TODO show notification
    public void ShowNotification()
    {
        switch (nextAvailableIndex)
        {
            case 1:
                notification1.transform.DOMove(new Vector3(1648.15f, 969.0867f, 0f), 1f).SetEase(Ease.OutExpo);
                break;
            case 2:
                notification2.transform.DOMove(new Vector3(1648.15f, 894.6866f, 0f), 1f).SetEase(Ease.OutExpo);
                break;
            case 3:
                notification3.transform.DOMove(new Vector3(1648.15f, 821.0867f, 0f), 1f).SetEase(Ease.OutExpo);
                break;
        }
    }

    //TODO reset position
    public IEnumerator ResetNotificationPosition()
    {
        int index = nextAvailableIndex;
        yield return new WaitForSeconds(waitTime);
        switch (index)
        {
            case 1:
                oneAvailable = true;
                notification1.transform.DOMove(startingPosition1, 1f).SetEase(Ease.OutExpo);
                break;
            case 2:
                twoAvailable = true;
                notification2.transform.DOMove(startingPosition2, 1f).SetEase(Ease.OutExpo);
                break;
            case 3:
                threeAvailable = true;
                notification3.transform.DOMove(startingPosition3, 1f).SetEase(Ease.OutExpo);
                break;
        }
    }

    //TODO start notificaiton flow
    public void StartNotificationFlow(string notifactionMessage)
    {
        this.SelectNextAvailableNotification();
        this.SetNotifcationMessage(notifactionMessage);
        this.ShowNotification();
        StartCoroutine(ResetNotificationPosition());
    }

    public void SendRequestToHost(string notifactionMessage, string actingPlayerId)
    {
        notificationNetcode.InvokeShowNotificationAsHostEvent(notifactionMessage,actingPlayerId);
    }

   
}
