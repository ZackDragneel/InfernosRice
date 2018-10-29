using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{

    public BulletMotor bulletmotor;
    public Animator animator;
    

    private int ammo = 2;

    public Sprite fullTazon;
    public Sprite medTazon;
    public Sprite minTazon;
    public Sprite emptyTazon;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = fullTazon;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (bulletmotor.bullet == ammo)
        {
            animator.SetInteger("ammo",3);
        }
        else if (bulletmotor.bullet == ammo - 1)
        {
            animator.SetInteger("ammo", 2);
        }
        else if (bulletmotor.bullet == ammo - 2)
        {
            animator.SetInteger("ammo", 1);
        }
        else
        {
            animator.SetInteger("ammo", 0);
        }


        if (bulletmotor.isReloading) 
        {
            {
                animator.SetBool("isreloading", true);
 
            }

        }

    }
}
