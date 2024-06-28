using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

namespace Onelemental.Managers
{
    public class StageRuleManager : MonoBehaviour
    {
        private Dictionary<Elemental, Player> ElementalPlayers = new Dictionary<Elemental, Player>();
        public Elemental PlayerElemental = Elemental.Fire;
        public List<Node> AllNodesInStage = new List<Node>();


        // 처음에 스테이지에 배치되어있는 노드들을 읽고,
        // Player와 AIPlayer 들을 생성하여 노드들을 할당합니다.
        public void Start()
        {
            GameManager.StageRuleManager = this;
             
            Node[] startnodes = Resources.FindObjectsOfTypeAll<Node>();
            foreach (Node startnode in startnodes)
            {
                AllNodesInStage.Add(startnode);

                if (startnode.GetCurrentElemental() != Elemental.Neutral) 
                {
                    if (startnode.GetCurrentElemental() == PlayerElemental)
                    {
                        if (!ElementalPlayers.ContainsKey(PlayerElemental))
                        {
                            GameObject newPlayer = new GameObject { };
                            Player playerComp = newPlayer.AddComponent<Player>();

                            ElementalPlayers.Add(startnode.GetCurrentElemental(), playerComp);
                        }
                    }
                    else
                    {
                        if (!ElementalPlayers.ContainsKey(startnode.GetCurrentElemental()))
                        {
                            GameObject newAIPlayer = new GameObject { };
                            AIPlayer aiPlayerComp = newAIPlayer.AddComponent<AIPlayer>();

                            ElementalPlayers.Add(startnode.GetCurrentElemental(), aiPlayerComp);
                        }
                    }

                    Player player = ElementalPlayers[startnode.GetCurrentElemental()];

                    if (startnode.IsMainNode)
                        player.Initialize(startnode);

                    player.AddOwningNode(startnode); 
                }
            }
        }

        // 현재 클릭 가능한 노드인지 확인합니다.
        public bool IsClickableNode(Node node)
        {
            if (PlayerElemental != node.GetCurrentElemental())
                return false;

            if (node.CurrentWorshipers <= 10)
                return false;

            return true;
        }

        // 노드의 주인이 바뀌었을 때 호출됩니다.
        public void NodeOwnerChanged(Node node, Elemental newElemental)
        {
            if (node.GetCurrentElemental() != Elemental.Neutral)
            { 
                Player losePlayer = ElementalPlayers[node.GetCurrentElemental()];
                losePlayer.LoseOwningNode(node);
            } 
            Player winPlayer = ElementalPlayers[newElemental];
            winPlayer.AddOwningNode(node);

            node.SetCurrentElemental(newElemental);
        }
    }
}