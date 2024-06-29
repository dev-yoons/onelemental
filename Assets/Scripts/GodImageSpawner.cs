using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;
using Onelemental.Managers;

public class GodImageSpawner : MonoBehaviour
{
    public GameObject GodSymbol;

    public GameObject FireGodPrefab;
    public GameObject WaterGodPrefab;
    public GameObject WindGodPrefab;
    public GameObject GroundGodPrefab;

    public Sprite FireGodSymbol;
    public Sprite WaterGodSymbol;
    public Sprite WindGodSymbol;
    public Sprite GroundGodSymbol;

    public void SetGod(Elemental godElemental)
    {
        GameObject godPrefab;
        Sprite godSprite;
        switch (godElemental)
        {
            case Elemental.Fire:
                godPrefab = FireGodPrefab;
                godSprite = FireGodSymbol;
                break;
            case Elemental.Water:
                godPrefab = WaterGodPrefab;
                godSprite = WaterGodSymbol;
                break;
            case Elemental.Wind:
                godPrefab = WindGodPrefab;
                godSprite = WindGodSymbol;
                break;
            case Elemental.Ground:
                godPrefab = GroundGodPrefab;
                godSprite = GroundGodSymbol;
                break;
            default:
                godPrefab = FireGodPrefab;
                godSprite = FireGodSymbol;
                break;
        }
        GameObject godObject = Instantiate(godPrefab, gameObject.transform.position, Quaternion.identity);

        GodSymbol.GetComponent<SpriteRenderer>().sprite = godSprite;
    }
}
