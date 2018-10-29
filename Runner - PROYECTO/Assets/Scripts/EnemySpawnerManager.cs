using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour {

    public GameObject[] SpawnPoints;
    public GameObject enemyPrefab;
    int Index;

   
    private int numEnemies;

    private void OnTriggerEnter(Collider _player)
    {
        if(_player.gameObject.tag == "Player")
        {
            numEnemies = Random.Range(0,2);

                SpawnEnemy();
     
        }
    }

    private void SpawnEnemy()
    {
     
        for (int i = 0; i <= numEnemies; i++)
        {
            
            Index = Random.Range(0, SpawnPoints.Length);
           
            Instantiate(enemyPrefab, SpawnPoints[Index].transform.position, SpawnPoints[Index].transform.rotation);
            
        }
    }
    
}

