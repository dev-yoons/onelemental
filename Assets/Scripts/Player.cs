using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Unity.VisualScripting;
using Onelemental.Managers;

public class Player : MonoBehaviour
{
    protected List<Node> OwningNodes = new List<Node>();
    protected Node MainNode;

    public Elemental Elemental; 
    
    public void Initialize(Node mainNode, Elemental elemental)
    {
        MainNode = mainNode;
        Elemental = elemental;
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
        for (int i = OwningNodes.Count-1; i>=0; i--)
        {
            OwningNodes[i].SetCurrentElemental(Elemental.Neutral);
            OwningNodes[i].CurrentWorshipers = 0;
            OwningNodes[i].NodeTextMesh.text = OwningNodes[i].CurrentWorshipers.ToString();
            OwningNodes[i].SendingEnd();
            OwningNodes.Remove(OwningNodes[i]);
        }
        
        MainNode.IsMainNode = false;
        GameManager.StageRuleManager.IsDefeat[Elemental] = true;
        Debug.Log("Defeat");
        // 플레이어 화면에서 사라짐
        Destroy(this.gameObject);
    }
}
