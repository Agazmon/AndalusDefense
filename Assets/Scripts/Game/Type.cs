using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Fire,
    Water,
    Grass
}
static class TypeUtils
{
    public static Type GetRandomType()
    {
        System.Random random = new System.Random();
        System.Array values = typeof(Type).GetEnumValues();
        return (Type)values.GetValue(random.Next(values.Length));
    }
}

