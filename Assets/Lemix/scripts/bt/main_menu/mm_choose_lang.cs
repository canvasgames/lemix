using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class mm_choose_lang : BtsGuiClick
{

	// Use this for initialization
	void Start () 
	{
		if(GLOBALS.Singleton.LANGUAGE == 1)
		{
			GetComponent<Animator>().Play("portuguese");
		}
		else if(GLOBALS.Singleton.LANGUAGE == 0)
		{
			GetComponent<Animator>().Play("language");
		}
	}

    public override void ActBT()
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
