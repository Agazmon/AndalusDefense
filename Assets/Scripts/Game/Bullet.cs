using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        // Destroy the bullet after 2 seconds.
        Destroy(gameObject, 2.0f);
    }
    void OnTriggerEnter(Collider other)
    {
        // Destroy the bullet if collide with Enemy.
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

    }
}
