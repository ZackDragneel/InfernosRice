/*
    ESTE SCRIPT LO UTILIZAMOS PARA MANEJAR EL PUNTAJE EN EL VIDEOJUEGO
    Y LA DIFICULTAD
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Agregamos esto para manejar las propiedades UI (Canvas, Text, Image, etc).

public class Score : MonoBehaviour {

    public GameObject enemy;

    public float score = 0.0f; //Variable que contendrá el puntaje

    private int difficultyLevel = 1; //Nivel de dificultad actual
    private int maxDifficultyLevel = 10; //Nivel máximo de dificultad
    private int scoreToNextLevel = 10; //Puntaje necesario para pasar al siguiente lvl de dificultad

    private bool isDead = false;

    public Text scoreText; //Variable del texto que se visualizará en pantalla en el videojuego. La definimos como pública para poder arrastrar el objeto Text desde el Inspector
    public Text lifeText;
    public GameObject ScoreBonus;

    public DeathMenuManager deathMenu;
 
    // Use this for initialization
    void Start () {
        ScoreBonus.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (isDead)
            return;

        if (score >= scoreToNextLevel)
            LevelUp();

        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString(); //Convertimos la variable Score en String (texto - cadena de caracteres)

        /*life = GetComponent<PlayerMotor>().life;
        lifeText.text = "Vidas: " + life.ToString();*/
       
    }

    /*FUNCIÓN QUE UTILIZAMOS PARA "SUBIR EL NIVEL"*/
    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel) //Si llega al nivel máximo, retornamos esta función (se complejiza más)
            return;
        scoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel); //Traemos el script PlayerMotor para utilizar la función pública SetSpeed.
        enemy.GetComponent<EnemyMotor>().SetSpeed(difficultyLevel);
    }

    /*
    FUNCIÓN PÚBLICA QUE UTILIZAREMOS EN EL PLAYERMOTOR.CS PARA DEJAR DE QUE EL
    PUNTAJE SE SIGA AUMENTANDO CUANDO EL PLAYER MURIÓ
    */

    public void OnDeath ()
    {
        isDead = true;
        /*
        ACLARACIÓN: PARA QUE ESTO FUNCIONE TIENEN QUE COMPILAR EL JUEGO, EJECUTARLO Y JUNTAR ALGO DE PUNTAJE.
        LUEGO DE ESO, SALEN Y APRETAN LA TECLA WIN+R (ESCRIBEN REGEDIT) Y BUSCAN:
        EN HKEY_CURRENT_USER > SOFTWARE > TDMM1 > BaseRunner (EN ESTE CASO SE LLAMA ASÍ)
        */
        if(PlayerPrefs.GetFloat("Highscore") < score) //Si el puntaje actual es mayor que el guardado, guardamos el nuevo puntaje
            PlayerPrefs.SetFloat("Highscore", score);
        deathMenu.ToggleEndMenu(score);
    }


    //Se utiliza para aumentar el score cuando se triggerea con un bonus de score
    public void GetScore (float _score)
    {
        //Debug.Log(_score);
        ScoreBonus.SetActive(true);
        ScoreBonus.GetComponent<Text>().text = "+" + _score.ToString();
        score += _score;
        Invoke("Reset", 1f);
       
    }

    public void Reset()
    {
        ScoreBonus.SetActive(false);
    }
}
