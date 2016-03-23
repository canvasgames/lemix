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
    StreamWriter outfile, outfile_with_abbreviations;
    bool verify = true;
    public string word_8_letters_max;
    int actual_word_i = 0;
    int array_word_size = 0;
    int threads_active = 0;

    List<string> wordsletters = new List<string>();

    void Start()
    {
        s = this;
        
        verifyWords(word_8_letters_max);
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
        array_word_size = wordsletters.Count;
         outfile = new StreamWriter("All_Anagrams_File.txt");
      //  outfile_with_abbreviations = new StreamWriter("All_Anagrams_File_With_Abbreviations.txt");

        int theards_number = 0;

        if(array_word_size > 200)
        {
            theards_number = 200;
        }
        else
        {
            theards_number = array_word_size - 10;
        }

        threads_active = theards_number+1;

        StartCoroutine(verify_cambridge(wordsletters[0].ToLower(), ((int)array_word_size/ theards_number)));
        Debug.Log(" começaaaaaaaa " + ((int)array_word_size / theards_number));
        int multiplicador = 1;
        while (multiplicador < theards_number )
        {
            
            StartCoroutine(verify_cambridge(wordsletters[((multiplicador * (int)(array_word_size / theards_number)) + 1)].ToLower(), ((multiplicador + 1) * (int)(array_word_size / theards_number))));
            multiplicador++;
            
        }
        if(((multiplicador * (int)(array_word_size / theards_number)) + 1) < array_word_size)
        {
            StartCoroutine(verify_cambridge(wordsletters[((multiplicador * (int)(array_word_size / theards_number)) + 1)].ToLower(), array_word_size));
        }
        

        // StartCoroutine(verify_cambridge(wordsletters[((multiplicador * (int)(array_word_size / theards_number)) + 1)].ToLower(), array_word_size));

        /*
              StartCoroutine(verify_cambridge(wordsletters[0].ToLower(), ((int)array_word_size/ theards_number)));
        StartCoroutine(verify_cambridge(wordsletters[((int)array_word_size / theards_number) +1].ToLower(), (2*(int)array_word_size / theards_number)));
        StartCoroutine(verify_cambridge(wordsletters[((2 * (int)(array_word_size / theards_number))+1)].ToLower(), (3 * (int)(array_word_size / theards_number))));
        StartCoroutine(verify_cambridge(wordsletters[((3 * (int)(array_word_size / theards_number)) + 1)].ToLower(), array_word_size));
        
        
        
        for (pp = 0; pp < 10; pp++)
        {
            if(active_courotines >= 20)
            {
                pp--;
            }
            else
            {
                StartCoroutine(verify_cambridge(wordsletters[pp].ToLower(), pp));
            }
            
        }*/

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



    public IEnumerator verify_cambridge(string word, int limit)
    {

        WWW www = new WWW(url + word);
            yield return www;
            textfieldString = www.text;
        
        if (textfieldString[115] == 'n' && textfieldString[116] == 'o' && textfieldString[117] == 't')
        {
            //Debug.Log(" palavra n existe ");


        }
        else
        {
            Debug.Log(" ");
            Debug.Log("EXISTE AAAAAAAAAAAAA " + textfieldString.Length);

            //outfile.WriteLine(word);
            //outfile_with_abbreviations.WriteLine(word);
            Debug.Log(url + word);
            Debug.Log(textfieldString);
            if (textfieldString.Contains(": abbreviation") == true)
            {
                if (word == "cast")
                {
                    Debug.Log("ALERTA VERMELHO UIUIUIUI");
                  

                }
                Debug.Log("Abre o viado" + textfieldString.Length);
                //outfile.WriteLine(word);
            }
            else
            {
                if (word == "cast")
                {
                    Debug.Log("ALERTA VERDE UIUIUIUI");
                }
                Debug.Log("escreve essa porra caraleo que eu to mandanu");
                outfile.WriteLine(word);
            }
            Debug.Log(" ");
        }

        actual_word_i++;
        if (actual_word_i < limit)//array_word_size
        {
            StartCoroutine(verify_cambridge(wordsletters[actual_word_i].ToLower(), limit));
        }
        else
        {
            threads_active--;
            if(threads_active == 0)
            {
                outfile.Close();
            }
        }

    }
}
