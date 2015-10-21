using UnityEngine;
using System.Collections;

public class earthquakePlayer2 : MonoBehaviour {
	float shake, shakeAmount;
	float decreaseFactor  = 1f;
	Vector2 mypos;

	// Use this for initialization
	void Start () {
		mypos = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {

		if (shake > 0) 
		{

			transform.position = shakeAmount * Random.insideUnitCircle + mypos ;
			shake -= Time.deltaTime * decreaseFactor;
		} else {
			shake = 0f;
			transform.position = mypos;
		}
	}

	public void startEarthquake(float shakeTime, float shakeForce)
	{
		Debug.Log("Chamei");
		shake = shakeTime;
		shakeAmount = shakeForce;
	}
}
