﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  
using System.Linq;

using Thinksquirrel.WordGameBuilder;
using Thinksquirrel.WordGameBuilder.Gameplay;
using Thinksquirrel.WordGameBuilder.ObjectModel;
using DG.Tweening;
using UnityEngine.UI;

public class WController : MonoBehaviour {
	Word_Sorter_Controller[] wSort;
	private string word_original = "";
	public string word = ""; 
    int magicalBarScaleNumber = 4;
	//Array de chars com os tiles na mesa
	public char[] atable = new char[10];
	

	//Lista de palavras
	public List<MyWordInList> list = new List<MyWordInList>();
	public float numberofWords;
	private float wordsFounded;

	//Ballons
	Vector3 scaleB, walreadypos, wnotfoundpos, wfoundpointspos;
	//int triggerBallonP1 =0, triggerBallonP2 =0;
	float wAlreadyOrNotFinaltime = 0.6f;
	float wAlreadyOrNotFinalpos = -130f;
	GameObject BalonP1,BalonP2, PowerBarP1, PowerBarP2, wfounded;
	public float smooth;

	public GameObject walready, wnotfound, wfoundpoints; 

	//Classe da lista de palavras
	public class MyWordInList
	{
		public string myWord { get; set; }
		public bool found { get; set; }
		public int foundedByPlayer{ get; set; }
		public int goldLetterActive{ get; set; }
	}

	public string word_original_ret()
	{
		return word_original;
	}

	// Use this for initialization
	void Start () {
		//this.gameObject.AddComponent<PhotonView>();

		//Quantos arquivos tem a biblioteca de palavras
		//int numberOfFiles = GLOBALS.Singleton.NumberOfWordFiles;
		//Debug.Log(numberOfFiles);
		//Sorteia um dos arquivos de palavras
		wSort = FindObjectsOfType(typeof(Word_Sorter_Controller)) as Word_Sorter_Controller[];


		//Feedback position
		wnotfoundpos = wnotfound.transform.position;
		walreadypos = walready.transform.position;
		wfoundpointspos = wfoundpoints.transform.position;

		wnotfound.GetComponent<SpriteRenderer>().DOFade(0f,0f);
		walready.GetComponent<SpriteRenderer>().DOFade(0f,0f);
		//wfoundpoints.transform.DOFade(0f,0f);


		//Ballon adjust
		BalonP1 = GameObject.Find ("feed_baloon_blue"); 
		BalonP2 = GameObject.Find ("feed_baloon_red"); 
		PowerBarP1 = GameObject.Find ("hud_bar_blue"); 
		PowerBarP2 = GameObject.Find ("hud_bar_red"); 
		scaleB = BalonP1.transform.localScale;

		BalonP1.transform.localScale = new Vector3(0f, 0f, 1);
		BalonP2.transform.localScale = new Vector3(0f, 0f, 1);


		smooth = 0.8f;
		wfounded = GameObject.Find ("hud_words_found_counter"); 
		wfounded.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10; 

		int rand = GLOBALS.Singleton.ANAGRAM_ID;
		// Sort the word file if doesnt have one sorted
		if (rand == 0) {
			
            if(QA.s.can_choose_txt_file == true)
            {
                rand = QA.s.choose_txt_file_number;
            }
            else
            {
                Debug.Log("WORD ID Vazia, sorteando nova palavra");
                rand = wSort[0].sortWordAndReturnAnagramID("");
            }
		}

		Debug.Log("ANAGRAM ID: " + rand);
		
		string number = rand.ToString();
		//number = "1";

		//Open the word file
		string file;

		if(GLOBALS.Singleton.LANGUAGE == 0)
			file = "Word_" + number;
		else if(GLOBALS.Singleton.LANGUAGE == 1)
			file = "pt_Word_" + number;
		else
			file = "Word_" + number;


		//file = "Word_666";

		//string path = Application.dataPath;
		//string file = "pt_Word_" + number;
		//string file = path+"/Word_" + number +".txt";

		//var anagrama = Resources.Load(file) as TextAsset;
		//Debug.Log ("PRINTING FILE CONTENT FROM " + file + ": ");
		//Debug.Log (anagrama);

		LoadDictionary(file);

		//Seta variaveis das palavras
		int numberofWordstemp;
		
		numberofWordstemp = (int)numberofWords;
		word_original = list[numberofWordstemp-1].myWord;
//		Debug.Log (word_original + "ASDAS");
		word = word_original;
		
		
		Submit_And_Input_Ctrl[] submitScp = FindObjectsOfType(typeof(Submit_And_Input_Ctrl)) as Submit_And_Input_Ctrl[];
		ConstructTable[] constTab = FindObjectsOfType(typeof(ConstructTable)) as ConstructTable[];
		submitScp[0].ResizeandReposite();
		constTab[0].construct();

		f5WordsFounded();
	}
	
