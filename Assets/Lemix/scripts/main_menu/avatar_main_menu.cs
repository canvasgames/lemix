using UnityEngine;
using System.Collections;

public class avatar_main_menu : MonoBehaviour {
	public GameObject avatarMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown()
	{
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			GameObject menu = (GameObject)Instantiate (avatarMenu, new Vector3 (0,0 , 100), transform.rotation);
			GLOBALS.Singleton.MM_MENU_OPENED = true;
		}
	}

	public void changeAvatar(int type)
	{
		transform.GetComponent<Animator>().Play("lvl_"+type);
	}
}
