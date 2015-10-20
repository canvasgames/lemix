using UnityEngine;
using System.Collections;

public class MainMenuLogics : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Debug.Log("cretteatsas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown() 
	{
		Application.LoadLevel ("Gameplay");
		Debug.Log("LCICKSKCKSKLC");
	}
}
