﻿using UnityEngine;
using System.Collections;

public class bt_multiplayer : MonoBehaviour {
	public GameObject lobbymaster,cancelbt, botbt;

    Lobby_Master[] lb;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown(){
		Debug.Log ("CONNECT !");
		lobbymaster.SetActive(true);
		cancelbt.SetActive(true);
		botbt.SetActive(false);
		lb = FindObjectsOfType(typeof(Lobby_Master)) as Lobby_Master[];
		lb[0].Connect_to_photon();
	}

	void active_bts()
	{
		lobbymaster.SetActive(false);
		cancelbt.SetActive(false);
		botbt.SetActive(true);
	}
}
