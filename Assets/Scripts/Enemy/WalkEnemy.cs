using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(velocidadMovimiento);
    }

    private void FixedUpdate()
    {
        if (estaMuerto) return;
        if (DetectarPared()) 
        {
            CambiarDireccion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (estaMuerto) return;
        Walk();
    }
}
