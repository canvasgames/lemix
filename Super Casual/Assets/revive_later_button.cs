using UnityEngine;
using System.Collections;

public class revive_later_button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        globals.s.SHOW_VIDEO_AFTER = true;
        globals.s.CAN_RESTART = false;
        hud_controller.si.close_revive_menu();
        game_controller.s.revive_logic();
        hud_controller.si.hide_game_over();
    }
}
