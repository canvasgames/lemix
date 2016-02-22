using UnityEngine;
using System.Collections;

public class activate_pw_button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
        hud_controller.si.HUD_BUTTON_CLICKED = true;
    }
}
