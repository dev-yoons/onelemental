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
        // �浹 ���� �������ּ���.

        startNode = instartNode;
        endNode = inendNode;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
