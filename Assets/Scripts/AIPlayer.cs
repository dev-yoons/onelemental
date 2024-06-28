using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

public class AIPlayer : Player
{  
    private bool bIsDefeated = false;

    private Coroutine aiCoroutine;

    public float AIPatternTime = 5.0f; 

    void Start()
    {
        aiCoroutine = StartCoroutine(PlayAIBehavior());
    }

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

        // ������ ������ ���� 10�� �̻��ΰ�
        if (ainode.CurrentWorshipers <= Node.LeastWorshipers)
        {
            return;
        }

        // ����� ������ �� �߸����� �ִ°�
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
        // �ִٸ� ���������� �̵�
        if (bFlag)
        {
            ainode.SendAttack(targetNode.gameObject);
            return;
        }

        // ���ٸ�
        // �������� ����� �ٸ� ������ �� �������� ���������� 10�� �̻� ���� �������� ����
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
