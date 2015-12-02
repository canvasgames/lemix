using UnityEngine;
using System.Collections;



public class Submit_And_Input_Ctrl : MonoBehaviour {

	private int changed = 0;
	public GameObject LeftBar;
	public GameObject RightBar;

	public int submitFullTrigger=0;
	float timer2Submit;

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		//Desaparece o bt submit na primeira jogada
		if (changed < 8)
		{
			WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
			changeScale(wordCTRL[0].word.Length+1);
		}

		//Trigger para submeter a mesa cheia
		if(submitFullTrigger == 1 && GLOBALS.Singleton.GAME_RUNNING == true)
		{
			timer2Submit -= Time.deltaTime;
			if ( timer2Submit < 0 )
			{
				submitLetters();
			}
		}

		//Input

		if(Input.anyKeyDown && GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
		{
			if(Input.GetKeyDown("space"))
			{
				WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
				wordCTRL[0].reorganize();
			}
			else if(Input.GetKeyDown("return"))
			{
				submitFullTrigger =0;
				submitLetters();

			}
			else if(Input.GetKeyDown("backspace"))
			{
				inputBackspaceCase();
			}
			else if(Input.GetKeyDown("1"))
			{
				Power_Up_BT[] puBT = FindObjectsOfType(typeof(Power_Up_BT)) as Power_Up_BT[];
				
				if(puBT[0].myType == 0)
					puBT[0].clickOrKeyboard();
				else
					puBT[1].clickOrKeyboard();

			}
			else if(Input.GetKeyDown("2"))
			{
				Power_Up_BT[] puBT = FindObjectsOfType(typeof(Power_Up_BT)) as Power_Up_BT[];

				if(puBT[0].myType == 1)
					puBT[0].clickOrKeyboard();
				else
					puBT[1].clickOrKeyboard();
			}
			else
			{
				Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
				int i;

			
				//Search for the tile
				for(i=0;i<myTiles.Length;i++)
				{
					if(GLOBALS.Singleton.PUCHOOSELETTER == 1)
					{
						if(Input.GetKeyDown(myTiles[i]._myLetter.ToString().ToLower()))
						{
							if( myTiles[i].freezed == 0)
							{
								if(myTiles[i].PUNotClicable == 0)
									myTiles[i].PUChooseLetter();
								return;
							}
							else
							{
								myTiles[i].unfreezeMe();
								return;
							}
						}
					}
					else
					{
						if(Input.GetKeyDown(myTiles[i]._myLetter.ToString().ToLower()) && (myTiles[i].onTheTable == 0 || myTiles[i].onTheTable == 3))
						{
							if( myTiles[i].freezed == 0)
							{
								myTiles[i].move();
								return;
							}
							else
							{
								myTiles[i].unfreezeMe();
								return;
							}

						}
					}
				}
			}
		}


	}
	
	public void ResizeandReposite()
	{
		//Seta posiçao do bt submit na primeira rodada de acordo com o tamanho da palavra
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

		if(wordCTRL[0].word.Length==5)
		{
			transform.position -= new Vector3(240, 0, 0);

		}
		if(wordCTRL[0].word.Length==6)
		{
			transform.position -= new Vector3(160, 0, 0);
		}
		if(wordCTRL[0].word.Length==7)
		{
			transform.position -= new Vector3(80, 0, 0);
		}
		if(wordCTRL[0].word.Length==8)
		{	
			transform.position -= new Vector3(-10, 0, 0);
		}


	}
	void OnMouseDown() 
	{
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
			submitLetters();
	}

	public void verifyFullTable(int numberOfLetters)
	{
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

		//Se todas letras estao na mesa ativa o trigger para submeter automaticamente a palavra
		if(numberOfLetters == wordCTRL[0].word.Length)
		{
			timer2Submit = 0.7f;
			submitFullTrigger = 1;
		}
		
	}
	public void submitLetters()
	{
        if(GLOBALS.Singleton.GAME_RUNNING == true)
        {
            submitFullTrigger = 0;

            int j = 0;
            string word = "";

            WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

            //Constroi a string da palavra que esta no tabuleiro
            while (wordCTRL[0].atable[j] != '0' && wordCTRL[0].atable[j] != '\0')
            {
                word = word + wordCTRL[0].atable[j];
                j++;
            }

            //Verifica se palavra esta no dicionario ou nao
            wordCTRL[0].verifyWord(word);

            clearTable();

        }


	}

	void clearTable()
	{
		Tile[] myScriptObjects = FindObjectsOfType(typeof(Tile)) as Tile[];
		int i=0;

		//Retira os tiles da mesa de submissao
		for(i=0;i<myScriptObjects.Length;i++)
		{
			
			if(myScriptObjects[i].onTheTable == 1 || myScriptObjects[i].onTheTable == 2)
			{
				myScriptObjects[i].moveMe();
				
			}
		}
		
		//Zera a tabela de tiles na mesa
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		for(i=0;i<10;i++)
			wordCTRL[0].atable [i] = '\0';
		
		//Muda tamanho do BT submit
		changeScale(wordCTRL[0].word.Length+1);
	}

