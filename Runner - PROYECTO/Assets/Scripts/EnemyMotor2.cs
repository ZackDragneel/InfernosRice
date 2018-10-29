using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotor2 : MonoBehaviour {

    private int speed = 100;
    public float scorebonus = 50f;
    public Animator animator;

    public Rigidbody fuego;
    public GameObject playerScore;
    public GameObject FuegoSpawner;

    private void Start()
    {
        playerScore = GameObject.Find("Player");
        animator.SetTrigger("Active");
        Invoke("FireFuego", 1f);
    }

    private void Update()
    {
        if(playerScore.transform.position.z> gameObject.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }

    void FireFuego()
    {
        Rigidbody rocketClone = (Rigidbody)Instantiate(fuego, FuegoSpawner.transform.position, FuegoSpawner.transform.rotation);
        rocketClone.velocity = -transform.forward * speed * Time.deltaTime;
        Destroy(rocketClone.gameObject, 1f);
    }

    private IEnumerator OnCollisionEnter(Collision _arroz)
    {
        if (_arroz.gameObject.tag == "Arroz")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            playerScore.GetComponent<Score>().GetScore(scorebonus);
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
       
    }
}
