using UnityEngine;
using System.Collections;

public class cancel_bt : MonoBehaviour {
	public GameObject lobbymaster,multiplayer, botbt;
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

		lobbymaster.SetActive(false);
		multiplayer.SetActive(true);
		botbt.SetActive(true);
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
