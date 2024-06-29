using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Onelemental.Managers;
using TMPro;

public class Node : MonoBehaviour
{
    // 노드의 첫 숭배자 숫자. 중립이면 자동으로 0으로 덮어써집니다.
    public int StartWorshipers = 30;
    public int CurrentWorshipers { get; set; } = 30; 
     
    public float WorshiperSpeed { get; set; } = 10f;

    public float ProductionTime = 3.0f;

    private float curProductionTime;

    // 이 숫자보다 숭배자가 적어지면 공격 불가 상태가 됩니다.
    public const float LeastWorshipers = 10;

    public Elemental StartElemental = Elemental.Neutral;

    private Elemental CurrentElemental = Elemental.Neutral;
    public Elemental GetCurrentElemental() { return CurrentElemental; }

    public bool IsMainNode = false;

    public bool IsPlayerMainNode = false;

    public GameObject worshiperObject;

    // 베이스
    public SpriteRenderer NodeRenderer;
    public Sprite BaseFire;
    public Sprite BaseWater;
    public Sprite BaseWind;
    public Sprite BaseGround;
    
    // 거점
    public Sprite neutral;
    public Sprite fire;
    public Sprite water;
    public Sprite wind;
    public Sprite ground;

    // 신 이미지
    public GameObject GodSprite;

    public TextMeshPro NodeTextMesh;

    public List<Node> ConnectedNodes = new List<Node>();

    private Dictionary<GameObject, Coroutine> AttackCorroutine = new Dictionary<GameObject, Coroutine>();
    private Dictionary<GameObject, Line> AttackLine = new Dictionary<GameObject, Line>();

    public AudioSource shootAudio;
    public AudioSource successAudio;
    /// <summary>
    /// 다른 노드로 공격 보내기
    /// </summary>
    /// <param name="destination"></param>
    /// 

    void Awake()
    {
        CurrentWorshipers = StartWorshipers;
        SetCurrentElemental(StartElemental);
/*        if (StartElemental == Elemental.Neutral)
            CurrentWorshipers = 0;*/
        NodeTextMesh.text = CurrentWorshipers.ToString();
        curProductionTime = ProductionTime; 
    } 

    public bool IsAttackingTo(GameObject go)
    {
        return AttackLine.ContainsKey(go);
    }

    public void SendAttack(GameObject destination, Line attackLine)
    {
        if (AttackCorroutine.ContainsKey(destination))
            return;

        Coroutine newAttack = StartCoroutine(SendWorshipers(destination));
        AttackCorroutine.Add(destination, newAttack);
        AttackLine.Add(destination, attackLine);
    }

    public void StopSendingAttack(GameObject destination)
    {
        if (AttackCorroutine.ContainsKey(destination))
        {
            StopCoroutine(AttackCorroutine[destination]);
            AttackCorroutine.Remove(destination);
        }
        Destroy(AttackLine[destination].gameObject);
        AttackLine.Remove(destination);
    } 

    private void DecreaseWorshipers()
    {
        CurrentWorshipers--;
 
        NodeTextMesh.text = CurrentWorshipers.ToString();
    }

    // Worshiper가 node에 닿았을 때 호출됩니다.
    public void GetNewWorshiper(Worshiper newWorshiper)
    {
        if (newWorshiper.GetWorshiperElemental() == CurrentElemental)
        {
            CurrentWorshipers++;
        }
        else
        {
            int damage = 1;

            if(newWorshiper.GetWorshiperElemental() == Elemental.Fire)
            {
                if (UnityEngine.Random.value < EnumStatics.FireDoubleAttackRatio)
                {
                    damage += EnumStatics.FireAttackAdditionalDamage;
                }
            }   
            
            if (GetCurrentElemental() == Elemental.Ground)
            {
                if (UnityEngine.Random.value < EnumStatics.GroundProtectRatio)
                {
                    damage = 0;
                }
            }

            CurrentWorshipers -= damage;

            if (CurrentWorshipers <= 0)
            {
                NodeOwnerChanged(newWorshiper);
            }
        }

        NodeTextMesh.text = CurrentWorshipers.ToString();
    }

