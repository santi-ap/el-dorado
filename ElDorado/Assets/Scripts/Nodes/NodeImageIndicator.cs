using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeImageIndicator : MonoBehaviour
{
    public SpriteRenderer resourcePresenceImage;
    public SpriteRenderer highLifeLoss;
    public Sprite huntHighResourceImage;
    public Sprite forageHighResourceImage;
    public Sprite excavateHighResourceImage;
    // public Sprite exploreHighResourceImage;

    public Sprite huntHighLifeLossImage;
    public Sprite forageHighLifeLossImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetHuntHighResourceImage(){
        resourcePresenceImage.sprite = huntHighResourceImage;
    }

    public void SetForageHighResourceImage(){
        resourcePresenceImage.sprite = forageHighResourceImage;
    }

    public void SetExcavateHighResourceImage(){
        resourcePresenceImage.sprite = excavateHighResourceImage;
    }

    // public void SetExploreHighResourceImage(){
    //     resourcePresenceImage.sprite = exploreHighResourceImage;
    // }
//
    public void SetHuntHighLifeLossImage(){
        highLifeLoss.sprite = huntHighLifeLossImage;
    }

    public void SetForageHighLifeLossImage(){
        highLifeLoss.sprite = forageHighLifeLossImage;
    }

    public void SetExcavateHighLifeLossImage(){
        highLifeLoss.sprite = excavateHighResourceImage;
    }

    // public void SetExploreHighLifeLossImage(){
    //     highLifeLoss.sprite = exploreHighResourceImage;
    // }
    
}
