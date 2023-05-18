using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack : MonoBehaviour
{
    [Header("Stats Settings")]
    public int BaseDamage;
    public int BaseCooldown;
    [Header("Visual Settings")]
    public Mesh Model;
    public GameObject ImpactVFX;
    [Header("Hierarchy Settings")]
    public CannonController parent;

    private void Start()
    {
        Destroy(this.gameObject, 4f);
    }
    private IEnumerator OnCollisionEnter(Collision collision)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
