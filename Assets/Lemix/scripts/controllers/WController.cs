using UnityEngine;
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
	public mp_controller mp;

	//Array de chars com os tiles na mesa
	public char[] atable = new char[10];


	//Sons
	public AudioClip AudioAlready, AudioFound, AudioError;

	//Lista de palavras
	public List<MyWordInList> list = new List<MyWordInList>();
	public float numberofWords;
	private float wordsFounded;

	//Ballons
	Vector3 scaleB, walreadypos, wnotfoundpos;
	int triggerBallonP1 =0, triggerBallonP2 =0;
	float wAlreadyOrNotFinaltime = 0.6f;
	float wAlreadyOrNotFinalpos = -130f;
	GameObject BalonP1,BalonP2, PowerBarP1, PowerBarP2, wfounded;
	public float smooth;

	public GameObject walready, wnotfound; 

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

		wnotfoundpos = wnotfound.transform.position;
		walreadypos = walready.transform.position;
		wnotfound.GetComponent<SpriteRenderer>().DOFade(0f,0f);
		walready.GetComponent<SpriteRenderer>().DOFade(0f,0f);

		//Ballon adjust
		BalonP1 = GameObject.Find ("feed_baloon_blue"); 
		BalonP2 = GameObject.Find ("feed_baloon_red"); 
		PowerBarP1 = GameObject.Find ("hud_bar_blue"); 
		PowerBarP2 = GameObject.Find ("hud_bar_red"); 
		scaleB = BalonP1.transform.localScale;

		BalonP1.transform.localScale = new Vector3(0f, 0f, 1);
		BalonP2.transform.localScale = new Vector3(0f, 0f, 1);


		smooth = 9f;
		wfounded = GameObject.Find ("hud_words_found_counter"); 
		wfounded.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10; 

		int rand = GLOBALS.Singleton.ANAGRAM_ID;
		// Sorteia arquivo de palavra
		if (rand == 0) {

			rand = wSort[0].sortWordAndReturnAnagramID();
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
		ballonsstatus();
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
		Debug.Log (texto);

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
	
	//Chamado pelo botao shuffle para reorganizar as tiles
	public void reorganize()
	{
		//

		float pos = 0;
		int rand = 0;
		string numberstoSort = ""; 
		ConstructTable[] constTab = FindObjectsOfType(typeof(ConstructTable)) as ConstructTable[];
		//Casos de construir a mesa com numero de tiles par ou impar
		if (word.Length % 2 == 0) 
			pos = constTab[0].tiles_space/2;
		else 
			pos = 0;
		
		
		int i = 0;
		
		Tile[] tiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		int lengthWord = word_original.Length;
		
		//Cria uma string de 0 ate tamanho da palavra-1 para manipular as tiles
		while (lengthWord !=0)
		{
			numberstoSort= numberstoSort + (i.ToString());
			lengthWord--;
			i++;
		}
		
		
		double num=0;
		
		
		while (numberstoSort.Length >0) 
		{
			//Sorteia uma das posiçoes da string
			rand = Random.Range(0,(numberstoSort.Length));
			
			//Recebe o numero que esta na posiçao
			num =  char.GetNumericValue(numberstoSort[rand]);
			int value = (int)num;
			tiles[value].my_x_pos = pos;
			
			//Se nao esta na mesa move o tile
			if(tiles[value].onTheTable == 0 || tiles[value].onTheTable == 3)
				tiles[value].moveMe();
			
			//Retira o numero da string
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
					Debug.Log("MP MODE " + GLOBALS.Singleton.MP_MODE);
					if(GLOBALS.Singleton.MP_MODE == 1)
					{
						//Debug.Log("ENVIANDO PALAVRA DE ID: " + i);

						//mp_controller[] mp_ = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
						//mp_[0].send_word_found(i);

						mp.send_word_found(i);

					}
					//GetComponent<AudioSource>().PlayOneShot(AudioFound);
					AudioSource.PlayClipAtPoint(AudioFound, Camera.main.transform.position);

					return;
				}
				else
				{
					AudioSource.PlayClipAtPoint(AudioAlready, Camera.main.transform.position);


					dessapear_not_found();
					dessapear_already();
					
					walready.transform.DOMoveY(wAlreadyOrNotFinalpos,wAlreadyOrNotFinaltime).OnComplete(wait_msg_already);
					walready.GetComponent<SpriteRenderer>().DOFade(1f,0.6f);
					return;
				}
			}
		}

		AudioSource.PlayClipAtPoint(AudioError, Camera.main.transform.position);
		//Reposiciona msg 
		dessapear_not_found();
		dessapear_already();

		wnotfound.transform.DOMoveY(wAlreadyOrNotFinalpos,wAlreadyOrNotFinaltime).OnComplete(wait_msg_not_found);
		wnotfound.GetComponent<SpriteRenderer>().DOFade(1f,0.6f);

	}

	public bool bot_try_to_submit_word(int i)
	{
		if(list[i].found==false)
		{
			wordfound(GLOBALS.Singleton.OP_PLAYER,i,0);
			GetComponent<AudioSource>().PlayOneShot(AudioFound);

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
		
	}

	//UPDATE SCORE AND BARS, SHOW THE BALLOON
	void updateScoreWordFound(int player, int word_id, int goldLetterActive)
	{
		//SCORE Ctrlr
		if (player == GLOBALS.Singleton.MP_PLAYER) 
		{
			GLOBALS.Singleton.NumberOfWordsFounded++;
			GLOBALS.Singleton.MY_SCORE += list [word_id].myWord.Length * 10 + (goldLetterActive * (list [word_id].myWord.Length * 10));
		}
		else
			GLOBALS.Singleton.OP_SCORE+=list[word_id].myWord.Length*10 + (goldLetterActive * (list[word_id].myWord.Length*10));
		
		updateScoreAndBars(player);

		//Trigger the baloon
		if(player == GLOBALS.Singleton.MP_PLAYER)
		{
			//Show balloon
			triggerBallonP1 = 1;
			BalonP1.transform.localScale = new Vector3(0f, 0f, 1);
			BalonP1.GetComponentInChildren<TextMesh> ().text = list[word_id].myWord;
			BalonP1.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;
		}
		else
		{
			//Show balloon
			triggerBallonP2 = 1;
			BalonP2.transform.localScale = new Vector3(0f, 0f, 1);
			BalonP2.GetComponentInChildren<TextMesh> ().text = list[word_id].myWord;
			BalonP2.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;
		}
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
		float tempP1 = (100 + ( ((GLOBALS.Singleton.MY_SCORE - GLOBALS.Singleton.OP_SCORE)  * 100)/ (GLOBALS.Singleton.MAX_SCORE)));
		float tempP2 = (100 - ( ((GLOBALS.Singleton.MY_SCORE - GLOBALS.Singleton.OP_SCORE)  * 100)/ (GLOBALS.Singleton.MAX_SCORE)));
		
		PowerBarP1.transform.DOScaleX(tempP1/100,1f);
		PowerBarP2.transform.DOScaleX(tempP2/100,1f);
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
	}

	//TRIGGER FOR THE BALLOON
	void ballonsstatus()
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
	}

	//CHANGE THE NUMBER IN THE HUD THING
	void f5WordsFounded()
	{

		if(wordsFounded<10)
			wfounded.GetComponentInChildren<TextMesh> ().text = "0" + wordsFounded.ToString() + "  " + numberofWords.ToString() ;
		else
			wfounded.GetComponentInChildren<TextMesh> ().text = wordsFounded.ToString() + "  " + numberofWords.ToString() ;
	}


}
