using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Test : MonoBehaviour
{
    public GameObject Node;
    public GameObject Target;
    
    public Node node;
    
    void Start()
    {
        node = Node.GetComponent<Node>();
        
        node.Speed = 10f;
        node.WorshipersToSend = 10;
        node.CurrentWorshipers = 30;
        node.SendAttack(Target);
    }
}
