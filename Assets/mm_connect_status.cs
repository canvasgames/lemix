using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mm_connect_status : MonoBehaviour {
	TextMesh instruction;
	Animation erro;
	float timeTrigger = 0.6f, botTimer = 10f;
	int connectionState = 0;

	public GameObject searching_op;

	// Use this for initialization
	void Start () {
		instruction = GetComponent<TextMesh>();
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
		instruction.text = "Found";
	}
	// Update is called once per frame
	void Update () {

		if(connectionState == 1)
		{
			timeTrigger-= Time.unscaledDeltaTime ;
			botTimer -=  Time.unscaledDeltaTime ;
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

			if(botTimer <=0)
			{
				connectionState = 3;
				searching_op.GetComponent<Animator>().SetBool("Found",true);
				instruction.text = "Found";
				Application.LoadLevel("Gameplay");
			}
		}
		else if(connectionState == 2)
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

	}
}
