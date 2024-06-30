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
    public Sprite fireWorshiper;
    public Sprite waterWorshiper;
    public Sprite windWorshiper;
    public Sprite groundWorshiper;

    public GameObject WindEffect;

    public void Initialize(GameObject start, GameObject target, float moveSpeed)
    {
        WindEffect.SetActive(false);
        _startNode = start;
        _targetNode = target; 
        _speed = moveSpeed;
        _elemental = start.GetComponent<Node>().GetCurrentElemental();
        SetElementalColor(_elemental);
        if (_elemental == Elemental.Wind)
            _speed += EnumStatics.WindAdditionalSpeed; 
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

                WorshiperPool.Instance.ReturnPooledObject(gameObject); // 숭배자 파괴
            }
            // 원소신 본진 파괴 시
            if (GameManager.StageRuleManager.IsDefeat[_elemental] == true)
            {
                WorshiperPool.Instance.ReturnPooledObject(gameObject);
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

        if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID())
            return;

        if ((otherWorshiper._targetNode == _startNode) && (otherWorshiper._startNode == _targetNode))
        {
            Battle(otherWorshiper); 
        }
    }

    private void Battle(Worshiper otherWorshiper)
    {

        if (otherWorshiper.GetWorshiperElemental() == EnumStatics.WinningElemental(GetWorshiperElemental()))
        {
            WorshiperPool.Instance.ReturnPooledObject(otherWorshiper.gameObject);
            if (!StageRuleManager.CheckSurvive())
            {
                WorshiperPool.Instance.ReturnPooledObject(this.gameObject);
            }

        }
        else if (otherWorshiper.GetWorshiperElemental() == EnumStatics.LosingElemental(GetWorshiperElemental()))
        {
            WorshiperPool.Instance.ReturnPooledObject(this.gameObject);
            if (!StageRuleManager.CheckSurvive())
            {
                WorshiperPool.Instance.ReturnPooledObject(otherWorshiper.gameObject);
            }
        }
        else
        { 
            WorshiperPool.Instance.ReturnPooledObject(this.gameObject);
            WorshiperPool.Instance.ReturnPooledObject(otherWorshiper.gameObject);
        }
    }

    // 색깔 표시. 아트 에셋 나오면 교체합니다.
    public void SetElementalColor(Elemental newElemental)
    {
        switch (newElemental)
        {
            case Elemental.Fire :
                WorshiperRenderer.sprite = fireWorshiper;
                break;
            case Elemental.Water :
                WorshiperRenderer.sprite = waterWorshiper;
                break;
            case Elemental.Wind :
                WorshiperRenderer.sprite = windWorshiper;
                break;
            case Elemental.Ground :
                WorshiperRenderer.sprite = groundWorshiper;
                break;
        }

        if (newElemental == Elemental.Wind)
        {
            WindEffect.SetActive(true);
        }
    }
}