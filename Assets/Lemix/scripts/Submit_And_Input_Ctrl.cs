using UnityEngine;
using System.Collections;



public class Submit_And_Input_Ctrl : MonoBehaviour {

	private int changed = 0;


	public int submitFullTrigger=0;
	float timer2Submit;
	// Use this for initialization
	void Start () {
		//gController = otherGameObject.GetComponent<GameController>();
		//transform.localScale = new Vector3(1, 1, 1);
		//WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		//changeScale(wordCTRL[0].word.Length+1);

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
		if(submitFullTrigger == 1)
		{
			timer2Submit -= Time.deltaTime;
			if ( timer2Submit < 0 )
			{
				submitFullTrigger =0;
				submitLetters();
			}
		}

		//Input

		if(Input.anyKeyDown)
		{
			if(Input.GetKeyDown("space"))
			{
				WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
				wordCTRL[0].reorganize();
			}
			else if(Input.GetKeyDown("return"))
			{
				submitLetters();

			}
			else if(Input.GetKeyDown("backspace"))
			{
				inputBackspaceCase();
			}
			else if(Input.GetKeyDown("1"))
			{
				Power_Up_BT[] puBT = FindObjectsOfType(typeof(Power_Up_BT)) as Power_Up_BT[];
				puBT[0].clickOrKeyboard();
			}
			else if(Input.GetKeyDown("2"))
			{
				Power_Up_BT[] puBT = FindObjectsOfType(typeof(Power_Up_BT)) as Power_Up_BT[];
				puBT[1].clickOrKeyboard();
			}
			else
			{
				Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
				int i;

			
				//Procura o tile safadao
				for(i=0;i<myTiles.Length;i++)
				{
					if(SAFFER.Singleton.PUCHOOSELETTER == 1)
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
						if(Input.GetKeyDown(myTiles[i]._myLetter.ToString().ToLower()) && myTiles[i].onTheTable == 0)
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
		//Seta posiçao do bt submit de acordo com o tamanho da palavra
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		//Green_Left[] gleft = FindObjectsOfType(typeof(Green_Left)) as Green_Left[];
		//Green_Right[] gright = FindObjectsOfType(typeof(Green_Right)) as Green_Right[];
		//gleft[0].transform.localScale  = new Vector3(0f, 1, 1);
		//gright[0].transform.localScale  = new Vector3(0f, 1, 1);


		if(wordCTRL[0].word.Length==5)
		{
			//Debug.Log("555555555555555");
			transform.position -= new Vector3(210, 0, 0);
		}
		if(wordCTRL[0].word.Length==6)
		{
			//Debug.Log("66666666666666666");
			transform.position -= new Vector3(140, 0, 0);
		}
		if(wordCTRL[0].word.Length==7)
		{
			//Debug.Log("7777777777777");
			transform.position -= new Vector3(70, 0, 0);
		}
		if(wordCTRL[0].word.Length==8)
		{	
			//Debug.Log("888888888888888");
			transform.position -= new Vector3(0, 0, 0);

		}
	}
	void OnMouseDown() 
	{
		submitLetters();
	}

	public void verifyFullTable(int numberOfLetters)
	{
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

		//Se todas letras estao na mesa ativa o trigger para submeter automaticamente a palavra
		if(numberOfLetters == wordCTRL[0].word.Length)
		{
			timer2Submit = 0.5f;
			submitFullTrigger = 1;
		}
		
	}
	public void submitLetters()
	{
		int j = 0;
		string word = "";

		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

		//Constroi a string da palavra que esta no tabuleiro
		while (wordCTRL[0].atable[j] != '0' && wordCTRL[0].atable[j] != '\0') 
		{
			word = word + wordCTRL[0].atable [j];
			j++;
		}
		
		//Verifica se palavra esta no dicionario ou nao
		wordCTRL[0].verifyWord(word);

		clearTable();

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
		//Green_Left[] gleft = FindObjectsOfType(typeof(Green_Left)) as Green_Left[];
		//Green_Right[] gright = FindObjectsOfType(typeof(Green_Right)) as Green_Right[];

		//GameObject umnome = GameObject.Find ("bt-submit-corner"); 
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		TextSubmitBT[] SubmitTxt = FindObjectsOfType(typeof(TextSubmitBT)) as TextSubmitBT[];


		Debug.Log(numOfLetters);
	

		changed += 1;

			if(numOfLetters == 8)
				transform.localScale = new Vector3(3.53f, 1, 1);
			if(numOfLetters == 7)
				transform.localScale = new Vector3(3f, 1, 1);
			if(numOfLetters == 6)
				transform.localScale = new Vector3(2.5f, 1, 1);
			if(numOfLetters == 5)
				transform.localScale = new Vector3(1.99f, 1, 1);
			if(numOfLetters == 4)
				transform.localScale = new Vector3(1.49f, 1, 1);
			if(numOfLetters == 3)
				transform.localScale = new Vector3(1f, 1, 1);
			if(numOfLetters == 2)
				transform.localScale = new Vector3(0.52f, 1, 1);
			if(numOfLetters == 1)
			{

				transform.localScale = new Vector3(0f, 1, 1);
			}
		if(numOfLetters == wordCTRL[0].word.Length+1)
		{
			SubmitTxt[0].GetComponent<TextMesh> ().text = "+3 LETTER";

			Color colorida = GetComponent<Renderer>().material.color;
			colorida.a = 0.4f;
			GetComponent<Renderer>().material.color= colorida;

		}
		else if (numOfLetters == wordCTRL[0].word.Length)
		{
			SubmitTxt[0].GetComponent<TextMesh> ().text = "+2 LETTER";

			Color colorida = GetComponent<Renderer>().material.color;
			colorida.a = 0.4f;
			GetComponent<Renderer>().material.color= colorida;
		}
		else if (numOfLetters == wordCTRL[0].word.Length-1)
		{
			SubmitTxt[0].GetComponent<TextMesh> ().text = "+1 LETTER";

			Color colorida = GetComponent<Renderer>().material.color;
			colorida.a = 0.4f;
			GetComponent<Renderer>().material.color= colorida;
		}
		else
		{
			Color colorida = GetComponent<Renderer>().material.color;
			colorida.a = 1f;
			GetComponent<Renderer>().material.color= colorida;

			if(numOfLetters == 1)
				SubmitTxt[0].GetComponent<TextMesh> ().text = "";
			else
				SubmitTxt[0].GetComponent<TextMesh> ().text = "ENTER";
		}

		SubmitTxt[0].transform.position = new Vector3(transform.position.x - (transform.lossyScale.x*157),SubmitTxt[0].transform.position.y ,SubmitTxt[0].transform.position.z);
		/*if(numOfLetters == 1)
		{
			gright[0].transform.localScale = new Vector3(0,0,0);
			gleft[0].transform.localScale = new Vector3(0,0,0);
		}
		else
		{
			gright[0].transform.localScale = new Vector3(1, 1, 1);
			gleft[0].transform.localScale = new Vector3(1, 1, 1);
			gright[0].transform.position  = transform.position;
			float tempposx;
			tempposx = transform.position.x -  renderer.bounds.size.x;
			gleft[0].transform.position  = new Vector3(tempposx, gleft[0].transform.position.y, gleft[0].transform.position.z);
		}*/
	}

	void inputBackspaceCase()
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
			//if((i-1)>=1)
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
