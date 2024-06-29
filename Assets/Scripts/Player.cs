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
        for (int i = OwningNodes.Count-1; i>=0; i--)
        {
            OwningNodes[i].SetCurrentElemental(Elemental.Neutral);
            OwningNodes[i].CurrentWorshipers = 0;
            OwningNodes[i].NodeTextMesh.text = OwningNodes[i].CurrentWorshipers.ToString();
            OwningNodes.Remove(OwningNodes[i]);
        }
        
        MainNode.IsMainNode = false;
        
        Debug.Log("Defeat");
        // 플레이어 화면에서 사라짐
        Destroy(this.gameObject);
    }
}
