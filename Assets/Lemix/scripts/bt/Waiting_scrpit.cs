using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Waiting_scrpit : MonoBehaviour {
	Text instruction;
	float timeTrigger,bot_time, destruct_menu_time, reset_room_time;
	int waiting = 1, bot_mode;

	mp_controller[] mp;
	bt_revenge[] revMenu;

	// Use this for initialization
	void Start () {
		instruction = GetComponent<Text>();
		instruction.text = "Waiting response";
	
		revMenu = FindObjectsOfType(typeof(bt_revenge)) as bt_revenge[];
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];

		if(GLOBALS.Singleton.MP_MODE == 0)
		{
			bot_mode = 1;
			bot_time = 3f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(waiting == 1)
		{
			timeTrigger-= Time.unscaledDeltaTime ;
			if(timeTrigger <=0)
			{
				if(instruction.text == "Waiting response")
					instruction.text = "Waiting response.";
				else if (instruction.text == "Waiting response.")
					instruction.text = "Waiting response..";
				else if (instruction.text == "Waiting response..")
					instruction.text = "Waiting response...";
				else
					instruction.text = "Waiting response";

				timeTrigger = 0.6f;
			}
		}

		if(bot_mode == 1)
		{
			bot_time-= Time.unscaledDeltaTime ;
			if(bot_time <=0)
			{
				rematchRejected();
			}


		}

		if(destruct_menu_time >0)
		{
			destruct_menu_time -= Time.unscaledDeltaTime;
			if(destruct_menu_time <=0)
			{
				Destroy(transform.parent.parent.gameObject);
			}
		}
		if(reset_room_time >0)
		{
			reset_room_time -= Time.unscaledDeltaTime;
			if(reset_room_time <=0)
			{
				Destroy(transform.parent.parent.gameObject);
				mp [0].rematch_begins();
			}
		}


	}

	public void rematchRejected()
	{
		destruct_menu_time =4f;
		waiting = 0;
		bot_mode =0;
		instruction.text = " Rematch rejected";
		revMenu[0].deactivate_bt();
		//Time.timeScale=0;
	}

	public void rematchAcepted()
	{
		reset_room_time = 2f;
		waiting = 0;
		bot_mode =0;
		instruction.text = " Rematch acepted";
		revMenu[0].deactivate_bt();

		//Time.timeScale=0;
	}
}
