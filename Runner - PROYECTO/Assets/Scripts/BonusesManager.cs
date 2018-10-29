/*
 ESTE SCRIPT LO UTILIZAMOS PARA AÑADIRLO A LOS "BONUS". LO QUE NOS PERMITE ES DEFINIR EL BONUS PARTICULAR A CADA UNO, Y EN BASE AL
 TRIGGEREO CON EL PLAYER, EJECUTA SU BONUS (LE DA MAS TIEMPO, MAS PUNTAJE O MAS VIDA).
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusesManager : MonoBehaviour {

    /*
     LAS VARIABLES BOOLEANAS SIRVEN PARA DETERMINAR QUE TIPO DE BONUS ES. DESDE EL INSPECTOR MARCAMOS (SI ES TRUE, DA BONUS. SI ES FALSE, NO)
    */
    //public bool isTimeBonus;
    //public int timeBonus = 5;

    public bool isScoreBonus;
    public float scoreBonus = 5;

    public bool isLifeBonus;
    public int lifeBonus = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnTriggerEnter (Collider _player)
    {
        if (_player.gameObject.tag == "Player") //Si el bonus triggerea con el Player, ejecuta la función de bonus dependiendo del tipo.
        {
            /*if (isTimeBonus)
            _player.gameObject.GetComponent<TimeManager>().GetTime(timeBonus); //Se le pasa a la función del TimeManager el aumento de tiempo del bonus*/

            if (isLifeBonus)
                _player.gameObject.GetComponent<PlayerMotor>().GetLife(lifeBonus); //Se le pasa a la función del Player el aumento de vida del bonus

            if (isScoreBonus)
                _player.gameObject.GetComponent<Score>().GetScore(scoreBonus); //Se le pasa a la función del Score el aumento de puntaje del bonus

            gameObject.SetActive(false); //Desactivamos el bonus cuando ya lo agarramos
        }

    }
}
