using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Header("Basic Settings")]
    public Indicator HealthPoints;
    public Indicator Speed;
    public ElementSettings Element;
    [Header("Attack Settings")]
    public Attack Attack;
    public Attack MaxLevelAttack;
    public float AttackRange;
    [Header("Levelling Settings")]
    public int CurrentLevel = 1;
    public int MaxLevel = 10;
    public int HPPerLevel = 5;
    public int AttackPerLevel = 1;
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
        {
            CurrentLevel++;
            HealthPoints.CurrentValue += HPPerLevel;
            Attack.BaseDamage += AttackPerLevel;
        }
    }
    public void LevelTo(int level)
    {
        for (int i = 1; i <= level; i++)
            LevelUp();
    }

    public void PlayDeathAnimation(bool DestroyAfterAnimation = true)
    {
        
    }

}
