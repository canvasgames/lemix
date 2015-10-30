using UnityEngine;
using System.Collections;

public class bt_pause : MonoBehaviour {
	public GameObject pauseMenu;
	Menus_Controller[] menusctrl;
	// Use this for initialization
	void Start () {

		menusctrl = FindObjectsOfType(typeof(Menus_Controller)) as Menus_Controller[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false )
		{
			menusctrl[0].quitGame();
			GLOBALS.Singleton.GAME_QUIT_MENU = true;
		}
	}
}
