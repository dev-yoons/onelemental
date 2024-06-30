using Onelemental.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private int currentClearStage;
    private string sceneName;
    private int bestStage;
    public void DataReset()
    {
        PlayerPrefs.DeleteAll();
    }

    public void DataSave()
    {
        bestStage = PlayerPrefs.GetInt($"{GameManager.Instance.PlayerElemental}CurrentStage");

        Match match = Regex.Match(sceneName, @"\d+");

        if (match.Success)
        {
            // 숫자 부분이 발견되면 이를 정수로 변환하여 반환
            currentClearStage = int.Parse(match.Value);
        }
        else
        {
            // 숫자 부분이 없으면 0 반환 (또는 원하는 기본값 설정)
            currentClearStage = 0;
        }

        if ( currentClearStage > bestStage )
        {
            PlayerPrefs.SetInt($"{GameManager.Instance.PlayerElemental}CurrentStage", currentClearStage);
        }
    }
}
