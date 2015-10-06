using UnityEngine;
using System.Collections;

public class bt_multiplayer : MonoBehaviour {

    Lobby_Master[] lb;

	// Use this for initialization
	void Start () {
		lb = FindObjectsOfType(typeof(Lobby_Master)) as Lobby_Master[];
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseDown(){
		Debug.Log ("CONNECT !");
		lb[0].Connect_to_photon();
	}
}
