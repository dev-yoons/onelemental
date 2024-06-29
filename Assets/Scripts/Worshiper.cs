using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Onelemental.Managers;

public class Worshiper : MonoBehaviour
{
    private GameObject _targetNode;
    private GameObject _startNode;
    private float _speed;
    private Elemental _elemental;

    public Elemental GetWorshiperElemental() { return _elemental; }

    public SpriteRenderer WorshiperRenderer;

    public void Initialize(GameObject start, GameObject target, float moveSpeed)
    {
        _startNode = start;
        _targetNode = target; 
        _speed = moveSpeed;
        _elemental = start.GetComponent<Node>().GetCurrentElemental();
        SetElementalColor(_elemental);
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
                Node node = _targetNode.GetComponent<Node>();

                node.GetNewWorshiper(this);

                Destroy(gameObject); // 숭배자 파괴
            }
            // 원소신 본진 파괴 시
            if (GameManager.StageRuleManager.IsDefeat[_elemental] == true)
            {
                Destroy(gameObject);
            }

        }
    }

    // 다른 Worshiper와 충돌판정합니다.
    // 같은 길에 있을 때만 판정합니다.
    void OnTriggerEnter2D(Collider2D other)
    {
        Worshiper otherWorshiper = other.gameObject.GetComponent<Worshiper>();
        if (!otherWorshiper)
            return;

        if (otherWorshiper._targetNode == _startNode)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    // 색깔 표시. 아트 에셋 나오면 교체합니다.
    public void SetElementalColor(Elemental newElemental)
    {
        WorshiperRenderer.color = EnumStatics.GetElementalColor(newElemental); 
    }
}