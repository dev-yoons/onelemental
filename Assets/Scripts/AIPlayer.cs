using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Onelemental.Managers;

public class AIPlayer : Player
{  
    private bool bIsDefeated = false;

    private Coroutine aiCoroutine;

    public float AIPatternTime = 5.0f;

    public float AIPatternStartDelayTime = 1.0f;

    public int AttackWorshiperCriterion = 5;

    // 시작 지연 시간 걸고 AI 패턴 시작

    private void Awake()
    {
        AIPatternStartDelayTime = Random.Range(1.0f, 3.0f);
    }
    void Start()
    {  
    }

    private IEnumerator StartAIPattern()
    {
        yield return new WaitForSeconds(AIPatternStartDelayTime);

        aiCoroutine = StartCoroutine(PlayAIBehavior());
    }

    // AI 패턴
    private IEnumerator PlayAIBehavior()
    {
        while (!bIsDefeated)
        {
            foreach(Node node in OwningNodes) 
            {
                AINodeCheck(node);
            }

            yield return new WaitForSeconds(AIPatternTime);
        }
    }

    private void AINodeCheck(Node ainode)
    {
        Node targetNode = null;

        // 거점의 숭배자 수가 10명 이상인가
        if (ainode.CurrentWorshipers <= Node.LeastWorshipers)
        {
            return;
        }

        // 연결된 거점지 중 중립지가 있는가
        List<Node> neutralNodes = new List<Node>();
        GameObject lineObject;
        foreach (Node connectednode in ainode.ConnectedNodes)
        {
            if (connectednode.GetCurrentElemental() == Elemental.Neutral)
            {
                neutralNodes.Add(connectednode);
            }
        }

        // 중립 노드가 있다면 대상 거점지로 이동
        if (neutralNodes.Count > 0)
        {
            targetNode = neutralNodes[Random.Range(0, neutralNodes.Count)];
            lineObject = Instantiate(GameManager.PrefabManager.LinePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            lineObject.GetComponent<Line>().Init(ainode, targetNode);
            ainode.SendAttack(targetNode.gameObject, lineObject.GetComponent<Line>());
            return;
        }

        // 없다면
        // 거점지에 연결된 다른 거점지 중 거점지의 숭배지보다 10개 이상 적은 거점지의 개수
        List<Node> targetableNodes = new List<Node>();
        foreach (Node connectednode in ainode.ConnectedNodes)
        {
            if (connectednode.GetCurrentElemental() != Elemental.Neutral)
            {
                if (connectednode.CurrentWorshipers + AttackWorshiperCriterion < ainode.CurrentWorshipers)
                {
                    targetableNodes.Add(connectednode);

                }
            }
        }

        if (targetableNodes.Count == 0)
        {
            ainode.SendingEnd();
            return;
        }

        targetNode = targetableNodes[Random.Range(0, targetableNodes.Count)];
        lineObject = Instantiate(GameManager.PrefabManager.LinePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        lineObject.GetComponent<Line>().Init(ainode, targetNode);
        ainode.SendAttack(targetNode.gameObject, lineObject.GetComponent<Line>());


    }

    public override void Defeat()
    {
        bIsDefeated = true;
        
        if (aiCoroutine != null)
        {
            StopCoroutine(aiCoroutine);
        }
        
        base.Defeat();
    }

    public override void Initialize(Node mainNode, Elemental elemental)
    {
        base.Initialize(mainNode, elemental);
        StartCoroutine(StartAIPattern());
        Debug.Log("Hi");
    }
}
