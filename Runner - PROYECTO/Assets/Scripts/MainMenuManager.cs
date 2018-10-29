/*
    ESTE SCRIPT LO UTILIZAMOS PARA MANEJAR EL MENÚ PRINCIPAL DEL VIDEOJUEGO Y GUARDAR EL PUNTAJE MÁS ALTO
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Añadimos para utilizar el cambio de Escenas
using UnityEngine.UI; //Añadimos para manejar UI

public class MainMenuManager : MonoBehaviour
{
    public Text highscoreText; //Variable de texto que utilizamos para referenciar al Highscore
    // Use this for initialization
    void Start()
    {
        highscoreText.text = "Mayor puntaje: " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoPlay()
    {
        SceneManager.LoadScene("Level1"); //Cargamos la Escena "Level1"
    }
}
