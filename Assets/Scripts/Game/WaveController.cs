using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Wave Status")]
    public int CurrentWave;
    public int WaveModifier;
    [Header("Enemy Status")]
    public int EnemyCount;
    public int TotalEnemyHP;
    [Header("Wave Settings")]
    public int BaseEnemySpawn = 10;
    [Tooltip("Aplicado cada ronda")]
    public int EnemyCountAdd = 5;
    public int CurrentEnemyAdded;
    [Tooltip("Aplicado cada 5 rondas")]
    public int EnemyHPIncrease = 5;
    public int CurrentEnemyHPIncreased;
    [Header("Wave GameObjects")]
    public List<GameObject> SpawnPoints;
    public List<Stats> Enemies;
    public void Start()
    {
        SpawnPoints = new List<GameObject>();
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Enemies/SpawnZone"))
            if (gameObject.GetComponent<EnemySpawnArea>() == null)
                Debug.LogError("Error: Hay un SpawnArea sin el Script EnemySpawnArea");
            else
                SpawnPoints.Add(gameObject);
        if(SpawnPoints.Count == 0)
                Debug.LogError("Error: No hay SpawnArea en la Escena");
       
    }
    public void StartWave()
    {
        int currentWaveHPAdded = 0;
        if (CurrentWave % 5 == 0)
            CurrentEnemyHPIncreased += EnemyHPIncrease;
        //TODO Deshabilitar zonas aleatoriamente
        //TODO Calcular los puntos de vida
        //TODO Seleccionar los puntos de Spawn
    }
    public void FinishWave()
    {
        //TODO Aumentar contador de Wave

    }
    public void DisableEnemy(Stats enemyRemoved)
    {
        if (!Enemies.Contains(enemyRemoved))
            Debug.LogError("Error: El enemigo no se ha encontrado en la Lista de la Wave");
        else
            Enemies.Remove(enemyRemoved);
        enemyRemoved.PlayDeathAnimation(true);
    }

    public void CalculateRemainingHP()
    {
        TotalEnemyHP = 0;
        foreach(Stats enemy in Enemies)
            TotalEnemyHP += enemy.HealthPoints.CurrentValue;
    }
}
