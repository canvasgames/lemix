using UnityEngine;
using System.Collections;

public class yes_are_you_sure : MonoBehaviour {
	mp_controller[] mp;
	// Use this for initialization
	void Start () {
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		mp [0].send_accept_rematch ();
	}
}
