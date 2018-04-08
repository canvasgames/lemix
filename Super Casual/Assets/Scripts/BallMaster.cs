using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallMaster : MonoBehaviour {
	public static BallMaster s;
//	public ArrayList balls;
	public List<ball_hero> balls;
	public GameObject ballPrefab;
	public int currentBall;
	// Use this for initialization
	void Awake(){
		s = this;
//		Invoke ("Test", 1f);
	}

	public void AddNewBall(ball_hero b){
		balls.Add (b);
	}

	public void RemoveBall(ball_hero b){
		balls.Remove (b);
	}

	public void UpdateBallsSkin(){
		foreach(ball_hero b in balls.ToArray()){
			b.UpdateMySkin();
		}
	}


	public void DeactivateBallsForRestart(){
		balls.ToArray () [1].gameObject.SetActive(false);
		balls.ToArray () [0].gameObject.SetActive(false);
	}

	void Test(){
//		Debug.Log ("0 TTTTTTTEST CALLED");
//		balls.ToArray () [0].test ();
//		Debug.Log ("1 TTTTTTTEST CALLED");
//		balls.ToArray () [1].test ();
	}

	public void NewGameLogic(){
		if (QA.s.TRACE_PROFUNDITY > 0) Debug.Log (" BALLMASTER! NEW GAME LOGIC");
		foreach(ball_hero b in balls.ToArray()){
			b.grounded = false;
			b.son_created = false;
		}
		currentBall = 0;

		if(balls.ToArray () [1].enabled) balls.ToArray () [1].UpdateMySkin ();

		balls.ToArray () [1].gameObject.SetActive(false);

		balls.ToArray () [0].gameObject.SetActive(true);
		balls.ToArray () [0].transform.position = new Vector2 (-4.57f, -6.53f); 
		balls.ToArray () [0].Init_first_ball ();
	}


	public GameObject ReturnInactiveBall(){

		if (currentBall == 0) {
			balls.ToArray ()[1].gameObject.SetActive(true);
			currentBall = 1;

		} else {
			balls.ToArray ()[0].gameObject.SetActive(true);
			currentBall = 0;
		}

		return balls.ToArray () [currentBall].gameObject;
	}

	public bool CheckIfBallAreGrounded(){
		foreach(ball_hero b in balls.ToArray()){
			if (b.grounded == true)
				return true;
		}

		return false;
	}

	public void BallFirstJump(){
		foreach(ball_hero b in balls.ToArray()){
			if (b.grounded == true)
				StartCoroutine (b.Jump());
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
