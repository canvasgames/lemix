using UnityEngine;
using System.Collections;

public class bt_sound : BtsGuiClick
{
	AudioSource musicPlayer ;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void ActBT()
    {
		if(GLOBALS.Singleton.MM_MENU_OPENED == false)
		{
			if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bt_sound"))
			{
				GetComponent<Animator>().Play("bt_sound_off");
				Sound_Controller.sController.muteSFX();
			}
			else
			{
				GetComponent<Animator>().Play("bt_sound");
				Sound_Controller.sController.unmuteSFX();
			}
		}
	}

}
