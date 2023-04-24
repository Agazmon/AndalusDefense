using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Basic Settings")]
    public Indicator HealthPoints;
    public Indicator Speed;
    public ElementSettings Element;
    [Header("Levelling Settings")]
    public int CurrentLevel = 1;
    public int MaxLevel = 10;
    public float LevelModifier = 1.1f;
    [Header("Attack Settings")]
    public Attack Attack;
    public Attack MaxLevelAttack;
    public float AttackRange;
    [Header("Sounds Settings")]
    public AudioSource DeathSound;
    public AudioSource AttackSound;
    [Header("VFX Settings")]
    public GameObject OnDeath;
    public GameObject OnAttack;

    public void LevelUp()
    {
        if (CurrentLevel >= MaxLevel)
            return;
        else
            CurrentLevel++;
    }

    public void PlayDeathAnimation(bool DestroyAfterAnimation = true)
    {
        
    }

}
