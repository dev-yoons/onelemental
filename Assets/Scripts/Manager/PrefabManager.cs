using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Managers;

public class PrefabManager : MonoBehaviour
{
    public GameObject LinePrefab;

    public GameObject FireEffectPrefab; 

    public void Start()
    {
        GameManager.PrefabManager = this;
    }
}
