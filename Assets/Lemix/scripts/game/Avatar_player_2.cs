using UnityEngine;
using System.Collections;

public class Avatar_player_2 : MonoBehaviour {
	public static Avatar_player_2 acess;

	float happyTime = 0;
	float sadTime = 0;
	public bool losing = false;
	// Use this for initialization
	void Start () {
		acess = this;
        GetComponent<Animator>().runtimeAnimatorController = Resources.Load("avatares/lvl_" + GLOBALS.Singleton.AVATAR_TYPE_OP + "_avatar") as RuntimeAnimatorController;
    }
	
	// Update is called once per frame
	void Update () {
		if(sadTime > 0)
		{
			sadTime -= Time.deltaTime;
			if(happyTime <= 0 && sadTime <= 0)
				backState();
		}		
	
		
		if(happyTime >0)
		{
			happyTime -= Time.deltaTime;
			if(happyTime <= 0 && sadTime <= 0)
				backState();
		}
	}
	
	public void changeAvatar(int type)
	{
		transform.GetComponent<Animator>().Play("lvl_"+type);
	}
	
	public void happy()
	{
		transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP+"_happy");
		happyTime = 3f;
		sadTime = 0f;
	}
	
	public void sad()
	{
		transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP+"_sad");
		happyTime = 0f;
		sadTime = 3f;
	}
	
	public void desperate()
	{
		losing = true;
		if(happyTime <= 0 && sadTime <= 0)
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP+"_desperate");
	}

	public void normal ()
	{
		losing = false;
		if(happyTime <= 0 && sadTime <= 0)
			transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP);
	}
	
	public void backState ()
	{
		if(losing == false)
			normal();
		else
			desperate();
	}
}
