using UnityEngine;
using System.Collections;

public class bot_bt_main : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	public void pressed()
	{
		//SAFFER.Singleton.Reset_Globals ();
		Application.LoadLevel("Gameplay");
	}

	public void OnMouseDown(){
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			print ("AHHHHHHH");
			GLOBALS.Singleton.CONNECTED = 0;
			GLOBALS.Singleton.MP_PLAYER = 0;
			GLOBALS.Singleton.OP_PLAYER = 0;
			GLOBALS.Singleton.MP_MODE = 0;
			Application.LoadLevel("Gameplay");
		}

	}

	void OnMouseOver() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
	}

	void OnMouseExit() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}

}
