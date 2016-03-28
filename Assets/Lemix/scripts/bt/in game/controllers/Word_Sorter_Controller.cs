using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine.UI;


public class Word_Sorter_Controller : MonoBehaviour {
    public static Word_Sorter_Controller s = null;
    int numberOfFiles;
    string tempWords, tempWordsOP;

    //Temp list to receive the string of founded words
    List<string> idsList = new List<string>();

    // Use this for initialization
    void Start () {
        s = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Just the Host call this method
	public int sortWordAndReturnAnagramID(string wordsOP)
	{
        int anagram_id = 0;

        //Verify the language
        verify_language();

        //Define temp variables
        tempWordsOP = wordsOP;
        tempWords = PlayerPrefs.GetString("WordsAlreadySorted");

        //Put a "," between my wordlist and opwordlist
        if(tempWords != "" && tempWordsOP != "")
            tempWords = tempWords + "," + tempWordsOP;
        //If my word list is empty
        else if(tempWordsOP != "")
            tempWords =  tempWordsOP;

        //If temp word have sorted words
        if (tempWords != "")
        {
            anagram_id = search_no_sorted_word();
        }
        //Temp words empty, just sort all the list of files
        else
        {
            anagram_id = Random.Range(1, numberOfFiles + 1);
        }

       //Add anagram ID to playerPrefs and to the Global
       add_to_sorted_list(anagram_id);

        //Return the sorted word id
        return anagram_id;
	}

    //Searches a word not sorted for both player
    public int search_no_sorted_word()
    {
        int anagram = 1;
        // PlayerPrefs.SetInt("WordsFounded", tempWords)
        idsList.Clear();
        idsList = tempWords.Split(',').ToList();
        Debug.Log(tempWords + " LISTA DAS PALAVRINHAS JA SORTEADAS");

        int i = 0;
        int nOnTheList;
        bool alreadyInTheList = false;
        //Search a not sorted word in the list

        while (i < 100)
        {
            anagram = Random.Range(1, numberOfFiles + 1);
            i++;
            foreach (string id in idsList)
            {

                nOnTheList = System.Convert.ToInt32(id);

                if (nOnTheList == anagram)
                {
                    alreadyInTheList = true;
                    break;
                }
            }

            //Verify if word was found in the foreach
            if (alreadyInTheList == true)
            {
                //Sort again
                alreadyInTheList = false;
            }
            //Else new word found, PARA TUDOOOOOOOOOOOOOOOOOOOOO
            else
            {
                return anagram;
            }

            //While ended, dont sort last word at least
            if (i == 100)
            {
                if (anagram == GLOBALS.Singleton.ANAGRAM_ID)
                {
                    if (anagram < numberOfFiles)
                    {
                        anagram++;
                    }
                    else
                    {
                        anagram--;
                    }
                }
            }
        }
        return anagram;
    }

    //Add the word sorted to playerPrefs and to ANAGRAM_ID Global
    public void  add_to_sorted_list(int sortedWord)
    {
        string wordList, tempStringMyWords;
       
        tempStringMyWords = PlayerPrefs.GetString("WordsAlreadySorted");
        //Add word to words list
        if (tempStringMyWords != "")
        {
            idsList.Clear();
            idsList = tempStringMyWords.Split(',').ToList();

            //Gambiarra if the number of files is less than 20
            if (numberOfFiles < 20)
            {
                if (idsList.Count >= 3)
                {
                    //Delete the first, add the word to the end of the list
                    idsList.RemoveAt(0);
                    idsList.Add(sortedWord.ToString());
                }
                else
                {
                    idsList.Add(sortedWord.ToString());
                }
            }
            else
            {
                //This will be happen if have more than 20 files
                if (idsList.Count >= numberOfFiles / 10)
                {
                    //Delete the first, add the word to the end of the list
                    idsList.RemoveAt(0);
                    idsList.Add(sortedWord.ToString());
                }
                else
                {
                    idsList.Add(sortedWord.ToString());
                }
            }

            //Receive the idlist of List and create the string of words
            wordList = "";
            foreach (string id in idsList)
            {
                if (wordList != "")
                    wordList = wordList + "," + id;
                else
                    wordList = id;
            }
        }
        //If is the first word on PlayerPrefs
        else
        {
            wordList = sortedWord.ToString();
        }

        //Add to sorted PlayerPref and define the Global
        PlayerPrefs.SetString("WordsAlreadySorted", wordList);
        GLOBALS.Singleton.ANAGRAM_ID = sortedWord;

        //Debug
        Debug.Log(wordList + " LISTA DE PALAVRAS JA SORTEADAS ADICIONADAS");
    }

    //Method for the Guest add the word to your playerPrefs
    public void addSortedWordOP(int word_id)
    {
        verify_language();

        //Add word id to sorted list
        add_to_sorted_list(word_id);



        //string receive = PlayerPrefs.GetString("WordsAlreadySorted");

        //if(receive != "")
        //  receive = receive + "," + word_id.ToString();
        //else
        //   receive = word_id.ToString();

       // PlayerPrefs.SetString("WordsAlreadySorted", receive);
       // GLOBALS.Singleton.ANAGRAM_ID = word_id;
    }

    void verify_language()
    {
        //IF ENGLISH
        if (GLOBALS.Singleton.LANGUAGE == 0)
            numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesENG;
        //IF PORTUGUESE
        else if (GLOBALS.Singleton.LANGUAGE == 1)
            numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesPORT;
        else
            numberOfFiles = GLOBALS.Singleton.NumberOfWordFilesENG;
    }
}
