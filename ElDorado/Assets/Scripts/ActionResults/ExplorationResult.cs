using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationResult : MonoBehaviour
{
    public TrailHandler trailHandler;
    public GameObject nodeHolder;
    public NetCode netCode;
    public NotificationNetcode notificationNetcode;

    // Start is called before the first frame update
    void Start()
    {
        trailHandler = gameObject.AddComponent<TrailHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ReturnExplorationResult(PlayerModel player, NodeModel currentNode, int actionSuccessRange, int lifeLossRange, EventModel result)
    {
        //if the current node does have a secret path and if the path is actually still a secret
        if(currentNode.secretPath != null && currentNode.secretPath.GetComponent<TrailHandler>().isSecretPath)
        {
            if(player.GetPlayerRole().roleName == "Local Guide")
            {
                // find secret path, show it to everyone, make the connecting node available
                Debug.Log("Found a secret path!");
                // trailHandler.ShowSecretPath(currentNode.secretPath);
                // ShowSecretPath(currentNode.name);
                netCode.InvokeShowSecretTrailToGuestEvent(currentNode.name);
                notificationNetcode.InvokeShowNotificationAsHostEvent(player.playerName+ " found a secret path.",player.GetPlayerId());

                return "You found a secret path!";
            }else
            {
                //roll the dice for other roles
                if(actionSuccessRange == 4)
                {
                    netCode.InvokeShowSecretTrailToGuestEvent(currentNode.name);
                    notificationNetcode.InvokeShowNotificationAsHostEvent(player.playerName+ " found a secret path.",player.GetPlayerId());
                    return "You found a secret path!";
                }
                Debug.Log("Might have found a secret path - not a local guide");
                notificationNetcode.InvokeShowNotificationAsHostEvent(player.playerName+ " looked for a secret path but didn't find any. Instead they found "+ result.resourceGain + " food and lost " + result.lifeLoss + " health.",player.GetPlayerId());
                return "You didn't find any secret paths! But you did find " + result.resourceGain + " food and lost " + result.lifeLoss + " health!";
            }
        }else
        {
            // no secret path to find
            Debug.Log("No secret paths here!");
            notificationNetcode.InvokeShowNotificationAsHostEvent(player.playerName+ " looked for a secret path but didn't find any. Instead they found "+ result.resourceGain + " food and lost " + result.lifeLoss + " health.",player.GetPlayerId());
            return "You didn't find any secret paths! But you did find " + result.resourceGain + " food and lost " + result.lifeLoss + " health!";
        }
    }

    public void ShowSecretPath(string nodeName)
    {
        //looks for the node that has the same name as the input
        foreach (Transform nodeTransform in nodeHolder.transform)
        {
            //if it finds the node it will show the secret path that's linked to it
            if (nodeTransform.gameObject.name == nodeName)
            {
                trailHandler.ShowSecretPath(nodeTransform.gameObject.GetComponent<NodeModel>().secretPath);
            }
        }
    }
}
