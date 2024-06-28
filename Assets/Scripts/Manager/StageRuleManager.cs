using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

namespace Onelemental.Managers
{
    public class StageRuleManager : MonoBehaviour
    { 
        private Dictionary<Elemental, Node> ElementalMainNodes;
        private List<GameObject> ElementalAIPlayers;
        public Elemental PlayerElemental = Elemental.Fire;

        public void Start()
        {
            // �ʱ� ���� �о����
            Node[] startnodes = Resources.FindObjectsOfTypeAll<Node>();
            foreach (Node startnode in startnodes)
            {
                if (startnode.CurrentOwningElemental != Elemental.Neutral) 
                {
                    if (startnode.CurrentOwningElemental == PlayerElemental)
                        continue;

                    if (startnode.IsMainNode)
                    {
                        ElementalMainNodes.Add(startnode.CurrentOwningElemental, startnode);

                        GameObject newAIPlayer = new GameObject { };
                        AIPlayer aiPlayerComp = newAIPlayer.AddComponent<AIPlayer>();

                        aiPlayerComp.AIElemental = startnode.CurrentOwningElemental;

                        ElementalAIPlayers.Add(newAIPlayer); 
                    }
                }
            }
        }
    }
}