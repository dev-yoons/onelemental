using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

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

    public void Defeat()
    {

    }
}
