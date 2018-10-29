using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public int life;
    public int numHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Update is called once per frame
    void Update () {

        life = GetComponent<PlayerMotor>().life;

        if (life> numHearts)
        {
            life = numHearts;
        }


        for(int i = 0; i<hearts.Length; i++)
        {
            if (i < life)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numHearts)
            {
               hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
		      
	}

   
}
