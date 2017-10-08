using UnityEngine;
using System.Collections;

public class RewardScreen : MonoBehaviour {

	[SerializeField] GameObject[] lightsTopLine, lightsBottomtLine;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable(){
		StartCoroutine (LightAnimations());
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
			if (max > 100)
				break;
		}

	}
}
