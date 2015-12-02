using UnityEngine;
using System.Collections;

public class bt_equip_menu : BtsMenuClassCollider
{
	public GameObject avatar;
	// Use this for initialization
	void Start () {
        GLOBALS.Singleton.AVATAR_TYPE = GLOBALS.Singleton.MY_LVL;
        GLOBALS.Singleton.LVL_UP_MENU = true;
        avatar.transform.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("avatares/lvl_" + GLOBALS.Singleton.AVATAR_TYPE + "_avatar") as RuntimeAnimatorController;
        avatar.transform.GetComponent<Animator>().Play("normal");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    {
        base.ActBT();
		change_avatar_match_end_P1.acesss.changeAvatar ();
		GLOBALS.Singleton.LVL_UP_MENU = false;
		Destroy (transform.parent.gameObject);
	}

    public override void clicked()
    {
        base.clicked();
    }
}
