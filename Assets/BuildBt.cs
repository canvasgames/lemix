﻿using UnityEngine;
using System.Collections;

public class BuildBt : ButtonCap {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    {
        if(GLOBALS.s.TUTORIAL_PHASE == 6)
        {
            TutorialController.s.clickedBuildBt();
        }
    }
}
