using UnityEngine;
using System.Collections;

public class bt_music : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void mouseClick()
	{
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			//
			if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bt_music"))
				GetComponent<Animator>().Play("bt_music_off");
			else
				GetComponent<Animator>().Play("bt_music");
		}

	}

}
