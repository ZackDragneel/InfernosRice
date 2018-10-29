using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour {


    //Creamos una variable para guardar el valor del Tiempo restante.
    float distRestante;
    //Creamos una variable para guardar el valor del Tiempo maximo.
    int distMaximo;
    //Creamos una variable para llamar a la aguja
    public GameObject aguja;

    public GameObject goal;
    //Posicion x de la aguja
    float yAguja;

    void Start()
    {
        distMaximo = ((int)goal.transform.position.z);
       
    }

    void Update()
    {
        distRestante = ((int)distMaximo - (gameObject.transform.position.z));

        //Este debug es para comprabar que estemos levantando bien el tiempo restante, comentenlo despues
        Debug.Log("hola soy dist " + distRestante);
        Debug.Log("hola soy distMaximo " + distMaximo);

        //Cambiamos la posición de la aguja dependiendo del tiempo restante
        yAguja = Remap(distRestante, 0, distMaximo, 150, -150);
        aguja.transform.localPosition = new Vector3(yAguja, 0,0);
    }

    //Esta función tiene la misma lógica que el map en processing. Le pasamos el tiempoRestante como referencia, su valor mínimo y máximo, y la posición de la aguja, máxima y mínima. Estas ultimas dependen del "camino"
    //Así cuando esta en el tiempo mínimo, la aguja aparece al principio del camino y cuando llega al máximo de tiempo esta en el final.
    float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

}
