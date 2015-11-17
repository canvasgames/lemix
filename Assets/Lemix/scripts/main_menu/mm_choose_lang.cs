using UnityEngine;
using System.Collections;

public class mm_choose_lang : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void cliked()
	{
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			if(GLOBALS.Singleton.LANGUAGE == 0)
			{
				GLOBALS.Singleton.LANGUAGE = 1;
				GetComponent<Animator>().Play("portuguese");
			}
			else if(GLOBALS.Singleton.LANGUAGE == 1)
			{
				GLOBALS.Singleton.LANGUAGE = 0;
				GetComponent<Animator>().Play("language");
			}
		}
	}


}
