﻿using UnityEngine;
using System.Collections;

public class bt_multiplayer : BtsGuiClick
{
	public GameObject lobbymaster, txt_conection_state, lang_bt, avatar_bt, bot_bt, statistics_txt, cancel_bt;
	public Animator tchauBT;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(PhotonNetwork.connected);
	}

    public override void ActBT()
    { 
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
            base.ActBT();
			Debug.Log ("CONNECT !");
			lobbymaster.SetActive(true);
			txt_conection_state.SetActive(true);
			cancel_bt.SetActive(true);
			GLOBALS.Singleton.MM_SEARCHING_MATCH = true;

            lang_bt.GetComponent<mm_choose_lang>().DeactivateBt();

            avatar_bt.SetActive(false);
            
            bot_bt.SetActive(false);
			statistics_txt.SetActive(false);

            //tchauBT.SetTrigger("clicked");


            Lobby_Master.s.Connect_to_photon();

			gameObject.SetActive(false);
		}

	}

}
