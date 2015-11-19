using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GLOBALS : MonoBehaviour {
	public static GLOBALS Singleton;

	[HideInInspector] public int AVATAR_TYPE = 1;
	[HideInInspector] public int AVATAR_TYPE_OP = 1;

	[HideInInspector] public int MY_LVL = 1;
	[HideInInspector] public int OP_LVL = 1;

	[HideInInspector] public int ANAGRAM_ID = 0;
	[HideInInspector] public int RECEIVED_ANAGRAM_ID = 0;

	[HideInInspector] public int MP_MODE = 0;
	[HideInInspector] public int CONNECTED = 0;
	[HideInInspector] public int MP_PLAYER = 0;
	[HideInInspector] public int OP_PLAYER = 0;

	//Game Designer
	[HideInInspector] public int NumberOfWordFilesENG = 7;
	[HideInInspector] public int NumberOfWordFilesPORT = 7;

	[HideInInspector] public float PULetterInitPercentage = 0.2f;
	[HideInInspector] public float PULetterPlusPercentage = 0.1f;

	//STATUS
	[HideInInspector] public bool WIN = false;
	[HideInInspector] public bool LOOSE = false;
	[HideInInspector] public bool DRAW = false;

	[HideInInspector] public int PUCHOOSELETTER = 0;
	[HideInInspector] public int PUGOLDLETTERACTIVE = 0;

	//MAIN MENU STATUS
	[HideInInspector] public bool MM_MENU_OPENED = false;
	[HideInInspector] public bool MM_SEARCHING_MATCH = false;

	//GAME STATUS
	[HideInInspector] public bool GAME_RUNNING = false;
	[HideInInspector] public bool GAME_QUIT_MENU = false;
	[HideInInspector] public bool LVL_UP_MENU = false;
	[HideInInspector] public bool DISCONNECTED_MENU = false;

	//SCORE
	[HideInInspector] public int MY_SCORE = 0;
	[HideInInspector] public int OP_SCORE = 0;
	[HideInInspector] public int MAX_SCORE = 0;

	[HideInInspector] public int USER_HAT_POWER = 0;

	[HideInInspector] public int NumberOfWordsFounded = 0;
	[HideInInspector] public int NumberOfWordsFoundedOP = 0;
	// REMATCH
	[HideInInspector] public int REMATCH_SENT = 0;
	[HideInInspector] public int REMATCH_RECEIVED = 0; // 1 = ACCEPTED | 2 = REJECTED
	[HideInInspector] public int REMATCH_ACCEPT_STATUS = 0; // 1 = ACCEPTED | 2 = REJECTED

	//LANGUAGE |||| 0 ENG |||||| 1 PORT ||||
	[HideInInspector] public int LANGUAGE = 0;

	[HideInInspector] public bool SOUND_ON = true;
	[HideInInspector] public bool MUSIC_ON = true;

	void Awake()
	{

		Reset_Globals ();
		Singleton = this;
		Debug.Log ("SINGLETON INITIALIZED");
		DontDestroyOnLoad(transform.gameObject);
		AVATAR_TYPE = 1;
		MY_LVL = 10;
	}

	public void get()
	{ 
	
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset_Globals(){
		Time.timeScale = 1;
		WIN = false;
		LOOSE = false;
		DRAW = false;

		GAME_RUNNING = false;
		GAME_QUIT_MENU = false;

		MY_SCORE = 0;
		OP_SCORE = 0;

		REMATCH_SENT = 0;
		REMATCH_RECEIVED = 0; // 1 = ACCEPTED | 2 = REJECTED
		REMATCH_ACCEPT_STATUS = 0;

		RECEIVED_ANAGRAM_ID = 0;

		NumberOfWordsFounded = 0;
	}
}
