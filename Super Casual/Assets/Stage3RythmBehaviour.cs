﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Stage3RythmBehaviour : RythmScenarioBehaviour {
	//GlowState my_state = GlowState.FadeOut;
	int my_state = 0;

	public GameObject myBoard1, myBoard2, myBoard3;
	public GameObject myBg;
	public GameObject[] myLights;
	public int lightStep = 4;

	public override void RestartMusic(){
		myBoard1.GetComponent<Animator> ().Play ("normal");
		myBoard2.GetComponent<Animator> ().Play ("normal");
		myBoard3.GetComponent<Animator> ().Play ("normal");

		myBg.GetComponent<Animator> ().Play ("normal");
	}

	void FixedUpdateas(){
		if (RythmController.s.current_step == 4) {
			foreach(GameObject light in myLights)
				light.GetComponent<SpriteRenderer> ().DOFade (1, GD.s.GlowInTime);
		}
	}

	void FixedUpdate () {
		if (RythmController.s.current_step == RythmController.s.step_glow_in && my_state != 0) {
//			foreach (GameObject light in myLights) {
//				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.25f);
//				light.GetComponent<SpriteRenderer> ().DOFade (1f, (float)lightStep * RythmController.s.current_step_time);
//			}
			my_state = 0;
			myBoard1.GetComponent<Animator> ().Play ("normal", 0, 0);
			myBoard2.GetComponent<Animator> ().Play ("normal", 0, 0);
			myBoard3.GetComponent<Animator> ().Play ("normal", 0, 0);

			myBg.GetComponent<Animator> ().Play ("normal", 0, 0);
			//Debug.Log("(( FLOOR GLOW IN! " + Time.time);

		} else if (RythmController.s.current_step == lightStep && my_state != 1) {
			my_state = 1;
			foreach (GameObject light in myLights) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.25f);
				light.GetComponent<SpriteRenderer> ().DOFade (1f, GD.s.GlowInTime);
			}
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
		
		} else if (RythmController.s.current_step == lightStep + 3 && my_state != 2) {
			my_state = 2;
			
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
		} 

		else if (RythmController.s.current_step == lightStep + 6 && my_state != 3) {
			foreach (GameObject light in myLights) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.25f, GD.s.GlowOutTime);
			}
			my_state = 3;
			//Debug.Log("(( FLOOR GLOW OUT " + Time.time);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
