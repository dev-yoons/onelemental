using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Managers;

public class DrawLine : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public bool _isDragging = false;
    public Node _startNode;

    public GameObject StageRuleObject;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            Node node = hit.collider?.GetComponent<Node>();
            if (node != null)
            {
                StageRuleManager stageRuleManager = StageRuleObject.GetComponent<StageRuleManager>();
                if (!stageRuleManager.IsClickableNode(node))
                    return;

                if (_startNode != null)
                {
                    // _startNode.SetHighlight(false); // 이전 노드의 표시 해제
                }
                _isDragging = true;
                _startNode = node;
                // _startNode.SetHighlight(true); 현재 노드 표시
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _startNode.transform. position);
                
            }
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            // 마우스 드래그 중
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            _lineRenderer.SetPosition(1, mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            // 마우스 버튼 떼었을 때
            _isDragging = false;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            Node endNode = hit.collider?.GetComponent<Node>();
            if (endNode != null && endNode != _startNode)
            {
                _lineRenderer.SetPosition(1, endNode.transform.position);
                _startNode.SendAttack(hit.collider.gameObject);
            }
            else
            {
                _lineRenderer.enabled = false; 
            }

            // _startNode.SetHighlight(false); // 드래그가 끝나면 표시
        }
    }
}