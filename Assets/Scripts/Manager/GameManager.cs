using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

namespace Onelemental.Managers
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { Init(); return instance; } }

        private StageRuleManager stageRuleManager; 

        public static StageRuleManager StageRuleManager { get { return Instance.stageRuleManager; } set { Instance.stageRuleManager = value; } }


        private PrefabManager prefabManager;

        public static PrefabManager PrefabManager { get { return Instance.prefabManager; } set { Instance.prefabManager = value; } }

        public Elemental PlayerElemental;

        void Awake()
        { 
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