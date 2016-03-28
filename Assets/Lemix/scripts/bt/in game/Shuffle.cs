using UnityEngine;
using System.Collections;

public class Shuffle : MonoBehaviour {
	public int state;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() 
	{
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
            WController.s.reorganize();
	}
}
