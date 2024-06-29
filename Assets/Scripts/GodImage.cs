using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

public class GodImage : MonoBehaviour
{
    public GameObject GodImageObject;
    public GameObject UnderBarImageObject;
    public GameObject SymbolObject;

    public Sprite FireGodSprite;
    public Sprite WaterGodSprite;
    public Sprite WindGodSprite;
    public Sprite GroundGodSprite;

    public Sprite FireGodDeadSprite;
    public Sprite WaterGodDeadSprite;
    public Sprite WindGodDeadSprite;
    public Sprite GroundGodDeadSprite;

    public Sprite FireGodUnderbarSprite;
    public Sprite WaterGodUnderbarSprite;
    public Sprite WindGodUnderbarSprite;
    public Sprite GroundGodUnderbarSprite;

    public Sprite FireGodUnderbarDeadSprite;
    public Sprite WaterGodUnderbarDeadSprite;
    public Sprite WindGodUnderbarDeadSprite;
    public Sprite GroundGodUnderbarDeadSprite;

    public Sprite FireSymbolSprite;
    public Sprite WaterSymbolSprite;
    public Sprite WindSymbolSprite;
    public Sprite GroundSymbolSprite;

    private Sprite GodSprite;
    private Sprite GodDeadSprite;
    private Sprite GodUnderBarSprite;
    private Sprite GodUnderBarDeadSprite;
    private Sprite SymbolSprite;

    public void SetElemental(Elemental elemental)
    {
        switch (elemental)
        {
            case Elemental.Fire:
                GodSprite = FireGodSprite;
                GodDeadSprite = FireGodDeadSprite;
                GodUnderBarSprite = FireGodUnderbarSprite;
                GodUnderBarDeadSprite = FireGodUnderbarDeadSprite;
                SymbolSprite = FireSymbolSprite;
                break;
            case Elemental.Water:
                GodSprite = WaterGodSprite;
                GodDeadSprite = WaterGodDeadSprite;
                GodUnderBarSprite = WaterGodUnderbarSprite;
                GodUnderBarDeadSprite = WaterGodUnderbarDeadSprite;
                SymbolSprite = WaterSymbolSprite;
                break;
            case Elemental.Wind:
                GodSprite = WindGodSprite;
                GodDeadSprite = WindGodDeadSprite;
                GodUnderBarSprite = WindGodUnderbarSprite;
                GodUnderBarDeadSprite = WindGodUnderbarDeadSprite;
                SymbolSprite = WindSymbolSprite;
                break;
            case Elemental.Ground:
                GodSprite = GroundGodSprite;
                GodDeadSprite = GroundGodDeadSprite;
                GodUnderBarSprite = GroundGodUnderbarSprite;
                GodUnderBarDeadSprite = GroundGodUnderbarDeadSprite;
                SymbolSprite = GroundSymbolSprite;
                break; 
            default:
                break;
        }

        GodImageObject.GetComponent<SpriteRenderer>().sprite = GodSprite;
        UnderBarImageObject.GetComponent<SpriteRenderer>().sprite = GodUnderBarSprite;
        SymbolObject.GetComponent<SpriteRenderer>().sprite = SymbolSprite;

        Vector3 objectPosition = this.gameObject.transform.position;
        if (objectPosition.x < 0 && objectPosition.y < 0)
        {
            SymbolObject.transform.position = new Vector3(-3.80940962f, 1.59326315f, 0) + gameObject.transform.position;
        }
        else if (objectPosition.x < 0 && objectPosition.y > 0)
        {
            SymbolObject.transform.position = new Vector3(-3.80940962f, 2.4000001f, 0) + gameObject.transform.position;
        }
        else if (objectPosition.x > 0 && objectPosition.y < 0)
        {
            SymbolObject.transform.position = new Vector3(-2.18000007f, 1.59326315f, 0) + gameObject.transform.position;
        }
        else
        {
            SymbolObject.transform.position = new Vector3(-2.18000007f, 2.4000001f, 0) + gameObject.transform.position;
        }
    }

    public void GodDead()
    { 
        GodImageObject.GetComponent<SpriteRenderer>().sprite = GodDeadSprite;
        UnderBarImageObject.GetComponent<SpriteRenderer>().sprite = GodUnderBarDeadSprite;
    }
}
