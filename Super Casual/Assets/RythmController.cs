using UnityEngine;
using System.Collections;

public class RythmController : MonoBehaviour {
	public static RythmController s;
	public int total_steps = 24, current_step = 0;
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
	
	// Update is called once per frame
	void FixedUpdate () {
		if (music_started == true) {
			if (already_started == false) {
				already_started = true;
				current_step = 0;
				next_step_time = Time.time + step_time;
			}

			if (Time.time > next_step_time) {
				next_step_time = next_step_time + step_time;
				current_step++;
				if (current_step == total_steps)
					current_step = 0;
			}
		}
		Debug.Log ("time: " + Time.time);
	}



}
