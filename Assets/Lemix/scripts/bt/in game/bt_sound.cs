using UnityEngine;
using System.Collections;

public class bt_sound : MonoBehaviour {
	AudioSource musicPlayer ;
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
			if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("sound_on"))
				GetComponent<Animator>().Play("sound_off");
			else
				GetComponent<Animator>().Play("sound_on");
		}
	}
	void OnMouseOver() {

			this.transform.GetComponent<SpriteRenderer> ().color = Color.green;

		
	}
	
	void OnMouseExit() {
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			this.transform.GetComponent<SpriteRenderer> ().color = Color.white;
		}
	}
}
