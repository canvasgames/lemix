using UnityEngine;
using System.Collections;

public class change_avatar_match_end_P1 : MonoBehaviour {
	public static change_avatar_match_end_P1 acesss;
	// Use this for initialization
	void Start () {
		acesss = this;
		changeAvatar();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeAvatar()
	{
		if(GLOBALS.Singleton.LOOSE == true)
		{
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE+"_sad");
		}
		else if(GLOBALS.Singleton.WIN == true)
		{
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE+"_win");
		}
		else
		{
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE);
		}
	}
}
