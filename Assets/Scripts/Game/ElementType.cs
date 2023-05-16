using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Fire,
    Water,
    Grass
}
static class TypeUtils
{
    public static ElementType GetRandomType()
    {
        System.Random random = new System.Random();
        System.Array values = typeof(ElementType).GetEnumValues();
        return (ElementType)values.GetValue(random.Next(values.Length));
    }
}

