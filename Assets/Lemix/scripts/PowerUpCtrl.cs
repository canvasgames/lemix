using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpCtrl : MonoBehaviour {

	GameObject curtain;
	public GameObject BlackNight;
	public float smooth;
	float curtainTimer = 0f;
	private Vector3 newPosition;
	Vector3 positionA;
	Vector3 positionB;

	float goldLetterTime = 0f;


	mp_controller[] mp;

	// Use this for initialization
	void Start () 
	{
		smooth = 2.7f;
		positionA = new Vector3(0, 200, 0);
		positionB = new Vector3(0,750,0);

		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Curtain
		if(curtain != null)
		{
			curtainTimer -= Time.deltaTime;

			if(curtainTimer <=0)
				PositionChangingBack();
			else
				PositionChangingGoing();
				
		}

		//Gold Letter
		if(SAFFER.Singleton.PUGOLDLETTERACTIVE == 1)
		{
			goldLetterTime -= Time.deltaTime;
			if(goldLetterTime<=0)
			{
				endGoldLetter();
			}
		}
	}

	void PositionChangingGoing ()
	{
		curtain.transform.position = Vector3.Lerp(curtain.transform.position, positionA, smooth * Time.deltaTime);
	}
	void PositionChangingBack ()
	{
		curtain.transform.position = Vector3.Lerp(curtain.transform.position, positionB, smooth * Time.deltaTime);
		if(curtain.transform.position.y>740)
		{
			curtainTimer = 0;
			Destroy(curtain);
		}
	}
	//For first and second letter
	public void  showLetter(int letter_pos)
	{
		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i;
		List<int> list = new List<int>();

		//Adiciona posiçoes que podem ainda nao foram mostradas a uma lista
		for(i=0;i<squares.Length;i++)
		{
			if(squares[i].myIDindex == letter_pos && squares[i].showed == 0)
			{
				list.Add(i);
			}
		}

		//Embaralhando a string da palavra
		int rand;
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		
	
		

		//Porcentagem e talz
		float percentage;

		//Hat

		percentage = wordCTRL[0].list.Count * (SAFFER.Singleton.PULetterInitPercentage + (SAFFER.Singleton.PULetterPlusPercentage * SAFFER.Singleton.USER_HAT_POWER)) ;

		while (percentage > 0 && list.Count>0 ) 
		{

			rand = Random.Range(0,list.Count);
			squares[list[rand]].appearPowerUp();
			percentage--;
			list.Remove(list[rand]);

		}	
	}
	

	public void showLastLetter()
	{
		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i;
		List<int> list = new List<int>();
		
		//Adiciona posiçoes que podem ainda nao foram mostradas a uma lista
		for(i=0;i<squares.Length;i++)
		{
			if(squares[i].lastletter == true && squares[i].showed == 0)
			{
				list.Add(i);
			}
		}
		
		//Embaralhando a string da palavra
		int rand;
		WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];
		
		
		
		
		//Porcentagem e talz
		float percentage;
		
		//Hat
		
		percentage = wordCTRL[0].list.Count * (SAFFER.Singleton.PULetterInitPercentage + (SAFFER.Singleton.PULetterPlusPercentage * SAFFER.Singleton.USER_HAT_POWER)) ;
		
		while (percentage > 0 && list.Count>0 ) 
		{
			
			rand = Random.Range(0,list.Count);
			squares[list[rand]].appearPowerUp();
			percentage--;
			list.Remove(list[rand]);
			
		}	
	}


	
	public void showThisLetter(string letter)
	{
		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i,cont=0;
		List<int> list = new List<int>();
		
		//Adiciona posiçoes que podem ainda nao foram mostradas a uma lista e conta quantos 
		//quadrados tem com a letra escolhida para calculo de porcentagem
		for(i=0;i<squares.Length;i++)
		{
			if(squares[i].myLetter == letter )
			{
				cont++;
				if(squares[i].showed == 0)
					list.Add(i);
			}
		}
		
		//Embaralhando a string da palavra
		int rand;
		
		//Porcentagem e talz
		float percentage;
		
		//Hat
		
		percentage =  cont * (SAFFER.Singleton.PULetterInitPercentage + (SAFFER.Singleton.PULetterPlusPercentage * SAFFER.Singleton.USER_HAT_POWER)) ;
		
		while (percentage > 0 && list.Count>0 ) 
		{
			
			rand = Random.Range(0,list.Count);
			squares[list[rand]].appearPowerUp();
			percentage--;
			list.Remove(list[rand]);
			
		}	
	}

	public bool verifyLetterInWhiteSquares(string letter)
	{
		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i;
		
		//Procura white squares
		for(i=0;i<squares.Length;i++)
		{
			if(squares[i].myLetter == letter && squares[i].showed == 0)
			{

				return true;
			}

		}

		return false;
	}



	public void freezeLetter()
	{
		Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		int i;
		List<int> list = new List<int>();

		for(i=0;i<myTiles.Length;i++)
		{

			if(myTiles[i].freezed == 0)
			{
				list.Add(i);
			}
		}



		int rand;
		if(list.Count >0)
		{
			rand = Random.Range(0,list.Count);
			Debug.Log (rand);
			myTiles[list[rand]].tryFreezeMe();
		}
	}

	public void night()
	{
		if(curtainTimer == 0)
		{

			curtain = (GameObject)Instantiate (BlackNight, new Vector3(0,700,0), transform.rotation);
			curtain.GetComponent<Renderer>().sortingOrder = 50;
			curtainTimer = 10f;
		}

	}

	//Hit me baby one more time
	public void moreTime()
	{
		GameController[] gcontroller = FindObjectsOfType(typeof(GameController)) as GameController[];
		gcontroller[0].AddTime(30f);
	}

	public void goldLetter()
	{
		SAFFER.Singleton.PUGOLDLETTERACTIVE = 1;

		Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		int i;

		goldLetterTime = 10f;
		for(i=0;i<myTiles.Length;i++)
		{
			
			myTiles[i].GetComponentInChildren<SpriteRenderer> ().color = Color.cyan;
			
		}
	}

	void endGoldLetter()
	{
		SAFFER.Singleton.PUGOLDLETTERACTIVE = 0;
		goldLetterTime = 0f;
		Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		int i;


		for(i=0;i<myTiles.Length;i++)
		{
			
			myTiles[i].backOriginalColor();
			
		}
	}

	public void eraseWord()
	{

		WController[] wctrl = FindObjectsOfType(typeof(WController)) as WController[];
		int i;
		List<int> temp = new List<int>();

		//Procura palavras ja encontradas
		for(i=0;i<wctrl[0].list.Count;i++)
		{
			if(wctrl[0].list[i].foundedByPlayer == SAFFER.Singleton.OP_PLAYER )
			{
					temp.Add(i);
			}
		}

		int sortID;
		sortID =  Random.Range(0,temp.Count);
		//Se foi encontrada alguma palavra
		if(temp.Count>0)
		{


			wctrl[0].list[temp[sortID]].found = false;
			wctrl[0].list[temp[sortID]].foundedByPlayer = 0;

			WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];

			//Procura white squares da palavra e apaga
			for(i=0;i<squares.Length;i++)
			{
				if(squares[i].myID == temp[sortID])
				{
					squares[i].erasePowerUp();
				}
			
			}
			mp[0].send_erase(temp[sortID]);
		}


	}

	public void eraseWordReceive(int ID)
	{
		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i;

		for(i=0;i<squares.Length;i++)
		{
			if(squares[i].myID == ID)
			{
				squares[i].erasePowerUp();
			}
			
		}
	}
}
