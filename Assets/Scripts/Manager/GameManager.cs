using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int points = 0;
    public static int vidas = 3;

    [Header("UI elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI textoVidas;

    public GameObject botonBrinco;
    public GameObject botonIzquierda;
    public GameObject botonDerecha;

    // Start is called before the first frame update
    void Start()
    {
        AgregarPuntos(0);
        textoVidas.text = vidas.ToString();
#if (UNITY_STANDALONE || UNITY_WEBGL)
        botonBrinco.SetActive(false);
        botonIzquierda.SetActive(false);
        botonDerecha.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AgregarPuntos(int puntos)
    {
        points += puntos;
        scoreText.text = points.ToString("000000");
    }

    public void HaMuertoElPersonaje() 
    {
        Debug.Log("me llaman");
        vidas--;
        textoVidas.text = vidas.ToString();
        if (vidas <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else 
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TerminoNivel()
    {
        int indexActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexActual + 1);
    }
}
