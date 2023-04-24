using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ElementSettings
{
    //Type Enum
    public enum Type
    {
        Fire,
        Water,
        Grass
    }
    [Header("Element Settings")]
    public Type Element;
    public Material ElementMaterial;
    [Space(5)]
    [Header("Strong Against Element - 1.5x Damage")]
    public Type StrongAgainstElement;
    float StrongAgainsModifier = 1.5f;
    [Space(5)]
    [Header("Weak Against Element - 0.5x Damage")]
    public Type WeakAgaisntElement;
    float WeakAgainsModifier = .5f;
    
    public float GetElementCooldownModifier()
    {
        switch (Element)
        {
            case Type.Fire:
                return 1.5f;
            case Type.Grass:
                return 1.0f;
            case Type.Water:
                return 0.5f;
            default:
                return 1.0f;
        }
    }

}
