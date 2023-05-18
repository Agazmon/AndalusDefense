using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyStatus
    {
        WALKING,
        SHOOTING,
        DEAD
    }
    // STATUS
    public EnemyStatus currentStatus = EnemyStatus.WALKING;
    // NAVIGATION
    [Header("Navigation Settings")]
    private NavMeshAgent agent;
    private Vector3 objectivePosition;
    public int stopThreshold = 1;
    [Header("Dropped Resources Settings")]
    public GameObject DroppedResource;
    //Private Resources
    private WaveController waveController;
    private Stats stats;
    public Vector3 shootingTarget;
  
    
    // Start is clled before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<Stats>();
        waveController = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveController>();
        agent.speed = stats.Speed.CurrentValue;
        agent.stoppingDistance = stats.AttackRange;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowards(shootingTarget);
        if (agent.remainingDistance - agent.stoppingDistance <= stopThreshold && (currentStatus != EnemyStatus.SHOOTING && currentStatus != EnemyStatus.DEAD))
        {
            currentStatus = EnemyStatus.SHOOTING;
            StartCoroutine(Shoot());
        }
    }
    public void SetTarget(Vector3 target)
    {
        this.shootingTarget = target;
    }
   
    private IEnumerator Shoot()
    {
        while (currentStatus == EnemyStatus.SHOOTING) { 
            GameObject instantiatedAttack = Instantiate(stats.Attack, transform.position+(Vector3.forward*3f), Quaternion.identity);
            instantiatedAttack.GetComponent<Rigidbody>().AddForce((transform.forward* (Random.Range(30.0f, 45.0f)) + Vector3.up*Random.Range(10.0f,15.0f)), ForceMode.Impulse);
            yield return new WaitForSeconds(stats.Attack.GetComponent<Attack>().BaseCooldown);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player/Attack"))
        {
            stats.UpdateHealthPoints(stats.HealthPoints.CurrentValue - collision.gameObject.GetComponent<Attack>().BaseDamage);
            if (stats.HealthPoints.CurrentValue <= 0){
                waveController.RemoveEnemy(stats);
                collision.gameObject.GetComponent<Attack>().parent.RemoveInRangeEnemy(this.gameObject);
                //SFX de muerte
                //Generar recursos
                GameObject resource = Instantiate(DroppedResource, this.transform);
                resource.GetComponent<Resource>().setType(stats.Element.Element);
                Destroy(this.gameObject);
            }
            waveController.CalculateRemainingHP();
        }
    }
    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1);
    }
}
