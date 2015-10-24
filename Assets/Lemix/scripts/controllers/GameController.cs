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
	float matchTotaltime = 5f;
	mp_controller[] mp;
	public GameObject fail,win, draw;

	public GameObject failTitle, winTitle, drawTitle;
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
		Debug.Log(submitScp.Length);
		clock = GameObject.Find ("hud_clock"); 
		clock.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;
		GLOBALS.Singleton.Reset_Globals ();
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
		 calculate_and_send_level();
		//matchTotaltime = 1;
		//SAFFER.Singleton.MY_SCORE = 5;
		//SAFFER.Singleton.OP_SCORE = 5;
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
			mp[0].send_lvl(level);
		}
		else
		{
			int temp;

			temp = UnityEngine.Random.Range(level,level+4);
			GameObject bot = GameObject.Find ("hud_p2_level"); 
			bot.GetComponent<TextMesh> ().text = "LVL " + temp.ToString ();
		}
		
	}


	void Update () {
		//
		//
		//
		if (matchTotaltime > 0) {
			// SHOW TIME 
			matchTotaltime -= Time.deltaTime;
			if (matchTotaltime % 60 >= 10)
				clock.GetComponentInChildren<TextMesh> ().text = (Math.Truncate (matchTotaltime / 60)).ToString () + " " + (Math.Truncate (matchTotaltime % 60)).ToString ();
			else
				clock.GetComponentInChildren<TextMesh> ().text = (Math.Truncate (matchTotaltime / 60)).ToString () + " 0" + (Math.Truncate (matchTotaltime % 60)).ToString ();
			//Debug.Log(matchTotaltime);
		}

		//Debug.Log(SAFFER.Singleton.DRAW);
		if(matchTotaltime<=0 && GLOBALS.Singleton.WIN == false && GLOBALS.Singleton.LOOSE == false && GLOBALS.Singleton.DRAW == false)
		{
			int tempMatches = PlayerPrefs.GetInt ("NumberofMatches");
			tempMatches ++;
			PlayerPrefs.SetInt("NumberofMatches",tempMatches);

			int tempWords  = PlayerPrefs.GetInt ("WordsFounded");
			tempWords += GLOBALS.Singleton.NumberOfWordsFounded;
			PlayerPrefs.SetInt("WordsFounded",tempWords);

			//Debug.Log(SAFFER.Singleton.MY_SCORE);
			//Debug.Log(SAFFER.Singleton.OP_SCORE);
			if(GLOBALS.Singleton.MY_SCORE <= GLOBALS.Singleton.OP_SCORE)
			{
				// DRAW CASE
				if(GLOBALS.Singleton.MY_SCORE == GLOBALS.Singleton.OP_SCORE)
				{
					PlayerPrefs.SetInt("WinStreak",0);

					GameObject empate = (GameObject)Instantiate (draw, new Vector3 (0,0 , 100), transform.rotation);
					GLOBALS.Singleton.DRAW = true;

					//empate.renderer.sortingOrder = 5;
					//GameObject empateTit = (GameObject)Instantiate (drawTitle, new Vector3 (0,80 , 100), transform.rotation);
					//empateTit.renderer.sortingOrder = 10;
				}
				// LOOSE CASE
				else
				{

					PlayerPrefs.SetInt("WinStreak",0);

					GameObject lose = (GameObject)Instantiate (fail, new Vector3 (0,0 , 100), transform.rotation);
					GLOBALS.Singleton.LOOSE = true;
					//lose.renderer.sortingOrder = 5;
					//GameObject loseTit = (GameObject)Instantiate (failTitle, new Vector3 (0,80 , 100), transform.rotation);
					//loseTit.renderer.sortingOrder = 10;
				}
			}
			//WIN CASE
			else
			{
				int tempWins = PlayerPrefs.GetInt ("NumberofWins");
				tempWins ++;
				PlayerPrefs.SetInt("NumberofWins",tempWins);

				int tempStreak = PlayerPrefs.GetInt("WinStreak");
				tempStreak ++;
				Debug.Log ("Ganhei uru" + tempStreak); 
				PlayerPrefs.SetInt("WinStreak",tempStreak);

				GameObject vitoria = (GameObject)Instantiate (win, new Vector3 (0,0 , 100), transform.rotation);
				GLOBALS.Singleton.WIN = true;
				//vitoria.renderer.sortingOrder = 5;
				//GameObject vitoriaTit = (GameObject)Instantiate (winTitle, new Vector3 (0,80 , 100), transform.rotation);
				//vitoriaTit.renderer.sortingOrder = 10;
			}
			//GameObject rstt = (GameObject)Instantiate (restartBT, new Vector3 (0,-120 , 100), transform.rotation);
			//rstt.renderer.sortingOrder = 10;
			Time.timeScale = 0 ;
		}
		//
	}


	public void AddTime(float time)
	{
		matchTotaltime +=time;
	}


	//================== CHANGE ROOMS ==================

	public void go_to_lobby(){
		//foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) 
		//	Destroy(o);

		PhotonNetwork.Disconnect();

		Application.LoadLevel("Lobby");
	}


	



}
