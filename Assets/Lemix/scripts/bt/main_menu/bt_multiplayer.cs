using UnityEngine;
using System.Collections;

public class bt_multiplayer : MonoBehaviour {
	public GameObject lobbymaster, txt_conection_state, lang_bt, avatar_bt, bot_bt, statistics_txt;
	public Animator tchauBT;
    Lobby_Master[] lb;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void pressed(){
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			Debug.Log ("CONNECT !");
			lobbymaster.SetActive(true);
			txt_conection_state.SetActive(true);
			lang_bt.SetActive(false);
			avatar_bt.SetActive(false);
			bot_bt.SetActive(false);
			statistics_txt.SetActive(false);
			//tchauBT.SetTrigger("clicked");
			gameObject.SetActive(false);
			//botbt.SetActive(false);
			//gameObject.SetActive(false);

			lb = FindObjectsOfType(typeof(Lobby_Master)) as Lobby_Master[];
			lb[0].Connect_to_photon();

		}

	}

}
