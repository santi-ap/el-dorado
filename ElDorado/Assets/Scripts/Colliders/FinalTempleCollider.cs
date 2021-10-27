using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTempleCollider : MonoBehaviour
{
    public EndofGameHandler endofGameHandler;
    public bool hasFoundTemple = false;

    public GameObject hiddenNode1;
    public GameObject hiddenNode2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasFoundTemple && (collision.gameObject.tag == "VisibleArea"))
        {
            gameObject.transform.Find("FinalTempleSprite").gameObject.SetActive(true);
            hasFoundTemple = true;
            hiddenNode1.SetActive(true);
            hiddenNode2.SetActive(true);
        }
        else if(!(collision.gameObject.tag == "VisibleArea") && hasFoundTemple)
        {
            endofGameHandler.ShowEndOfGame();
        }
    }
}
