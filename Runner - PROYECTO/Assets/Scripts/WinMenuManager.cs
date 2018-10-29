using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenuManager : MonoBehaviour {
    public Text highscoreText;
    public Text scoreText; //La utilizamos para traer el puntaje final
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false); //Desactivamos el menú (para que no aparezca cuando el Player está vivio)
    }


    /*
    FUNCIÓN QUE UTILIZAREMOS PARA ACTIVAR EL MENÚ CUANDO EL PLAYER GANA
    Y PASARLE EL PUNTAJE FINAL
    */

    public void ToggleWinMenu(float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        highscoreText.text = "Mayor puntaje: " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString();
    }

    /*
    FUNCIÓN QUE UTILIZAREMOS PARA RESETEAR EL VIDEOJUEGO
    */

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /*
    FUNCIÓN QUE UTILIZAREMOS PARA IR AL MENÚ PRINCIPAL DEL VIDEOJUEGO
    */

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); //Cargamos la Escena "Menu"
    }
}