    private void NodeOwnerChanged(Worshiper newWorshiper)
    {
        CurrentWorshipers = 10;

        GameManager.StageRuleManager.NodeOwnerChanged(this, newWorshiper.GetWorshiperElemental()); 

        if (GetCurrentElemental() == GameManager.StageRuleManager.PlayerElemental)
        {
            successAudio.Play();
        }
    }

    private IEnumerator SendWorshipers(GameObject destination)
    {
        while (CurrentWorshipers > LeastWorshipers)
        {
            // 숭배자 생성 및 초기화
            GameObject worshiper = WorshiperPool.Instance.GetPooledObject();
            
            if (worshiper == null)
            {
                yield return new WaitForSeconds(0.5f); // 시간 수정 가능
                continue;
            }
            
            worshiper.transform.position = transform.position;
            worshiper.SetActive(true);
            
            Worshiper worshiperComponent = worshiper.GetComponent<Worshiper>();
            worshiperComponent.Initialize(gameObject, destination, WorshiperSpeed);

            // 숭배자 수 감소 
            DecreaseWorshipers();

            shootAudio.Play();
            
            // 잠시 대기 후 반복
            yield return new WaitForSeconds(0.5f); // 필요에 따라 시간 조정
        }
        
        if (CurrentWorshipers <= LeastWorshipers)
        {
            SendingEnd();
        }
    }

    public void SendingEnd()
    {
            List<GameObject> corroutineKeys = new List<GameObject>();

            foreach (GameObject attackNode in AttackLine.Keys)
            {
                corroutineKeys.Add(attackNode);
            }

            foreach (GameObject attackNode in corroutineKeys)
            { 
                StopSendingAttack(attackNode);
            }
    }
    public void SetCurrentElemental(Elemental newElemental)
    {
        CurrentElemental = newElemental;

        if (IsMainNode)
        {
            switch (CurrentElemental)
            {
                case Elemental.Fire :
                    NodeRenderer.sprite = BaseFire;
                    break;
                case Elemental.Water :
                    NodeRenderer.sprite = BaseWater;
                    break;
                case Elemental.Wind :
                    NodeRenderer.sprite = BaseWind;
                    break;
                case Elemental.Ground :
                    NodeRenderer.sprite = BaseGround;
                    break;
            }
        }
        else
        {
            switch (CurrentElemental)
            {
                case Elemental.Fire :
                    NodeRenderer.sprite = fire;
                    break;
                case Elemental.Water :
                    NodeRenderer.sprite = water;
                    break;
                case Elemental.Wind :
                    NodeRenderer.sprite = wind;
                    break;
                case Elemental.Ground :
                    NodeRenderer.sprite = ground;
                    break;
                case Elemental.Neutral :
                    NodeRenderer.sprite = neutral;
                    break;
            }
        }
    }

    // node가 ConncectedNodes 리스트에 있는지 확인.
    public bool IsConnectedToNode(Node node)
    {
        return ConnectedNodes.Contains(node);
    }

    // node가 어떤 노드로부터 공격받고 있는지 확인.
    public bool IsBeingAttackedBy(Node node)
    {
        return AttackLine.ContainsKey(node.gameObject);
    }

    private void Update()
    {
        curProductionTime -= Time.deltaTime;
        if (curProductionTime <= 0)
        {
            if (CurrentElemental != Elemental.Neutral)
            {
                CurrentWorshipers++;
                NodeTextMesh.text = CurrentWorshipers.ToString(); 

                if (GetCurrentElemental() == Elemental.Water)
                {
                    curProductionTime = (ProductionTime - EnumStatics.WaterProductionReduceTime);
                }
                else
                {
                    curProductionTime = ProductionTime; 
                } 
            }

        } 
    }
}