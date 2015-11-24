using UnityEngine;
using System.Collections;

public class bt_sound_menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(Sound_Controller.sController.efxSource.volume == 0)
			GetComponent<Animator>().Play("bt_sound_menu_off");
		else
			GetComponent<Animator>().Play("bt_sound_menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{

		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bt_sound_menu"))
		{
			GetComponent<Animator>().Play("bt_sound_menu_off");
			Sound_Controller.sController.muteSFX();
		}
		else
		{
			GetComponent<Animator>().Play("bt_sound_menu");
			Sound_Controller.sController.unmuteSFX();
		}

	}

}
