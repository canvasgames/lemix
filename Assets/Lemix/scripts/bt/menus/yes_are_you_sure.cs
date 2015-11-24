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
		if(GLOBALS.Singleton.LVL_UP_MENU == false && GLOBALS.Singleton.DISCONNECTED_MENU == false)
		{
			mp [0].send_accept_rematch ();
			Destroy(transform.parent.gameObject);
		}
	}

	void OnMouseEnter() {
		if(GLOBALS.Singleton.LVL_UP_MENU == false && GLOBALS.Singleton.DISCONNECTED_MENU == false)
		{
			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		}
		
	}
	
	void OnMouseExit() {
		
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		
	}
}
