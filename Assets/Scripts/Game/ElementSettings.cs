using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ElementSettings
{
    //Type Enum
   
    [Header("Element Settings")]
    public ElementType Element;
    public Material ElementMaterial;
    [Space(5)]
    [Header("Strong Against Element - 1.5x Damage")]
    public ElementType StrongAgainstElement;
    float StrongAgainsModifier = 1.5f;
    [Space(5)]
    [Header("Weak Against Element - 0.5x Damage")]
    public ElementType WeakAgaisntElement;
    float WeakAgainsModifier = .5f;
    
    public float GetElementCooldownModifier()
    {
        switch (Element)
        {
            case ElementType.Fire:
                return 1.5f;
            case ElementType.Grass:
                return 1.0f;
            case ElementType.Water:
                return 0.5f;
            default:
                return 1.0f;
        }
    }

}
