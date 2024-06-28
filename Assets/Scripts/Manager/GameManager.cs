using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace Onelemental.Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { Init(); return instance; } }

        StageRuleManager stageRuleManager = new StageRuleManager();
        
        // Manager ¸ñ·Ï

        public static StageRuleManager StageRuleManager { get { return Instance.stageRuleManager; } }

        void Awake()
        {
            Debug.Log("Hi");
            Init();
        }

        static void Init()
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<GameManager>();
                }
                instance = go.GetComponent<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
        }
    }
}