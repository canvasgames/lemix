using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class activate_pw_button : MonoBehaviour {

	public GameObject my_time, my_spin_now,my_glow;
	Button myBt;

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

		GetComponent<Button> ().interactable = false;

		//myBt.interactable = false;
	}

	public void SetSPinNowState(){
		Debug.Log ("SET SPIIIIIIIIN NOW STATE");

		my_time.SetActive (false);
		my_spin_now.SetActive (true);
		my_glow.SetActive (true);

		GetComponent<Button> ().interactable = true;


		//myBt.interactable = true;

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