	// Update is called once per frame
	void Update () {
		//ballonsstatus();
	}
	
	private bool LoadDictionary(string fileName)
	{
		// Handle any problems that might arise when reading the text
	//	Debug.Log ("LOAD DICTIONARY CALLED!");
		string line;
		// Create a new StreamReader, tell it which file to read and what encoding the file
		// was saved as
		//StreamReader theReader = new StreamReader(fileName+".txt", Encoding.Default);

		TextAsset anagrama = Resources.Load(fileName) as TextAsset;
		string texto = anagrama.text;

		string[] palavras = anagrama.text.Split("\n"[0]);
	//	Debug.Log ("IMPRIMINDO PALAVRAS");
//		Debug.Log (palavras[3]);
		int i = 0;

		do
		{
			line = palavras[i];
			//Debug.Log ("LINE NOT NULL: "+line + " | L: " + line.Length);
			line = line.Replace("\r","");
			//Debug.Log ("AFTER: "+line + " | L: " + line.Length);
			if (line != null && line != "" && line != " " && line != "\n")
			{

				MyWordInList tempLecture = new MyWordInList();
				
				tempLecture.myWord = line;
				tempLecture.found = false;
				tempLecture.foundedByPlayer = 0;
				
				list.Add(tempLecture);
				//Debug.Log ("LINE NOT NULL: "+line);
				//Debug.Log ("WORD "+list[i].myWord);

				i++;

			}
			else {line = null;}

		}while(i < palavras.Length && line != null);
		numberofWords = i;
		return true;


		
		//Debug.Log ("printing "+fileName);
		//Debug.Log(anagrama);
		//XmlReader reader = XmlReader.Create(new StringReader(xmlData.text)));

		/*
		//using (XmlReader reader = XmlReader.Create(new StringReader(xmlData.text)))
		using (theReader)
		{
			int i = 0;
			// While there's lines left in the text file, do this:
			do
			{
				line = theReader.ReadLine();
				
				if (line != null)
				{
					Debug.Log ("LINE NOT NULL");
					MyWordInList tempLecture = new MyWordInList();
					
					tempLecture.myWord = line;
					tempLecture.found = false;
					
					list.Add(tempLecture);
					
					i++;	
				}		
			}
			while (line != null);
			
			numberofWords = i;
			
			// Done reading, close the reader and return true to broadcast success    
			theReader.Close();
			return true;
		}
		//}
		
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		/*catch (Exception e)
		{
			//Console.WriteLine("{0}\n", e.Message);
			return false;
		}*/
		//Debug.Log ("LOAD DICTIONARY ENDED!");
	}
	
