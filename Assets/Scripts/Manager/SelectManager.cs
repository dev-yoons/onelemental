using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Onelemental.Enum;
using Onelemental.Managers;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public Button fireButton;
    public Button waterButton;
    public Button windButton;
    public Button groundButton;

    public static Elemental SelectedElemental;
    
    private void Start()
    {
        fireButton.onClick.AddListener(() => OnButtonClicked(Elemental.Fire));
        waterButton.onClick.AddListener(() => OnButtonClicked(Elemental.Water));
        windButton.onClick.AddListener(() => OnButtonClicked(Elemental.Wind));
        groundButton.onClick.AddListener(() => OnButtonClicked(Elemental.Ground));
    }
    
    private void OnButtonClicked(Elemental selectedElement)
    {
        SelectedElemental = selectedElement;
        Debug.Log(SelectedElemental.ToString());
        Debug.Log(selectedElement.ToString());
        SceneManager.LoadScene("Stage1");
    }
}
