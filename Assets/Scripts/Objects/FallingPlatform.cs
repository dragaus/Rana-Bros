using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float tiempoPlataforma;
    Rigidbody2D rigi;
    public bool seNecesitaRestablecer;

    public GameObject modelo;
    Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        rigi.bodyType = RigidbodyType2D.Static;
        posicionInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //revisar que la colision sea de un personaje
        if (collision.gameObject.CompareTag("Player")) 
        {
            //es empezar una rutina para que se caiga despues de un tiempo la plataforma
            StartCoroutine(EmpezarACaer());
        }
    }

    private void OnTriggerEnter2D(Collider2D target) 
    {
        if (target.CompareTag("Collector")) 
        {
            if (seNecesitaRestablecer) 
            { 
                Instantiate(modelo, posicionInicial, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator EmpezarACaer() 
    {
        yield return new WaitForSeconds(tiempoPlataforma);
        rigi.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Animator>().Play("apagado");
    }
}
