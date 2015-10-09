using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
public class PowerUpCtrl : MonoBehaviour {

	GameObject curtain;
	public GameObject BlackNight, Eraser, Frozen, CurtainOP ;
	public float smooth;
	float curtainTimer = 0f, freezeOPTime=0f, curtainTimerOP;
	private Vector3 newPosition;
	Vector3 positionACurtain;
	Vector3 positionBCurtain;

	float goldLetterTime = 0f;


	mp_controller[] mp;

	// Use this for initialization
	void Start () 
	{
		smooth = 2.7f;

		//Posiçao da curtinada
		positionACurtain = new Vector3(0, 220, 0);
		positionBCurtain = new Vector3(0,950,0);

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

		if(freezeOPTime >0)
		{
			freezeOPTime  -= Time.deltaTime;
			if(freezeOPTime <0)
				unfreezeOPSprite();
		}

		if(curtainTimerOP >0)
		{
			curtainTimerOP  -= Time.deltaTime;
			if(curtainTimerOP <0)
				uncurtainOPSprite();
		}
	}

	void PositionChangingGoing ()
	{
		curtain.transform.position = Vector3.Lerp(curtain.transform.position, positionACurtain, smooth * Time.deltaTime);
	}
	void PositionChangingBack ()
	{
		curtain.transform.position = Vector3.Lerp(curtain.transform.position, positionBCurtain, smooth * Time.deltaTime);
		if(curtain.transform.position.y>(int)positionBCurtain.y)
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


	public void freezeOPSprite()
	{
		Frozen.SetActive(true);
		if(SAFFER.Singleton.MP_MODE == 0)
			freezeOPTime =Random.Range(3f,7f);
	}
	public void unfreezeOPSprite()
	{
		freezeOPTime = 0;
		Frozen.SetActive(false);
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

			curtain = (GameObject)Instantiate (BlackNight, new Vector3(0,880,0), transform.rotation);

			curtain.transform.localScale = new Vector3(1f,1.09f,1f);
			curtain.GetComponent<Renderer>().sortingOrder = 50;
			curtainTimer = 10f;
		}

	}

	public void curtainOPSprite()
	{
		CurtainOP.SetActive(true);
		CurtainOP.transform.DOLocalMoveY(445,0.5f);
		curtainTimerOP = 10f;
	}

	public void uncurtainOPSprite()
	{
		curtainTimerOP = 0;
		CurtainOP.transform.DOLocalMoveY(650,0.5f).OnComplete(unactiveCurtain);
	}

	void unactiveCurtain()
	{
		CurtainOP.SetActive(false);
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

	public void eraseWord(int player)
	{

		WController[] wctrl = FindObjectsOfType(typeof(WController)) as WController[];
		int i;
		List<int> temp = new List<int>();

		//Procura palavras ja encontradas
		for(i=0;i<wctrl[0].list.Count;i++)
		{
			//Se foi encontrada pelo player passado (OP ou MP)
			if(wctrl[0].list[i].foundedByPlayer == player)
			{
					//Adiciona no temporario
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
			Eraser.SetActive(true);
			eraser_sprite[] eraser = FindObjectsOfType(typeof(eraser_sprite)) as eraser_sprite[];

		
			eraser[0].step1();
			//se esta no MP envia a palavra
			if(SAFFER.Singleton.MP_MODE == 1)
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
