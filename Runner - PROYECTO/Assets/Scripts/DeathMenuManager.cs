/*
    ESTE SCRIPT LO UTILIZAMOS PARA MANEJAR EL MENÚ CUANDO MUERE EL PLAYER
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Añadimos para manejar UI
using UnityEngine.SceneManagement; //Añadimos para utilizar el cambio de Escenas

public class DeathMenuManager : MonoBehaviour {

    public Text scoreText; //La utilizamos para traer el puntaje final

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false); //Desactivamos el menú (para que no aparezca cuando el Player está vivio)
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    FUNCIÓN QUE UTILIZAREMOS PARA ACTIVAR EL MENÚ CUANDO EL PLAYER MUERE
    Y PASARLE EL PUNTAJE FINAL
    */

    public void ToggleEndMenu (float score)
    {
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
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
