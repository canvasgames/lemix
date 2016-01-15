using UnityEngine;
using System.Collections;

public class catastropheBT : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
        if (GLOBALS.s.TUTORIAL_OCCURING == false)
        {
            Application.LoadLevelAdditive("CATastrophe");
        }
    }
}
