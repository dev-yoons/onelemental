using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worshiper : MonoBehaviour
{
    private GameObject _targetNode;
    private float _speed; 

    public void Initialize(GameObject target, float moveSpeed)
    {
        _targetNode = target;
        _speed = moveSpeed;
    }

    void Update()
    {
        if (_targetNode != null)
        {
            // 타겟 노드로 이동
            transform.position = Vector3.MoveTowards(transform.position, _targetNode.transform.position, _speed * Time.deltaTime);

            // 목표 지점에 도달 시
            if (Vector3.Distance(transform.position, _targetNode.transform.position) < 0.1f)
            {
                Destroy(gameObject); // 숭배자 파괴
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("What");
    }
}