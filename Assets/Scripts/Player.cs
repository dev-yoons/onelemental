using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    protected List<Node> OwningNodes = new List<Node>();
    protected Node MainNode;

    public Elemental Elemental; 
    
    public void Initialize(Node mainNode)
    {
        MainNode = mainNode;
        Elemental = MainNode.GetCurrentElemental();
    }
    public void AddOwningNode(Node newNode)
    {
        OwningNodes.Add(newNode);
    }
    public void LoseOwningNode(Node loseNode)
    {
        OwningNodes.Remove(loseNode);

        if (loseNode == MainNode)
        {
            Defeat();
        }
    }

    public bool OwnsNode(Node node)
    {
        return OwningNodes.Contains(node);
    }
     
    public virtual void Defeat()
    {
        // 기획 질문 : 내가 가지고 있던 거점 노드는 어떻게 되는 건지?
        // 현재: 가지고 있는 노드 중립 지역으로, OwningNodes에서 제거
        // AI 문제 해결되면 테스트 해볼게요
        foreach (Node node in OwningNodes)
        {
            node.SetCurrentElemental(Elemental.Neutral);
            node.CurrentWorshipers = 0;
            OwningNodes.Remove(node);
        }

        MainNode.IsMainNode = false;
        // 플레이어 화면에서 사라짐
        Destroy(this.gameObject);
    }
}
