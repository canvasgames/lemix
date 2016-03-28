using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menu_loading_script : MonoBehaviour {
	TextMesh instruction;
	int waiting = 1;
	float timeTrigger = 0.4f;

	// Use this for initialization
	void Start () {
		instruction = GetComponent<TextMesh>();
		instruction.text = "Starts in 3";
	}
	
	// Update is called once per frame
	void Update () {

		//change txt
		if(waiting == 1)
		{
			timeTrigger-= Time.unscaledDeltaTime ;
			if(timeTrigger <=0)
			{
				if(instruction.text == "Starts in 3")
					instruction.text = "Starts in 2";
				else if (instruction.text == "Starts in 2")
					instruction.text = "Starts in 1";
				else if (instruction.text == "Starts in 1")
					instruction.text = "Game Started!";
				else if (instruction.text == "Game Started!")
				{
                    GameController.s.start_for_real();
					Destroy(transform.parent.gameObject);
				}
				timeTrigger = 0.4f;
			}
		}
	}
}
