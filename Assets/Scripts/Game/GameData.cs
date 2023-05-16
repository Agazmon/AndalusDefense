using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (TryServer())
        {
            // Guardado en la nube
        }else if (TryLocal())
        {
            // Guardado en local
        }
        else
        {
            //TODO No hay guardado
        }
    }

    private bool TryServer()
    {
        //TODO Implementar
        return false;
    }
    private bool TryLocal()
    {
        return true;
    }
}
