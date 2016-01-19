using UnityEngine;
using System.Collections;

public class catastropheBT : MonoBehaviour {

    int opened = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
        if (GLOBALS.s.TUTORIAL_OCCURING == false && GLOBALS.s.DIALOG_ALREADY_OPENED == false)
        {
            Application.LoadLevelAdditive("CATastrophe");

       
        }
    }
}
