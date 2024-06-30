using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    //restart 버튼을 누르면
    public void OnClickRestart()
    {
        //첫 장면을 가져오게 된다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("StartScene"); // StartScene 생성 후 Build Settings에 추가
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
        // 정규 표현식으로 숫자 추출
        Match match = Regex.Match(sceneName, @"\d+");

        if (match.Success)
        {
            // 숫자 부분이 발견되면 이를 정수로 변환하여 1 더하기
            int number = int.Parse(match.Value);
            number += 1;

            // 새로운 씬 이름 생성 (기존 문자열에서 숫자 부분을 새로운 숫자로 교체)
            string newSceneName = Regex.Replace(sceneName, @"\d+", number.ToString());
            return newSceneName;
        }
        else
        {
            // 숫자 부분이 없으면 원래 문자열 반환 (또는 다른 예외 처리)
            return sceneName;
        }
    }
}
