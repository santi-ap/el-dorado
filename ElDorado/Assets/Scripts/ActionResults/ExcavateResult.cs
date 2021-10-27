using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavateResult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string ReturnExcavateResult(int lifeposition, int resourcenumber, double lifeLoss, double resourceGain)
    {
        string[,] excavateOutcomeArray = new string[4, 4]
            {
                {"excavateEvent 1,1", "excavateEvent 1,2" ,"excavateEvent 1,3", event1_4(lifeLoss, resourceGain)},
                {"excavateEvent 2,1", "excavateEvent 2,2" ,"excavateEvent 2,3", event2_4(lifeLoss, resourceGain)},
                {event3_1(lifeLoss, resourceGain), event3_2(lifeLoss, resourceGain) ,event3_3(lifeLoss, resourceGain), event3_4(lifeLoss, resourceGain)},
                {event4_1(lifeLoss, resourceGain), event4_2(lifeLoss, resourceGain) ,event4_3(lifeLoss, resourceGain), event4_4(lifeLoss, resourceGain)}
            };

        return excavateOutcomeArray[lifeposition, resourcenumber];
    }

    string event1_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event2_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event3_1(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event3_2(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event3_3(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event3_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event4_1(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event4_2(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event4_3(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }

    string event4_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " artifacts."; ;
    }
}
