using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System;
using System.Xml;
public class WordVerifyTabajara : MonoBehaviour {
    public static WordVerifyTabajara s = null;
    // Use this for initialization
    // public string word;
    string url = "dictionary.cambridge.org/dictionary/english/";
    string textfieldString;
    StreamWriter outfile;
    bool verify = true;
    //public string word_8_letters_max;

    int actual_word_i = 0;
    int array_word_size = 0;
    int threads_active = 0;
    int output_files_count = 0;

    List<string> allwordsofgame = new List<string>();
    List<string> wordsletters = new List<string>();
    List<string> finallist = new List<string>();

    void Start()
    {
        s = this;

        LoadDictionary("All_Words");
    }

    private void LoadDictionary(string fileName)
    {

        string line;

        TextAsset anagrama = Resources.Load(fileName) as TextAsset;
        //string texto = anagrama.text;

        string[] palavras = anagrama.text.Split("\n"[0]);
        int i = 0;

        do
        {
            line = palavras[i];
            line = line.Replace("\r", "");

            if (line != null && line != "" && line != " " && line != "\n")
            {
                if(line.Length > 3 && line.Length < 8)
                {
                    allwordsofgame.Add(line);
                }
                
                i++;

            }
            else { line = null; }

        } while (i < palavras.Length && line != null);


        Debug.Log("a lista tem " + allwordsofgame.Count + " palavras, boa sorte");
        Debug.Log(allwordsofgame[0]);
        verifyWords(allwordsofgame[0]);
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
         
      //  outfile_with_abbreviations = new StreamWriter("All_Anagrams_File_With_Abbreviations.txt");

        int theards_number = 0;

        if(array_word_size > 200)
        {
            theards_number = 100;
        }
        else
        {
            theards_number = array_word_size - 10;
        }


       // Debug.Log("de : " + 0);
       // Debug.Log("até : " + ((int)array_word_size / theards_number));
        StartCoroutine(verify_cambridge(wordsletters[0].ToLower(), ((int)array_word_size/ theards_number)));
        int multiplicador = 1;
        while (multiplicador < theards_number )
        {
           // Debug.Log("de : " + ((multiplicador * (int)(array_word_size / theards_number)) + 1));
           // Debug.Log("até : " + ((multiplicador + 1) * (int)(array_word_size / theards_number)));
            StartCoroutine(verify_cambridge(wordsletters[((multiplicador * (int)(array_word_size / theards_number)) + 1)].ToLower(), ((multiplicador + 1) * (int)(array_word_size / theards_number))));
            multiplicador++;
            
        }
        if(((multiplicador * (int)(array_word_size / theards_number)) + 1) < array_word_size)
        {
            //Debug.Log("de : " + ((multiplicador * (int)(array_word_size / theards_number)) + 1));
           // Debug.Log("até : " + array_word_size);
            threads_active = theards_number + 1;
            StartCoroutine(verify_cambridge(wordsletters[((multiplicador * (int)(array_word_size / theards_number)) + 1)].ToLower(), array_word_size));
        }
        else
        {
           // Debug.Log("foi no else, n pegou a sobrinha");
            threads_active = theards_number;
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



    public IEnumerator verify_cambridge(string word, int limit)
    {

        if (word == "ability")
        {
            Debug.Log("vai tomar no cu Douglas");
        }
        WWW www = new WWW(url + word);
            yield return www;
            textfieldString = www.text;
        if(textfieldString.Length != 0)
        {
            if (textfieldString.Contains("Page not found"))
            {
                // Debug.Log(word);
                //Debug.Log(" palavra n existe ");
                if (word == "ability")
                {
                    Debug.Log("deu page not found");
                }

            }
            else
            {
                string[] stringSeparators = new string[] { "<span class=\"def-block\">" };
                string[] temp;

                string temp3 = textfieldString;
                temp = textfieldString.Split(stringSeparators, StringSplitOptions.None);

                if (temp.Length > 1)
                {
                    int tempi, count = 1;

                    for (tempi = 0; count != 0 && tempi < temp[1].Length; tempi++)
                    {

                        if (temp[1][tempi] == '<' && temp[1][tempi + 1] == 's' && temp[1][tempi + 2] == 'p')
                        {

                            count++;
                        }
                        if (temp[1][tempi] == '<' && temp[1][tempi + 1] == '/' && temp[1][tempi + 2] == 's' && temp[1][tempi + 3] == 'p')
                        {
                            count--;
                        }
                    }
                    string temp_2_string;
                    temp_2_string = temp[1].Substring(0, tempi);

                    if (temp_2_string.Contains("abbreviation"))
                    {
                        if (word == "ability")
                        {
                            Debug.Log("deu abreviação");
                        }
                        //Debug.Log(word);
                        //Debug.Log(textfieldString);
                        // Debug.Log("ALERTA VERMELHO UIUIUIUI");
                    }
                    else
                    {

                        //Debug.Log("escreve essa porra caraleo que eu to mandanu");
                        //outfile.WriteLine(word);
                        if (word == "ability")
                        {
                            Debug.Log("adicionou, ué");
                        }
                        finallist.Add(word);
                    }

                }
                else
                {

                    //  Debug.Log(word);
                    //Debug.Log(textfieldString.Length);
                    // Debug.Log(temp3.Length);
                    // Debug.Log("NULLLLLLLLLLLLLLLLLLLL");
                    //Debug.Log("escreve essa porra caraleo que eu to mandanu");
                    if (word == "ability")
                    {
                        Debug.Log("adicionou, ué");
                    }
                    finallist.Add(word);
                }


            }

        }


        if (textfieldString.Length != 0)
        {
            actual_word_i++;
        }
        else
        {
            Debug.Log("não veio nada no txt, tentando de novo palavra " + word);
        }

        if (actual_word_i < limit)//array_word_size
        {
                StartCoroutine(verify_cambridge(wordsletters[actual_word_i].ToLower(), limit));
        }
        else
        {
            threads_active--;
            //Debug.Log(threads_active + "    threads ativas");
            if(array_word_size == limit)
            {

                Debug.Log("to rodande de boas");
            }
            if (threads_active == 0)
            {
                //outfile.Close();
                Debug.Log("ENDED THIS WORD");
                write_words_txt_file();
            }
        }
    }

    void write_words_txt_file()
    {
        outfile = new StreamWriter("Word_"+ output_files_count + ".txt");
        
        finallist.Sort();
        string s_ant = "a";
        foreach (string s in SortByLength(finallist))
        {
            if (s == "ability")
            {
                Debug.Log("sou eu papai");
            }
            if (s != s_ant)
            {
                outfile.WriteLine(s);
                if (s == "ability")
                {
                    Debug.Log("to dentro papai");
                }
            }
            else
            {
                if (s == "ability")
                {
                    Debug.Log("não to dentro papai");
                }
            }
            s_ant = s;
        }

        actual_word_i = 0;
        outfile.Close();
        finallist.Clear();
        wordsletters.Clear();
        output_files_count++;

        if (output_files_count < allwordsofgame.Count )
        {
            Debug.Log(allwordsofgame[output_files_count]);
            Debug.Log("Palavra numero " + output_files_count);
            verifyWords(allwordsofgame[output_files_count]);
        }
        else
        {
            Debug.Log("ENDED FOR REAL");
        }
    }
}
