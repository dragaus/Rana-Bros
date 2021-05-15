using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public void SaveDataNow(string json) 
    {
        //En este vamos a traducir texto a lenguaje maquina
        BinaryFormatter formatter = new BinaryFormatter();
        //Aqui abrimos la direccion de nuestro archivo para poder escribir en el
        FileStream file = File.Create(Application.persistentDataPath + "/data.json");
        //Guaramos la informacion
        formatter.Serialize(file, json);
        //Cerramos el archivo para evitar errrores
        file.Close();
    }

    public DataGuardable LoadData()
    {
        //Reviusamos si existe nuestrp archivo
        if (File.Exists(Application.persistentDataPath + "/data.json")) {
            //Iniciomos el formater
            BinaryFormatter formater = new BinaryFormatter();
            //Abrir el archivo para poder leerlo
            FileStream file = File.Open(Application.persistentDataPath +
                "/data.json", FileMode.Open);
            //Obtemos el json
            var json = formater.Deserialize(file).ToString();
            //Para revsiar el retorno
            Debug.Log(json);
            //Estamos cerrando el archivo
            file.Close();

            //Estoy regresando la informacion
            return JsonUtility.FromJson<DataGuardable>(json);
        }

        //Si no existe elarchivo regreso un archivo en limpio
        return new DataGuardable();
    }
}

[Serializable]
public class DataGuardable 
{
    public string nombre;
    public int puntaje;
    public float tiempoJuego;
}
