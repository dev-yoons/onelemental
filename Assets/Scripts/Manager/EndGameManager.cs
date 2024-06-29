using UnityEngine;

namespace Onelemental.Managers
{
    public class EndGameManager : MonoBehaviour
    {
        public void GameExit()
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                    Application.Quit();
            #endif
        }
    }
}