using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Wave Status")]
    public int CurrentWave = 0;
    public int EnabledSpawns = 1;
    public int TotalEnemyHP;
    [Header("Enemy Status")]
    public int EnemyCount;
    public int CurrentEnemyLevel = 1;
    [Header("Wave Settings")]
    public int BaseEnemySpawn = 10;
    [Tooltip("Aplicado cada ronda")]
    public int EnemyCountAdd = 5;
    public int CurrentEnemyAdded;
    [Tooltip("Aplicado cada 5 rondas")]
    public int EnemyLevelIncrease = 1;
    public int EnemyLevelIncreaseCooldown = 5;
    [Tooltip("Cada cuantas rondas se incrementa el número de Spawn Points")]
    public int SpawnIncrementRoundCooldown = 5;
    [Header("Wave GameObjects")]
    public List<GameObject> SpawnPoints;
    public List<Stats> Enemies;
    public void Start()
    {
        SpawnPoints = new List<GameObject>();
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Enemies/SpawnZone"))
            if (gameObject.GetComponent<Spawner>() == null)
                Debug.LogError("Error: Hay un SpawnArea sin el Script Spawner");
            else
                SpawnPoints.Add(gameObject);
        if (SpawnPoints.Count == 0)
            Debug.LogError("Error: No hay SpawnArea en la Escena");
        StartWave();

    }
    public void StartWave()
    {
        CurrentWave++;
        //INCREMENTOS DE RONDA
        if (CurrentWave % EnemyLevelIncreaseCooldown == 0)
            CurrentEnemyLevel += EnemyLevelIncrease;
        if (CurrentWave % SpawnIncrementRoundCooldown == 0)
            EnabledSpawns += 1;
        BaseEnemySpawn += EnemyCountAdd;
        /**if (EnabledSpawns <= SpawnPoints.Count) {
            int currentlyEnabled = 0;
            while (currentlyEnabled < EnabledSpawns) {
                // Elegir uno de la lista
                int tryEnable = Random.Range(0, SpawnPoints.Count + 1);
                // Mirar si está habilitado
                if (!SpawnPoints[tryEnable].activeSelf) {
                    // Si no lo está habilitarlo e incrementar el contador
                    SpawnPoints[tryEnable].SetActive(true);
                    currentlyEnabled++;
                }
            }
        }*/
        double[] EnemiesPerSpawnPoint = GenerateRandomNormalizedArray(EnabledSpawns);
        bool[] ActivePoints = GenerateRandomActivePoints(EnabledSpawns);

        int count = 0;
        for(int i=0;i < SpawnPoints.Count;i++)
        {
            SpawnPoints[i].SetActive(ActivePoints[i]);

            if (ActivePoints[i])
            {
                SpawnPoints[i].GetComponent<Spawner>().Spawn(TypeUtils.GetRandomType(), (int) System.Math.Round(EnemiesPerSpawnPoint[count] * BaseEnemySpawn), CurrentWave);
                count++;
            }

        }
    }
    public void FinishWave()
    {
        //TODO Avisar gameController
    }
    public void RemoveEnemy(Stats enemyRemoved)
    {
        if (!Enemies.Contains(enemyRemoved))
            Debug.LogError("Error: El enemigo no se ha encontrado en la Lista de la Wave");
        else
            Enemies.Remove(enemyRemoved);
        enemyRemoved.PlayDeathAnimation(true);
        if (Enemies.Count == 0)
            FinishWave();
    }
    public void CalculateRemainingHP()
    {
        TotalEnemyHP = 0;
        foreach (Stats enemy in Enemies)
            TotalEnemyHP += enemy.HealthPoints.CurrentValue;
    }
    
    public bool[] GenerateRandomActivePoints(int nActive)
    {
        List<bool> l = new List<bool>();
        for(int i = 0; i < SpawnPoints.Count; i++)
        {
            l.Add(i < nActive);
        }

        System.Random rand = new System.Random();
        var shuffled = l.OrderBy(_ => rand.Next()).ToList();

        return shuffled.ToArray();
    }
    public double[] GenerateRandomNormalizedArray(int n)
    {
        List<double> r = new List<double>();
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            float aux = Random.Range(0f, 1f);
            r.Add(aux);
            sum += aux;
        }
        //normalizamos
        for (int i = 0; i < r.Count; i++)
        {
            r[i] /= sum;
        }
        return r.ToArray();
    }
    public void SetAllSpawnPointState(bool newState)
    {
        foreach(GameObject SpawnPoint in SpawnPoints)
            SpawnPoint.SetActive(newState);
    }
    
}
