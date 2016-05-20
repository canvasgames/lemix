using UnityEngine;
using System.Collections;

public class bt_sound : MonoBehaviour
{
	AudioSource musicPlayer ;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActBT()
    {

		if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("bt_sound"))
		{

            GetComponent<Animator>().Play("bt_sound_off");
			sound_controller.s.muteSFX();
            sound_controller.s.muteMusic();
            USER.s.SOUND_MUTED = 1;
            PlayerPrefs.SetInt("sound_muted", 1);
        }
		else
		{

            GetComponent<Animator>().Play("bt_sound");
            sound_controller.s.unmuteSFX();
            sound_controller.s.unmuteMusic();
            USER.s.SOUND_MUTED = 0;
            PlayerPrefs.SetInt("sound_muted", 0);
        }
		
	}

}
