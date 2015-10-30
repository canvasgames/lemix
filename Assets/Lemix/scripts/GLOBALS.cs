using UnityEngine;
using System.Collections;


public class GLOBALS : MonoBehaviour {
	public static GLOBALS Singleton;

	[HideInInspector] public int ANAGRAM_ID = 0;
	[HideInInspector] public int RECEIVED_ANAGRAM_ID = 0;

	[HideInInspector] public int MP_MODE = 0;
	[HideInInspector] public int CONNECTED = 0;
	[HideInInspector] public int MP_PLAYER = 0;
	[HideInInspector] public int OP_PLAYER = 0;

	//Game Designer
	[HideInInspector] public int NumberOfWordFiles = 7;
	[HideInInspector] public float PULetterInitPercentage = 0.2f;
	[HideInInspector] public float PULetterPlusPercentage = 0.1f;

	//STATUS
	[HideInInspector] public bool WIN = false;
	[HideInInspector] public bool LOOSE = false;
	[HideInInspector] public bool DRAW = false;

	[HideInInspector] public int PUCHOOSELETTER = 0;
	[HideInInspector] public int PUGOLDLETTERACTIVE = 0;
	//GAME STATUS
	[HideInInspector] public bool GAME_RUNNING = false;
	[HideInInspector] public bool GAME_QUIT_MENU = false;

	//SCORE
	[HideInInspector] public int MY_SCORE = 0;
	[HideInInspector] public int OP_SCORE = 0;
	[HideInInspector] public int MAX_SCORE = 0;

	[HideInInspector] public int USER_HAT_POWER = 0;

	[HideInInspector] public int NumberOfWordsFounded = 0;

	// REMATCH
	[HideInInspector] public int REMATCH_SENT = 0;
	[HideInInspector] public int REMATCH_RECEIVED = 0; // 1 = ACCEPTED | 2 = REJECTED
	[HideInInspector] public int REMATCH_ACCEPT_STATUS = 0; // 1 = ACCEPTED | 2 = REJECTED


	void Awake()
	{
		Reset_Globals ();
		Singleton = this;
		Debug.Log ("SINGLETON INITIALIZED");
		DontDestroyOnLoad(transform.gameObject);
	}

	public void get()
	{ 
		Debug.Log ("HELO WORLD");
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
