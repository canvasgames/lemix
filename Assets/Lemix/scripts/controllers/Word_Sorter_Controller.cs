using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine.UI;


public class Word_Sorter_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int sortWordAndReturnAnagramID(string wordsOP )
	{
		int anagram_id = 0;
		int numberOfFiles;

        List<string> idsList = new List<string>();

        string tempWords = PlayerPrefs.GetString("WordsAlreadySorted");
        tempWords = tempWords + wordsOP;

        // PlayerPrefs.SetInt("WordsFounded", tempWords)
        idsList = tempWords.Split(',').ToList();

        //IF ENGLISH
        if (GLOBALS.Singleton.LANGUAGE == 0)
			numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesENG;
		//IF PORTUGUESE
		else if(GLOBALS.Singleton.LANGUAGE == 1)
			numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesPORT;
		else 
			numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesENG;

        int i=0;
        int result;

        //Search a not sorted word in the list
        while (i < 100)
        {
            anagram_id = Random.Range(1, numberOfFiles+1);
            i++;
            foreach (string id in idsList)
            { 
                result = System.Convert.ToInt32(id);
                if (result == anagram_id)
                {
                    i = 1000;
                }
            }

            //While ended, dont sort last word
            if(i==100)
            {
                if (anagram_id == GLOBALS.Singleton.ANAGRAM_ID)
                {
                    if (anagram_id < numberOfFiles)
                    {
                        anagram_id++;
                    }
                    else
                    {
                        anagram_id--;
                    }
                }
            }
        }


        //Add sorted word to list of sorted words
        tempWords = PlayerPrefs.GetString("WordsAlreadySorted");
        idsList.Clear();
        idsList = tempWords.Split(',').ToList();

        //Gambiarra if the number fo files is less than 20
        if (numberOfFiles < 20)
        {
            if (idsList.Count >= 3)
            {
                idsList.RemoveAt(0);
                idsList.Add(anagram_id.ToString());
            }
        }
        else
        {
            //This will be happen if have more than 20 files
            if (idsList.Count >= numberOfFiles / 10)
            {
                idsList.RemoveAt(0);
                idsList.Add(anagram_id.ToString());
            }
        }

        //Receive the idlist of LIst and save in the Playerprefs
        string receive = "";
        foreach (string id in idsList)
        {
            receive = receive + id;
        }
        PlayerPrefs.SetString("WordsAlreadySorted", receive);

        //Set my global and return the word id
        GLOBALS.Singleton.ANAGRAM_ID = anagram_id;
        return GLOBALS.Singleton.ANAGRAM_ID;
	}

    public void addSortedWordOP(int word_id)
    {
        //Add word id to sorted list
        string receive = PlayerPrefs.GetString("WordsAlreadySorted");
        receive = receive + word_id.ToString();
        PlayerPrefs.SetString("WordsAlreadySorted", receive);
        GLOBALS.Singleton.ANAGRAM_ID = word_id;
    }
}
