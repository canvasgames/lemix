using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menu_sync : MonoBehaviour {
	TextMesh instruction;
	int waiting = 1;
	float timeTrigger;
	// Use this for initialization
	void Start () {
		instruction = GetComponent<TextMesh>();
		instruction.text = "Synchronizing";
		timeTrigger = 0.6f;
	}
	
	// Update is called once per frame
	void Update () {

			timeTrigger-= Time.unscaledDeltaTime ;
			if(timeTrigger <=0)
			{
				if(instruction.text == "Synchronizing")
					instruction.text = "Synchronizing.";
				else if (instruction.text == "Synchronizing.")
					instruction.text = "Synchronizing..";
				else if (instruction.text == "Synchronizing..")
					instruction.text = "Synchronizing...";
				else
					instruction.text = "Synchronizing";
				
				timeTrigger = 0.6f;
			}
		
	
	}
}
