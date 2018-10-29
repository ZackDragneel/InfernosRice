	/*
	LA FUNCIONALIDAD DE ESTE SCRIPT ES DARLE PROPIEDADES A LA CÁMARA PARA QUE SIGA AL PLAYER
	*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {


	//Esta variable de tipo Transform la utilizamos para traer el Vector3 (X,Y,Z) del Player
	private Transform lookAt;
	//Esta variable de tipo Vector3 la utilizamos para mantener SIEMPRE la posición incial de la cámara en sus ejes (X,Y,Z)
	private Vector3 initialOffset;
	// Use this for initialization

	private Vector3 moveVector;
	void Start () {
		//Utilizamos el tag "Player" para que el Script sepa a qué transform nos referimos (en este caso, el transform del Player)
		lookAt = GameObject.FindGameObjectWithTag ("Player").transform;
		//Definimos que la posición inicial de la cámara va a ser su posición actual restandole la posición del Player
		initialOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Por cada frame, chequeamos la posición del Player y le sumamos la posición inicial de la cámara (para que SIEMPRE se mantenga la distancia)
		moveVector = lookAt.position + initialOffset;

		// Definimos que la cámara SIEMPRE va a estar en la posición 0 de X, para que no se mueva con el personaje y siempre quede centrada.
		moveVector.x = 0;

        //Luego lo asignamos al transform de la Cámara
        transform.position = moveVector;
	}
}
