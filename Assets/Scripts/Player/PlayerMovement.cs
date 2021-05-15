using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables movimiento
    public float xSpeed;
    public float jumpForce;
    bool isLookingRight = true;
    bool estaVivo = true;
    int direccion = 0;

    //Variables brinco
    bool isJumping;
    bool isTouchingGround = true;
    Rigidbody2D rigi;
    CircleCollider2D circle;
    public LayerMask whatIsFloor;

    GameManager manager;
    public GameObject collected;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        manager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
    }

    public void ButtonJump() 
    {
        isJumping = true;
    }

    public void ApretarBoton(int valor) 
    {
        direccion = valor;
    }

    public void LiberarBoton() 
    {
        direccion = 0;
    }

    private void FixedUpdate()
    {
        isTouchingGround = Physics2D.IsTouchingLayers(circle, whatIsFloor) && rigi.velocity.y <= 0;

        if (isJumping && isTouchingGround) 
        {
            rigi.AddForce(Vector2.up * jumpForce);
            isJumping = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (estaVivo)
        {
#if (UNITY_ANDROID || UNITY_IOS)
            float horizontal = direccion;
#else
            float horizontal = Input.GetAxis("Horizontal");
#endif
            anim.SetBool("xMovement", horizontal != 0);
            anim.SetFloat("ySpeed", rigi.velocity.y);

            if (horizontal != 0)
            {
                transform.Translate(Vector2.right * horizontal * xSpeed * Time.deltaTime);

                if (horizontal > 0 && !isLookingRight)
                {
                    transform.localScale = Vector3.one;
                    isLookingRight = true;
                }
                else if (horizontal < 0 && isLookingRight)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    isLookingRight = false;
                }
            }

#if (UNITY_ANDROID || UNITY_IOS)

#else
            isJumping = Input.GetAxis("Jump") > 0 && isTouchingGround;
#endif
        }
    }

    public void Mori() 
    {
        circle.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetTrigger("meGolpearon");
        transform.localScale = new Vector3(1f, -1f, 1f);
        estaVivo = false;
    }

    public void MatePersonaje(int valorMuerte) {
        manager.AgregarPuntos(valorMuerte);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        switch (target.tag) 
        { 
            case "Finish":
                manager.TerminoNivel();
                break;
            case "Collector":
                if (estaVivo)
                {
                    manager.HaMuertoElPersonaje();
                    estaVivo = false;
                }
                break;
            case "Fruit":
                manager.AgregarPuntos(target.GetComponent<Fruit>().pointValue);
                Instantiate(collected, target.transform.position, Quaternion.identity);
                Destroy(target.gameObject);
                break;
        } 

    }
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma") && transform.position.y > collision.transform.position.y)
        { 
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            transform.SetParent(null);
        }
    }

    private void OnBecameInvisible()
    {
        if (!estaVivo) 
        {
            StartCoroutine(EsperarMuerte());
        }
    }

    IEnumerator EsperarMuerte()
    {
        yield return new WaitForSeconds(2f);
        manager.HaMuertoElPersonaje();
    }
}
