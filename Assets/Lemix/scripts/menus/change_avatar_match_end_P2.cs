using UnityEngine;
using System.Collections;

public class change_avatar_match_end_P2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(GLOBALS.Singleton.LOOSE == true)
		{
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP+"_win");
		}
		else if(GLOBALS.Singleton.WIN == true)
		{
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP+"_sad");
		}
		else
		{
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
