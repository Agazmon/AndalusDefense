using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Status")]
    public int PendingToSpawn = 0;
    [Header("Enemy Settings")]
    public ElementType SpawnType;
    public int EnemyLevel;
    public GameObject EnemyPrefab;
    //Internal
    private MeshCollider meshCollider;
    private WaveController waveController;

    public bool isLoaded=false;
    
    private void Start()
    {
        isLoaded = false;
        meshCollider = GetComponent<MeshCollider>();
        waveController = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveController>();
        isLoaded = true;
    }
    public void Spawn(ElementType ElementType, int EnemiesToSpawn, int level)
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
            spawned.GetComponent<NavMeshAgent>().SetDestination(this.gameObject.transform.GetChild(0).transform.position);
            spawned.GetComponent<EnemyController>().SetTarget(this.gameObject.transform.GetChild(1).transform.position);

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
