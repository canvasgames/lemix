using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class activate_pw_button : MonoBehaviour {

	public GameObject my_time, my_spin_now,my_glow, my_icon_countdown, my_icon_spinnow;
	public GameObject HandTut;
	Button myBt;
	bool interactable = false;

	// Use this for initialization
	void Start () {
		myBt = GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCountownState(){
		Debug.Log ("SET COUNTDOWN STATE");
		my_time.SetActive (true);
		my_spin_now.SetActive (false);
		my_glow.SetActive (false);

		my_icon_countdown.SetActive (true);
		my_icon_spinnow.SetActive (false);

		GetComponent<Button> ().interactable = false;
		interactable = false;
		HandTut.SetActive (false);
		//myBt.interactable = false;
	}

	public void HandTutLogic(){
		if(PlayerPrefs.GetInt("PwHandTutShowed", 0) == 0){
			HandTut.SetActive (true);
			PlayerPrefs.SetInt ("PwHandTutShowed", 1);
		}
		else
			HandTut.SetActive (false);
	}

	public void SetSPinNowState(){
		

			

		Debug.Log ("SET SPIIIIIIIIN NOW STATE");

		my_time.SetActive (false);
		my_spin_now.SetActive (true);
		my_glow.SetActive (true);

		my_icon_countdown.SetActive (false);
		my_icon_spinnow.SetActive (true);

		GetComponent<Button> ().interactable = true;

		interactable = true;

		//blinkText ();
		//myBt.interactable = true;

	}

	void blinkText(){
		if(interactable)
		{
			Color rand_color = new Color(Random.Range(0f,1f), Random.Range(0.9f,1f),Random.Range(0f,1f));
			my_spin_now.GetComponent<Text> ().DOColor (rand_color, 0.5f).OnComplete (blinkText);
		}
	}


    public void click()
    {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];
        foreach (ball_hero b in bolas)
        {
            //Destroy(b.gameObject);
            b.send_actual_balls();
            break;
        }

       // hud_controller.si.HUD_BUTTON_CLICKED = true;

		hud_controller.si.RodaMenu ();
//

    }
}
