using UnityEngine;
using System.Collections;

public class bt_statistics : MonoBehaviour {
	public GameObject statMenu;
	public bool active = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void inactive()
	{
		active = false;
		this.transform.GetComponent<SpriteRenderer> ().color = Color.gray;
	}
	void OnMouseDown(){
		if(active == true)
		{
			Instantiate (statMenu, new Vector3 (0, 0, -8), transform.rotation);
		}
		//menu.gameObject.transform.
	}

	void OnMouseOver() {
		if(active == true)
		{
			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		}
	}
	
	void OnMouseExit() {
		if(active == true)
		{
			this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
