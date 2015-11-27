using UnityEngine;
using System.Collections;

public class bt_pause : MonoBehaviour {
	public GameObject pauseMenu;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false )
		{
			Menus_Controller.acesss.quitGame();
			GLOBALS.Singleton.GAME_QUIT_MENU = true;
		}
	}
}
