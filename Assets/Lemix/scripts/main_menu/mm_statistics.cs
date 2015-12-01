using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mm_statistics : MonoBehaviour {

	// Use this for initialization
	void Start () {

		int tempMatches = PlayerPrefs.GetInt ("NumberofMatches");
		int tempWords  = PlayerPrefs.GetInt ("WordsFounded");
		int tempStreak = PlayerPrefs.GetInt("WinStreak");
        int tempWins = PlayerPrefs.GetInt("NumberofWins");

        int level = GLOBALS.Singleton.actualLevel();

        this.GetComponent<Text> ().text = "STATISTICS \n\nLEVEL " + level + "\nMATCHES " + tempMatches 
			+ "\nWINS " + tempWins + "\nWIN STREAK " +  tempStreak+ "\nWORDS FOUNDED " + tempWords;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		//Destroy (transform.gameObject);

	}
}
