using UnityEngine;
using System.Collections;

public class bt_no_leave : MonoBehaviour {
	//Menus_Controller[] menusctrl;
	// Use this for initialization
	void Start () {
		//menusctrl = FindObjectsOfType(typeof(Menus_Controller)) as Menus_Controller[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		if (GLOBALS.Singleton.DISCONNECTED_MENU == false) {
			GLOBALS.Singleton.GAME_QUIT_MENU = false;
			GLOBALS.Singleton.LVL_UP_MENU = false;
			Destroy (transform.parent.gameObject);
		}
	}
	
	void OnMouseEnter() {
		if (GLOBALS.Singleton.DISCONNECTED_MENU == false) {
			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		}
	}
	
	void OnMouseExit() {
		
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		
	}
}
