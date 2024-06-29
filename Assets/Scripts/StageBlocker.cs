using Onelemental.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBlocker : MonoBehaviour
{
    public GameObject Stage2Blocker;
    public GameObject Stage3Blocker;
    public GameObject Stage4Blocker;
    public GameObject Stage5Blocker;
    public GameObject Stage6Blocker;
    public GameObject Stage7Blocker;
    public GameObject Stage8Blocker;
    public GameObject Stage9Blocker;
    public GameObject Stage10Blocker;

    private string currentElement;
    private int currentStage;
    // Start is called before the first frame update
    void Update()
    {
        currentElement = GameManager.Instance.PlayerElemental.ToString();
        if (PlayerPrefs.HasKey($"{currentElement}CurrentStage"))
        {
            currentStage = PlayerPrefs.GetInt($"{currentElement}CurrentStage");
            for (int i = 1; i < currentStage; i++)
            {
                OpenBlocker(i);
            }
        }
        else
        {
            PlayerPrefs.SetInt($"{currentElement}CurrentStage", 1);
        }

    }

    private void OpenBlocker(int i)
    {
        switch(i)
        {
            case 2:
                Stage2Blocker.SetActive(false);
                return;
            case 3:
                Stage3Blocker.SetActive(false);
                return;
            case 4:
                Stage4Blocker.SetActive(false);
                return;
            case 5:
                Stage5Blocker.SetActive(false);
                return;
            case 6:
                Stage6Blocker.SetActive(false);
                return;
            case 7:
                Stage7Blocker.SetActive(false);
                return;
            case 8:
                Stage8Blocker.SetActive(false);
                return;
            case 9:
                Stage9Blocker.SetActive(false);
                return;
            case 10:
                Stage10Blocker.SetActive(false);
                return;
        }
    }
}
