using UnityEngine;
using System.Collections;

public class bt_music : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnMouseDown()
	{
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			//Debug.Log();
			if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("music_on"))
				GetComponent<Animator>().Play("music_off");
			else
				GetComponent<Animator>().Play("music_on");
		}

	}
	void OnMouseOver() {
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;
		}

	}
	
	void OnMouseExit() {
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
