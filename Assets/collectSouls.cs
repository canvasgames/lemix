using UnityEngine;
using System.Collections;

public class collectSouls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
        GLOBALS.s.TUTORIAL_OCCURING = false;
        BE.SceneTown.instance.CapacityCheck();
        BE.SceneTown.Elixir.ChangeDelta((double)200);
        Application.UnloadLevel("CATastrophe"); 
    }
}
