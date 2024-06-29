using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Onelemental.Managers;

public class GodImageSpawner : MonoBehaviour
{

    public GameObject GodPrefab;

    private GameObject godObject;

    public void SetGod(Elemental godElemental)
    { 
        godObject = Instantiate(GodPrefab, gameObject.transform.position, Quaternion.identity);
        godObject.GetComponent<GodImage>().SetElemental(godElemental);
    }
    
    public void GodDead()
    {
        godObject.GetComponent<GodImage>().GodDead();
    }
}
