using UnityEngine;
using System.Collections;

public class CreateSingleton : MonoBehaviour {
	public GameObject single;
	// Use this for initialization
	void Start () {
		GLOBALS[] single2 = FindObjectsOfType (typeof(GLOBALS)) as GLOBALS[];
		if (single2.Length == 0) {
			GameObject obj = (GameObject)Instantiate (single, new Vector3 (0, 0, 0), transform.rotation);
			GLOBALS final = obj.GetComponent<GLOBALS> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
