using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Status")]
    public int PendingToSpawn = 0;
    [Header("Enemy Settings")]
    public Type SpawnType;
    public int EnemyLevel;
    public GameObject EnemyPrefab;
    //Internal
    private MeshCollider meshCollider;
    private WaveController waveController;
    
    private void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        waveController = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveController>();
    }
    public void Spawn(Type ElementType, int EnemiesToSpawn, int level)
    {
        Debug.Log("Turret:" + gameObject.name + "Element:" + ElementType + " EnemiesToSpawn:" + EnemiesToSpawn + " level:" + level);
        PendingToSpawn = EnemiesToSpawn;
        SpawnType = ElementType;
        EnemyLevel = level;
        StartCoroutine(StartSpawn());
    }
    private IEnumerator StartSpawn()
    {
        int enemiesToSpawn = Random.Range(1, PendingToSpawn);
        for(int i = 1; i<=enemiesToSpawn; i++)
        {
            GameObject spawned = Instantiate(EnemyPrefab,GetRandomPointInsideSpawnArea(), Quaternion.identity, this.transform);
            //TODO esto se debe hacer en el wave controller
            waveController.Enemies.Add(spawned.GetComponent<Stats>());
        }
        PendingToSpawn -= enemiesToSpawn;
        yield return new WaitForSeconds(Random.Range(1, 4));
        if(PendingToSpawn>0)
            yield return StartSpawn();
    }
    private Vector3 GetRandomPointInsideSpawnArea()
    {
        return new Vector3(Random.Range(meshCollider.bounds.min.x, meshCollider.bounds.max.x), transform.position.y, Random.Range(meshCollider.bounds.min.z, meshCollider.bounds.max.z));
    }
}
