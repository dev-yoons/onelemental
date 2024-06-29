using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Onelemental.Managers;

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

    public GameObject worshiperObject;

    public SpriteRenderer NodeRenderer;

    public TextMesh NodeTextMesh;

    public List<Node> ConnectedNodes;

    private Dictionary<GameObject, Coroutine> AttackCorroutine = new Dictionary<GameObject, Coroutine>();
    private Dictionary<GameObject, Line> AttackLine = new Dictionary<GameObject, Line>();

    /// <summary>
    /// 다른 노드로 공격 보내기
    /// </summary>
    /// <param name="destination"></param>
    /// 

    void Start()
    {
        CurrentWorshipers = StartWorshipers;
        SetCurrentElemental(StartElemental);
        if (StartElemental == Elemental.Neutral)
            CurrentWorshipers = 0;
        NodeTextMesh.text = CurrentWorshipers.ToString();
        curProductionTime = ProductionTime;
    } 

    // 여기서 Line 정보 저장하고 있어요.
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
            CurrentWorshipers--;
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
    }

    private IEnumerator SendWorshipers(GameObject destination)
    {
        while (CurrentWorshipers > LeastWorshipers)
        {
            // 숭배자 생성 및 초기화
            GameObject worshiper = Instantiate(worshiperObject, transform.position, Quaternion.identity);
            Worshiper worshiperComponent = worshiper.GetComponent<Worshiper>();
            worshiperComponent.Initialize(gameObject, destination, WorshiperSpeed);

            // 숭배자 수 감소 
            DecreaseWorshipers();
            
            // 잠시 대기 후 반복
            yield return new WaitForSeconds(0.5f); // 필요에 따라 시간 조정
        } 

        SendingEnd();
    }

    private void SendingEnd()
    {
        if (CurrentWorshipers <= LeastWorshipers)
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

    }
    public void SetCurrentElemental(Elemental newElemental)
    {
        CurrentElemental = newElemental;

        // 아트 에셋 생기면 그 때 바꿉니다.
        switch(newElemental)
        {
            case Elemental.Fire:
                NodeRenderer.color = Color.red;
                break;
            case Elemental.Water:
                NodeRenderer.color = Color.blue;
                break;
            case Elemental.Wind:
                NodeRenderer.color = Color.gray;
                break;
            case Elemental.Ground:
                NodeRenderer.color = Color.yellow;
                break; 
        } 
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
                curProductionTime = ProductionTime; 
            }

        } 
    } 
}