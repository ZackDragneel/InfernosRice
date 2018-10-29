using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : MonoBehaviour {
    public WinMenuManager winMenu;

    public Score score;
    public PlayerMotor player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            winMenu.ToggleWinMenu(score.score);
            player.hasWon = true;
        }
    }
}
