using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeModel : MonoBehaviour
{
    public float huntingNodeSuccessDificulty; //difficulty or chances of succeeding task on this node
    public float huntingNodeLifeLossDificulty; //difficulty or chances of loosing life on this node

    public float forageNodeSuccessDificulty; //difficulty or chances of succeeding task on this node
    public float forageNodeLifeLossDificulty; //difficulty or chances of loosing life on this node

    public float exploreNodeSuccessDificulty; //difficulty or chances of succeeding task on this node
    public float exploreNodeLifeLossDificulty; //difficulty or chances of loosing life on this node

    public float excavateNodeSuccessDificulty; //difficulty or chances of succeeding task on this node
    public float excavateNodeLifeLossDificulty; //difficulty or chances of loosing life on this node

    public Color nodeChoiceColor; //color of node while voting to move to a different node
    public GameObject secretPath;
    public List<NodeModel> adjacientNodesList = new List<NodeModel>(); //list of the nodes adjacent to this one
    public bool isFinalNode;

    private GameObject thisNodeGameObject; //the game object that this node model is linked to

    //constructor
    public NodeModel()
    {

    }

    //constructor 2
    public NodeModel(float nodeSuccessDificulty, float nodeLifeLossDificulty, List<NodeModel> adjacientNodes)
    {
        // this.nodeSuccessDificulty = nodeSuccessDificulty;
        // this.nodeLifeLossDificulty = nodeLifeLossDificulty;
        this.adjacientNodesList = adjacientNodes;
    }

    //runs before the game is even started
    void Awake()
    {
        this.thisNodeGameObject = gameObject; //links the game object this node model is on to this node model
        //this.SetRandomDifficulty(); //sets the difficulty of this node
    }

    void SetRandomDifficulty()
    {
        //get the name of the gameobject
        string name = this.gameObject.name;

        switch (name)
        {
            case "Node13":
                this.huntingNodeSuccessDificulty = 1;
                this.huntingNodeLifeLossDificulty = 1;
                this.forageNodeSuccessDificulty = Random.Range(1, 50);
                this.forageNodeLifeLossDificulty = Random.Range(1, 50);
                this.exploreNodeSuccessDificulty = Random.Range(1, 50);
                this.exploreNodeLifeLossDificulty = Random.Range(1, 50);
                this.excavateNodeSuccessDificulty = Random.Range(1, 50);
                this.excavateNodeLifeLossDificulty = Random.Range(1, 50);
                break;
            case "Node14":
                this.huntingNodeSuccessDificulty = Random.Range(1, 50);
                this.huntingNodeLifeLossDificulty = Random.Range(1, 50);
                this.forageNodeSuccessDificulty = 1;
                this.forageNodeLifeLossDificulty = 1;
                this.exploreNodeSuccessDificulty = Random.Range(1, 50);
                this.exploreNodeLifeLossDificulty = Random.Range(1, 50);
                this.excavateNodeSuccessDificulty = Random.Range(1, 50);
                this.excavateNodeLifeLossDificulty = Random.Range(1, 50);
                break;
            case "Node38":
                this.huntingNodeSuccessDificulty = Random.Range(1, 50);
                this.huntingNodeLifeLossDificulty = Random.Range(1, 50);
                this.forageNodeSuccessDificulty = Random.Range(1, 50);
                this.forageNodeLifeLossDificulty = Random.Range(1, 50);
                this.exploreNodeSuccessDificulty = Random.Range(1, 50);
                this.exploreNodeLifeLossDificulty = Random.Range(1, 50);
                this.excavateNodeSuccessDificulty = 1;
                this.excavateNodeLifeLossDificulty = 1;
                break;
            default:
                this.huntingNodeSuccessDificulty = Random.Range(1, 50);
                this.huntingNodeLifeLossDificulty = Random.Range(1, 50);
                this.forageNodeSuccessDificulty = Random.Range(1, 50);
                this.forageNodeLifeLossDificulty = Random.Range(1, 50);
                this.exploreNodeSuccessDificulty = Random.Range(1, 50);
                this.exploreNodeLifeLossDificulty = Random.Range(1, 50);
                this.excavateNodeSuccessDificulty = Random.Range(1, 50);
                this.excavateNodeLifeLossDificulty = Random.Range(1, 50);
                break;
        }
    }

}