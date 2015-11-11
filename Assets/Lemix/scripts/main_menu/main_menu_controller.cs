using UnityEngine;
using System.Collections;

public class main_menu_controller : MonoBehaviour {
	public GameObject single;
	GameObject LvlP1;

	avatar_main_menu[] avatar;
	// Use this for initialization
	void Start () {
		GLOBALS[] single2 = FindObjectsOfType (typeof(GLOBALS)) as GLOBALS[];
		if (single2.Length == 0) {
			GameObject obj = (GameObject)Instantiate (single, new Vector3 (0, 0, 0), transform.rotation);
			GLOBALS final = obj.GetComponent<GLOBALS> ();
		}

		avatar = FindObjectsOfType(typeof(avatar_main_menu)) as avatar_main_menu[];

		avatar[0].changeAvatar(GLOBALS.Singleton.AVATAR_TYPE);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
