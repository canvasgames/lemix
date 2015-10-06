using UnityEngine;
using System.Collections;

public class ConstructTable : MonoBehaviour {

	public GameObject tile;	
	public GameObject tileSpace;	
	public GameObject whiteSquare;	
	public int x_pos_init;

	//Posiçao inicial dos tiles e espaçamento
	int y_pos_init = -370;
	public int tiles_space, x_space, wordsPerCollumn, superior_Y, y_space, square_width;
	public string rand_word;
	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void construct ()
	{
		//Configuraçao dos white tiles
		tiles_space = 160;
		x_space = 80;
		wordsPerCollumn = 8;
		superior_Y = 280;
		y_space = 50;
		square_width = 40;
		
		constructInitialTiles();
		constructWhiteSquares();
	}
	void constructWhiteSquares()
	{
		

		int width_ant;
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

		float numberofWords = wordCTRL[0].numberofWords; 
		int numberofWordsInt;
		
		numberofWordsInt = (int)numberofWords;
		
		int tableWidth;
		//Debug.Log(numberofWords);
		//Pega a largura dos tiles brancos
		if(numberofWords/wordsPerCollumn <=1)
			tableWidth = (int)(CollumnMajorWord(0,numberofWordsInt-1)*square_width);
		
		else
		{
			tableWidth = (int)(CollumnMajorWord(0,7)*square_width)+ x_space;
			if(numberofWords/wordsPerCollumn <=2)
				tableWidth = tableWidth + (int)(CollumnMajorWord(8,numberofWordsInt-1)*square_width);
			else
			{
				tableWidth = tableWidth+(int)(CollumnMajorWord(8,15)*square_width)+ x_space;
				if(numberofWords/wordsPerCollumn <=3)
					tableWidth = tableWidth+(int)(CollumnMajorWord(16,numberofWordsInt-1)*square_width);
				else
				{
					tableWidth = tableWidth+(int)(CollumnMajorWord(16,23)*square_width)+ x_space;
					if(numberofWords/wordsPerCollumn <=4)
						tableWidth = tableWidth+(int)(CollumnMajorWord(24,numberofWordsInt-1)*square_width);
					else
					{
						tableWidth = tableWidth+(int)(((CollumnMajorWord(24,31))*square_width))+ x_space;
						tableWidth = tableWidth+(int)(((CollumnMajorWord(32,numberofWordsInt-1))*square_width));
					}
					
				}
			}
			
		}
		
		
		//1 coluna
		if(numberofWords/wordsPerCollumn <=1)
			constructColumnWhiteSquares (0,numberofWordsInt-1,-tableWidth/2);
		//2 colunas
		else if (numberofWords/wordsPerCollumn > 1 && numberofWords/wordsPerCollumn <=2 )
		{
			constructColumnWhiteSquares (0,7,-tableWidth/2);
			//Soma a largura da coluna anterior e mais o espaço entre as colunas
			width_ant = (int)(((CollumnMajorWord(0,7))*square_width));
			constructColumnWhiteSquares (8,numberofWordsInt-1,-tableWidth/2 + x_space + width_ant);
		}
		//3 colunas
		else if (numberofWords/wordsPerCollumn > 2 && numberofWords/wordsPerCollumn <=3 )
		{
			constructColumnWhiteSquares (0,7,-tableWidth/2);
			width_ant = (int)(((CollumnMajorWord(0,7))*square_width));
			constructColumnWhiteSquares (8,15,-tableWidth/2 + x_space + width_ant);
			width_ant = width_ant + (int)(((CollumnMajorWord(8,15))*square_width));
			constructColumnWhiteSquares (16,numberofWordsInt-1,-tableWidth/2 + 2*x_space + width_ant);
		}
		//4 colunas
		else if (numberofWords/wordsPerCollumn > 3 && numberofWords/wordsPerCollumn <=4 )
		{
			constructColumnWhiteSquares (0,7,-tableWidth/2);
			width_ant = (int)(((CollumnMajorWord(0,7))*square_width));
			constructColumnWhiteSquares (8,15,-tableWidth/2 + x_space + width_ant);
			width_ant = width_ant + (int)(((CollumnMajorWord(8,15))*square_width));
			constructColumnWhiteSquares (16,23,-tableWidth/2 + 2*x_space + width_ant);
			width_ant = width_ant + (int)(((CollumnMajorWord(16,23))*square_width));
			constructColumnWhiteSquares (24,numberofWordsInt-1,-tableWidth/2 + 3*x_space + width_ant);
		}
		//5 colunas
		else
		{
			constructColumnWhiteSquares (0,7,-tableWidth/2);
			width_ant = (int)(((CollumnMajorWord(0,7))*square_width));
			constructColumnWhiteSquares (8,15,-tableWidth/2 + x_space + width_ant);
			width_ant = width_ant + (int)(((CollumnMajorWord(8,15))*square_width));
			constructColumnWhiteSquares (16,23,-tableWidth/2 + 2*x_space + width_ant);
			width_ant = width_ant + (int)(((CollumnMajorWord(16,23))*square_width));
			constructColumnWhiteSquares (24,31,-tableWidth/2 + 3*x_space + width_ant);
			width_ant = width_ant + (int)(((CollumnMajorWord(24,31))*square_width));
			constructColumnWhiteSquares (32,numberofWordsInt-1,-tableWidth/2 + 4*x_space + width_ant);
		}
	} 	

