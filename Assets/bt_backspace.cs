using UnityEngine;
using System.Collections;

public class bt_backspace : MonoBehaviour {

	Submit_And_Input_Ctrl[] wordCTRL;
	// Use this for initialization
	void Start () {
		wordCTRL = FindObjectsOfType(typeof(Submit_And_Input_Ctrl)) as Submit_And_Input_Ctrl[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() 
	{
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
			wordCTRL[0].inputBackspaceCase();
	}
}
