using UnityEngine;
using System.Collections;

public class bt_multiplayer : MonoBehaviour {
	public GameObject lobbymaster,cancelbt, botbt, txt_conection_state;

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
		txt_conection_state.SetActive(true);

		//cancelbt.SetActive(true);
		//botbt.SetActive(false);
		gameObject.SetActive(false);

		lb = FindObjectsOfType(typeof(Lobby_Master)) as Lobby_Master[];
		lb[0].Connect_to_photon();
	}
	

	void OnMouseEnter() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
	}
	
	void OnMouseExit() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}


}
