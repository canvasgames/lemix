﻿using UnityEngine;
using System.Collections;

public class bt_yes_leave : BtsMenuClassCollider
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    { 
        PhotonNetwork.Disconnect();
        Application.LoadLevel("Lobby_GUI");
	}
	
}
