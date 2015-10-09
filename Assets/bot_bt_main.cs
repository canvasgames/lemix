using UnityEngine;
using System.Collections;

public class bot_bt_main : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pressed()
	{
		SAFFER.Singleton.Reset_Globals ();
		Application.LoadLevel("Gameplay");
	}
}
