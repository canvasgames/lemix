using UnityEngine;
using System.Collections;

public class Shuffle : MonoBehaviour {
	public int state;
	WController[] wordCTRL;
	// Use this for initialization
	void Start () {
		wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() 
	{
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
			wordCTRL[0].reorganize();
	}
}
