using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

namespace Onelemental.Managers
{
    public class StageRuleManager : MonoBehaviour
    {
        private Dictionary<Elemental, Player> ElementalPlayers = new Dictionary<Elemental, Player>();
        public Elemental PlayerElemental = Elemental.Fire; // 나중에 받아오는 코드로 수정
        public List<Node> AllNodesInStage = new List<Node>();
        public Dictionary <Elemental, bool> IsDefeat = new Dictionary<Elemental, bool>();
        public Timer gameTimer; // 타이머를 참조하기 위한 변수

        private bool _playerWins = false;
        private bool _playerLoses = false;

        public GameObject gameOver;
        public GameObject gameClear;

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

                        Debug.Log(targetElemental);
                        notInitializedElementals.Remove(targetElemental);
                    } 

                    Player player = ElementalPlayers[targetElemental];
                    startnode.SetCurrentElemental(targetElemental);
                    player.Initialize(startnode, targetElemental);  
                }

                if (gameTimer != null)
                {
                    gameTimer.ResetTimer();
                    gameTimer.StartTimer();
                }
                else
                {
                    Debug.LogError("Game Timer is not assigned in the StageRuleManager.");
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
            if (gameTimer != null)
                gameTimer.StopTimer();

            gameClear.GetComponent<ClearWidget>().Init(gameTimer.GetElapsedTime()); ;
            gameClear.SetActive(true);

            string currentElement = GameManager.Instance.PlayerElemental.ToString();

            int bestStage = PlayerPrefs.GetInt($"{GameManager.Instance.PlayerElemental}CurrentStage");
            int currentStage = CurrentStage();
            if (currentStage > bestStage)
            {
                PlayerPrefs.SetInt($"{GameManager.Instance.PlayerElemental}CurrentStage", currentStage);
            }

            return true;
        } 

        public void GameOver()
        {
            Debug.Log("Lose");
            gameOver.SetActive(true);
            if (gameTimer != null)
                gameTimer.StopTimer();
        }

        const float SurviveRatio = 0.1f;
        static public bool CheckSurvive()
        {
            return UnityEngine.Random.value < SurviveRatio;
        }
        private int CurrentStage()
        {
            string currentScene = SceneManager.GetActiveScene().name;
            Match match = Regex.Match(currentScene, @"\d+");

            if (match.Success)
            {
                // 숫자 부분이 발견되면 이를 정수로 변환하여 반환
                return int.Parse(match.Value);
            }
            else
            {
                // 숫자 부분이 없으면 0 반환 (또는 원하는 기본값 설정)
                return 0;
            }
        }
    }
}