	/*
		Este script lo utilizamos para generar "Tiles" (Los pisos en este caso) de manera automática en base a la distancia del Player.
		Esto es necesario ya que si le damos cierto dinamismo al script, podemos generar varios Prefabs de pisos, con distintos
		comportamientos que aumenten la dificultad y le den mayor "randomización" al videojuego. Además, no tenemos que diseñar de manera
		estática el nivel, sino que la máquina va generando los diferentes pisos que creen, y eso genera que cada vez que se inicie el
		juego, el nivel sea en algunos aspectos distinto.
	*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour {

	//Arreglo que contiene los Prefabs de los tiles que vamos a utilizar. Al ser "public", podemos elejir la dimensión del arreglo desde el Inspector
	public GameObject[]tilesPrefabs1;
    public GameObject[] tilesPrefabs2;

    private int levelIndex = 1;
    public CanvasGroup uiElement;
    private bool fading = true;

    //Variable que utilizaremos para guardar la posición actual del Player
    private Transform playerPosition;
	/*Variable que utilizaremos para indicar en qué posición del eje Z spawnearan los Tiles. (Definimos solamente el eje Z ya que en este script
	no contemplamos que los pisos se puedan mover en el eje X e Y)*/
	private float spawnZ = -5.0f;

	/*Variable que utilizaremos para indicar el largo del Tile en relación al eje Z. Para saber esto, simplemente desde la escena creamos 2 Tiles
	(uno inicial y el otro lo movemos en el eje Z hasta que veamos que se une con el anterior). Luego de eso, vemos en el Inspector qué valor de
	Z tiene en el Transform, y ese es el valor que vamos a utilizar. En este caso, los tiles son de aproximadamente 10*/
	private float tileLength = 10.0f;

	/*Variable que utilizamos para indicar la cantidad de Tiles en simultáneo que se generaran. Cuidado con esto ya que si hacemos que genere
	muchos a la vez, dependiendo de la máquina puede empeorar el rendimiento. El parámetro a seguir es que desde la cámara del Player no se note
	que los Tiles se van generando a medida que avanzamos, ya que perdemos inmersión*/

	private int amountOfTilesOnScreen = 8;

    private List<GameObject> activeTiles; //Utilizamos esta lista para guardar los Tiles que están en uso (y así borrar los que no y mejorar el rendimiento)

    private float safeZone = 10.0f; //Utilizamos esta variable para que se remuevan los Tiles con "delay", así evitamos remover tiles que puede necesitar pisar el Player.

    private int lastPrefabIndex = 0; // La usamos para que no spawneen de manera consecutiva los Tiles random

	// Use this for initialization
	private void Start () {

        activeTiles = new List<GameObject>(); //Inicializamos la lista.

		//Referencia a la posición actual del Player mediante su Tag
		playerPosition = GameObject.FindGameObjectWithTag("Player").transform;

		for (int i=0; i < amountOfTilesOnScreen; i++) {
            if (i < 2) //Creamos este If para no tener el problema de que el random genere los primeros dos Tiles con obstáculos.
                SpawnTile(0); //El valor 0 corresponde al Tile1 (que es plano, sin obstáculos).
            else //Si no, spawnee random.
			    SpawnTile();
		}
        
	}
	
	// Update is called once per frame
	private void Update () {

		if (playerPosition.position.z - safeZone  > (spawnZ - amountOfTilesOnScreen * tileLength)) {

			SpawnTile ();
            DeleteTile();
        }
        
        
        if (playerPosition.transform.position.z > 500)
        {
            levelIndex = 2;
            
            FadeIn();
        }
        
    }
    /*
     FUNCIÓN QUE UTILIZAMOS PARA GENERAR LOS TILES
    */
    private void SpawnTile (int prefabIndex = -1) {

        if (levelIndex == 1)
        {
            GameObject going;
            if (prefabIndex == -1)
                going = Instantiate(tilesPrefabs1[RandomPrefabIndex()]) as GameObject;
            else
                going = Instantiate(tilesPrefabs1[prefabIndex]) as GameObject;

            going.transform.SetParent(transform);
            going.transform.position = Vector3.forward * spawnZ;
            spawnZ += tileLength;
            activeTiles.Add(going);
        }
        else if(levelIndex == 2)
        {
            GameObject going;
            if (prefabIndex == -1)
                going = Instantiate(tilesPrefabs2[RandomPrefabIndex()]) as GameObject;
            else
                going = Instantiate(tilesPrefabs2[prefabIndex]) as GameObject;

            going.transform.SetParent(transform);
            going.transform.position = Vector3.forward * spawnZ;
            spawnZ += tileLength;
            activeTiles.Add(going);
        }
	}

    /*
     FUNCIÓN QUE UTILIZAMOS PARA RANDOMIZAR LA GENERACIÓN DE LOS TILES
    */

    private int RandomPrefabIndex()
    {
        if (tilesPrefabs1.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilesPrefabs1.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }

    /*
     FUNCIÓN QUE UTILIZAMOS PARA ELIMINAR LOS TILES
    */
    private void DeleteTile()
    {
        Destroy(activeTiles [0]);
        activeTiles.RemoveAt (0);
    }

    public void FadeIn()
    {
        if (fading)
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, 1.0f));
        }
        else
        {
            StopCoroutine("FadeCanvasGroup");
        }
        
    }

    public void FadeOut()
    {
        if (fading)
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, 1.0f));
        }
        else
        {
            StopCoroutine("FadeCanvasGroup");
        }
    }


    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForSeconds(2.0f);

            FadeOut();

            fading = false;
            
        }

    }
}
