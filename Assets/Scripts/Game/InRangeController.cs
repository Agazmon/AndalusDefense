using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRangeController : MonoBehaviour
{
    CannonController parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.GetChild(0).GetComponent<CannonController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemies/Enemy"))
            parent.AddInRangeEnemy(collision.gameObject);
    }

   /* private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemies/Enemy"))
            parent.AddInRangeEnemy(collision.gameObject);
    }*/
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemies/Enemy"))
            parent.RemoveInRangeEnemy(collision.gameObject);
    }
}
