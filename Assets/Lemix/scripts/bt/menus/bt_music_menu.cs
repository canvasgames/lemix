using UnityEngine;
using System.Collections;

public class bt_music_menu : BtsMenuClassCollider
{

	// Use this for initialization
	void Start () {
		if(Sound_Controller.sController.musicSource.volume == 0)
			GetComponent<Animator>().Play("bt_music_menu_off");
		else
			GetComponent<Animator>().Play("bt_music_menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bt_music_menu"))
			GetComponent<Animator>().Play("bt_music_menu_off");
		else
			GetComponent<Animator>().Play("bt_music_menu");
		
	}
}
