﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class RewardScreen : MonoBehaviour {

	public bool NotesReward;
	[SerializeField] GameObject[] lightsTopLine, lightsBottomtLine, lightsTopLineOff, lightsBottomtLineOff;
	[SerializeField] GameObject[] myCornerDarks;
	[SerializeField] GameObject[] myButtons;
	[SerializeField] GameObject mySparks;
	[SerializeField] Text myNotesRewardText;
	[SerializeField] Image myNotesRewardIcon;
	public GameObject myReward, myYouJustGot;	// Use this for initialization
	public Text myRewardName;
	Image myImage;


	void Awake () {
		myImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
//		StartCoroutine (LightAnimations());
		if( NotesReward == false) StartCoroutine( EnteringAnimationCharacter ());
		else StartCoroutine( EnteringAnimationNotes());
	}

	void OnDisable(){
		mySparks.SetActive (false);
	}

	IEnumerator EnteringAnimationNotes(){
		Color myColor = myImage.color;
		int yadj = 100;
		mySparks.SetActive (false);
//		myReward.transform.localScale = new Vector2 (0.84f, 0.84f);
//		float y = myReward.transform.localPosition.y;
//		myReward.transform.localPosition = new Vector2 (myReward.transform.localPosition.x, 61);

		//title you just got
		myYouJustGot.transform.localScale = Vector3.zero;

		myNotesRewardText.color = new Color (myNotesRewardText.color.r, myNotesRewardText.color.g, myNotesRewardText.color.b, 0);
		myNotesRewardIcon.color = new Color (myNotesRewardIcon.color.r,myNotesRewardIcon.color.g, myNotesRewardIcon.color.b, 0);

		myNotesRewardText.transform.localPosition = new Vector2 (myNotesRewardText.transform.localPosition.x, myNotesRewardText.transform.localPosition.y + yadj);
		myNotesRewardIcon.transform.localPosition = new Vector2 (myNotesRewardIcon.transform.localPosition.x, myNotesRewardIcon.transform.localPosition.y + yadj);


		//my reward name
//		myRewardName.color = 
//			new Color (myRewardName.color.r, 
//				myRewardName.color.g, 
//				myRewardName.color.b, 0);
//
//		myRewardName.transform.localPosition = new Vector2 (myRewardName.transform.localPosition.x, myRewardName.transform.localPosition.y + 30);
//		//		myRewardName.transform.localScale = Vector2.zero;
//		//		myRewardName.transform.DOScale (Vector2.one, 0.8f);
//
//		myReward.transform.DOScale (new Vector3 (1, 1, 1), 0.7f);
//		myReward.transform.DOLocalMoveY (y, 0.7f);

		//set blue bg alpha 0
		myImage.color = new Color (0, 0, 0 , 0);

		//set corner dark alpha 0 and color black
		Color cor = myCornerDarks [0].GetComponent<Image> ().color;
		cor = new Color (cor.r, cor.g, cor.b, 0);
		cor = myCornerDarks [1].GetComponent<Image> ().color;
		cor = new Color (cor.r, cor.g, cor.b, 0);

		myCornerDarks [0].GetComponent<Image> ().color = new Color (cor.r, cor.g, cor.b, 0);
		myCornerDarks [1].GetComponent<Image> ().color = new Color (cor.r, cor.g, cor.b, 0);

		//set buttons outside
		float localY = myButtons[0].transform.localPosition.y;
		myButtons [0].transform.localPosition = new Vector2 (myButtons [0].transform.localPosition.x, localY - 400);
		float localY2 = myButtons[1].transform.localPosition.y;
		myButtons [1].transform.localPosition = new Vector2 (myButtons [1].transform.localPosition.x, localY2 - 400);

		//ligth group
		foreach (GameObject lgt in lightsBottomtLine) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y - 400);
		}
		foreach (GameObject lgt in lightsBottomtLineOff) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y - 400);
		}
		foreach (GameObject lgt in lightsTopLine) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y + 400);
		}
		foreach (GameObject lgt in lightsTopLineOff) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y + 400);
		}

		yield return new WaitForSeconds (0.5f);

		myImage.DOFade (1, 0.3f).OnComplete( () => myImage.DOColor(myColor,0.3f));

		yield return new WaitForSeconds (0.3f);


		myYouJustGot.transform.DOScale (Vector3.one, 0.8f);

		myNotesRewardIcon.DOFade (1, 0.3f);
		myNotesRewardIcon.transform.DOLocalMoveY (myNotesRewardIcon.transform.localPosition.y - yadj, 0.7f).SetEase (Ease.OutQuad);

		myNotesRewardText.DOFade (1, 0.3f);
		myNotesRewardText.transform.DOLocalMoveY (myNotesRewardText.transform.localPosition.y - yadj, 0.7f).SetEase (Ease.OutQuad);

		myCornerDarks [0].GetComponent<Image> ().DOFade (1, 0.5f);
		myCornerDarks [1].GetComponent<Image> ().DOFade (1, 0.5f);

