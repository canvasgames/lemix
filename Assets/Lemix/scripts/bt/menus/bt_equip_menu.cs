using UnityEngine;
using System.Collections;

public class bt_equip_menu : MonoBehaviour {
	public GameObject avatar;
	// Use this for initialization
	void Start () {
		GLOBALS.Singleton.LVL_UP_MENU = true;
		GLOBALS.Singleton.MY_LVL = 4;
		avatar.transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.MY_LVL);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp()
	{
		if (GLOBALS.Singleton.DISCONNECTED_MENU == false) {
			GLOBALS.Singleton.AVATAR_TYPE = GLOBALS.Singleton.MY_LVL;
			change_avatar_match_end_P1.acesss.changeAvatar ();

			GLOBALS.Singleton.LVL_UP_MENU = false;
			Destroy (transform.parent.gameObject);
		}
	}

	
	void OnMouseEnter() {
		if (GLOBALS.Singleton.DISCONNECTED_MENU == false) {
			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		
		}
	}
	
	void OnMouseExit() {
		
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		
	}
}
