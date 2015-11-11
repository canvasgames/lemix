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

	void OnMouseDown(){
		GLOBALS.Singleton.GAME_QUIT_MENU = false;
		Destroy(transform.parent.gameObject);
	}
	
	void OnMouseEnter() {
		
		this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		
	}
	
	void OnMouseExit() {
		
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		
	}
}
