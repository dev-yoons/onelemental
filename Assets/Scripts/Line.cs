using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{  
    public Node startNode;
    public Node endNode;

    // Start is called before the first frame update
    public void Init(Node instartNode, Node inendNode)
    {
        // 충돌 판정 구현해주세요.

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, instartNode.transform.position);
        lineRenderer.SetPosition(1, inendNode.transform.position);  

        startNode = instartNode; 
        endNode = inendNode;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
