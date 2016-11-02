using UnityEngine;
using System.Collections;

public class SuperJumpEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().Stop ();
		GetComponent<Animator> ().Play ("SuperJumpEffectAnimation");
		Invoke("InitMe", Random.Range(0, 0.5f));
	}
	public void InitMe(){
		Debug.Log ("start engine");
		//GetComponent<Animator> ().SetTrigger ("play");
		GetComponent<Animator> ().Play ("SuperJumpEffectAnimation");
		//GetComponent<Animator> ().pl;

	}
	// Update is called once per frame
	void Update () {
	
	}
}