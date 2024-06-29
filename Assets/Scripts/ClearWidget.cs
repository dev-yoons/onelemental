using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearWidget : MonoBehaviour
{

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public void Init(float elapsedTime)
    {
        Debug.Log(elapsedTime);
        if (elapsedTime > 120.0f)
        {
            Star2.SetActive(false); 
        }
        if (elapsedTime > 60.0f)
        {
            Star3.SetActive(false);
        }
    }
}
