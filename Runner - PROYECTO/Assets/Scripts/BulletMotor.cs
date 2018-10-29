using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletMotor : MonoBehaviour
{
    // Require the rocket to be a rigidbody.
    // This way we the user can not assign a prefab without rigidbody
    public Rigidbody arroz;
    public float speed = 10f;
    public int bullet = 2;
    public float reloadTime = 1f;
    public bool isReloading;


    // Calls the fire method when holding down ctrl or mouse
    void Update()
    {
       
        if (isReloading)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
     
            FireArroz();
        }
        if(bullet < 0)
        {
            StartCoroutine(recarga());
            return;
        }
    
    }

    void FireArroz()
    {
        Rigidbody rocketClone = (Rigidbody)Instantiate(arroz, transform.position, transform.rotation);
        rocketClone.velocity = transform.forward * speed;
        Destroy(rocketClone.gameObject, 1f);
        bullet--;
    }

   IEnumerator recarga()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        bullet = 2;
        isReloading = false;
    }
}