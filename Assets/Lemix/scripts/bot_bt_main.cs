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
		print ("AHHHHHHH");
		Application.LoadLevel("Gameplay");

	}

	void OnMouseEnter() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
	}

	void OnMouseExit() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}

}