	//Acha o tamanho da maior palavra da coluna 
	int CollumnMajorWord(int initial_index_string,int final_index_string)
	{
		int majorWordSize=0;
		int i;
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		//Encontra a maior palavra da coluna
		for(i=initial_index_string;i<=final_index_string;i++)
		{
			if(wordCTRL[0].list[i].myWord.Length > majorWordSize)
				majorWordSize = wordCTRL[0].list[i].myWord.Length ;
		}
		return majorWordSize;
	}
	
	void constructColumnWhiteSquares (int initial_index_string,int final_index_string, int initial_x_pos)
	{
		
		int i,k;
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		//Constroi a coluna
		for(i=initial_index_string,k=0;i<=final_index_string;i++,k++)
		{
			int j;
			int lengtword = wordCTRL[0].list[i].myWord.Length;
			for(j=0;lengtword>0;j++)		
			{
				GameObject go = (GameObject)Instantiate (whiteSquare, new Vector3 (initial_x_pos+square_width*j, superior_Y - y_space*k , 0), transform.rotation);
				WhiteSquare wtSquare = go.GetComponent<WhiteSquare> ();
				lengtword --;
				//wtSquare.GetComponent<TextMesh> ().text = list[i].myWord[j].ToString ();
				wtSquare.GetComponent<TextMesh> ().text = "";
				wtSquare.myLetter = wordCTRL[0].list[i].myWord[j].ToString ();
				wtSquare.myID = i;
				wtSquare.myIDindex = j;

				//Marca ultima letra para o power up
				if(lengtword == 0)
					wtSquare.lastletter = true;
			}
			
		}
	}
	
	// #########################################
	public void constructInitialTiles()
	{
		int rand;
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

		string word_original;
		string[] temp;

		temp = wordCTRL[0].word_original_ret().Split("\n"[0]);
		word_original = temp [0];
		//word_original = wordCTRL[0].word_original_ret().Split("\n"[0]);
		//Debug.Log ("PALAVRA MAIOR: " + word_original);
		//Embaralhando a string da palavra
		while (word_original.Length>0) 
		{
			rand = Random.Range(0,(word_original.Length-1));
			rand_word = rand_word+word_original[rand];
			word_original = word_original.Remove (rand, 1);
		}	
		
		
		int pos = 0;
		int lengthWord = rand_word.Length;
		//Casos de construir a mesa com numero de tiles par ou impar
		if (lengthWord % 2 == 0) 
			pos = tiles_space/2;
		else 
			pos = 0;
		
		
		//Construçao das tiles
		while (lengthWord !=0) 
		{
			
			GameObject go = (GameObject)Instantiate (tile, new Vector3 (pos, y_pos_init, 0), transform.rotation);
			Tile tCTRL = go.GetComponent<Tile> ();

			GameObject tilegray = (GameObject)Instantiate (tileSpace, new Vector3 (pos, y_pos_init, 0), transform.rotation);
			Tile tgray = go.GetComponent<Tile> ();

			GameObject tilegrayUP = (GameObject)Instantiate (tileSpace, new Vector3 (pos,-190, 0), transform.rotation);
			Tile tgrayUP = go.GetComponent<Tile> ();

			tgray.GetComponent<Renderer>().sortingOrder = 5;
			tgrayUP.GetComponent<Renderer>().sortingOrder = 5;
			tCTRL.GetComponent<Renderer>().sortingOrder = 15;
			tCTRL.GetComponentInChildren<SpriteRenderer> ().GetComponent<Renderer>().sortingOrder = 10;

			//Guarda posiçao inicial
			tCTRL.my_x_pos = pos;	
			tCTRL.my_y_pos = y_pos_init;	
			
			//Seta letra e texto da tile
			tCTRL._myLetter = rand_word [lengthWord - 1];
			tCTRL.GetComponent<TextMesh> ().text = rand_word [lengthWord - 1].ToString ();
			
			lengthWord--;
			
			//Seta posiçao x para o proximo tile
			if (pos > 0)
				pos = pos * -1;
			else 
			{
				pos *= -1;
				pos += tiles_space;
			}
			if(pos < x_pos_init)
				x_pos_init = pos;
			
		}
		//reorganize();
	}
}
