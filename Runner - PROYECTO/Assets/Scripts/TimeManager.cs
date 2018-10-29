/*
 ESTE SCRIPT LO UTILIZAMOS PARA HACER UN COUNTDOWN DEL TIEMPO (SI LLEGA A 0, PERDES).
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Añadimos para manejar UI

public class TimeManager : MonoBehaviour {

    public DeathMenuManager deathMenu;

    public int time = 50; // Variable que contendrá el tiempo
    public Text timeText; //Variable del texto que se visualizará en pantalla en el videojuego. La definimos como pública para poder arrastrar el objeto Text desde el Inspector
    

    // Use this for initialization
    void Start () {
        StartCoroutine("LoseTime");
    }
	
	// Update is called once per frame
	void Update () {

        timeText.text = ("Tiempo restante: " + time);

        //Si el tiempo termina (llega a 0), ejecuta la función de muerte del Player.
        if (time <= 0)
        {
            StopCoroutine("LoseTime");
            timeText.text = "¡Terminó el tiempo!";
            GetComponent<PlayerMotor>().Death();
        }

    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }

    //Se utiliza para aumentar el tiempo cuando se triggerea con un bonus de tiempo

    public void GetTime(int _time)
    {
        time += _time;
    }
}
