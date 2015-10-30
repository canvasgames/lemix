using UnityEngine;
using System.Collections;

public class Word_Sorter_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int sortWordAndReturnAnagramID()
	{
		int anagram_id;
		int numberOfFiles;

		//IF ENGLISH
		if(GLOBALS.Singleton.LANGUAGE == 0)
			numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesENG;
		//IF PORTUGUESE
		else if(GLOBALS.Singleton.LANGUAGE == 1)
			numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesPORT;
		else 
			numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesENG;

		anagram_id = Random.Range (1, numberOfFiles);

		GLOBALS.Singleton.ANAGRAM_ID = anagram_id;

		return anagram_id;
	}
}
