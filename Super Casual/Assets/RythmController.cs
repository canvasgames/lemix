using UnityEngine;
using System.Collections;

public enum GlowState{
	FadeIn,
	Static,
	FadeOut,
}

public class RythmController : MonoBehaviour {
	public static RythmController s;
	public int total_steps = 24, current_step = 0;
	float current_step_time;
	public int step_glow_in = 0, step_glow_static = 3, step_glow_out = 6;
	public float step_time = 0.984f;
	[HideInInspector] public bool music_started = false;
	private bool already_started = false;
	private float next_step_time = 0;


	void Awake(){
		s = this;
	}
	// Use this for initialization
	void Start () {
	
	}

	public void OnMusicStarted(){
		already_started = false;
		music_started = true;
		current_step = 0;
		next_step_time = 0;
		current_step_time = step_time / total_steps;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (music_started == true) {
			if (already_started == false) {
				already_started = true;
				current_step = 0;
				next_step_time = Time.time + current_step_time;
			}

			if (Time.time > next_step_time) {
				next_step_time = next_step_time + current_step_time;
				current_step++;
				if (current_step == total_steps)
					current_step = 0;
			}
		}
//		Debug.Log("STEP: " + current_step);
//		if( current_step == step_glow_in)
//			Debug.Log("(( STEP GLOW IN! " + step_glow_in + " ./.  Time: " + Time.time);
//		if( current_step == step_glow_static)
//			Debug.Log("__ STEP GLOW STATIC! " + step_glow_static + " ./.  Time: " + Time.time);
//		if( current_step == step_glow_out)
//			Debug.Log("(( STEP GLOW OUT " + step_glow_out + " ./.  Time: " + Time.time);
//		Debug.Log ("time: " + Time.time);
	}



}
