using UnityEngine;
using System.Collections;

public class WordVerifyTabajara : MonoBehaviour {

    // Use this for initialization
    
    string url = "dictionary.cambridge.org/dictionary/english/aadsddd";
    string textfieldString;
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        textfieldString = www.text;
        Debug.Log(textfieldString);
    }

// Update is called once per frame
void Update () {
	
	}
}
