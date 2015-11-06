using UnityEngine;
using System.Collections;
using System;
using Thinksquirrel.WordGameBuilder;
using Thinksquirrel.WordGameBuilder.Gameplay;
using Thinksquirrel.WordGameBuilder.ObjectModel;
using DG.Tweening;

public class Tile : MonoBehaviour {

	//Nomes obvios
	public char _myLetter;
	public float my_x_pos;
	public float my_y_pos;

	//Time to go to the target
	float tileSpeed = 0.2f;

	//Posiçao do tile quando esta na mesa de submissao
	public int myID;
	//Tile esta na mesa ou nao ou em transiçao
	public int onTheTable;
	private Color originalColor;

	//Variavel Power Up escolher letra
	public int PUNotClicable;


	//Variaveis Power Up Freeze
	public GameObject MrFreeze, unfreezeParticle;
	GameObject freeza, particle;
	public int freezed;
	public int freezeCounter;
	int waitingtoFreeze;

	//Pra onde o tile esta indo
	private Vector3 target = new Vector3 (0, 0, 0);

	mp_controller[] mp;

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
		//verifyIfIsMoving ();
		if(freeza != null)
			freeza.transform.position = transform.position;
	}


	void OnMouseDown() 
	{
		//Se nao esta no caso do Power up de escolher letra
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
		{
			if(GLOBALS.Singleton.PUCHOOSELETTER == 0)
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
	}

	public void move()
	{
		
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		Submit_And_Input_Ctrl[] submitBT = FindObjectsOfType(typeof(Submit_And_Input_Ctrl)) as Submit_And_Input_Ctrl[];
		Debug.Log(onTheTable);
		//If is not on the submission table or is goin to low position
		if (onTheTable == 0 || onTheTable == 3) 
		{
			ConstructTable[] constTab = FindObjectsOfType(typeof(ConstructTable)) as ConstructTable[];
			int i = 0;

			Debug.Log("Do que vc esta falando leigo");
			//Search a position on the submission table
			while (wordCTRL[0].atable[i] != '\0') 
				i++;
			
			//Empty position receive the letter
			wordCTRL[0].atable [i] = _myLetter;
			
			
			//Set my ID on the table
			myID = i;
			
			//Move the tile
			float pos = constTab[0].x_pos_init + i * constTab[0].tiles_space;
			target = new Vector3 (pos, constTab[0].y_pos_submit_table, 0);
			transform.DOKill();
			transform.DOMove(target,tileSpeed).OnStepComplete(changeStatusTo1);
			onTheTable = 2;
			
			//Changes the scale of submit BT 
			if(i>=0)
				submitBT[0].changeScale(wordCTRL[0].word.Length - i);
			
			/*//Verifica se eh pra dar autosubmit
			submitBT[0].verifyFullTable(i+1);*/
			
			
		} 
		//If is on the table or goin 2
		else if(onTheTable == 1 || onTheTable == 2) 
		{
			int i = 0;
			
			//Search a position on the table
			while (wordCTRL[0].atable[i] != '\0') 
				i++;
			
			//Clear the last ocuppied position
			wordCTRL[0].atable [i-1] = '\0';
			
			///Changes the scale of submit BT 
			if((i-1)>=1)
				submitBT[0].changeScale(wordCTRL[0].word.Length - (i-2));
			else 
				submitBT[0].changeScale(1);
			
			
			Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
			
			
			i=0;
			int majorID=0;
			int majorNUM=0;
			
			//Search for a tile on the last position of the table
			for(i=0;i<myTiles.Length;i++)
			{
				if(myTiles[i].myID > majorID)
				{
					majorID = myTiles[i].myID;
					majorNUM=i;
				}
			}
			
			//Move the tile with the biggest position
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
		transform.DOKill();
		transform.DOMove(target,tileSpeed).OnStepComplete(changeStatusTo0);
	}

	void changeStatusTo1()
	{
		onTheTable = 1;

		Submit_And_Input_Ctrl[] submitBT = FindObjectsOfType(typeof(Submit_And_Input_Ctrl)) as Submit_And_Input_Ctrl[];

		//Verifica se eh pra dar autosubmit
		submitBT[0].verifyFullTable(myID+1);
	}
	void changeStatusTo0()
	{
		onTheTable = 0;
		if(waitingtoFreeze == 1)
			freeze();
	}





	//Try to frooze
	public void tryFreezeMe()
	{
		//If is on the table frooze
		if(onTheTable == 0)
			freeze();
		//Eles wait until is on the table
		else
			waitingtoFreeze = 1;
	}

	//Freezing
	void freeze()
	{
		waitingtoFreeze = 0;
		freeza = (GameObject)Instantiate (MrFreeze, transform.position, transform.rotation);
		freeza.GetComponent<Renderer>().sortingOrder = 55;

		freezed = 1;
		freezeCounter = 5;

		PowerUpCtrl[] pwCTRL = FindObjectsOfType(typeof(PowerUpCtrl)) as PowerUpCtrl[];
		pwCTRL[0].freezeTxt();
	}

	//Unfreezing
	public void unfreezeMe()
	{
		freezeCounter--;
		freeza.transform.DOShakePosition(1f,4f);
		freeza.GetComponent<Animator>().Play("pw_frozen_broked");
		Destroy(particle);
		particle = (GameObject)Instantiate (unfreezeParticle, transform.position, transform.rotation);
		//Destruct the ice
		if(freezeCounter <=0)
		{
			freezed = 0;
			mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
			Destroy(freeza);
			Destroy(particle,3f);
			if(GLOBALS.Singleton.MP_MODE == 1)
				mp[0].send_end_of_freeze_time();

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


		int i;
		
		//Unlock all the buttons and back to the original color
		for(i=0;i<myTiles.Length;i++)
		{
			myTiles[i].PUNotClicable = 0;
			myTiles[i].GetComponentInChildren<SpriteRenderer> ().color = originalColor;
			myTiles[i].transform.DOKill();
		}
	}





	/*public void moveMe()
	{
		myID = -1;
		onTheTable = 3;
		target = new Vector3 (my_x_pos, my_y_pos, 0);
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
			target = new Vector3 (pos, constTab[0].y_pos_submit_table, 0);
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
	}*/

	/*void verifyIfIsMoving ()
	{
		//Debug.Log(onTheTable);
		//Moving
		if ( (onTheTable == 2||onTheTable == 3) && GLOBALS.Singleton.GAME_RUNNING == true ) 
		{
			//transform.position = Vector3.Lerp (transform.position, target, Time.deltaTime * speed);
			if(freezed == 1)
				freeza.transform.position = transform.position;

			//When is close to target change the flag
			if(transform.position.y>target.y && transform.position.y<target.y && transform.position.x>target.x  && transform.position.x<target.x)
			{
				transform.position = target;

				//Moved from the bottom to the submission table
				if(onTheTable == 2)
					onTheTable = 1;
				//Moved from the submission table to the bottom
				else 
				{
					onTheTable = 0;
					if(waitingtoFreeze == 1)
						freeze();
				}
			}
		}
	}*/
}
