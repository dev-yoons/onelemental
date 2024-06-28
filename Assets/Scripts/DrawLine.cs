using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer _lineRenderer;
    public bool _isDragging = false;
    public GameObject _startNode;

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

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Node")) // Node에 태그 추가 필요
            {
                _isDragging = true;
                _startNode = hit.collider.gameObject;
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
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Node") && hit.collider.gameObject != _startNode)
            {
                // 다른 노드 위에서 마우스를 떼었을 때
                _lineRenderer.SetPosition(1, hit.collider.gameObject.transform.position);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }
    }
}