	//Called for shuffle or earthquake to reoganize the letters
	public void reorganize()
	{
		//

		float pos = 0;
		int rand = 0;
		string numberstoSort = ""; 
		ConstructTable[] constTab = FindObjectsOfType(typeof(ConstructTable)) as ConstructTable[];
		//Cases of even or odd number of tiles
		if (word.Length % 2 == 0) 
			pos = constTab[0].tiles_space/2;
		else 
			pos = 0;
		
		
		int i = 0;
		
		Tile[] tiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		int lengthWord = word_original.Length;
		
		//Create a string to manipulate the tiles
		while (lengthWord !=0)
		{
		
			numberstoSort = numberstoSort + (i.ToString());

			lengthWord--;
			i++;
		}
		
		double num=0;
		
		while (numberstoSort.Length >0) 
		{
			//Sort a position of the string

			rand = Random.Range(0,(numberstoSort.Length));

			
			//Receives the number that was in the position
			num =  char.GetNumericValue(numberstoSort[rand]);
			int value = (int)num;
			tiles[value].my_x_pos = pos;
			
			//If was not on the submittion table, moves the tile
			if((tiles[value].onTheTable == 0 || tiles[value].onTheTable == 3)  )
				tiles[value].moveMe();


            //Withdraws the number of the string
            numberstoSort = numberstoSort.Remove (rand, 1);
			
			if (pos > 0)
				pos = pos * -1;
			else 
			{
				pos *= -1;
				pos += constTab[0].tiles_space;
				
			}	
		}
        

    }
	
	
	public void verifyWord(string word)
	{
		Debug.Log ("VERIFY: "+word + " n "+ numberofWords);
		//bool  verify;
		
		//MyWordInList tempLecture = new MyWordInList();
		//tempLecture.myWord = word;
		//tempLecture.found = false;
		//verify = list.Contains(tempLecture);

		int i;
		for(i=0;i<numberofWords;i++)
		{
			//Debug.Log ("VERIFY WORD: " + list[i].myWord + " | L: " + list[i].myWord.Length + " | MY WORD " + word + " | L : " + word.Length);
			if(list[i].myWord == word)
			{
				//Debug.Log (" WORD FUCKING FOUND" );
				if(list[i].found==false)
				{
					wordfound(GLOBALS.Singleton.MP_PLAYER,i,GLOBALS.Singleton.PUGOLDLETTERACTIVE);
					if(GLOBALS.Singleton.MP_MODE == 1)
					{
                        //Debug.Log("ENVIANDO PALAVRA DE ID: " + i);

                        mp_controller.access.send_word_found(i);

					}

					Sound_Controller.sController.AudFound();

					wfoundpoints.SetActive(true);
					wfoundpoints.transform.DOMoveY(wAlreadyOrNotFinalpos,wAlreadyOrNotFinaltime).OnComplete(wait_msg_found_points);
					wfoundpoints.GetComponent<SpriteRenderer>().DOFade(1f,0.6f);

					return;
				}
				else
				{
					Sound_Controller.sController.AudAlready();
					Avatar_player_1.acess.sad();
					dessapear_not_found();
					dessapear_already();

					walready.SetActive(true);
					walready.transform.DOMoveY(wAlreadyOrNotFinalpos,wAlreadyOrNotFinaltime).OnComplete(wait_msg_already);
					walready.GetComponent<SpriteRenderer>().DOFade(1f,0.6f);
					return;
				}
			}
		}
		Avatar_player_1.acess.sad();
		Sound_Controller.sController.AudError();
		//AudioSource.PlayClipAtPoint(AudioError, Camera.main.transform.position);
		//Reposiciona msg 
		dessapear_not_found();
		dessapear_already();

		wnotfound.SetActive(true);
		wnotfound.transform.DOMoveY(wAlreadyOrNotFinalpos,wAlreadyOrNotFinaltime).OnComplete(wait_msg_not_found);
		wnotfound.GetComponent<SpriteRenderer>().DOFade(1f,0.6f);

	}

	public bool bot_try_to_submit_word(int i)
	{
		if(list[i].found==false)
		{
			Avatar_player_2.acess.happy();
			wordfound(GLOBALS.Singleton.OP_PLAYER,i,0);
			Sound_Controller.sController.AudFound();

			return true;

		}
		else
		{
			return false;
		}
	}

	public float bot_number_of_words_founded()
	{
		int i;
		float wordsFounded=0;
		for(i=0;i<numberofWords;i++)
		{
			if(list[i].found==true)
			{
				wordsFounded++;
			}
		}

		return wordsFounded;
	}

	//Deixa a msg um pouco na tela
	void wait_msg_not_found()
	{
		wnotfound.GetComponent<SpriteRenderer>().DOFade(1f,1f).OnComplete(dessapear_not_found);
	}
	//Desaparece a msg
	void dessapear_not_found()
	{
		wnotfound.transform.DOKill();
		wnotfound.GetComponent<SpriteRenderer>().DOKill();
		wnotfound.transform.DOMoveY(wnotfoundpos.y,0f);
		wnotfound.GetComponent<SpriteRenderer>().DOFade(0f,0f);
		wnotfound.SetActive(false);
	}

	void wait_msg_already()
	{
		walready.GetComponent<SpriteRenderer>().DOFade(1f,1f).OnComplete(dessapear_already);
	}

	void dessapear_already()
	{
		walready.transform.DOKill();
		walready.GetComponent<SpriteRenderer>().DOKill();
		walready.transform.DOMoveY(walreadypos.y,0f);
		walready.GetComponent<SpriteRenderer>().DOFade(0f,0f);
		walready.SetActive(false);
	}

	void wait_msg_found_points()
	{
		//wfoundpoints.GetComponent<MeshRenderer>().DOFade(1f,1f).OnComplete(dessapear_found_points);
		wfoundpoints.transform.DOMoveY(wAlreadyOrNotFinalpos,1f).OnComplete(dessapear_found_points);
	}
	
	void dessapear_found_points()
	{
		wfoundpoints.transform.DOKill();
		wfoundpoints.GetComponent<MeshRenderer>().DOKill();
		wfoundpoints.transform.DOMoveY(wfoundpointspos.y,0f);
		//wfoundpoints.GetComponent<MeshRenderer>().DOFade(0f,0f);
		wfoundpoints.SetActive(false);
	}



