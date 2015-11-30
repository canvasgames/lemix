using UnityEngine;
using System.Collections;

public class bt_revenge : BtsMenuClassCollider
{
	mp_controller[] mp;

	int menuCreated =0;
	// Use this for initialization
	void Start () {
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
	
		if(GLOBALS.Singleton.CONNECTED == 0 && GLOBALS.Singleton.MP_MODE == 1)
            base.DeactivateBt();

    }
	
	// Update is called once per frame
	void Update () {
        if (GLOBALS.Singleton.CONNECTED == 0 && GLOBALS.Singleton.MP_MODE == 1)
            base.DeactivateBt();
    }

    public override void ActBT()
    { 
		if(GLOBALS.Singleton.LVL_UP_MENU == false)
		{
		    if (menuCreated == 0)
		    {
			    menuCreated = 1;
		      	Menus_Controller.acesss.waiting();
			    Debug.Log ("ASK FOR REMATCH BUTTON PRESSED");
			    if(GLOBALS.Singleton.MP_MODE == 1)
				    mp[0].send_ask_for_rematch ();
            }
		}
	}

}
