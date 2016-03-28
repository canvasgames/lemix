using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Word_Filter_Tabajara : MonoBehaviour {

    int actual_file_i = 1;
    int file_in_the_rules_i = 0;
    int file_out_of_the_rules_i = 0;

    List<string> words_this_file = new List<string>();

    StreamWriter outfile;

    // Use this for initialization
    void Start () {
        File.Delete("Word_" + actual_file_i);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void search_word_files()
    {

        string line;
        string filename = "Word_" + actual_file_i;
        TextAsset anagrama = Resources.Load(filename) as TextAsset;
        //string texto = anagrama.text;

        string[] palavras = anagrama.text.Split("\n"[0]);
        int i = 0;

        do
        {
            line = palavras[i];
            line = line.Replace("\r", "");

            if (line != null && line != "" && line != " " && line != "\n")
            {
                if (line.Length > 3 && line.Length < 8)
                {
                    words_this_file.Add(line);
                }

                i++;

            }
            else { line = null; }

        } while (i < palavras.Length && line != null);


        Debug.Log("a lista tem " + words_this_file.Count + " palavras, boa sorte");

        int word_lenght = words_this_file[words_this_file.Count - 1].Length;

        if (word_lenght == 4)
        {
            if(words_this_file.Count >= 8)
            {
                write_words_txt_file(true);
            }
            else
            {
                write_words_txt_file(false);
            }
        }
        else if(word_lenght == 5)
        {
            if (words_this_file.Count >= 12)
            {
                write_words_txt_file(true);
            }
            else
            {
                write_words_txt_file(false);
            }
        }
        else if(word_lenght >= 6)
        {
            if (words_this_file.Count >= 15)
            {
                write_words_txt_file(true);
            }
            else
            {
                write_words_txt_file(false);
            }
        }

    }

    void write_words_txt_file(bool in_the_rules)
    {
        if(in_the_rules == true)
        {
            outfile = new StreamWriter("Word_" + file_in_the_rules_i + ".txt");
            File.Delete("Word_" + actual_file_i);
            file_in_the_rules_i++;
        }
        else
        {
            outfile = new StreamWriter("outr_of_the_rules/Word_" + file_out_of_the_rules_i + ".txt");
            file_out_of_the_rules_i++;
        }


        foreach (string s in words_this_file)
        {
             outfile.WriteLine(s);
        }

        outfile.Close();
        words_this_file.Clear();

        
    }
}