	public void wordfound(int player, int word_id, int goldLetterActive)
	{
		list[word_id].found = true;
		list[word_id].foundedByPlayer = player;
		list[word_id].goldLetterActive = goldLetterActive;

		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i;
		for(i=0;i<squares.Length;i++)
		{
			squares[i].appear(word_id, player);
		}

		updateScoreWordFound(player,word_id,goldLetterActive);

		wordsFounded++;
		f5WordsFounded();

		if(wordsFounded == numberofWords)
		{
			GLOBALS.Singleton.GAME_RUNNING = false;
            Menus_Controller.acesss.destructQuitGame();
            GameController[] gCtrlr =  FindObjectsOfType(typeof(GameController)) as GameController[];
			gCtrlr[0].match_end();
		}
		
	}

	//UPDATE SCORE AND BARS, SHOW THE BALLOON
	void updateScoreWordFound(int player, int word_id, int goldLetterActive)
	{
		//SCORE Ctrlr
		if (player == GLOBALS.Singleton.MP_PLAYER) 
		{
			Avatar_player_1.acess.happy();
			GLOBALS.Singleton.NumberOfWordsFounded++;

			GLOBALS.Singleton.MY_SCORE += list [word_id].myWord.Length * 10 + (goldLetterActive * (list [word_id].myWord.Length * 10));
			wfoundpoints.GetComponent<TextMesh>().text = "+ " + (list[word_id].myWord.Length * 10 + (goldLetterActive * (list [word_id].myWord.Length * 10))).ToString () + " points";
		}
		else
		{
			Avatar_player_2.acess.happy();
			GLOBALS.Singleton.NumberOfWordsFoundedOP++;
			GLOBALS.Singleton.OP_SCORE+=list[word_id].myWord.Length*10 + (goldLetterActive * (list[word_id].myWord.Length*10));
		}
		
		updateScoreAndBars(player);

		//Trigger the baloon
		if(player == GLOBALS.Singleton.MP_PLAYER)
		{
			//Show balloon
			//triggerBallonP1 = 1;
			BalonP1.transform.localScale = new Vector3(0f, 0f, 1);

			BalonP1.transform.DOKill();
			BalonP1.transform.DOScale(scaleB,smooth).OnComplete(stopBaloonP1);

			BalonP1.GetComponentInChildren<TextMesh> ().text = list[word_id].myWord;
			BalonP1.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;
		}
		else
		{
			//Show balloon
			//triggerBallonP2 = 1;
			BalonP2.transform.localScale = new Vector3(0f, 0f, 1);

			BalonP2.transform.DOKill();
			BalonP2.transform.DOScale(scaleB,smooth).OnComplete(stopBaloonP2);

			BalonP2.GetComponentInChildren<TextMesh> ().text = list[word_id].myWord;
			BalonP2.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;
		}
	}

	void stopBaloonP1()
	{
		BalonP1.transform.DOScale(scaleB,0.5f).OnComplete(backTo0BaloonP1);
	}
	void backTo0BaloonP1()
	{

		BalonP1.transform.DOScale(new Vector3 (0.3f,0.3f,0),smooth).OnComplete(killKillKillTheBaloonP1);
	}

	void killKillKillTheBaloonP1()
	{
		BalonP1.transform.localScale = new Vector3(0f, 0f, 1);
	}

	
	void stopBaloonP2()
	{
		BalonP2.transform.DOScale(scaleB,0.5f).OnComplete(backTo0BaloonP2);
	}
	void backTo0BaloonP2()
	{
		BalonP2.transform.DOScale(new Vector3 (0.3f,0.3f,0),smooth).OnComplete(killKillKillTheBaloonP2);
	}
	
	void killKillKillTheBaloonP2()
	{
		BalonP2.transform.localScale = new Vector3(0f, 0f, 1);
	}

	//ERASE POINTS OF ERASED WORD
	public void eraseWordUpdateScore(int player, int word_id, int goldLetterActive)
	{ 
		//SCORE Ctrlr
		if(player == GLOBALS.Singleton.MP_PLAYER)
			GLOBALS.Singleton.MY_SCORE-=list[word_id].myWord.Length*10 + (goldLetterActive * (list[word_id].myWord.Length*10));
		else
			GLOBALS.Singleton.OP_SCORE-=list[word_id].myWord.Length*10 + (goldLetterActive * (list[word_id].myWord.Length*10));

		updateScoreAndBars(player);

		wordsFounded--;
		f5WordsFounded();
	}

