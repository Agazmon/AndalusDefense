using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    UNDEFINED ,GOLD, PLANT, WATER, FIRE
}
public class Resource : MonoBehaviour
{
    [Header("Resource Type Settings")]
    public ResourceType resourceType;
    public float goldDropChance = 0.5f;
    [Header("Amount Settings")]
    public int amount;
    public int SpawnThreshold = 5;
    
    private void Start()
    {
        WaveController waveController = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveController>();
        if (waveController.CurrentWave < SpawnThreshold)
            amount = Random.Range(1, SpawnThreshold + 1);
        else
            amount = Random.Range(waveController.CurrentWave - SpawnThreshold, waveController.CurrentWave);
        StartCoroutine(StartJob());
    }
    public void setType(ElementType type)
    {
        //TODO Cambiar Mesh en función del tipo
        switch (type)
        {
            case ElementType.Fire:
                resourceType = ResourceType.FIRE;
                break;
            case ElementType.Grass:
                resourceType = ResourceType.PLANT;
                break;
            case ElementType.Water:
                resourceType = ResourceType.WATER;
                break;
            default:
                resourceType = ResourceType.GOLD;
                break;
        }
    }
    private IEnumerator StartJob()
    {
        yield return new WaitUntil(() => resourceType != ResourceType.UNDEFINED);
        //CHANCE DE CAMBIAR EL TIPO
        if (Random.Range(0f, 1.0f) <= goldDropChance)
            resourceType = ResourceType.GOLD;

        //TODO ANIMAR

        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("GameController").GetComponent<ResourcesController>().AddResource(resourceType , amount));

    }
}

