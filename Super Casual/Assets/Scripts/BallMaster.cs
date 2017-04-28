using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallMaster : MonoBehaviour {
	public static BallMaster s;
//	public ArrayList balls;
	public List<ball_hero> balls;
	// Use this for initialization
	void Awake(){
		s = this;
//		balls = new ArrayList();
		balls = new List<ball_hero>();
	}

	public void AddNewBall(ball_hero b){
		balls.Add (b);
	}

	public void RemoveBall(ball_hero b){
		balls.Remove (b);
	}

	public bool CheckIfBallAreGrounded(){
		foreach(ball_hero b in balls.ToArray()){
			if (b.grounded == true)
				return true;
		}

		return false;
	}


	// Update is called once per frame
	void Update () {
	
	}
}
