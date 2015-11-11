using UnityEngine;
using System.Collections;

public class bt_yes_leave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		PhotonNetwork.Disconnect();
		Application.LoadLevel ("Lobby");
	}
	
	void OnMouseEnter() {

			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
	
	}
	
	void OnMouseExit() {

			this.transform.GetComponent<SpriteRenderer> ().color = Color.white;

	}
}
