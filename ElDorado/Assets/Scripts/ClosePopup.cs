using UnityEngine;

public class ClosePopup : MonoBehaviour
{
    public GameObject Panel;
    void Update()
    {
        //checks if (left click) happens
        if (Input.GetMouseButtonDown(0)){
            if (Panel.activeSelf == true)
        {
            Panel.SetActive(false);
        }
        }
    }
}
