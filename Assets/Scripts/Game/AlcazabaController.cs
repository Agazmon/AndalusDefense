using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcazabaController : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemies/Attack"))
        manager.DamagePlayer(collision.gameObject.GetComponent<Attack>().BaseDamage);
    }

}
