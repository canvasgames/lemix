﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class New_Game_QA : MonoBehaviour {
    int cont_click = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        hud_controller.si.HUD_BUTTON_CLICKED = true;
        cont_click++;
        if (cont_click >= 5)
        {
            cont_click = 0;
            USER.s.TOTAL_GAMES = 0;
             PlayerPrefs.SetInt("total_games", 0);
            transform.GetComponent<Image>().color = Color.blue;
            Invoke("back_to_white", 2);
        }
    }

    void back_to_white()
    {
        transform.GetComponent<Image>().color = Color.white;
    }
}
