using System.Collections;
using System.Collections.Generic;
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

    public void NextStage()
    {
        SceneManager.LoadScene("Stage2"); // 수정 필요
    }
}
