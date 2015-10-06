using UnityEngine;
using System.Collections;

public class bt_revenge : MonoBehaviour {
	
	mp_controller[] mp;
	// Use this for initialization
	void Start () {
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log ("ASK FOR REMATCH BUTTON PRESSED");
		mp[0].send_ask_for_rematch ();
	}
}
