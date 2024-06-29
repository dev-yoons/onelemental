using System.Collections.Generic;
using UnityEngine;

public class WorshiperPool : MonoBehaviour
{
    // 싱글톤
    public static WorshiperPool Instance;

    public GameObject worshiperPrefab;
    public int poolSize = 30;

    private List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(worshiperPrefab, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        
        return Instantiate(worshiperPrefab, transform);
    }

    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}