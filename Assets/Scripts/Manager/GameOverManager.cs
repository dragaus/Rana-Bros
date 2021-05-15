using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();


        var dataGuardada = FindObjectOfType<SaveData>().LoadData();
        Debug.Log($"puntos anteriores {dataGuardada.puntaje}");
        DataGuardable infoAGuaradar = new DataGuardable();
        infoAGuaradar.nombre = "pedro";
        infoAGuaradar.puntaje = 500;
        infoAGuaradar.tiempoJuego = 15.60f;
        string json = JsonUtility.ToJson(infoAGuaradar);
        Debug.Log($"Json es {json}");
        FindObjectOfType<SaveData>().SaveDataNow(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
