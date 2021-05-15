using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float distancia;
    public float velocidad;
    public float direccion = 1;
    public float distanciaParaActivar;

    Transform target;
    Vector2 posicionInicial;
    Vector2 posicionFinal;

    bool estaVisible;
    bool seEstaMoviendo;

    // Start is called before the first frame update
    void Start()
    {
        //Simpere supongo que el valor final esta a la derecha numeros positivos
        //y el valor inicial a la izquierda o hacia los negativos
        target = FindObjectOfType<PlayerMovement>().transform;
        distancia = Mathf.Abs(distancia);
        posicionInicial = new Vector2(transform.position.x - distancia, transform.position.y);
        posicionFinal = new Vector2(transform.position.x + distancia, transform.position.y);

        //if (distancia < 0)
        //{
        //    posicionInicial = new Vector2(transform.position.x + distancia, transform.position.y);
        //    posicionFinal = transform.position;
        //}
        //else
        //{
        //    posicionInicial = transform.position;
        //    posicionFinal = new Vector2(transform.position.x + distancia, transform.position.y);
        //}
        // Esta seruia forma uno
    }

    // Update is called once per frame
    void Update()
    {
        if (estaVisible)
        {
            MoverPlataforma();
        }
        else 
        {
            bool seDebeMover 
                = Vector3.Distance(transform.position, target.position) < distanciaParaActivar;
            if (seDebeMover) 
            {
                MoverPlataforma();
            }
        }
    }

    private void OnBecameVisible()
    {
        estaVisible = true;
    }

    private void OnBecameInvisible()
    {
        estaVisible = false;
    }

    private void MoverPlataforma()
    {
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        if (posicionFinal.x <= transform.position.x)
        {
            direccion = -1;
        }

        if (posicionInicial.x >= transform.position.x)
        {
            direccion = 1;
        }
    }
}
