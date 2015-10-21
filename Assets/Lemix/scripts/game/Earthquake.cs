using UnityEngine;
using System.Collections;

public class Earthquake : MonoBehaviour {
	public Camera camera; // set this via inspector
	float shake, reoganize, shakeAmount, reorgInterval;
	float decreaseFactor  = 1f;
	Shuffle[] earth;
	WController[] wordCTRL;


	// Use this for initialization
	void Start () {
		wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

	}
	
	// Update is called once per frame
	void Update () {
		if (shake > 0) {
			camera.transform.localPosition = Random.insideUnitCircle * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;
			reoganize -= Time.deltaTime * decreaseFactor;
			if(reoganize <0)
			{
				wordCTRL[0].reorganize();
				reoganize = reorgInterval;
			}
		} else {
			shake = 0f;
		}
	}

	public void startEarthquake(float shakeTime, float reorganizeInterval, float shakeForce)
	{
		shake = shakeTime;
		reoganize = reorganizeInterval;
		reorgInterval = reorganizeInterval;
		shakeAmount = shakeForce;
	}
}
