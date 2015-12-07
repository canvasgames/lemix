using UnityEngine;
using System.Collections;

public class bt_music : BtsGuiClick
{

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override void ActBT()
    {
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
            //
            base.ActBT();
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bt_music"))
				GetComponent<Animator>().Play("bt_music_off");
			else
				GetComponent<Animator>().Play("bt_music");
		}

	}

}
