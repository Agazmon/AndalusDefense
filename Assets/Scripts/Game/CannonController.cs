using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonStatus
{
    DISABLED,
    SHOOTING,
    IDLE,
    COOLDOWN
}
public class CannonController : MonoBehaviour
{
    [Header("Cannon Stats")]
    public ElementType activeType;
    public Stats activeStats;
    public int activeCooldown;
    public Stats FireStats;
    public Stats PlantStats;
    public Stats WaterStats;

    [Header("Cannon Status")]
    public CannonStatus currentStatus = CannonStatus.DISABLED;
    [Header("Objective Settings")]
    public List<GameObject> inRangeEnemies;
    public GameObject selectedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        //Cargo las stats del cannon
        foreach(Stats stats in GetComponents<Stats>())
        {
            if (stats.Element.Element == ElementType.Fire)
                FireStats = stats;
            else if (stats.Element.Element == ElementType.Water)
                WaterStats = stats;
            else if (stats.Element.Element == ElementType.Grass)
                PlantStats = stats;
        }
        activeStats = getStatsByType(ElementType.Fire);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedEnemy != null)
        {
            // Calculate the distance between the turret and the player.
            float distance = Vector3.Distance(transform.position, selectedEnemy.transform.position);

            // If the player is within range, and it has been at least fireInterval seconds since the last fire, fire a shot.
            if (distance <= activeStats.AttackRange && (currentStatus != CannonStatus.DISABLED && currentStatus != CannonStatus.SHOOTING))
            {
                currentStatus = CannonStatus.SHOOTING;
                StartCoroutine(Shoot());
            }
            PerformRotation();
        }
        else
            selectNewEnemy();
    }
    public void ChangeStatus(CannonStatus newStatus)
    {
        this.currentStatus = newStatus;
    }
    public void AddInRangeEnemy(GameObject enemyToAdd)
    {
        if (enemyToAdd != null)
            inRangeEnemies.Add(enemyToAdd);
        selectNewEnemy();
    }

   
    public void RemoveInRangeEnemy(GameObject enemyToRemove)
    {
        if (enemyToRemove != null)
            inRangeEnemies.Remove(enemyToRemove);
        selectNewEnemy();
    }
    public void selectNewEnemy()
    {
        //If there is any enemy in range, we select one randomly.
        if (inRangeEnemies.Count > 0)
        {
            int ind = Random.Range(0, inRangeEnemies.Count);
            selectedEnemy = inRangeEnemies[ind];
            if (selectedEnemy == null) selectNewEnemy();
        }
        else
            currentStatus = CannonStatus.IDLE;
        
    }

    private void PerformRotation()
    {
        Vector3 direction = selectedEnemy.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }
    private IEnumerator Shoot()
    {
        while (currentStatus == CannonStatus.SHOOTING)
        {
            GameObject instantiatedAttack = Instantiate(activeStats.Attack, transform.position + (Vector3.forward * 1.3f), Quaternion.identity);
            instantiatedAttack.GetComponent<Rigidbody>().AddForce((transform.forward * 80), ForceMode.Impulse);
            instantiatedAttack.GetComponent<Attack>().parent = this;
            yield return new WaitForSeconds(activeStats.Attack.GetComponent<Attack>().BaseCooldown * activeStats.Element.GetElementCooldownModifier());
        }
    }

    public void SetActiveStats(ElementType type)
    {
        Debug.Log("ha cambiado las stats a:" + type);
        activeType = type;
        activeStats = getStatsByType(type);
        this.gameObject.transform.parent.GetChild(2).GetComponent<MeshRenderer>().material = activeStats.Element.ElementMaterial;
        activeCooldown = activeStats.Attack.GetComponent<Attack>().BaseCooldown;
    }
    public Stats getStatsByType(ElementType type)
    {
        switch (type)
        {
            case ElementType.Fire:
                return this.FireStats;
            case ElementType.Water:
                return this.WaterStats;
            case ElementType.Grass:
                return this.PlantStats;
            default:
                return this.FireStats;
        }
    }
   
    [System.Serializable]
    public class CannonStats
    {
        [SerializeField]
        private ElementType type;
        [SerializeField]
        private Stats stats;
        public CannonStats(Stats stats)
        {
            setElement(stats.Element.Element);
            setStats(stats);
        }
        public void setElement(ElementType type)
        {
            this.type = type;
        }
        public void setStats(Stats stats)
        {
            if (stats == null)
                Debug.Log("Error al inicializar unas Cannon Stats");
            else
                this.stats = stats;
        }
        public Stats getStats()
        {
            return this.stats;
        }
    }
}
