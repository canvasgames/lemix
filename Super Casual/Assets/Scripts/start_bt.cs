using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class start_bt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
			GetComponent<Button>().interactable = false;
		if (1==2 && Input.GetMouseButtonDown(0) && globals.s.GAME_STARTED == false && globals.s.MENU_OPEN == false)
			{
				globals.s.GAME_STARTED = true;
				hud_controller.si.start_game();
			}

		#endif

		#if UNITY_WEBPLAYER || UNITY_IPHONE || UNITY_ANDROID

			GetComponent<Button>().interactable = true;
		#endif



	}

    public void click()
    {

        if (globals.s.GAME_STARTED == false && globals.s.MENU_OPEN == false)
        {
            globals.s.GAME_STARTED = true;
			hud_controller.si.start_game_coroutine ();
        }

    }
}
