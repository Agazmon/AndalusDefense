using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Stats Settings")]
    public int BaseDamage;
    public int BaseCooldown;
    [Header("Visual Settings")]
    GameObject Model;
    GameObject ImpactVFX;
}
