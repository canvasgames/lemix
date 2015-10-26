using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  
using System.Linq;
using System;

using Thinksquirrel.WordGameBuilder;
using Thinksquirrel.WordGameBuilder.Gameplay;
using Thinksquirrel.WordGameBuilder.ObjectModel;


public class GameController : MonoBehaviour {
	//float matchTotaltime = 120f;
	float matchTotaltime = 1f, wait_Menu = 1, time2Sicronize, waitingOtherPlayer, timer2RecallOtherP = 0.1f;
	mp_controller[] mpCtrl;
	public GameObject fail,win, draw, loading;
	
	GameObject clock;
	
	public GameObject restartBT;

	public GameObject single;


	// Use this for initializatmon
	void Awake()
	{
		//SAFFER.Singleton.Reset_Globals ();
		GLOBALS[] single2 = FindObjectsOfType (typeof(GLOBALS)) as GLOBALS[];
		if (single2.Length == 0) {
			GameObject obj = (GameObject)Instantiate (single, new Vector3 (0, 0, 0), transform.rotation);
			GLOBALS final = obj.GetComponent<GLOBALS> ();
		}
	}

	void Start () {

		GLOBALS[] submitScp = FindObjectsOfType(typeof(GLOBALS)) as GLOBALS[];
		GLOBALS.Singleton.Reset_Globals ();

		clock = GameObject.Find ("hud_clock"); 
		clock.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;



		mpCtrl = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];

		if(GLOBALS.Singleton.MP_PLAYER == 1 && GLOBALS.Singleton.MP_MODE == 1)
		{
			waitingOtherPlayer = 1;
		}
		else
		{
			if(GLOBALS.Singleton.MP_MODE == 0)
			{
				GameObject load = (GameObject)Instantiate (loading, new Vector3 (0,0 , 100), transform.rotation);
			}
		}

		calculate_and_send_level();
		Time.timeScale = 0 ;


	}

	public void start_for_real()
	{
		Time.timeScale = 1;
	}

	void calculate_and_send_level()
	{
		int tempWins = PlayerPrefs.GetInt ("NumberofWins");
		int level;
		int whatLevel = 1;
		
		//Discover the user level ----- x = n((n+1)/2) ------ n is the level x is the number of victories
		if (tempWins == 0)
			level = 0;
		else 
		{
			while((whatLevel*((whatLevel+1)/2)) < tempWins)
			{
				whatLevel++;
			}
			
			if(whatLevel!=1)
				level = whatLevel - 1;
			else
				level = 1;
		}
		
		GameObject umnome = GameObject.Find ("hud_p1_level"); 
		umnome.GetComponent<TextMesh> ().text = "LVL " + level.ToString ();
		
		if(GLOBALS.Singleton.MP_MODE == 1)
		{
			mpCtrl[0].send_lvl(level);
		}
		else
		{
			int temp;

			temp = UnityEngine.Random.Range(level,level+4);
			GameObject bot = GameObject.Find ("hud_p2_level"); 
			bot.GetComponent<TextMesh> ().text = "LVL " + temp.ToString ();
		}
		
	}

	public void sinc_received(float time)
	{
		waitingOtherPlayer = 2;
		time2Sicronize = time;
	}


	void sincronize_issues()
	{
		if(waitingOtherPlayer == 1 && GLOBALS.Singleton.MP_PLAYER == 1)
		{
			//SENDING AND SENDING ARE YOU HERE?
			timer2RecallOtherP -= Time.unscaledDeltaTime;
			if(timer2RecallOtherP == 0)
			{
				timer2RecallOtherP = 0.1f;
				mpCtrl[0].send_are_you_here();
			}
		}
		//ARE YOU HERE RECEIVED
		else if(waitingOtherPlayer == 2)
		{
			time2Sicronize-= Time.unscaledDeltaTime;
			if(time2Sicronize <= (float) PhotonNetwork.time)
			{
				waitingOtherPlayer = 0;
				GameObject load = (GameObject)Instantiate (loading, new Vector3 (0,0 , 100), transform.rotation);
			}
		}
	}
	void Update () {

		//SINCRONIZE
		if(GLOBALS.Singleton.MP_MODE == 1)
		{
			sincronize_issues();
		}
		//MATCH ENDED
		if(matchTotaltime<=0 && GLOBALS.Singleton.WIN == false && GLOBALS.Singleton.LOOSE == false && GLOBALS.Singleton.DRAW == false)
		{
			match_end();
			Time.timeScale = 0 ;
		}

		if (matchTotaltime > 0) {
			update_clock();
		}
		//
	}


	public void AddTime(float time)
	{
		matchTotaltime +=time;
	}

	void update_clock()
	{
		// SHOW TIME 
		matchTotaltime -= Time.deltaTime;
		if (matchTotaltime % 60 >= 10)
			clock.GetComponentInChildren<TextMesh> ().text = (Math.Truncate (matchTotaltime / 60)).ToString () + " " + (Math.Truncate (matchTotaltime % 60)).ToString ();
		else
			clock.GetComponentInChildren<TextMesh> ().text = (Math.Truncate (matchTotaltime / 60)).ToString () + " 0" + (Math.Truncate (matchTotaltime % 60)).ToString ();
		//Debug.Log(matchTotaltime);
	}


	void match_end()
	{
		match_end_F5_statistics();
		
		if(GLOBALS.Singleton.MY_SCORE <= GLOBALS.Singleton.OP_SCORE)
		{
			// DRAW CASE
			if(GLOBALS.Singleton.MY_SCORE == GLOBALS.Singleton.OP_SCORE)
			{
				GLOBALS.Singleton.DRAW = true;
				draw_case_statistics();
				
				
			}
			// LOSE CASE
			else
			{
				GLOBALS.Singleton.LOOSE = true;
				lose_case_statistics();
			}
		}
		//WIN CASE
		else
		{
			GLOBALS.Singleton.WIN = true;
			win_case_statistics();
		}
	}

	void match_end_F5_statistics()
	{
		int tempMatches = PlayerPrefs.GetInt ("NumberofMatches");
		tempMatches ++;
		PlayerPrefs.SetInt("NumberofMatches",tempMatches);
		
		int tempWords  = PlayerPrefs.GetInt ("WordsFounded");
		tempWords += GLOBALS.Singleton.NumberOfWordsFounded;
		PlayerPrefs.SetInt("WordsFounded",tempWords);
	}

	void win_case_statistics()
	{
		int tempWins = PlayerPrefs.GetInt ("NumberofWins");
		tempWins ++;
		PlayerPrefs.SetInt("NumberofWins",tempWins);
		
		int tempStreak = PlayerPrefs.GetInt("WinStreak");
		tempStreak ++;
		Debug.Log ("Ganhei uru" + tempStreak); 
		PlayerPrefs.SetInt("WinStreak",tempStreak);
		
		GameObject vitoria = (GameObject)Instantiate (win, new Vector3 (0,0 , 100), transform.rotation);

	}

	void draw_case_statistics()
	{
		PlayerPrefs.SetInt("WinStreak",0);

		GameObject empate = (GameObject)Instantiate (draw, new Vector3 (0,0 , 100), transform.rotation);

	}

	void lose_case_statistics()
	{
		PlayerPrefs.SetInt("WinStreak",0);
		
		GameObject lose = (GameObject)Instantiate (fail, new Vector3 (0,0 , 100), transform.rotation);

	}
	//================== CHANGE ROOMS ==================

	public void go_to_lobby(){
		PhotonNetwork.Disconnect();

		Application.LoadLevel("Lobby");
	}


	



}
