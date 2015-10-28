using UnityEngine;
using System.Collections;

public class bt_revenge : MonoBehaviour {
	mp_controller[] mp;
	Menus_Controller[] menusctrl;
	int menuCreated =0;
	int deactivate = 0;
	// Use this for initialization
	void Start () {
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
		menusctrl = FindObjectsOfType(typeof(Menus_Controller)) as Menus_Controller[];

	}
	
	// Update is called once per frame
	void Update () {
		if(GLOBALS.Singleton.CONNECTED == 0)
			deactivate_bt();
	}

	void OnMouseDown(){
		//var objects = GameObject.FindObjectsOfType(GameObject);
		if (menuCreated == 0 && deactivate == 0)
		{
			menuCreated = 1;
			menusctrl[0].waiting();
			Debug.Log ("ASK FOR REMATCH BUTTON PRESSED");
			if(GLOBALS.Singleton.MP_MODE == 1)
				mp[0].send_ask_for_rematch ();

		}

		/*
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) 
			Destroy(o);
		Time.timeScale = 1;
		Application.LoadLevel ("Gameplay");*/


		
	}

	public void deactivate_bt()
	{
		deactivate = 1;
		this.transform.GetComponent<SpriteRenderer> ().color = Color.gray;
	}
	void OnMouseEnter() {
		if(deactivate == 0)
			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		//this.transform.DOScale()
	}

	void OnMouseExit() {
		if(deactivate == 0)
			this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
