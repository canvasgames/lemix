using UnityEngine;
using System.Collections;

public class bt_multiplayer : MonoBehaviour {
	public GameObject lobbymaster, txt_conection_state;
	public Animator tchauBT;
    Lobby_Master[] lb;
	bt_statistics[] statistics;
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

			tchauBT.SetTrigger("clicked");
			//botbt.SetActive(false);
			//gameObject.SetActive(false);

			lb = FindObjectsOfType(typeof(Lobby_Master)) as Lobby_Master[];
			statistics = FindObjectsOfType(typeof(bt_statistics)) as bt_statistics[];
			lb[0].Connect_to_photon();
//			statistics[0].inactive();
		}

	}
	

/*	void OnMouseOver() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
	}
	
	void OnMouseExit() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}
*/

}
