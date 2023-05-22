using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasContextual : MonoBehaviour
{
    void Update()
    {
        //Miramos a la camara
        transform.LookAt(Camera.main.transform);

        //Comprobamos si se roto o no el padre
        if(transform.localScale.x *-1 != transform.parent.localScale.x)
        {
            Vector3 escala = transform.localScale;
            escala.x = escala.x * -1;
            transform.localScale = escala;
        }


    }
}
