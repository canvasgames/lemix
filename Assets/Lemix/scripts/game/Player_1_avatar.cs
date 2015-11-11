using UnityEngine;
using System.Collections;

public class Player_1_avatar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		changeAvatar(GLOBALS.Singleton.AVATAR_TYPE);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeAvatar(int type)
	{
		transform.GetComponent<Animator>().Play("lvl_"+type);
	}
}
