using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[ExecuteInEditMode]
public class TrailHandler : MonoBehaviour
{
    public GameObject trailGameObject;
    public LineRenderer trailLineRenderer;
    public GameObject originalTrailNode;
    public bool isSecretPath = false;
    public NodeModel nodeA;
    public NodeModel nodeB;
    public Color secretPathColor;
    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        foreach (Transform trailNode in gameObject.transform)
        {
            trailNode.GetComponent<SpriteRenderer>().color = new Vector4(trailNode.GetComponent<SpriteRenderer>().color.r, trailNode.GetComponent<SpriteRenderer>().color.g, trailNode.GetComponent<SpriteRenderer>().color.b, 0f);
        }
        HideSecretPath();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        int trailNodeCount = 0;
        foreach (Transform trailNode in gameObject.transform)
        {
            trailNodeCount++;
        }

        if (!isPlaying && !isSecretPath)
        {
            if (trailNodeCount != trailLineRenderer.positionCount)
            {
                Debug.Log("Mismatch in counts");
                addTrailNode();
            }

        }

        int i = 0;
        foreach (Transform trailNode in gameObject.transform)
        {
            // Debug.Log("trail: "+gameObject.name);
            if (!isPlaying)
            {
                trailNode.GetComponent<SpriteRenderer>().color = new Vector4(trailNode.GetComponent<SpriteRenderer>().color.r, trailNode.GetComponent<SpriteRenderer>().color.g, trailNode.GetComponent<SpriteRenderer>().color.b, 255f);
            }
            trailNode.localPosition = trailLineRenderer.GetPosition(i);
            i++;
        }
#endif

    }

    // Update is called once per frame
    void addTrailNode()
    {
        ClearNodeList(gameObject.transform);
        for (int i = 0; i < trailLineRenderer.positionCount; i++)
        {
            GameObject trailNode = Instantiate(originalTrailNode, gameObject.transform);
            trailNode.SetActive(true);
            trailNode.transform.localPosition = trailLineRenderer.GetPosition(i);
            i++;
        }
    }

    public void ClearNodeList(Transform parent)
    {
        //looks for all the transforms in the parent game object and destroys them
        foreach (Transform childTransform in parent)
        {
            //unless it's the prefab or the option to stay on the current node
            if (childTransform.gameObject != originalTrailNode)
            {
                DestroyImmediate(childTransform.gameObject);
            }
        }
    }

    public void HideSecretPath()
    {
        if (isSecretPath)
        {
            gameObject.GetComponent<LineRenderer>().enabled = false;
        }
    }

    public void ShowSecretPath(GameObject secretPath)
    {
        secretPath.GetComponent<LineRenderer>().enabled = true;
        secretPath.GetComponent<TrailHandler>().isSecretPath = false;
    }
}
