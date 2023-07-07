using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Usage { Walk, Jump, SpecialMovement}
public class MovementPattern : ScriptableObject
{
    public Usage usage;

    /// <summary>
    /// Berechnet das aktuelle Movement über eine Funktion zu dem übergebenen Vektor
    /// </summary>
    /// <param name="vec"></param>
    /// <returns></returns>
    public virtual Vector2 CalculateMovement(Vector2 vec)
    {
        Vector2 movementVector2 = new Vector2();

        return movementVector2;
    }

    public virtual float CalculateMovement(float x)
    {
        return Mathf.Pow(x, 2);
    }

}
