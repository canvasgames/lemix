using UnityEngine;
using System.Collections;

public class bt_pause : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Time.timeScale = 0;
		Debug.Log ("GO TO LOBBY NOW !");
	}
}
