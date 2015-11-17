using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class mm_statistics : MonoBehaviour {
	public Button tecu; 
	// Use this for initialization
	void Start () {
		int tempWins = PlayerPrefs.GetInt ("NumberofWins");
		int level;
		int whatLevel = 1;
		//tecu.
		//Discover the user level ----- x = n((n+1)/2) ------ n is the level x is the number of victories
		if (tempWins == 0)
			level = 0;
		else 
		{
			while((whatLevel*((whatLevel+1)/2)) < tempWins)
			{
				whatLevel++;
			}

			if(whatLevel!=1)
				level = whatLevel - 1;
			else
				level = 1;
		}

		int tempMatches = PlayerPrefs.GetInt ("NumberofMatches");
		int tempWords  = PlayerPrefs.GetInt ("WordsFounded");
		int tempStreak = PlayerPrefs.GetInt("WinStreak");


		GetComponent<Text> ().text = "STATISTICS \n\nLEVEL " + level + "\nMATCHES " + tempMatches 
			+ "\nWINS " + tempWins + "\nWIN STREAK " +  tempStreak+ "\nWORDS FOUNDED " + tempWords;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Destroy (transform.gameObject);

	}
}
