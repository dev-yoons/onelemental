using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

namespace Onelemental.Managers
{
    public class StageRuleManager : MonoBehaviour
    {
        private Dictionary<Elemental, AIPlayer> ElementalAIPlayers = new Dictionary<Elemental, AIPlayer>();
        public Elemental PlayerElemental = Elemental.Fire;
        public List<Node> AllNodesInStage = new List<Node>();

        public void Start()
        {
            // 초기 세팅 읽어오기
            Node[] startnodes = Resources.FindObjectsOfTypeAll<Node>();
            foreach (Node startnode in startnodes)
            {
                AllNodesInStage.Add(startnode);

                if (startnode.GetCurrentElemental() != Elemental.Neutral) 
                {
                    if (startnode.GetCurrentElemental() == PlayerElemental)
                        continue;

                    if (!ElementalAIPlayers.ContainsKey(startnode.GetCurrentElemental()))
                    {
                        GameObject newAIPlayer = new GameObject { };
                        AIPlayer aiPlayerComp = newAIPlayer.AddComponent<AIPlayer>();

                        ElementalAIPlayers.Add(startnode.GetCurrentElemental(), aiPlayerComp); 
                    }
                    
                    AIPlayer player = ElementalAIPlayers[startnode.GetCurrentElemental()];

                    if (startnode.IsMainNode)
                        player.Initialize(startnode);  

                    player.AddOwningNode(startnode);
                }
            }
        }

        public bool IsClickableNode(Node node)
        {
            if (PlayerElemental != node.GetCurrentElemental())
                return false;

            if (node.CurrentWorshipers <= 10)
                return false;

            return true;
        }
    }
}