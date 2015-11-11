using UnityEngine;
using System.Collections;

public class Avatar_player_2 : MonoBehaviour {

	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void changeAvatar(int type)
	{

		transform.GetComponent<Animator>().Play("lvl_"+type);
	}
}
