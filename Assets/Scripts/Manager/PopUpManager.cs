using UnityEngine;

namespace Manager
{
    public class PopUpManager : MonoBehaviour
    {
        public void ShowPopUp(GameObject obj)
        {
            obj.SetActive(true);
        }

        public void ClosePopUp(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}