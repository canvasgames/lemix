using UnityEngine;
using System.Collections;

public class bt_no_leave : BtsMenuClassCollider
{
	//Menus_Controller[] menusctrl;
	// Use this for initialization
	void Start () {
		//menusctrl = FindObjectsOfType(typeof(Menus_Controller)) as Menus_Controller[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    {
        base.ActBT();
		GLOBALS.Singleton.GAME_QUIT_MENU = false;
		GLOBALS.Singleton.LVL_UP_MENU = false;
		Destroy (transform.parent.gameObject);
	}

    public override void clicked()
    {
        base.clicked();
    }
}
