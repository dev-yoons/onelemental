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
            for (int i = currentStage; i < 10; i++)
            {
                CloseBlocker(i);
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
            case 1:
                Stage2Blocker.SetActive(false);
                return;
            case 2:
                Stage3Blocker.SetActive(false);
                return;
            case 3:
                Stage4Blocker.SetActive(false);
                return;
            case 4:
                Stage5Blocker.SetActive(false);
                return;
            case 5:
                Stage6Blocker.SetActive(false);
                return;
            case 6:
                Stage7Blocker.SetActive(false);
                return;
            case 7:
                Stage8Blocker.SetActive(false);
                return;
            case 8:
                Stage9Blocker.SetActive(false);
                return;
            case 9:
                Stage10Blocker.SetActive(false);
                return;
        }
    }

    private void CloseBlocker(int i)
    {
        switch (i)
        {
            case 1:
                Stage2Blocker.SetActive(true);
                return;
            case 2:
                Stage3Blocker.SetActive(true);
                return;
            case 3:
                Stage4Blocker.SetActive(true);
                return;
            case 4:
                Stage5Blocker.SetActive(true);
                return;
            case 5:
                Stage6Blocker.SetActive(true);
                return;
            case 6:
                Stage7Blocker.SetActive(true);
                return;
            case 7:
                Stage8Blocker.SetActive(true);
                return;
            case 8:
                Stage9Blocker.SetActive(true);
                return;
            case 9:
                Stage10Blocker.SetActive(true);
                return;
        }
    }
}
