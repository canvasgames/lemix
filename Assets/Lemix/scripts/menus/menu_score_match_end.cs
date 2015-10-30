using UnityEngine;
using System.Collections;

public class menu_score_match_end : MonoBehaviour {
	public int player;
	// Use this for initialization
	void Start () {

		this.GetComponent<Renderer>().sortingLayerID = this.transform.parent.GetComponent<Renderer>().sortingLayerID  ;
		if(player == 1)
		{
			GetComponent<TextMesh> ().text = GLOBALS.Singleton.MY_SCORE.ToString ();
		}
		else if(player == 2)
		{
			   GetComponent<TextMesh> ().text = GLOBALS.Singleton.OP_SCORE.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
