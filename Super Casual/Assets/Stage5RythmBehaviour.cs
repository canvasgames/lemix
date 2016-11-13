using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Stage5RythmBehaviour : RythmScenarioBehaviour {
	//GlowState my_state = GlowState.FadeOut;
	int my_state = 0;

	public GameObject[] RythmElements;
	public GameObject[] myGlowElements;
	public GameObject[] myGlowElements2;

	public int lightStep = 4, lightStep2 = 5;

	public override void RestartMusic(){

		foreach (GameObject r in RythmElements) {
			if (r!= null) r.GetComponent<Animator> ().Play ("normal", 0, 0);
		}

	}

	void FixedUpdateas(){
		if (RythmController.s.current_step == 4) {
			foreach(GameObject light in myGlowElements)
				light.GetComponent<SpriteRenderer> ().DOFade (1, GD.s.GlowInTime);
		}
	}

	void FixedUpdate () {
		if (RythmController.s.current_step == RythmController.s.step_glow_in && my_state != 0) {

			my_state = 0;

			foreach (GameObject r in RythmElements) {
				if (r!= null) r.GetComponent<Animator> ().Play ("normal", 0, 0);
			}

			//Debug.Log("(( FLOOR GLOW IN! " + Time.time);

		} else if (RythmController.s.current_step == lightStep && my_state != 1) {
			my_state = 1;
			foreach (GameObject light in myGlowElements) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.14f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.9f, 0.05f);
			}
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);

		} else if (RythmController.s.current_step == lightStep2 && my_state != 2) {
			my_state = 2;

			foreach (GameObject light in myGlowElements2) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.14f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.9f, 0.05f);
			}
			//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
		} 

		else if (RythmController.s.current_step == lightStep + 6 && my_state != 3) {
			foreach (GameObject light in myGlowElements) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.9f);
				light.GetComponent<SpriteRenderer> ().DOFade (0.14f, GD.s.GlowOutTime);
			}

			my_state = 3;
			//Debug.Log("(( FLOOR GLOW OUT " + Time.time);
		}

		else if (RythmController.s.current_step == lightStep2 + 6 && my_state != 4) {
			my_state = 4;
				foreach (GameObject light in myGlowElements2) {
				light.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.9f);
					light.GetComponent<SpriteRenderer> ().DOFade (0.15f, GD.s.GlowOutTime);
				}
		}
		//Debug.Log("(( FLOOR GLOW STATIC! " + Time.time);
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
