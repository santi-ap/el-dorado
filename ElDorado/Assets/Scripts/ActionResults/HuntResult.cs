using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntResult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public string ReturnHuntResult(int lifeposition, int resourcenumber, double lifeLoss, double resourceGain)
    {
        string[,] huntOutcomeArray = new string[4, 4]
            {
                {"huntEvent 1,1", "huntEvent 1,2" ,"huntEvent 1,3", event1_4(lifeLoss, resourceGain)},
                {"huntEvent 2,1", "huntEvent 2,2" ,"huntEvent 2,3", event2_4(lifeLoss, resourceGain)},
                {event3_1(lifeLoss, resourceGain), event3_2(lifeLoss, resourceGain) ,event3_3(lifeLoss, resourceGain), event3_4(lifeLoss, resourceGain)},
                {event4_1(lifeLoss, resourceGain), event4_2(lifeLoss, resourceGain) ,event4_3(lifeLoss, resourceGain), event4_4(lifeLoss, resourceGain)}
            };

        return huntOutcomeArray[lifeposition, resourcenumber];
    }

    string event1_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event2_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event3_1(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event3_2(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event3_3(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event3_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event4_1(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event4_2(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event4_3(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }

    string event4_4(double lifeLoss, double resourceGain)
    {
        return "Something happened and you lost " + lifeLoss + " life and you gained: " + resourceGain + " food."; ;
    }
}
