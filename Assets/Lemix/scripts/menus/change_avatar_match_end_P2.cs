using UnityEngine;
using System.Collections;

public class change_avatar_match_end_P2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().runtimeAnimatorController = Resources.Load("avatares/lvl_" + GLOBALS.Singleton.AVATAR_TYPE + "_avatar") as RuntimeAnimatorController;
        if (GLOBALS.Singleton.LOOSE == true)
		{
			transform.GetComponent<Animator>().Play("win");
		}
		else if(GLOBALS.Singleton.WIN == true)
		{
			transform.GetComponent<Animator>().Play("sad");
		}
		else
		{
			transform.GetComponent<Animator>().Play("normal");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
