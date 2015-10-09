using UnityEngine;
using System.Collections;

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
		Debug.Log ("GO TO LOBBY NOW !");
		Time.timeScale = 1;
		gc [0].go_to_lobby ();

	}
}
