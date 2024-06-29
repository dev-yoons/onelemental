using System.Collections;
using System.Collections.Generic;
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

    public void NextStage()
    {
        SceneManager.LoadScene("Stage2"); // ���� �ʿ�
    }
}
