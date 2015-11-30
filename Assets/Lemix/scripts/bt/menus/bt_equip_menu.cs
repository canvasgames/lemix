using UnityEngine;
using System.Collections;

public class bt_equip_menu : BtsMenuClassCollider
{
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

    public override void ActBT()
    {
        GLOBALS.Singleton.AVATAR_TYPE = GLOBALS.Singleton.MY_LVL;
		change_avatar_match_end_P1.acesss.changeAvatar ();

		GLOBALS.Singleton.LVL_UP_MENU = false;
		Destroy (transform.parent.gameObject);
	}
}
