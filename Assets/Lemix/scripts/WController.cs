using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  
using System.Linq;

using Thinksquirrel.WordGameBuilder;
using Thinksquirrel.WordGameBuilder.Gameplay;
using Thinksquirrel.WordGameBuilder.ObjectModel;

public class WController : MonoBehaviour {

	private string word_original = "";
	public string word = ""; 
	public mp_controller mp;

	//Array de chars com os tiles na mesa
	public char[] atable = new char[10];


	//Sons
	public AudioClip AudioAlready;
	public AudioClip AudioFound;
	public AudioClip AudioError;
 
	//Lista de palavras
	public List<MyWordInList> list = new List<MyWordInList>();
	public float numberofWords;
	private float wordsFounded;

	//Ballons
	Vector3 scaleB;
	int triggerBallonP1 =0;
	int triggerBallonP2 =0;
	GameObject BalonP1;
	GameObject BalonP2;
	public float smooth;
	GameObject wfounded; 

	//Classe da lista de palavras
	public class MyWordInList
	{
		public string myWord { get; set; }
		public bool found { get; set; }
		public int foundedByPlayer{ get; set; }
	}

	public string word_original_ret()
	{
		return word_original;
	}

	// Use this for initialization
	void Start () {
		//this.gameObject.AddComponent<PhotonView>();

		//Quantos arquivos tem a biblioteca de palavras
		//int numberOfFiles = SAFFER.Singleton.NumberOfWordFiles;
		//Debug.Log(numberOfFiles);
		//Sorteia um dos arquivos de palavras

		//Ballon adjust
		BalonP1 = GameObject.Find ("dialog blue"); 
		BalonP2 = GameObject.Find ("dialog red"); 
		scaleB = BalonP1.transform.localScale;

		BalonP1.transform.localScale = new Vector3(0f, 0f, 1);
		BalonP2.transform.localScale = new Vector3(0f, 0f, 1);
		smooth = 9f;
		wfounded = GameObject.Find ("word-found-counter"); 
		wfounded.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10; 

		int rand = SAFFER.Singleton.ANAGRAM_ID;
		// DEBUG SORTING CASE
		if (rand == 0) {
			int numberOfFiles = SAFFER.Singleton.NumberOfWordFiles;
			Debug.Log (numberOfFiles);
			//Sorteia um dos arquivos de palavras
			rand = Random.Range (1, numberOfFiles);
			SAFFER.Singleton.ANAGRAM_ID = rand;
		}

		Debug.Log("ANAGRAM ID: " + rand);
	
		string number = rand.ToString();
		//number = "5";
		string path = Application.dataPath;
		string file = "Word_" + number;
		//string file = "pt_Word_" + number;
		//string file = path+"/Word_" + number +".txt";

		//var anagrama = Resources.Load(file) as TextAsset;
		//Debug.Log ("PRINTING FILE CONTENT FROM " + file + ": ");
		//Debug.Log (anagrama);

		LoadDictionary(file);


		int numberofWordstemp;
		
		numberofWordstemp = (int)numberofWords;
		word_original = list[numberofWordstemp-1].myWord;
		Debug.Log (word_original + "ASDAS");
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
		Debug.Log ("LOAD DICTIONARY CALLED!");
		string line;
		// Create a new StreamReader, tell it which file to read and what encoding the file
		// was saved as
		//StreamReader theReader = new StreamReader(fileName+".txt", Encoding.Default);

		TextAsset anagrama = Resources.Load(fileName) as TextAsset;
		string texto = anagrama.text;
		Debug.Log (texto);

		string[] palavras = anagrama.text.Split("\n"[0]);
		Debug.Log ("IMPRIMINDO PALAVRAS");
		Debug.Log (palavras[3]);
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
		Debug.Log ("LOAD DICTIONARY ENDED!");
	}
	
	//Chamado pelo botao shuffle para reorganizar as tiles
	public void reorganize()
	{
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
			Debug.Log ("VERIFY WORD: " + list[i].myWord + " | L: " + list[i].myWord.Length + " | MY WORD " + word + " | L : " + word.Length);
			if(list[i].myWord == word)
			{
				Debug.Log (" WORD FUCKING FOUND" );
				if(list[i].found==false)
				{
					wordfound(SAFFER.Singleton.MP_PLAYER,i);
					Debug.Log("MP MODE " + SAFFER.Singleton.MP_MODE);
					if(SAFFER.Singleton.MP_MODE == 1)
					{
						Debug.Log("ENVIANDO PALAVRA DE ID: " + i);

						//mp_controller[] mp_ = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
						//mp_[0].send_word_found(i);

						mp.send_word_found(i);

					}
					GetComponent<AudioSource>().PlayOneShot(AudioFound);


					return;
				}
				else
				{
					GetComponent<AudioSource>().PlayOneShot(AudioAlready);
					return;
				}
			}
		}

		GetComponent<AudioSource>().PlayOneShot(AudioError);

	}

	public void wordfound(int player, int word_id)
	{
		list[word_id].found = true;
		list[word_id].foundedByPlayer = player;

		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i;
		for(i=0;i<squares.Length;i++)
		{
			squares[i].appear(word_id, player);
		}

		//SCORE Ctrlr
		if(player == SAFFER.Singleton.MP_PLAYER)
			SAFFER.Singleton.MY_SCORE+=list[word_id].myWord.Length*10 + (SAFFER.Singleton.PUGOLDLETTERACTIVE*(list[word_id].myWord.Length*10));
		else
			SAFFER.Singleton.OP_SCORE+=list[word_id].myWord.Length*10 + (SAFFER.Singleton.PUGOLDLETTERACTIVE*(list[word_id].myWord.Length*10));



		wordsFounded++;
		f5WordsFounded();


		if(player == 1)
		{
			GameObject umnome = GameObject.Find ("P1 Score"); 
			umnome.GetComponent<TextMesh> ().text = SAFFER.Singleton.MY_SCORE.ToString ();
			triggerBallonP1 = 1;
			BalonP1.transform.localScale = new Vector3(0f, 0f, 1);
			BalonP1.GetComponentInChildren<TextMesh> ().text = list[word_id].myWord;
			BalonP1.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;
		}
		else
		{
			//Write score
			GameObject umnome = GameObject.Find ("P2 Score"); 
			umnome.GetComponent<TextMesh> ().text = SAFFER.Singleton.MY_SCORE.ToString ();

			//Show balloon
			triggerBallonP2 = 1;
			BalonP2.transform.localScale = new Vector3(0f, 0f, 1);
			BalonP2.GetComponentInChildren<TextMesh> ().text = list[word_id].myWord;
			BalonP2.GetComponentInChildren<TextMesh> ().GetComponent<Renderer>().sortingOrder = 10;
		}
		
	}

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
				if(BalonP1.transform.localScale == Vector3.zero)
					triggerBallonP2 = 0;
				
			}
		}
	}

	void f5WordsFounded()
	{

		if(wordsFounded<10)
			wfounded.GetComponentInChildren<TextMesh> ().text = "0" + wordsFounded.ToString() + "  " + numberofWords.ToString() ;
		else
			wfounded.GetComponentInChildren<TextMesh> ().text = wordsFounded.ToString() + "  " + numberofWords.ToString() ;
	}


}
