using UnityEngine;
using System.Collections;

public class clickAndCloseMenus : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(transform.GetComponent<SpriteRenderer>().bounds.size.x);
	}
	
	void OnMouseDown(){
		Destroy (transform.parent.gameObject);
		GLOBALS.Singleton.MM_MENU_OPENED = false;
		
	}
}
