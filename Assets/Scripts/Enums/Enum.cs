using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
namespace Onelemental.Enum
{
    public enum Elemental
    {
        Neutral,
        Fire,
        Water,
        Wind,
        Ground
    }

    public class EnumStatics
    { 
        static public Color GetElementalColor(Elemental elemental)
        {
            switch (elemental)
            {
                case Elemental.Fire:
                    return Color.red;
                case Elemental.Water:
                    return Color.blue;
                case Elemental.Wind:
                    return Color.gray;
                case Elemental.Ground:
                    return Color.yellow;
                case Elemental.Neutral:
                    return Color.white;
                default:
                    return Color.white;
            }
        }
    }
     
}