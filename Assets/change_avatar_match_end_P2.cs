﻿using UnityEngine;
using System.Collections;

public class change_avatar_match_end_P2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<Animator>().Play("lvl_"+GLOBALS.Singleton.AVATAR_TYPE_OP);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
