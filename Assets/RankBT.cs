﻿using UnityEngine;
using System.Collections;

public class RankBT : MonoBehaviour {
    GameObject tempObject;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
         if(GLOBALS.s.TUTORIAL_OCCURING == false)
        {
            tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DemonList/DemonList"));
            MenusController.s.enterFromLeft(tempObject, "DemonList", 0, 0);

        }

        
    }
}
