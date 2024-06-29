using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Line : MonoBehaviour
{
    // 공격 시작할 때 그어지는 줄 클래스입니다.

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
}
