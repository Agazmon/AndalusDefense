using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        // Destroy the bullet after 2 seconds.
       
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        // Destroy the bullet if collide with Enemy.
        if (other.CompareTag("Enemy"))
        {
           
        }

    }
}
