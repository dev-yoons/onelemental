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
            // 초기 세팅 읽어오기
            Node[] startnodes = Resources.FindObjectsOfTypeAll<Node>();
            foreach (Node startnode in startnodes)
            {
                if (startnode.GetCurrentElemental() != Elemental.Neutral) 
                {
                    if (startnode.GetCurrentElemental() == PlayerElemental)
                        continue;

                    if (startnode.IsMainNode)
                    {
                        ElementalMainNodes.Add(startnode.GetCurrentElemental(), startnode);

                        GameObject newAIPlayer = new GameObject { };
                        AIPlayer aiPlayerComp = newAIPlayer.AddComponent<AIPlayer>();

                        aiPlayerComp.AIElemental = startnode.GetCurrentElemental();

                        ElementalAIPlayers.Add(newAIPlayer); 
                    }
                }
            }
        }

        public bool IsUserNode(Node node)
        {
            return PlayerElemental == node.GetCurrentElemental();
        }
    }
}