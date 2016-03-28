using UnityEngine;
using System.Collections;

public class bt_backspace : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() 
	{
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
            Submit_And_Input_Ctrl.s.inputBackspaceCase();
	}
}
