using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

public class Line : MonoBehaviour
{  
    public Node startNode;
    public Node endNode;

    // Start is called before the first frame update
    public void Init(Node instartNode, Node inendNode)
    {
        startNode = instartNode;
        endNode = inendNode;

        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, instartNode.transform.position);
        lineRenderer.SetPosition(1, inendNode.transform.position);

        Color startColor = EnumStatics.GetElementalColor(instartNode.GetCurrentElemental());
        startColor.a = 0.3f;
        
        Color endColor = EnumStatics.GetElementalColor(inendNode.GetCurrentElemental());
        endColor.a = 0.3f;

        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor; 

        // BoxCollider2D 설정.
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            // LineRenderer의 중점 위치 계산.
            Vector3 startPosition = instartNode.transform.position;
            Vector3 endPosition = inendNode.transform.position;
            Vector3 middlePoint = (startPosition + endPosition) / 2;
            boxCollider.transform.position = middlePoint;

            // Collider의 크기 및 회전 설정.
            float lineLength = Vector3.Distance(startPosition, endPosition);
            boxCollider.size = new Vector2(lineLength - 1.0f, lineRenderer.startWidth);
            boxCollider.transform.rotation = Quaternion.FromToRotation(Vector3.right, endPosition - startPosition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
