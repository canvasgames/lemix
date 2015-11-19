using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class mm_choose_lang : MonoBehaviour {

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
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void cliked()
	{
		if(GLOBALS.Singleton.MM_MENU_OPENED == false && GLOBALS.Singleton.MM_SEARCHING_MATCH == false)
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

	public void inactivate()
	{
		this.transform.GetComponent<Button>().interactable = false; 
	}

	public void activate()
	{
		this.transform.GetComponent<Button>().interactable = true; 
	}

}
