﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallMaster : MonoBehaviour {
	#region === VARS ===
	public static BallMaster s;
	public GameObject ballExplosion;
//	public ArrayList balls;
	public List<ball_hero> balls;
	public Follower[] followersBall1, followersBall2;
	public GameObject ballPrefab;
	public int currentBall;
	public int clothChangerCurrent = 1;


	void Awake(){
		s = this;
//		Invoke ("Test", 1f);
	}
	#endregion
//	public void(

	#region === Followers ===

	public Follower[] GimmeMyFollowers(int n){
		Follower[] tempFollowers = new Follower[n];

		for (int i = 0; i < n; i++) {
			tempFollowers [i] = followersBall1 [i];
		}

		return tempFollowers;
	}

	public void UpdateBallsSkin(){
		Debug.Log ("[BALL MASTER] UPDATE BALL SKINS");
		foreach(ball_hero b in balls.ToArray()){
			b.UpdateMySkin();
		}
	}

	public void DeactivateUnnusedFollowers(){
		if (globals.s.ACTUAL_SKIN.bandN > 0) {
			for (int i = 0; i < followersBall1.Length; i++) {
				Debug.Log ("DEactivate unnused!!!!! " + i);
				if (i > globals.s.ACTUAL_SKIN.bandN - 2) {
					Debug.Log ("DEppppppppppactivate unnused!!!!! " + i);

					followersBall1 [i].gameObject.SetActive (false);
					followersBall2 [i].gameObject.SetActive (false);
				}
			}
		} else {
			for (int i = 0; i < followersBall1.Length; i++) {
				followersBall1 [i].gameObject.SetActive(false);
				followersBall2 [i].gameObject.SetActive(false);
			}
		}
	}


	public void IEnumeratorInitFollowersMovement(bool iAmLeft, Vector2 myPos, Vector2 speed){ 
		StartCoroutine (InitFollowersMovement (iAmLeft, myPos, speed));
	}


	public IEnumerator InitFollowersMovement(bool iAmLeft, Vector2 myPos, Vector2 speed){
		for (int i = 0; i <= globals.s.ACTUAL_SKIN.bandN - 2; i++) {
			yield return new WaitForSeconds (GD.s.FOLLOWER_DELAY);
			Follower f = null;
			if (iAmLeft) {
				f = followersBall1 [i];

			} else {
				f = followersBall2 [i];
			}

			f.gameObject.SetActive (true);
			f.transform.position = myPos;
			f.InitMovement (speed);
		}
	}

	public void IEnumeratorJumpMyFollowers(bool iAmLeft){
		StartCoroutine (JumpMothaFockaJump (iAmLeft));

	}

	IEnumerator JumpMothaFockaJump(bool iAmLeft){
		for (int i = 0; i <= globals.s.ACTUAL_SKIN.bandN - 2; i++) {
			yield return new WaitForSeconds (GD.s.FOLLOWER_DELAY);
			Follower f = null;
			if (iAmLeft) {
				f = followersBall1 [i];
			} else {
				f = followersBall2 [i];
			}

			f.JumpOn ();
		}
	}


	#endregion

	#region === INIT ===
	public void DeactivateBallsForRestart(){
		balls.ToArray () [1].gameObject.SetActive(false);
		balls.ToArray () [0].gameObject.SetActive(false);
	}

	public void NewGameLogic(){
		if (QA.s.TRACE_PROFUNDITY > 0) Debug.Log ("[BALLMASTER] NEW GAME LOGIC");
		foreach(ball_hero b in balls.ToArray()){
			b.grounded = false;
			b.son_created = false;
		}
		currentBall = 0;

		Debug.Log ("[BALLMASTER]! NEW GAME LOGIC - UPDATE BALL SKIN ");
		if (balls.ToArray () [1].enabled)
			balls.ToArray () [1].UpdateMySkin ();
		else {
			balls.ToArray () [1].gameObject.SetActive (true);
			balls.ToArray () [1].UpdateMySkin ();
			balls.ToArray () [1].gameObject.SetActive (false);
		}

		balls.ToArray () [1].gameObject.SetActive(false);

		balls.ToArray () [0].gameObject.SetActive(true);
		balls.ToArray () [0].transform.position = new Vector2 (-4.57f, -6.53f); 
		balls.ToArray () [0].UpdateMySkin();
		balls.ToArray () [0].Init_first_ball ();
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


	#endregion


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

	public void IEnumeratorforDeath(bool iAmLeft, Vector2 deathPos){
		StartCoroutine(FollowerMariaVaiComAsOutras(iAmLeft, deathPos));
	}

	IEnumerator FollowerMariaVaiComAsOutras (bool iAmLeft, Vector2 deathPos) {

		for(int i = 0; i < globals.s.ACTUAL_SKIN.bandN-1; i++){
			yield return new WaitForSeconds(GD.s.FOLLOWER_DELAY);
			 
			if (iAmLeft) {
				if (followersBall1 [i].isActiveAndEnabled) {
					followersBall1 [i].gameObject.SetActive (false);
					BallMaster.s.CreateExplosion (deathPos);
				} 
				else
					Debug.Log ("THIS SHOULD NEVER HAPPEN");
			} else {
				if (followersBall2 [i].isActiveAndEnabled) {
					followersBall2 [i].gameObject.SetActive (false);
					BallMaster.s.CreateExplosion (deathPos);
				}
				else
					Debug.Log ("THIS SHOULD NEVER HAPPEN");
			}

		}
	}

	public void CreateExplosion(Vector3 pos){
		Instantiate(ballExplosion, pos, transform.rotation);
	}

//	public void AddNewBall(ball_hero b){
//		balls.Add (b);
//	}
//
//	public void RemoveBall(ball_hero b){
//		balls.Remove (b);
//	}


}
