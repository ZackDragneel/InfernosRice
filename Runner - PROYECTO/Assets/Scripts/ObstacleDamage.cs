/*
 ESTE SCRIPT LO UTILIZAMOS PARA AÑADIRLO A LOS "ENEMIGOS". LO QUE NOS PERMITE ES DEFINIR UN DAÑO PARTICULAR A CADA UNO, Y EN BASE A LA
 COLISIÓN CON EL PLAYER, EJECUTA SU DAÑO (LE RESTA LA VIDA AL PLAYER) 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{

    public int damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator OnCollisionEnter(Collision _player)
    {
        if (_player.gameObject.tag == "Player") //Si el obstáculo choca con el Player, ejecuta la función de daño del jugador para restarle la vida en base a su daño
        {
            if(_player.gameObject.GetComponent<PlayerMotor>().invencible == true)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                yield return new WaitForSeconds(2);
            }
            else if (_player.gameObject.GetComponent<PlayerMotor>().life - damage > 0 && _player.gameObject.GetComponent<PlayerMotor>().invencible == false)
            {
                _player.gameObject.GetComponent<PlayerMotor>().Damage(damage);
                gameObject.GetComponent<Collider>().enabled = false;
                yield return new WaitForSeconds(2);
            }
            else if (_player.gameObject.GetComponent<PlayerMotor>().life - damage <= 0 && _player.gameObject.GetComponent<PlayerMotor>().invencible == false)
            {
                _player.gameObject.GetComponent<PlayerMotor>().Death();
            }
            gameObject.GetComponent<Collider>().enabled = true;

        }

    }

}

