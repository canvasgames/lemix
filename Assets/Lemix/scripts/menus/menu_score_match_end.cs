using UnityEngine;
using System.Collections;

public class menu_score_match_end : MonoBehaviour {
	public int player;
	// Use this for initialization
	void Start () {

		this.GetComponent<Renderer>().sortingLayerID = this.transform.parent.GetComponent<Renderer>().sortingLayerID  ;
	
		GetComponent<TextMesh> ().text = "SCORE   " + GLOBALS.Singleton.MY_SCORE.ToString () + "    " +GLOBALS.Singleton.OP_SCORE.ToString () 
			+ "\nWF    "+ GLOBALS.Singleton.NumberOfWordsFounded.ToString () + "    " +GLOBALS.Singleton.NumberOfWordsFoundedOP.ToString () 
			+ "\nLVL   "+ GLOBALS.Singleton.MY_LVL.ToString () + "    " + GLOBALS.Singleton.OP_LVL.ToString ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
