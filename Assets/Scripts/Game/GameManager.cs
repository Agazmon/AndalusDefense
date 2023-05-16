using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Indicator PlayerHealth;
    public Indicator TimeSpent;
    public string CurrentPlayer;
    GameData DataManager;

    private void Start()
    {
        TimeSpent.autoUpdate = true;
        TimeSpent.autoUpdateRate = 1;
        TimeSpent.UpdateRateSeconds = 1;
        StartCoroutine(TimeSpent.AutoUpdateStart());
    }
    public void UpdatePlayerHealth(int newCurrentHealth)
    {
        PlayerHealth.CurrentValue = newCurrentHealth;
    }
    public void DamagePlayer(int damage)
    {
        PlayerHealth.CurrentValue -= damage;
    }
}
