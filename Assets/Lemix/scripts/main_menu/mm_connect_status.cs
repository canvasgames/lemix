using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mm_connect_status : MonoBehaviour {
	Text instruction;
	Animation erro;
	float timeTrigger = 0.6f, botTimer = 8f, timerMatchFound = 3f;
	int connectionState = 0;

	Lobby_Master[] lobby_master;
	
	public GameObject searching_op;

	// Use this for initialization
	void Start () {
		instruction = GetComponent<Text>();
		instruction.text = "Connecting";
	}

	public void connectionState1()
	{
		connectionState = 1;

	}

	public void connectionState2()
	{
		botTimer = 10f;
		connectionState = 2;
		searching_op.SetActive(true);

	}

	public void connectionState3()
	{
		connectionState = 3;
		searching_op.GetComponent<Animator>().SetBool("Found",true);
		instruction.text = "Match Found";
	}

	public void connectionState3Guest()
	{
		connectionState = 666;
		searching_op.GetComponent<Animator>().SetBool("Found",true);
		instruction.text = "Match Found";
	}

	public void letsPlay()
	{
		lobby_master = FindObjectsOfType(typeof(Lobby_Master)) as Lobby_Master[];
		lobby_master[0].send_player_info();
	}

	public void LetsPlayBot()
	{
		GLOBALS.Singleton.CONNECTED = 0;
		GLOBALS.Singleton.MP_PLAYER = 0;
		GLOBALS.Singleton.OP_PLAYER = 0;
		GLOBALS.Singleton.MP_MODE = 0;
		Application.LoadLevel("Gameplay");
	}

	// Update is called once per frame
	void Update () {

		if(connectionState == 1)
		{
			changeConnectingTxt();
		}
		else if(connectionState == 2)
		{
			changeSearchingTxt();
			/*botTimer -=  Time.unscaledDeltaTime ;
			if(botTimer <=0)
			{
				connectionState = 4;
				searching_op.GetComponent<Animator>().SetBool("Found",true);
				instruction.text = "Match Found";
				
			}*/
		}

		//Find the player
		else if(connectionState == 3)
		{
			timerMatchFound-= Time.unscaledDeltaTime ;
			if(timerMatchFound <=0)
				letsPlay();
		}
		//Hmkay, play with the bot
		else if(connectionState == 4)
		{
			timerMatchFound-= Time.unscaledDeltaTime ;
			if(timerMatchFound <=0)
				LetsPlayBot();
		}

	}

	void changeSearchingTxt()
	{
		timeTrigger-= Time.unscaledDeltaTime ;
		if(timeTrigger <=0)
		{
			if(instruction.text == "Searching")
				instruction.text = "Searching.";
			else if (instruction.text == "Searching.")
				instruction.text = "Searching..";
			else if (instruction.text == "Searching..")
				instruction.text = "Searching...";
			else
				instruction.text = "Searching";
			
			timeTrigger = 0.6f;
		}
	}

	void changeConnectingTxt()
	{
		timeTrigger-= Time.unscaledDeltaTime ;
		if(timeTrigger <=0)
		{
			if(instruction.text == "Connecting")
				instruction.text = "Connecting.";
			else if (instruction.text == "Connecting.")
				instruction.text = "Connecting..";
			else if (instruction.text == "Connecting..")
				instruction.text = "Connecting...";
			else
				instruction.text = "Connecting";
			
			timeTrigger = 0.6f;
		}
	}
}
