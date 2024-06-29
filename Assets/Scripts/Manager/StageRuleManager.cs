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
        public Dictionary <Elemental, bool> IsDefeat = new Dictionary<Elemental, bool>();


        // ó���� ���������� ��ġ�Ǿ��ִ� ������ �а�,
        // Player�� AIPlayer ���� �����Ͽ� ������ �Ҵ��մϴ�.
        public void Start()
        {
            GameManager.StageRuleManager = this;
            
            IsDefeat.Add(Elemental.Fire, false);
            IsDefeat.Add(Elemental.Water, false);
            IsDefeat.Add(Elemental.Wind, false);
            IsDefeat.Add(Elemental.Ground, false);

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

                            playerComp.Initialize(startnode); // 초기화.
                        }
                    }
                    else
                    {
                        if (!ElementalPlayers.ContainsKey(startnode.GetCurrentElemental()))
                        {
                            GameObject newAIPlayer = new GameObject { };
                            AIPlayer aiPlayerComp = newAIPlayer.AddComponent<AIPlayer>();

                            ElementalPlayers.Add(startnode.GetCurrentElemental(), aiPlayerComp);

                            aiPlayerComp.Initialize(startnode); // AIPlayer 초기화.
                        }
                    }

                    Player player = ElementalPlayers[startnode.GetCurrentElemental()];

                    if (startnode.IsMainNode)
                        player.Initialize(startnode);

                    player.AddOwningNode(startnode); 
                }
            }
        }

        // ���� Ŭ�� ������ ������� Ȯ���մϴ�.
        public bool IsClickableNode(Node node)
        {
            if (PlayerElemental != node.GetCurrentElemental())
                return false;

            if (node.CurrentWorshipers <= 10)
                return false;

            return true;
        }

        // ����� ������ �ٲ���� �� ȣ��˴ϴ�.
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

        // 소유권 확인.
        public bool PlayerOwnsNode(Node node)
        {
            if (ElementalPlayers.TryGetValue(PlayerElemental, out Player player))
            {
                return player.OwnsNode(node);
            }
            return false;
        }
    }
}