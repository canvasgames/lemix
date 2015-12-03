using UnityEngine;
using System.Collections;

public class bt_revenge : BtsMenuClassCollider
{


	int menuCreated =0;
	// Use this for initialization
	void Start () {
	
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
        base.ActBT();
		if (menuCreated == 0 && GLOBALS.Singleton.REMATCH_RECEIVED == 0)
		{
			menuCreated = 1;
		    Menus_Controller.acesss.waiting();
			Debug.Log ("ASK FOR REMATCH BUTTON PRESSED");
			if(GLOBALS.Singleton.MP_MODE == 1)
                mp_controller.access.send_ask_for_rematch ();
		}
	}

    public override void clicked()
    {
        if (GLOBALS.Singleton.LVL_UP_MENU == false)
        {
            base.clicked();
        }
    }
}
