using UnityEngine;
using System.Collections;

public class start_bt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {

        if (globals.s.GAME_STARTED == false)
        {
            globals.s.GAME_STARTED = true;
            hud_controller.si.start_game();
        }

    }
}
