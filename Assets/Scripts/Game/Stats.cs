using System;
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
    public GameObject Attack;
    public GameObject MaxLevelAttack;
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
            Debug.Log("Ha subido de LVL");
            CurrentLevel++;
            HealthPoints.CurrentValue += HPPerLevel;
            Attack.GetComponent<Attack>().BaseDamage += AttackPerLevel;
        }
    }
    public void LevelTo(int level)
    {
        for (int i = CurrentLevel; i <= level; i++)
            LevelUp();
    }

    public void UpdateHealthPoints(int newHealthPoints)
    {
        HealthPoints.CurrentValue = newHealthPoints;
    }

    public IEnumerator PlayDeathAnimation(bool DestroyAfterAnimation = true)
    {
        //esperar hasta que termine la animacion
        yield return new WaitForSeconds(2);
        if (DestroyAfterAnimation) Destroy(this.gameObject);
    }

    public override bool Equals(object obj)
    {
        return obj is Stats stats &&
               (((Stats)obj).Element.Element == stats.Element.Element);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Element);
    }
}
