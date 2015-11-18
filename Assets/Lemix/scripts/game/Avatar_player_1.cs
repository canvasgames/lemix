using UnityEngine;
using System.Collections;

public class Avatar_player_1 : MonoBehaviour {
	public static Avatar_player_1 acess;

	float happyTime = 0;
	float sadTime = 0;
	public bool losing = false;

	// Use this for initialization
	void Start () {
		acess = this;
		changeAvatar(GLOBALS.Singleton.AVATAR_TYPE);
	}
	
	// Update is called once per frame
	void Update () {
		if(sadTime > 0)
		{
			sadTime -= Time.deltaTime;
			if(sadTime <= 0 && happyTime <= 0)
				backState ();
		}

		if(happyTime >0)
		{
			happyTime -= Time.deltaTime;
			if(happyTime <=0 && sadTime <= 0)
				backState ();
		}
	}

	public void changeAvatar(int type)
	{
		transform.GetComponent<Animator>().Play("lvl_"+type);
	}

	public void happy()
	{
		transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE+"_happy");
		happyTime = 3f;
		sadTime = 0f;
	}

	public void sad()
	{
		transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE+"_sad");
		happyTime = 0f;
		sadTime = 3f;
	}

	public void desperate()
	{
		losing = true;
		if(happyTime <= 0 && sadTime <= 0)
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE+"_desperate");
	}

	public void normal ()
	{
		losing = false;

		if(happyTime <= 0 && sadTime <= 0)
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE);
	}
	public void backState ()
	{
		Debug.Log(losing);
		if(losing == false)
			normal();
		else
			desperate();

	}

}
