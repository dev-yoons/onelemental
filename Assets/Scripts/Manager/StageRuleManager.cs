using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

namespace Onelemental.Managers
{
    public class StageRuleManager : MonoBehaviour
    {
        private Dictionary<Elemental, Player> ElementalPlayers = new Dictionary<Elemental, Player>();
        public Elemental PlayerElemental = Elemental.Fire; // 나중에 받아오는 코드로 수정
        public List<Node> AllNodesInStage = new List<Node>();
        public Dictionary <Elemental, bool> IsDefeat = new Dictionary<Elemental, bool>();

        private bool _playerWins = false;
        private bool _playerLoses = false;


        // ó���� ���������� ��ġ�Ǿ��ִ� ������ �а�,
        // Player�� AIPlayer ���� �����Ͽ� ������ �Ҵ��մϴ�.
        public void Start()
        {
            PlayerElemental = GameManager.Instance.PlayerElemental;
            GameManager.StageRuleManager = this;
            
            IsDefeat.Add(Elemental.Fire, false);
            IsDefeat.Add(Elemental.Water, false);
            IsDefeat.Add(Elemental.Wind, false);
            IsDefeat.Add(Elemental.Ground, false);

            foreach (Elemental elemental in System.Enum.GetValues(typeof(Elemental)))
            {
                if (elemental == Elemental.Neutral)
                    continue;

                GameObject newPlayer = new GameObject { };
                Player playerComp;
                if (PlayerElemental == elemental)
                    playerComp = newPlayer.AddComponent<Player>();
                else
                    playerComp = newPlayer.AddComponent<AIPlayer>();
                ElementalPlayers.Add(elemental, playerComp);
            }

            List<Elemental> notInitializedElementals = new List<Elemental>();
            foreach (Elemental elemental in System.Enum.GetValues(typeof(Elemental)))
            {
                if (elemental == Elemental.Neutral)
                    continue;
                if (elemental == PlayerElemental)
                    continue;
                notInitializedElementals.Add(elemental);
            } 

            Node[] startnodes = Resources.FindObjectsOfTypeAll<Node>();
            foreach (Node startnode in startnodes)
            {
                AllNodesInStage.Add(startnode);
                if (startnode.GetCurrentElemental() != Elemental.Neutral) 
                { 
                    Player player = ElementalPlayers[startnode.GetCurrentElemental()];

                    player.AddOwningNode(startnode); 
                }

                if (startnode.IsMainNode)
                {
                    Elemental targetElemental;

                    if (startnode.IsPlayerMainNode)
                    {
                        targetElemental = PlayerElemental;
                    }
                    else
                    {
                        targetElemental = notInitializedElementals[UnityEngine.Random.Range(0, notInitializedElementals.Count)];
                        notInitializedElementals.Remove(targetElemental);
                    } 

                    Player player = ElementalPlayers[targetElemental];
                    player.Initialize(startnode, targetElemental);
                    startnode.SetCurrentElemental(targetElemental);  
                } 
            }
        }

        public void Update()
        {
            if (IsDefeat[PlayerElemental] && !_playerLoses)
            {
                GameOver();
                _playerLoses = true;
            }
            if(!_playerWins)
            {
                _playerWins = PlayerWins();
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

        // 플레이어가 이겼는지 확인하는 함수
        public bool PlayerWins()
        {
            foreach (var variable in IsDefeat)
            {
                if (variable.Key != PlayerElemental && variable.Value == false)
                    return false;
                if (variable.Key == PlayerElemental && variable.Value == true)
                    return false;
            }
            Debug.Log("win!");
            return true;
        }
        public void GameOver()
        {
            Debug.Log("Lose");
        }
    }
}