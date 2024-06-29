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

        static public Elemental WinningElemental(Elemental elemental)
        {
            switch(elemental)
            {
                case Elemental.Fire:
                    return Elemental.Water;
                case Elemental.Water:
                    return Elemental.Ground;
                case Elemental.Ground:
                    return Elemental.Wind;
                case Elemental.Wind:
                    return Elemental.Fire;
                default:
                    return Elemental.Fire;
            }
        }
        static public Elemental LosingElemental(Elemental elemental)
        {
            switch (elemental)
            {
                case Elemental.Fire:
                    return Elemental.Wind;
                case Elemental.Water:
                    return Elemental.Fire;
                case Elemental.Ground:
                    return Elemental.Water;
                case Elemental.Wind:
                    return Elemental.Ground;
                default:
                    return Elemental.Fire;
            }
        }
    }
     
}