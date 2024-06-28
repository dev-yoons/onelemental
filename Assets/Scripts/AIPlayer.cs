using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

public class AIPlayer : Player
{  
    private bool bIsDefeated = false;

    private Coroutine aiCoroutine;

    public float AIPatternTime = 5.0f;

    public float AIPatternStartDelayTime = 1.0f;

    // 시작 지연 시간 걸고 AI 패턴 시작
    void Start()
    {
        StartCoroutine(StartAIPattern());
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
        bool bFlag = false;
        
        foreach (Node connectednode in ainode.ConnectedNodes)
        {
            if (connectednode.GetCurrentElemental() == Elemental.Neutral)
            {
                bFlag = true;
                targetNode = connectednode;
                break;
            }
        }
        // 있다면 대상거점지로 이동
        if (bFlag)
        {
            ainode.SendAttack(targetNode.gameObject);
            return;
        }

        // 없다면
        // 거점지에 연결된 다른 거점지 중 거점지의 숭배지보다 10개 이상 적은 거점지의 개수
        List<Node> targetableNodes = new List<Node>();
        foreach (Node connectednode in ainode.ConnectedNodes)
        {
            if (connectednode.GetCurrentElemental() != Elemental.Neutral)
            {
                if (connectednode.CurrentWorshipers + 10 < ainode.CurrentWorshipers)
                {
                    targetableNodes.Add(connectednode);
                }
            }
        }

        if (targetableNodes.Count == 0)
            return;

        ainode.SendAttack(targetableNodes[Random.Range(0, targetableNodes.Count)].gameObject);
    } 
}
