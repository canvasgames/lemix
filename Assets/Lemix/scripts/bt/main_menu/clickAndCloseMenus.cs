using UnityEngine;
using System.Collections;

public class clickAndCloseMenus : BtsGuiClick
{
	public GameObject avatarMenu;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(transform.GetComponent<SpriteRenderer>().bounds.size.x);
	}

    public override void ActBT()
    {
		//Destroy (transform.parent.gameObject);
		avatarMenu.SetActive(false);
		GLOBALS.Singleton.MM_MENU_OPENED = false;
		
	}
}
