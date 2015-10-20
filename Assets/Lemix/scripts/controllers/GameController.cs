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
	float matchTotaltime = 120f;
	//float matchTotaltime = 3f;
	
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
		//matchTotaltime = 1;
		//SAFFER.Singleton.MY_SCORE = 5;
		//SAFFER.Singleton.OP_SCORE = 5;
	}

	void Update () {

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
			//Debug.Log(SAFFER.Singleton.MY_SCORE);
			//Debug.Log(SAFFER.Singleton.OP_SCORE);
			if(GLOBALS.Singleton.MY_SCORE <= GLOBALS.Singleton.OP_SCORE)
			{
				// DRAW CASE
				if(GLOBALS.Singleton.MY_SCORE == GLOBALS.Singleton.OP_SCORE)
				{
					GameObject empate = (GameObject)Instantiate (draw, new Vector3 (0,0 , 100), transform.rotation);
					GLOBALS.Singleton.DRAW = true;

					//empate.renderer.sortingOrder = 5;
					//GameObject empateTit = (GameObject)Instantiate (drawTitle, new Vector3 (0,80 , 100), transform.rotation);
					//empateTit.renderer.sortingOrder = 10;
				}
				// LOOSE CASE
				else
				{
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
		GLOBALS.Singleton.Reset_Globals ();
		Application.LoadLevel("Lobby");
	}


	



}
