using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class TurretAgent : Agent
{
    public Transform player; // The transform of the player game object.
    public float range = 100f; // The maximum range at which the turret can fire.
    public float fireInterval = 0.5f; // The interval at which the turret fires, in seconds.
    public float fuerza = 10f; // The force applied to the bullet 
    public GameObject bullet; //Prefab of the bullet
    public GameObject bulletSpawn; //Prefab of bullet's spawn


    private float lastFireTime; // The time at which the turret last fired.
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    Rigidbody m_Rigidbody;
    
    void Start()
    {
        // Set the initial position and rotation of the turret.
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    void Update()
    {
        // Calculate the distance between the turret and the player.
        float distance = Vector3.Distance(transform.position, player.position);

        // If the player is within range, and it has been at least fireInterval seconds since the last fire, fire a shot.
        if (distance <= range && Time.time - lastFireTime >= fireInterval)
        {
            //AddReward(1f); // Add a reward for hitting the player.
            lastFireTime = Time.time;
            FireShot();
        }

        // Rotate the turret so that it always faces the player.
        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    void FireShot()
    {
        // Implement the logic for firing a shot from the turret.
        bullet.transform.position = bulletSpawn.transform.position;
        var cloneBullet = Instantiate(bullet);

        // Calculate the direction to the player and set the bullet's velocity accordingly.
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        cloneBullet.GetComponent<Rigidbody>().AddForce(transform.localToWorldMatrix * Vector3.forward * fuerza, ForceMode.Impulse);


    }

    public override void OnEpisodeBegin()
    {
        // Reset the position and rotation of the turret to its initial values.
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Reset any other necessary variables.
        lastFireTime = -fireInterval;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
       /* // Add the current position and rotation of the turret to the observations.
        sensor.AddObservation(transform.position);
        sensor.AddObservation(transform.rotation);

        // Add the current position of the player to the observations.
        sensor.AddObservation(player.position);*/
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Choca");

        if (collision.gameObject.CompareTag("Pared"))
        {
            Debug.Log("Castigo");
            AddReward(-1.0f);
            EndEpisode();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Premio");
            AddReward(1.0f);
            EndEpisode();
        }
    }
}
