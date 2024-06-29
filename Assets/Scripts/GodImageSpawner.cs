using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Onelemental.Managers;

public class GodImageSpawner : MonoBehaviour
{

    public GameObject FireGodPrefab;
    public GameObject WaterGodPrefab;
    public GameObject WindGodPrefab;
    public GameObject GroundGodPrefab;

    public void SetGod(Elemental godElemental)
    {
        GameObject godPrefab;
        switch (godElemental)
        {
            case Elemental.Fire:
                godPrefab = FireGodPrefab;
                break;
            case Elemental.Water:
                godPrefab = WaterGodPrefab;
                break;
            case Elemental.Wind:
                godPrefab = WindGodPrefab;
                break;
            case Elemental.Ground:
                godPrefab = GroundGodPrefab;
                break;
            default:
                godPrefab = FireGodPrefab; ;
                break;
        }
        GameObject godObject = Instantiate(godPrefab, gameObject.transform.position, Quaternion.identity); 
    }
}