//		myRewardName.GetComponent<Text> ().DOFade (1, 0.3f);
//		myRewardName.transform.DOLocalMoveY (0, 0.7f).SetEase (Ease.OutQuad);

		//		myCornerDarks[0].GetComponent<Image>().color = new Color (myImage.color.a, myImage.color.g, myImage.color.b, 0);

		myButtons [0].transform.DOLocalMoveY (localY, 0.5f).SetEase (Ease.OutQuad);
		myButtons [1].transform.DOLocalMoveY (localY2, 0.5f).SetEase (Ease.OutQuad);

		yield return new WaitForSeconds (0.3f);

		mySparks.SetActive (true);

		//ligth group
		for (int i = 0; i < lightsBottomtLine.Length/2; i++) {
			//			Ease ease = QA.s.ease1;
			Ease ease = Ease.OutQuad;
			lightsBottomtLine[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsBottomtLineOff[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

			lightsBottomtLine[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsBottomtLineOff[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

			lightsTopLine[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsTopLineOff[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

			lightsTopLine[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsTopLineOff[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

			//			lightsBottomtLine[i].transform.DOLocalMoveY(0, 0.15f);
			//			lightsBottomtLineOff[i].transform.DOLocalMoveY(0, 0.15f);
			//
			//			lightsBottomtLine[lightsBottomtLine.Length-i-1].transform.DOLocalMoveY(0, 0.15f);
			//			lightsBottomtLineOff[lightsBottomtLine.Length-i-1].transform.DOLocalMoveY(0, 0.15f);

			yield return new WaitForSeconds (0.02f);
		}
		//		foreach (GameObject lgt in lightsBottomtLine) {
		//			lgt.transform.DOLocalMoveY(0, 0.15f);
		//		}
		//		foreach (GameObject lgt in lightsBottomtLineOff) {
		//			lgt.transform.DOLocalMoveY(0, 0.15f);
		//			yield return new WaitForSeconds (0.01f);
		//		}
		StartCoroutine (LightAnimations ());
	}

	IEnumerator EnteringAnimationCharacter(){
		Color myColor = myImage.color;

		mySparks.SetActive (false);
		myReward.transform.localScale = new Vector2 (0.84f, 0.84f);
		float y = myReward.transform.localPosition.y;
		myReward.transform.localPosition = new Vector2 (myReward.transform.localPosition.x, 61);

		//title you just got
		myYouJustGot.transform.localScale = Vector3.zero;

		//my reward name
		myRewardName.color = new Color (myRewardName.color.r, myRewardName.color.g, myRewardName.color.b, 0);

		myRewardName.transform.localPosition = new Vector2 (myRewardName.transform.localPosition.x, myRewardName.transform.localPosition.y + 30);
//		myRewardName.transform.localScale = Vector2.zero;
//		myRewardName.transform.DOScale (Vector2.one, 0.8f);

		myReward.transform.DOScale (new Vector3 (1, 1, 1), 0.7f);
		myReward.transform.DOLocalMoveY (y, 0.7f);

		//set blue bg alpha 0
		myImage.color = new Color (0, 0, 0 , 0);

		//set corner dark alpha 0 and color black
		Color cor = myCornerDarks [0].GetComponent<Image> ().color;
		cor = new Color (cor.r, cor.g, cor.b, 0);
		cor = myCornerDarks [1].GetComponent<Image> ().color;
		cor = new Color (cor.r, cor.g, cor.b, 0);
	
		myCornerDarks [0].GetComponent<Image> ().color = new Color (cor.r, cor.g, cor.b, 0);
		myCornerDarks [1].GetComponent<Image> ().color = new Color (cor.r, cor.g, cor.b, 0);

		//set buttons outside
		float localY = myButtons[0].transform.localPosition.y;
		myButtons [0].transform.localPosition = new Vector2 (myButtons [0].transform.localPosition.x, localY - 400);
		float localY2 = myButtons[1].transform.localPosition.y;
		myButtons [1].transform.localPosition = new Vector2 (myButtons [1].transform.localPosition.x, localY2 - 400);

		//ligth group
		foreach (GameObject lgt in lightsBottomtLine) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y - 400);
		}
		foreach (GameObject lgt in lightsBottomtLineOff) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y - 400);
		}
		foreach (GameObject lgt in lightsTopLine) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y + 400);
		}
		foreach (GameObject lgt in lightsTopLineOff) {
			lgt.transform.localPosition = new Vector2 (lgt.transform.localPosition.x, lgt.transform.localPosition.y + 400);
		}

		yield return new WaitForSeconds (0.5f);

		myImage.DOFade (1, 0.3f).OnComplete( () => myImage.DOColor(myColor,0.3f));

		yield return new WaitForSeconds (0.3f);

		myYouJustGot.transform.DOScale (Vector3.one, 0.8f);


		myCornerDarks [0].GetComponent<Image> ().DOFade (1, 0.5f);
		myCornerDarks [1].GetComponent<Image> ().DOFade (1, 0.5f);

		myRewardName.GetComponent<Text> ().DOFade (1, 0.3f);
		myRewardName.transform.DOLocalMoveY (0, 0.7f).SetEase (Ease.OutQuad);

//		myCornerDarks[0].GetComponent<Image>().color = new Color (myImage.color.a, myImage.color.g, myImage.color.b, 0);

		myButtons [0].transform.DOLocalMoveY (localY, 0.5f).SetEase (Ease.OutQuad);
		myButtons [1].transform.DOLocalMoveY (localY2, 0.5f).SetEase (Ease.OutQuad);

		yield return new WaitForSeconds (0.3f);

		mySparks.SetActive (true);

		//ligth group
		for (int i = 0; i < lightsBottomtLine.Length/2; i++) {
//			Ease ease = QA.s.ease1;
			Ease ease = Ease.OutQuad;
			lightsBottomtLine[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsBottomtLineOff[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

			lightsBottomtLine[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsBottomtLineOff[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

			lightsTopLine[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsTopLineOff[lightsBottomtLine.Length/2 - i-1].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

			lightsTopLine[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);
			lightsTopLineOff[lightsBottomtLine.Length/2 + i].transform.DOLocalMoveY(0, 0.15f).SetEase(ease);

//			lightsBottomtLine[i].transform.DOLocalMoveY(0, 0.15f);
//			lightsBottomtLineOff[i].transform.DOLocalMoveY(0, 0.15f);
//
//			lightsBottomtLine[lightsBottomtLine.Length-i-1].transform.DOLocalMoveY(0, 0.15f);
//			lightsBottomtLineOff[lightsBottomtLine.Length-i-1].transform.DOLocalMoveY(0, 0.15f);

			yield return new WaitForSeconds (0.02f);
		}
//		foreach (GameObject lgt in lightsBottomtLine) {
//			lgt.transform.DOLocalMoveY(0, 0.15f);
//		}
//		foreach (GameObject lgt in lightsBottomtLineOff) {
//			lgt.transform.DOLocalMoveY(0, 0.15f);
//			yield return new WaitForSeconds (0.01f);
//		}
		StartCoroutine (LightAnimations ());
	}


	IEnumerator LightAnimations(){
		int curLine = 0;
		int max = 0;
		for (int i = 0; ;i++) {
			if (1==1 || globals.s.curGameScreen == GameScreen.Store) {
				lightsTopLine [i].SetActive (false);
				lightsBottomtLine [i].SetActive (false);
//				lightsTopLine [i].SetActive (true);
//				lightsBottomtLine [i].SetActive (true);
				yield return new WaitForSeconds (0.035f);

				lightsTopLine [i].SetActive (true);
				lightsBottomtLine [i].SetActive (true);
//				lightsTopLine [i].SetActive (false);
//				lightsBottomtLine [i].SetActive (false);


				if (i == lightsTopLine.Length-1) {
					i = -1;
					yield return new WaitForSeconds (0.17f);
				}


			} else
				break;

			max++;
			if (max > 10000)
				break;
		}
	}

	public void SetMyRewardChar(RuntimeAnimatorController anim, string name){
		myReward.GetComponent<Animator>().runtimeAnimatorController = anim;
		myRewardName.text = name;
	}
}
