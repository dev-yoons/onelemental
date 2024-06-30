using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    //restart ��ư�� ������
    public void OnClickRestart()
    {
        //ù ����� �������� �ȴ�.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("StartScene"); // StartScene ���� �� Build Settings�� �߰�
    }

    public void GoSelectScene()
    {
        SceneManager.LoadScene("SelectElementScene");
    }

    public void NextStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string nextScene = IncrementSceneNumber(currentScene);
        SceneManager.LoadScene(nextScene);
    }

    public void GoStage(int stageNum)
    {
        SceneManager.LoadScene($"Stage{stageNum}");
    }

    public void GoStageN(int n)
    {
        string sceneName = "Stage";
        sceneName += n;
        SceneManager.LoadScene(sceneName);
    }

    string IncrementSceneNumber(string sceneName)
    {
        // ���� ǥ�������� ���� ����
        Match match = Regex.Match(sceneName, @"\d+");

        if (match.Success)
        {
            // ���� �κ��� �߰ߵǸ� �̸� ������ ��ȯ�Ͽ� 1 ���ϱ�
            int number = int.Parse(match.Value);
            number += 1;

            // ���ο� �� �̸� ���� (���� ���ڿ����� ���� �κ��� ���ο� ���ڷ� ��ü)
            string newSceneName = Regex.Replace(sceneName, @"\d+", number.ToString());
            return newSceneName;
        }
        else
        {
            // ���� �κ��� ������ ���� ���ڿ� ��ȯ (�Ǵ� �ٸ� ���� ó��)
            return sceneName;
        }
    }
}
