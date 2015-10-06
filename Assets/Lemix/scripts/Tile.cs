using UnityEngine;
using System.Collections;
using System;
using Thinksquirrel.WordGameBuilder;
using Thinksquirrel.WordGameBuilder.Gameplay;
using Thinksquirrel.WordGameBuilder.ObjectModel;

public class Tile : MonoBehaviour {

	//Nomes obvios
	public char _myLetter;
	public float my_x_pos;
	public float my_y_pos;
		
	//Posiçao do tile quando esta na mesa de submissao
	public int myID;
	//Tile esta na mesa ou nao ou em transiçao
	public int onTheTable;
	private Color originalColor;

	//Variavel Power Up escolher letra
	public int PUNotClicable;


	//Variaveis Power Up Freeze
	public GameObject MrFreeze;
	GameObject freeza;
	public int freezed;
	public int freezeCounter;
	int waitingtoFreeze;

	//Pra onde o tile esta indo
	private Vector3 target = new Vector3 (0, 0, 0);
	//Velocidade que o tile muda de posiçao
	private int speed = 10;

	void Awake ()
	{
		myID = -1;
	}
	// Use this for initialization
	void Start () 
	{
		originalColor = GetComponent<Renderer>().material.color;

	}
	
	// Update is called once per frame
	void Update () 
	{
		verifyIfIsMoving ();
	}

	void verifyIfIsMoving ()
	{
		//Se o tile foi clicado faz o movimento
		if (onTheTable == 2||onTheTable == 3 ) 
		{
			transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime * speed);
			if(freezed == 1)
				freeza.transform.position = transform.position;
			//Como o movimento vai ficando cada vez mais lento 
			//Quando esta perto o suficiente para o movimento e posiciona o tile
			if(transform.position.y>target.y - 5 && transform.position.y<target.y+5 && transform.position.x>target.x - 5 && transform.position.x<target.x+5)
			{
				transform.position = target;
				
				//Foi movido de baixo para a mesa
				if(onTheTable == 2)
					onTheTable = 1;
				//Foi movido da mesa para baixo
				else 
				{
					onTheTable = 0;
					if(waitingtoFreeze == 1)
						freeze();
				}
			}
		}
	}
	void OnMouseDown() 
	{
		//Se nao esta no caso do Power up de escolher letra
		if(SAFFER.Singleton.PUCHOOSELETTER == 0)
		{
			if(freezed == 0)
				move();
			else
				unfreezeMe();
		}
		else
		{
			//Se o botao nao esta bloqueado
			if(PUNotClicable == 0 && freezed == 0)
			{
				PUChooseLetter();
			}
			else
			{
				if(freezed == 1)
					unfreezeMe();

			}
			//GetComponentInChildren<SpriteRenderer> ().color = Color.gray;
		}
	}

	public void move()
	{

		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		Submit_And_Input_Ctrl[] submitBT = FindObjectsOfType(typeof(Submit_And_Input_Ctrl)) as Submit_And_Input_Ctrl[];

		//Se nao esta na mesa 
		if (onTheTable == 0) 
		{
			ConstructTable[] constTab = FindObjectsOfType(typeof(ConstructTable)) as ConstructTable[];
			int i = 0;

			//Procura uma posiçao vaga na mesa
			while (wordCTRL[0].atable[i] != '\0') 
				i++;

			//Posiçao vaga recebe a letra
			wordCTRL[0].atable [i] = _myLetter;


			//Seta minha ID na mesa
			myID = i;

			//Movimenta o tile
			float pos = constTab[0].x_pos_init + i * constTab[0].tiles_space;
			target = new Vector3 (pos, -190, 0);
			onTheTable = 2;

			//Muda scale do BT submit
			if(i>=0)
				submitBT[0].changeScale(wordCTRL[0].word.Length - i);

			//Verifica se eh pra dar autosubmit
			submitBT[0].verifyFullTable(i+1);


		} 
		//Se esta na mesa
		else if(onTheTable == 1) 
		{
			int i = 0;

			//Procura uma posiçao vaga na mesa
			while (wordCTRL[0].atable[i] != '\0') 
				i++;

			//Limpa a ultima posiçao ocupada
			wordCTRL[0].atable [i-1] = '\0';

			//Muda scale do BT submit
			if((i-1)>=1)
				submitBT[0].changeScale(wordCTRL[0].word.Length - (i-2));
			else 
				submitBT[0].changeScale(1);


			Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];


			i=0;
			int majorID=0;
			int majorNUM=0;

			//Procura o tile na ultima posiçao da mesa
			for(i=0;i<myTiles.Length;i++)
			{
				if(myTiles[i].myID > majorID)
				{
					majorID = myTiles[i].myID;
					majorNUM=i;
				}
			}

			//Move tile com maior posiçao
			if(majorID == 0)
				moveMe();
			else
				myTiles[majorNUM].moveMe();
		
		}	
	}

	public void moveMe()
	{
		myID = -1;
		onTheTable = 3;
		target = new Vector3 (my_x_pos, my_y_pos, 0);
	}

	//Tenta congelar
	public void tryFreezeMe()
	{
		//Se esta na mesa congela
		if(onTheTable == 0)
			freeze();
		//Senao aguarda ate volta a mesa
		else
			waitingtoFreeze = 1;
	}

	//Congelando
	void freeze()
	{
		waitingtoFreeze = 0;
		freeza = (GameObject)Instantiate (MrFreeze, transform.position, transform.rotation);
		freeza.GetComponent<Renderer>().sortingOrder = 5;
		freezed = 1;
		freezeCounter = 5;
	}

	//Descongelando
	public void unfreezeMe()
	{
		freezeCounter--;

		//Detroi o gelo
		if(freezeCounter <=0)
		{
			freezed = 0;

			Destroy(freeza);
		}

	}

	public void backOriginalColor()
	{
		GetComponentInChildren<SpriteRenderer> ().color = originalColor;
	}

	public void PUChooseLetter()
	{
		PowerUpCtrl[] pwctrl = FindObjectsOfType(typeof(PowerUpCtrl)) as PowerUpCtrl[];
		pwctrl[0].showThisLetter(_myLetter.ToString());
		
		Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		//Zera variavel do caso Power Up
		SAFFER.Singleton.PUCHOOSELETTER = 0;
		
		int i;
		
		//Desbloqueia todos botoes e reverte a cor original
		for(i=0;i<myTiles.Length;i++)
		{
			myTiles[i].PUNotClicable = 0;
			myTiles[i].GetComponentInChildren<SpriteRenderer> ().color = originalColor;
		}
	}
}
