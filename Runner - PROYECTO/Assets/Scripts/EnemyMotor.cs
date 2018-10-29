using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotor : MonoBehaviour {

    public float speed;
    private const float baseSpeed = -5.0f;

    private Vector3 initialPos;

    public float scorebonus = 50f;

    public GameObject playerScore;


	// Use this for initialization
	void Start () {
        playerScore = GameObject.Find("Player");
        speed = baseSpeed;
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.position += transform.forward * speed * Time.deltaTime;
        if (playerScore.transform.position.z > initialPos.z)
        {
            Destroy(gameObject);
        }
       
    }

    public void SetSpeed(float modifier)
    {
        speed = baseSpeed - modifier;  
    }

    private IEnumerator OnCollisionEnter(Collision _arroz)
    {
        if (_arroz.gameObject.tag == "Arroz")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            playerScore.GetComponent<Score>().GetScore(scorebonus);
            speed = 0;
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
        else if(_arroz.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

}
