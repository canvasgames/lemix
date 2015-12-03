using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GLOBALS : MonoBehaviour {
	public static GLOBALS Singleton;

	[HideInInspector] public int AVATAR_TYPE = 1;
	[HideInInspector] public int AVATAR_TYPE_OP = 1;

	[HideInInspector] public int MY_LVL = 0;
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
    [HideInInspector] public bool WAITING_MENU = false;
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
    void OnLevelWasLoaded(int level)
    {
        Debug.Log("AAAAAAAAAAAAA");
        GLOBALS.Singleton.MY_LVL = calculateActualLevel();
        
    }
    void Awake()
	{
        
        
        Reset_Globals();
        Singleton = this;
       // GLOBALS.Singleton.MY_LVL = actualLevel();
        Debug.Log ("SINGLETON INITIALIZED");
		DontDestroyOnLoad(transform.gameObject);
		//AVATAR_TYPE = 3;
		
	}

	public void get()
	{ 
	
	}
	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();
        GLOBALS.Singleton.MY_LVL = calculateActualLevel();
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
        REMATCH_SENT = 0;

        RECEIVED_ANAGRAM_ID = 0;

		NumberOfWordsFounded = 0;
	}

    public int calculateActualLevel()
    {
        int level;
       // PlayerPrefs.SetInt("NumberofWins",73);
        int tempWins = PlayerPrefs.GetInt("NumberofWins");
        int whatLevel = 1;

        //Discover the user level ----- x = n((n+1)/2) ------ n is the level x is the number of victories
        //evolution of values http://www.wolframalpha.com/input/?i=n%28%28n%2B1%29%2F2%29 -> Values

        Debug.Log(tempWins + " EUWWWWWWWWWWWWWWWWWWWW");
        if (tempWins == 0)
            level = 0;
        else
        {
            while ((whatLevel * ((whatLevel + 1) / 2)) < tempWins)
            {
                whatLevel++;
            }

            if (whatLevel != 1)
                level = whatLevel - 1;
            else
                level = 1;
        }

        return level;
    }
}
