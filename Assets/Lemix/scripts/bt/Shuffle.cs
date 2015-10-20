using UnityEngine;
using System.Collections;

public class Shuffle : MonoBehaviour {
	public int state;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() 
	{
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		wordCTRL[0].reorganize();
	}
}
