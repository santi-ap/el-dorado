using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteCounter : MonoBehaviour
{
    //this whole class is only used to keep track of the number of votes during voting
    //it's used by the GeneralVotingHandler class
    public int numberOfVotes = 0;
    public int optionId = 0;

    // Start is called before the first frame update
    void Start()
    {
        numberOfVotes = 0;
        // optionId = 0;
    }
}