	//Muda o tamanho do bt Submit conforme o numero de letras no tabuleiro
	public void changeScale(int numOfLetters)
	{
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		TextSubmitBT[] SubmitTxt = FindObjectsOfType(typeof(TextSubmitBT)) as TextSubmitBT[];


//		Debug.Log(numOfLetters);
	

		changed += 1;

			if(numOfLetters == 8)
				transform.localScale = new Vector3(3.417f, 1, 1);
			if(numOfLetters == 7)
				transform.localScale = new Vector3(2.92f, 1, 1);
			if(numOfLetters == 6)
				transform.localScale = new Vector3(2.42f, 1, 1);
			if(numOfLetters == 5)
				transform.localScale = new Vector3(1.9f, 1, 1);
			if(numOfLetters == 4)
				transform.localScale = new Vector3(1.42f, 1, 1);
			if(numOfLetters == 3)
				transform.localScale = new Vector3(0.91f, 1, 1);
			if(numOfLetters == 2)
				transform.localScale = new Vector3(0.407f, 1, 1);
			if(numOfLetters == 1)
			{

				transform.localScale = new Vector3(0f, 1, 1);
				LeftBar.transform.localScale = new Vector3(0f, 1, 1);
				RightBar.transform.localScale = new Vector3(0f, 1, 1);
			}
			else
			{
				LeftBar.transform.localScale = new Vector3(1, 1, 1);
				RightBar.transform.localScale = new Vector3(1, 1, 1);
			}

		//Guarda cor dos objetos do bt de submit
		Color corleft = LeftBar.GetComponent<Renderer>().material.color;
		Color corright = RightBar.GetComponent<Renderer>().material.color;
		Color colorida = GetComponent<Renderer>().material.color;

		//Verifica quantas letras foram colocadas no tabuleiro e muda a cor e o txt do bt
		if(numOfLetters == wordCTRL[0].word.Length+1)
		{
			SubmitTxt[0].GetComponent<TextMesh> ().text = "+3 LETTERS";

			colorida.a = 0.4f;
			corright.a = 0.4f;
			corleft.a = 0.4f;
		

		}
		else if (numOfLetters == wordCTRL[0].word.Length)
		{
			SubmitTxt[0].GetComponent<TextMesh> ().text = "+2 LETTERS";

			corright.a = 0.4f;
			corleft.a = 0.4f;
			colorida.a = 0.4f;

		}
		else if (numOfLetters == wordCTRL[0].word.Length-1)
		{
			SubmitTxt[0].GetComponent<TextMesh> ().text = "+1 LETTERS";


			corright.a = 0.4f;
			corleft.a = 0.4f;
			colorida.a = 0.4f;
		}
		else
		{
			corright.a = 1f;
			corleft.a = 1f;
			colorida.a = 1f;


			if(numOfLetters == 1)
				SubmitTxt[0].GetComponent<TextMesh> ().text = "";
			else
				SubmitTxt[0].GetComponent<TextMesh> ().text = "ENTER";
		}

		//muda o alpha do bt
		GetComponent<Renderer>().material.color= colorida;
		LeftBar.GetComponent<Renderer>().material.color = corleft;
		RightBar.GetComponent<Renderer>().material.color = corright;

		// posiçao do txt e das bordas
		var renderer = gameObject.GetComponent<Renderer>();
		SubmitTxt[0].transform.position = new Vector3(transform.position.x - (transform.lossyScale.x*157),SubmitTxt[0].transform.position.y ,SubmitTxt[0].transform.position.z);
		LeftBar.transform.position = new Vector3((transform.position.x - renderer.bounds.size.x), transform.position.y, transform.position.z) ;
		RightBar.transform.position = new Vector3((transform.position.x), transform.position.y, transform.position.z) ;
	}

	public void inputBackspaceCase()
	{
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		Submit_And_Input_Ctrl[] submitBT = FindObjectsOfType(typeof(Submit_And_Input_Ctrl)) as Submit_And_Input_Ctrl[];

		int i = 0;
		int have_letter = 0;
		//Procura uma posiçao vaga na mesa
		while (wordCTRL[0].atable[i] != '\0') 
		{
			i++;
			have_letter = 1;
		}


		if(have_letter == 1)
		{
			//Limpa a ultima posiçao ocupada
			wordCTRL[0].atable [i-1] = '\0';
		
			//Muda scale do BT submit
			if((i-1)>=0)
				submitBT[0].changeScale(wordCTRL[0].word.Length - (i-2));
			else 
				submitBT[0].changeScale(1);
		
		
			Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		
		
			int j;

		
			//Procura o tile na ultima posiçao da mesa
			for(j=0;j<myTiles.Length;j++)
			{
				if(myTiles[j].myID == (i-1) )
				{
					myTiles[j].moveMe();
					return;
				}
			}

		}

	}

}
