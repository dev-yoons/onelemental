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
            // ���� �κ��� �߰ߵǸ� �̸� ������ ��ȯ�Ͽ� ��ȯ
            currentClearStage = int.Parse(match.Value);
        }
        else
        {
            // ���� �κ��� ������ 0 ��ȯ (�Ǵ� ���ϴ� �⺻�� ����)
            currentClearStage = 0;
        }

        if ( currentClearStage > bestStage )
        {
            PlayerPrefs.SetInt($"{GameManager.Instance.PlayerElemental}CurrentStage", currentClearStage);
        }
    }
}
