	/*
	LA FUNCIONALIDAD DE ESTE SCRIPT ES EL COMPORTAMIENTO DEL JUGADOR (PLAYER) Ya sea movimiento, su gravedad, velocidad, etc.
	*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {
    //Animador
    public Animator animator;

    //Variables de carril
    public int lane = 1;
    public const float laneWidth = 2.0f;
    private bool derecha;

    public bool hasWon = false;
	//Componente que se utiliza para controlar el comportamiento del Player
	private CharacterController controller;

    public bool invencible = false;

	private Vector3 movePlayer;

	//Variable que utilizamos para controlar la velocidad del Player
	[SerializeField] //Esto sirve para poder ver desde el Inspector la variable (Aunque sea privada)

    private const float baseSpeed = 3.0f; //Definimos una constante ya que es una buena práctica de programación. En este caso sirve para setear la velocidad en la función SetSpeed()
    private float speed; //Velocidad del Player
    

	[SerializeField]
	private float verticalVelocity = 0.0f;

    [SerializeField]
    private float jumpForce = 7.0f;

	[SerializeField]
	private float gravity = 12.0f;

    private bool isDead = false; //Variable booleana que utilizamos para saber si "murió" o no el Player
    //private const float radiusCollision = 1.0f;

    private bool jumping;

    public int maxLife = 3; //Máxima vida del Player
    public int life = 3; // Vida actual del Player
    

    // Use this for initialization
    void Start() {
        
        controller = GetComponent<CharacterController>();
        speed = baseSpeed; //Asignamos la velocidad base definida a la velocidad del Player.
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if (isDead) //Si está muerto, no dejamos que ejecute el resto (El player no se puede mover)
            return;
        if (hasWon)
            return;
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cambioCarril(true);
            animator.SetTrigger("MovesRight");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cambioCarril(false);
            animator.SetTrigger("MovesLeft");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {

            jumping = true;
        } 
    }

    void FixedUpdate()
    {
        //Reseteamos los valores de movimiento del Player
        movePlayer = Vector3.zero;

        Vector3 posCarril = transform.position.z * Vector3.forward;
        if(lane == 0)
        {
            posCarril += Vector3.left * laneWidth;
        }
        else if(lane == 2)
        {
            posCarril += Vector3.right * laneWidth;
        }

            movePlayer.x = (posCarril - transform.position).normalized.x * speed;
        // Controlamos el eje X (Izquierda y Derecha)
       // movePlayer.x = Input.GetAxisRaw("Horizontal") * speed;

        //Comprobamos si el Player está tocando el suelo.
        if (controller.isGrounded)
        {
            //Controlamos el eje Y (El salto)
            Jump();
        }
        else
        {
            //Si no está tocando el piso, se le aplica la caida en base a la gravedad
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //Controlamos el eje Z (Adelante y atrás), en este caso el personaje avanza sólo (no lo controlamos)
        movePlayer.z += speed;

        //Una vez que se calcula todo, se mueve el personaje en los ejes: (X,Y,Z)
        Vector3 moveVector = new Vector3(movePlayer.x, verticalVelocity, movePlayer.z);
        controller.Move(moveVector * Time.deltaTime);
       

    }

    /*
     ESTA FUNCIÓN LA UTILIZAMOS PARA INCREMENTAR LA VELOCIDAD EN BASE A UN VALOR FLOTANTE MODIFICADOR.
     ESTA FUNCIÓN NOS SIRVE PARA EL SCRIPT Score.cs (Cuanto mayor es la dificultad, más velocidad tiene el personaje)
     ES PÚBLICA PARA QUE PUEDA SER ACCEDIDA POR OTROS SCRIPTS.
    */
    public void SetSpeed (float modifier)
    {
        speed = baseSpeed + modifier;
    }

    /*
     ESTA FUNCIÓN LA UTILIZAMOS PARA APLICAR EL DAÑO AL PLAYER
    */
    public void Damage(int _damage)
    {

        if (life > 1 && invencible == false)
        {
            life -= _damage;
            invencible = true;
            animator.SetBool("Damage", true);
            Invoke("reset", 2);
        }
        else if (life <= 1 && invencible == false)
        {
            Death();
        }
     
    }



    public void GetLife(int _life)
    {
        if (life < maxLife)
        {
            life += _life;
        }
        
    }

    /*
     ESTA FUNCIÓN LA UTILIZAMOS PARA MATAR AL PLAYER SI SU VIDA LLEGA A 0
    */
    public void Death()
    {
       isDead = true;
       GetComponent<Score>().OnDeath(); //Cortamos el aumento de puntaje
      
    }
    /*
      ESTA FUNCIÓN LA UTILIZAMOS PARA QUE EL PLAYER SALTE CON LA BARRA ESPACIADORA
     */
    private void Jump()
    {
        verticalVelocity -= gravity * Time.deltaTime;

        if (jumping)
        {
            verticalVelocity = jumpForce;
            jumping = false;
        }
        
    }

    private void cambioCarril(bool derecha)
    {
        if (!derecha)
        {
            
            lane--;
            
            if (lane == -1)
            {
                lane = 0;
            }
        }
        else
        {
           
            lane++;
            
            if (lane == 3)
            {
                lane = 2;
            }
        }
    }

   public void reset()
    {
        invencible = false;
        animator.SetBool("Damage", false);
        Debug.Log(invencible);
    }
}

