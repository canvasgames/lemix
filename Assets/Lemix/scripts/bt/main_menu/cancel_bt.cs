using UnityEngine;
using System.Collections;

public class cancel_bt : MonoBehaviour {
	public GameObject lobbymaster,multiplayer, botbt, avatar_bt, statistics_txt, txt_connection, avatar_conection;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnMouseDown(){
		active_bts ();
	}

	public void active_bts()
	{
		GLOBALS.Singleton.MM_SEARCHING_MATCH = false;
		PhotonNetwork.LeaveLobby();
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.Disconnect();


		txt_connection.SetActive(false);
		avatar_conection.SetActive(false);
		lobbymaster.SetActive(false);

		statistics_txt.SetActive(true);
		multiplayer.SetActive(true);
		botbt.SetActive(true);
		avatar_bt.SetActive(true);

		gameObject.SetActive(false);
		//sair do lobby no photon
	}

	void OnMouseEnter() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
	}
	
	void OnMouseExit() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}

}
