using UnityEngine;
using System.Collections;

public class earthquakePlayer2 : MonoBehaviour {
	float shaketimer, shakeAmount;
	Vector2 mypos;

	// Use this for initialization
	void Start () {
		mypos = new Vector2(transform.position.x, transform.position.y);

	}
	
	// Update is called once per frame
	void Update () {
		if(shaketimer > 0)
		{
			transform.position = shakeAmount * Random.insideUnitCircle + mypos;
			shaketimer -= Time.deltaTime;
		}
		else
		{
			transform.position = mypos;
			shaketimer = 0;
		}

	}

	public void startEarthquake(float shakeTime, float shakeForce)
	{
		shaketimer = shakeTime;
		shakeAmount = shakeForce;
	}
}
