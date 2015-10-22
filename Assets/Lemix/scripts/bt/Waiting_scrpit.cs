using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Waiting_scrpit : MonoBehaviour {
	Text instruction;
	float timeTrigger;
	int waiting = 1;
	Menus_Controller[] menusctrl;


	// Use this for initialization
	void Start () {
		instruction = GetComponent<Text>();
		instruction.text = "Waiting response";
		menusctrl = FindObjectsOfType(typeof(Menus_Controller)) as Menus_Controller[];
	}
	
	// Update is called once per frame
	void Update () {
		if(waiting == 1)
		{
			timeTrigger-= Time.unscaledDeltaTime ;
			if(timeTrigger <=0)
			{
				if(instruction.text == "Waiting response")
					instruction.text = "Waiting response.";
				else if (instruction.text == "Waiting response.")
					instruction.text = "Waiting response..";
				else if (instruction.text == "Waiting response..")
					instruction.text = "Waiting response...";
				else
					instruction.text = "Waiting response";

				timeTrigger = 0.6f;
			}
		}
	}

	public void rematchRejected()
	{
		waiting = 0;
		instruction.text = "Rematch rejected =/";
		menusctrl[0].destructWaiting();
	}
}
