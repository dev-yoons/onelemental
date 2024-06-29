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

    // ���� ���� �ð� �ɰ� AI ���� ����
    void Start()
    {
        StartCoroutine(StartAIPattern());
    }

    private IEnumerator StartAIPattern()
    {
        yield return new WaitForSeconds(AIPatternStartDelayTime);

        aiCoroutine = StartCoroutine(PlayAIBehavior());
    }

    // AI ����
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
        GameObject lineObject;
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
            lineObject = Instantiate(GameManager.PrefabManager.LinePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            lineObject.GetComponent<Line>().Init(ainode, targetNode);
            ainode.SendAttack(targetNode.gameObject, lineObject.GetComponent<Line>());
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

        targetNode = targetableNodes[Random.Range(0, targetableNodes.Count)];
        lineObject = Instantiate(GameManager.PrefabManager.LinePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        lineObject.GetComponent<Line>().Init(ainode, targetNode);
        ainode.SendAttack(targetNode.gameObject, lineObject.GetComponent<Line>());
    } 
}
