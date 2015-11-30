using UnityEngine;
using System.Collections;

public class menu_op_disconnected : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GLOBALS.Singleton.DISCONNECTED_MENU = true;
		Destroy(gameObject, 4f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
		GLOBALS.Singleton.DISCONNECTED_MENU = false;
	}
}