	//UPDATE THE SIZE OF BARS AND THE SCORE
	void updateScoreAndBars(int player)
	{
		float tempP1 = (+ ( ((GLOBALS.Singleton.MY_SCORE - GLOBALS.Singleton.OP_SCORE)  * 100)/ (GLOBALS.Singleton.MAX_SCORE)));
		//float tempP2 = (- ( ((GLOBALS.Singleton.MY_SCORE - GLOBALS.Singleton.OP_SCORE)  * 100)/ (GLOBALS.Singleton.MAX_SCORE)));
        

       // PowerBarP1.transform.DOScaleX(tempP1 / 100, 1f);
       // PowerBarP2.transform.DOScaleX(tempP2 / 100, 1f);

        if ((tempP1 * magicalBarScaleNumber) < 100 && (-tempP1 * magicalBarScaleNumber) < 100)
        {
            PowerBarP1.transform.DOScaleX(((tempP1 * magicalBarScaleNumber) + 100) / 100, 1f);
            PowerBarP2.transform.DOScaleX((-(tempP1 * magicalBarScaleNumber) + 100) / 100, 1f);
        }
        else
        {
            if(tempP1 * 4 > 100)
            {
                PowerBarP1.transform.DOScaleX(2f, 1f);
                PowerBarP2.transform.DOScaleX(0f, 1f);
            }
            else
            {
                PowerBarP1.transform.DOScaleX(0f, 1f);
                PowerBarP2.transform.DOScaleX(2f, 1f);
            }
            
            
        }


		if(player == GLOBALS.Singleton.MP_PLAYER)
		{
			//Write score
			GameObject umnome = GameObject.Find ("hud_p1_score"); 
			umnome.GetComponent<TextMesh> ().text = GLOBALS.Singleton.MY_SCORE.ToString ();
		}
		else
		{
			//Write score
			GameObject umnome = GameObject.Find ("hud_p2_score"); 
			umnome.GetComponent<TextMesh> ().text = GLOBALS.Singleton.OP_SCORE.ToString ();
		}

		avatarStatus(tempP1);
	}

	//CHANGE THE NUMBER IN THE HUD THING
	void f5WordsFounded()
	{
		
		if(wordsFounded<10)
			wfounded.GetComponentInChildren<TextMesh> ().text = "0" + wordsFounded.ToString() + "  " + numberofWords.ToString() ;
		else
			wfounded.GetComponentInChildren<TextMesh> ().text = wordsFounded.ToString() + "  " + numberofWords.ToString() ;
	}

	void avatarStatus(float tempP1)
	{

        //Player 1 losing
        if (tempP1 * magicalBarScaleNumber < -35)
		{
			if(Avatar_player_1.acess.losing == false)
			{
				Avatar_player_1.acess.desperate();
			}
		}
		else
		{
			if(Avatar_player_1.acess.losing == true)
				Avatar_player_1.acess.normal();
		}

        //Player 2 losing
		if(tempP1 * magicalBarScaleNumber > 35)
		{
			if(Avatar_player_2.acess.losing == false)
				Avatar_player_2.acess.desperate();
		}
		else
		{
			if(Avatar_player_2.acess.losing == true)
				Avatar_player_2.acess.normal();
		}
	}
	//TRIGGER FOR THE BALLOON
	/*void ballonsstatus()
	{
        
		if(triggerBallonP1 == 1)
		{
           
            BalonP1.transform.localScale = Vector3.Lerp(BalonP1.transform.localScale, scaleB, smooth * Time.deltaTime);
			if(BalonP1.transform.localScale == scaleB)
				triggerBallonP1 = 2;
		}
		else
		{
			if(triggerBallonP1 == 2)
			{
				BalonP1.transform.localScale = Vector3.Lerp(BalonP1.transform.localScale,new Vector3 (0,0,0), smooth * Time.deltaTime);
				if(BalonP1.transform.localScale == Vector3.zero)
					triggerBallonP1 = 0;
				
			}
		}

		if(triggerBallonP2 == 1)
		{

			BalonP2.transform.localScale = Vector3.Lerp(BalonP2.transform.localScale, scaleB, smooth * Time.deltaTime);
			if(BalonP2.transform.localScale == scaleB)
				triggerBallonP2 = 2;
		}
		else
		{
			if(triggerBallonP2 == 2)
			{
				BalonP2.transform.localScale = Vector3.Lerp(BalonP2.transform.localScale,new Vector3 (0,0,0), smooth * Time.deltaTime);
				if(BalonP2.transform.localScale == Vector3.zero)
					triggerBallonP2 = 0;
				
			}
		}
	}*/



}
