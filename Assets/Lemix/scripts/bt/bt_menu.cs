using UnityEngine;
using System.Collections;
using DG.Tweening;

public class bt_menu : MonoBehaviour {

	GameController[] gc;
	// Use this for initialization
	void Start () {
		gc = FindObjectsOfType(typeof(GameController)) as GameController[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		if(GLOBALS.Singleton.LVL_UP_MENU == false && GLOBALS.Singleton.DISCONNECTED_MENU == false)
		{
			Debug.Log ("GO TO LOBBY NOW !");
			Time.timeScale = 1;
			gc [0].go_to_lobby ();
		}

	}

	
	void OnMouseEnter() {
		if(GLOBALS.Singleton.LVL_UP_MENU == false && GLOBALS.Singleton.DISCONNECTED_MENU == false)
		{
			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		}
		//this.transform.DOScale()
	}
	
	void OnMouseExit() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}

}
