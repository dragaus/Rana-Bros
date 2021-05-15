using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigi;
    protected Collider2D coli;

    public float velocidadMovimiento;
    Vector2 direccion = Vector2.left;

    public float fuerzaBrinco;
    protected bool necesitaBrincar;
    protected bool estaTocandoPiso;

    public bool mostrarGizmo;

    [Header("Deteccion Pared")]
    public bool necesitaDetectarPared;
    public float alturaVista;
    public float distanciaVista;
    public LayerMask queEsPared;

    [Header("Detecta Piso")]
    public bool necesitaDetectarPiso;
    public float radioPiso;
    public float distanciaPiso;
    public LayerMask queEsPiso;

    protected bool estaMuerto;
    protected bool yaSeVio;

    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();
    }


    protected void Jump()
    {
        if (estaTocandoPiso)
        {
            rigi.AddForce(Vector2.up * fuerzaBrinco);
        }
    }

    protected void Walk()
    {
        if (!yaSeVio) return;
        transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
    }

    protected bool DetectarPared()
    {
        if (!necesitaDetectarPared) return false;
        if (!yaSeVio) return false;
        var posicionInicio = new Vector2(transform.position.x,
            transform.position.y + alturaVista);
        var hit = Physics2D.Raycast(posicionInicio, direccion, distanciaVista, queEsPared);
        return hit.collider != null;
    }

    public void CambiarDireccion() 
    {
        if (direccion == Vector2.left)
        {
            direccion = Vector2.right;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            direccion = Vector2.left;
            transform.localScale = Vector3.one;
        }
    }

    public bool DetectarPiso() 
    {
        estaTocandoPiso = coli.IsTouchingLayers(queEsPiso);
        if (!necesitaDetectarPiso) return true;
        if (!yaSeVio) return true;

        var posicionCirculo = new Vector3(
            transform.position.x + -1 * distanciaVista,
            transform.position.y + alturaVista + distanciaPiso,
            transform.position.z
        );

        var circulo = Physics2D.OverlapCircle(posicionCirculo, radioPiso);
        return circulo != null;
    }

    private void OnDrawGizmos()
    {
        if (!mostrarGizmo) return;

        //Muestra la distancia de vista
        Gizmos.color = Color.red;
        //var posicionInicio = transform.position;
        //posicionInicio.y += alturaVista;
        var posicionIncio = new Vector3(transform.position.x,
            transform.position.y + alturaVista, transform.position.z);
        Gizmos.DrawLine(posicionIncio, posicionIncio + Vector3.left * distanciaVista);

        Gizmos.color = Color.green;

        var posicionCirculo = new Vector3(
            transform.position.x + -1 * distanciaVista, 
            transform.position.y + alturaVista + distanciaPiso,
            transform.position.z
        );
        Gizmos.DrawWireSphere(posicionCirculo, radioPiso);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !estaMuerto) 
        {
            collision.collider.GetComponent<PlayerMovement>().Mori();
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player")) 
        {
            estaMuerto = true;
            target.GetComponent<PlayerMovement>().MatePersonaje(100);
            GetComponent<Collider2D>().enabled = false;
        }

        if (target.CompareTag("Collector"))
        {
            Destroy(gameObject);
        }
    }

    //Cuando el enemigo deja de ser visible destrurlo
    private void OnBecameInvisible()
    {
        if (estaMuerto)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(EsperaParaDesactivar());
        }
    }

    IEnumerator EsperaParaDesactivar()
    {
        yield return new WaitForSeconds(5f);
        yaSeVio = false;
    }

    private void OnBecameVisible()
    {
        StopAllCoroutines();
        yaSeVio = true;
    }
}
