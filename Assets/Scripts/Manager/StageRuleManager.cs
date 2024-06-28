using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

namespace Onelemental.Managers
{
    public class StageRuleManager : MonoBehaviour
    {
        private Node[] NeutralNodes;
        private Dictionary<Elemental, Node> ElementCampNodes;
        public Elemental PlayerElemental;

        public void Start()
        {
            Node[] startnodes = Resources.FindObjectsOfTypeAll<Node>();
            foreach (Node startnode in startnodes)
            {
                    
            }
        }
    }
}