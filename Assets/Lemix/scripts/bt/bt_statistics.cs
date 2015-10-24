using UnityEngine;
using System.Collections;

public class bt_statistics : MonoBehaviour {
	public GameObject statMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		GameObject menu = (GameObject)Instantiate (statMenu, new Vector3 (0, 0, -8), transform.rotation);
		//menu.gameObject.transform.
	}

	void OnMouseEnter() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
	}
	
	void OnMouseExit() {
		this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
