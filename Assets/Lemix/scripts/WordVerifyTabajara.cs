using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

public class WordVerifyTabajara : MonoBehaviour {
    public static WordVerifyTabajara s = null;
    // Use this for initialization
    // public string word;
    string url = "dictionary.cambridge.org/dictionary/english/";
    string textfieldString;
    StreamWriter outfile;
    bool verify = true;

    void Start()
    {
        s = this;
        
        verifyWords("ANAGRAM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Pega a palavra e imprime todas sub-palavras existentes (ate 8 letras)
    public void verifyWords(string word2verify)
    {
        int i, j, k, l, m, n, o, p;
        int maxlenght = word2verify.Length;
        string tempString;

        List<string> wordsletters = new List<string>();



        bool verify_is_in_list;

        if (maxlenght > 2)
        {
            for (i = 0; i < maxlenght; i++)
            {
                for (j = 0; j < maxlenght; j++)
                {
                    for (k = 0; k < maxlenght; k++)
                    {
                        if (i != j && i != k && j != k)
                        {
                            tempString = "";
                            tempString = tempString + word2verify[i];
                            tempString = tempString + word2verify[j];
                            tempString = tempString + word2verify[k];

                           // StartCoroutine(verify_cambridge(tempString));


                            if (verify == true)
                            {
                                verify_is_in_list = wordsletters.Contains(tempString);
                                if (verify_is_in_list == false)
                                    wordsletters.Add(tempString);

                            }
                            if (maxlenght > 3)
                            {
                                for (l = 0; l < maxlenght; l++)
                                {
                                    if (l != i && l != j && l != k)
                                    {
                                        tempString = "";
                                        tempString = tempString + word2verify[i];
                                        tempString = tempString + word2verify[j];
                                        tempString = tempString + word2verify[k];
                                        tempString = tempString + word2verify[l];

                                     //   StartCoroutine(verify_cambridge(tempString));

                                        if (verify == true)
                                        {
                                            verify_is_in_list = wordsletters.Contains(tempString);
                                            if (verify_is_in_list == false)
                                                wordsletters.Add(tempString);

                                        }

                                        if (maxlenght > 4)
                                        {
                                            for (m = 0; m < maxlenght; m++)
                                            {
                                                if (m != i && m != j && m != k && m != l)
                                                {
                                                    tempString = "";
                                                    tempString = tempString + word2verify[i];
                                                    tempString = tempString + word2verify[j];
                                                    tempString = tempString + word2verify[k];
                                                    tempString = tempString + word2verify[l];
                                                    tempString = tempString + word2verify[m];

                                                 //   StartCoroutine(verify_cambridge(tempString));

                                                    if (verify == true)
                                                    {
                                                        verify_is_in_list = wordsletters.Contains(tempString);
                                                        if (verify_is_in_list == false)
                                                            wordsletters.Add(tempString);

                                                    }

                                                    if (maxlenght > 5)
                                                    {
                                                        for (n = 0; n < maxlenght; n++)
                                                        {
                                                            if (n != i && n != j && n != k && n != l && n != m)
                                                            {
                                                                tempString = "";
                                                                tempString = tempString + word2verify[i];
                                                                tempString = tempString + word2verify[j];
                                                                tempString = tempString + word2verify[k];
                                                                tempString = tempString + word2verify[l];
                                                                tempString = tempString + word2verify[m];
                                                                tempString = tempString + word2verify[n];

                                                             //   StartCoroutine(verify_cambridge(tempString));

                                                                if (verify == true)
                                                                {
                                                                    verify_is_in_list = wordsletters.Contains(tempString);
                                                                    if (verify_is_in_list == false)
                                                                        wordsletters.Add(tempString);

                                                                }

                                                                if (maxlenght > 6)
                                                                {
                                                                    for (o = 0; o < maxlenght; o++)
                                                                    {
                                                                        if (o != i && o != j && o != k && o != l && o != m && o != n)
                                                                        {
                                                                            tempString = "";
                                                                            tempString = tempString + word2verify[i];
                                                                            tempString = tempString + word2verify[j];
                                                                            tempString = tempString + word2verify[k];
                                                                            tempString = tempString + word2verify[l];
                                                                            tempString = tempString + word2verify[m];
                                                                            tempString = tempString + word2verify[n];
                                                                            tempString = tempString + word2verify[o];

                                                                    //        StartCoroutine(verify_cambridge(tempString));

                                                                            if (verify == true)
                                                                            {
                                                                                verify_is_in_list = wordsletters.Contains(tempString);
                                                                                if (verify_is_in_list == false)
                                                                                    wordsletters.Add(tempString);

                                                                            }

                                                                            if (maxlenght > 7)
                                                                            {
                                                                                for (p = 0; p < maxlenght; p++)
                                                                                {
                                                                                    if (p != i && p != j && p != k && p != l && p != m && p != n && p != o)
                                                                                    {
                                                                                        tempString = "";
                                                                                        tempString = tempString + word2verify[i];
                                                                                        tempString = tempString + word2verify[j];
                                                                                        tempString = tempString + word2verify[k];
                                                                                        tempString = tempString + word2verify[l];
                                                                                        tempString = tempString + word2verify[m];
                                                                                        tempString = tempString + word2verify[n];
                                                                                        tempString = tempString + word2verify[o];
                                                                                        tempString = tempString + word2verify[p];

                                                                                      //  StartCoroutine(verify_cambridge(tempString));


                                                                                        if (verify == true)
                                                                                        {
                                                                                            verify_is_in_list = wordsletters.Contains(tempString);
                                                                                            if (verify_is_in_list == false)
                                                                                                wordsletters.Add(tempString);

                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
        Debug.Log("PRIMINDO " + wordsletters.Count + " PALAVRINHAS MAROTAS");
        int pp = 0;
         outfile = new StreamWriter("All_Anagrams_File.txt");
        for (pp = 0; pp < 1000; pp++)
        {
            StartCoroutine(verify_cambridge(wordsletters[pp].ToLower(), pp));
        }

        /*    foreach (string s in wordsletters)
            {
                Debug.Log("verificando palavra " + s);

                StartCoroutine(verify_cambridge(s));

            }*/

        /* StreamWriter outfile = new StreamWriter("All_Anagrams_File.txt");
         foreach (string s in SortByLength(wordsletters))
         {
             Debug.Log(s);
             outfile.WriteLine(s);
         }
         outfile.Close();*/

    }

    public void click()
    {
        outfile.Close();
    }
    static IEnumerable<string> SortByLength(IEnumerable<string> e)
    {
        // Use LINQ to sort the array received and return a copy.
        var sorted = from s in e
                     orderby s.Length ascending
                     select s;
        return sorted;
    }



    public IEnumerator verify_cambridge(string word, int i)
    {
        
            WWW www = new WWW(url + word);
            yield return www;
            textfieldString = www.text;
        Debug.Log("VISH TRETETETETEAAAAAAA "+ word);
        if (textfieldString[115] == 'n' && textfieldString[116] == 'o' && textfieldString[117] == 't')
        {
            Debug.Log(i+" palavra n existe " + word);

        }
        else
        {
            Debug.Log(i + "EXISTE AAAAAAAAAAAAA " + word);
            outfile.WriteLine(word);
        }


    }
}
