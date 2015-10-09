using UnityEngine;
using System.Collections;

public class Bt_restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		//var objects = GameObject.FindObjectsOfType(GameObject);
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) 
			Destroy(o);
		Time.timeScale = 1;
		Application.LoadLevel ("Gameplay");

	}
}
