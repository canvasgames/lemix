using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
public class PowerUpCtrl : MonoBehaviour {

	GameObject curtain;
	//Objects
	public GameObject BlackNight, Eraser, Frozen, CurtainOP ;
	//Texts
	public GameObject pwTxt, pwChooseTxt; 
	public float smooth;


	float curtainTimer = 0f, freezeOPTime=0f, curtainTimerOP;
	private Vector3 newPosition;
	Vector3 positionACurtain, positionBCurtain, pwTxtPos ;
	float PwTxtTime = 0.6f, PwTxtFinalPos = -130f;

	float goldLetterTime = 0f;
	Earthquake[] earth;
	earthquakePlayer2[] earthP2;

	mp_controller[] mp;

	// Use this for initialization
	void Start () 
	{
		smooth = 2.7f;

		//Posiçao da curtinada
		positionACurtain = new Vector3(0, 240, 0);
		positionBCurtain = new Vector3(0,950,0);

		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
		earth = FindObjectsOfType(typeof(Earthquake)) as Earthquake[];
		earthP2 = FindObjectsOfType(typeof(earthquakePlayer2)) as earthquakePlayer2[];

		pwTxtPos = pwTxt.transform.position;
		pwTxt.GetComponent<SpriteRenderer>().DOFade(0f,0f);
		pwChooseTxt.GetComponent<SpriteRenderer>().DOFade(0f,0f);
	}
	
	// Update is called once per frame
	void Update () 
	{

		//My curtain
		if(curtain != null && GLOBALS.Singleton.GAME_RUNNING == true)
		{
			curtainTimer -= Time.deltaTime;

			if(curtainTimer <=0)
				PositionChangingBack();
			else
				PositionChangingGoing();
				
		}

		//Gold Letter
		if(GLOBALS.Singleton.PUGOLDLETTERACTIVE == 1 && GLOBALS.Singleton.GAME_RUNNING == true)
		{
			goldLetterTime -= Time.deltaTime;
			if(goldLetterTime<=0)
			{
				endGoldLetter();
			}
		}

		//Freeze op sprite
		if(freezeOPTime >0 && GLOBALS.Singleton.GAME_RUNNING == true)
		{
			freezeOPTime  -= Time.deltaTime;
			if(freezeOPTime <=0)
				unfreezeOPSprite();
		}

		//Curtain op
		if(curtainTimerOP >0 && GLOBALS.Singleton.GAME_RUNNING == true)
		{
			curtainTimerOP  -= Time.deltaTime;
			if(curtainTimerOP <=0)
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

		percentage = wordCTRL[0].list.Count * (GLOBALS.Singleton.PULetterInitPercentage + (GLOBALS.Singleton.PULetterPlusPercentage * GLOBALS.Singleton.USER_HAT_POWER)) ;

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
		
		percentage = wordCTRL[0].list.Count * (GLOBALS.Singleton.PULetterInitPercentage + (GLOBALS.Singleton.PULetterPlusPercentage * GLOBALS.Singleton.USER_HAT_POWER)) ;
		
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
		
		percentage =  cont * (GLOBALS.Singleton.PULetterInitPercentage + (GLOBALS.Singleton.PULetterPlusPercentage * GLOBALS.Singleton.USER_HAT_POWER)) ;
		
		while (percentage > 0 && list.Count>0 ) 
		{
			
			rand = Random.Range(0,list.Count);
			squares[list[rand]].appearPowerUp();
			percentage--;
			list.Remove(list[rand]);
			
		}	
		GLOBALS.Singleton.PUCHOOSELETTER = 0;
		dessapearChooseTxt();
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
		if(GLOBALS.Singleton.MP_MODE == 0)
			freezeOPTime = Random.Range(3f,7f);
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

	public void freezeTxt()
	{
		pwTxt.GetComponent<Animator>().Play("frozen");
		movePwTxt();
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
		CurtainOP.transform.DOLocalMoveY(500,0.5f);
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
		GLOBALS.Singleton.PUGOLDLETTERACTIVE = 1;

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
		GLOBALS.Singleton.PUGOLDLETTERACTIVE = 0;
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

			//Change the word variables
			wctrl[0].list[temp[sortID]].found = false;
			wctrl[0].list[temp[sortID]].foundedByPlayer = 0;


			WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];

			//Search the white squares of the word and erase
			for(i=0;i<squares.Length;i++)
			{
				if(squares[i].myID == temp[sortID])
				{
					squares[i].erasePowerUp();
				}
			
			}

			//Call the big eraser in the screen
			callEraser();

			//If is MP mode send the erased word for the OP
			if(GLOBALS.Singleton.MP_MODE == 1)
				mp[0].send_erase(temp[sortID]);

			wctrl[0].eraseWordUpdateScore(player, temp[sortID], wctrl[0].list[temp[sortID]].goldLetterActive);
			wctrl[0].list[temp[sortID]].goldLetterActive = 0;
		}


	}

	public void eraseWordReceive(int ID)
	{
		WController[] wctrl = FindObjectsOfType(typeof(WController)) as WController[];
		WhiteSquare[] squares = FindObjectsOfType(typeof(WhiteSquare)) as WhiteSquare[];
		int i;

		for(i=0;i<squares.Length;i++)
		{
			if(squares[i].myID == ID)
			{
				squares[i].erasePowerUp();
				wctrl[0].list[ID].found = false;
				wctrl[0].list[ID].foundedByPlayer = 0;
			}


		}
		wctrl[0].eraseWordUpdateScore(GLOBALS.Singleton.MP_PLAYER , ID, wctrl[0].list[ID].goldLetterActive);
		//Call the big eraser in the screen
		callEraser();
	}
	
	void callEraser()
	{
		//Call the big eraser in the screen
		Eraser.SetActive(true);
		eraser_sprite[] eraser = FindObjectsOfType(typeof(eraser_sprite)) as eraser_sprite[];
		eraser[0].step1();
	}
	public void earthquakeReceive ()
	{
		pwTxt.GetComponent<Animator>().Play("earthquake");
		movePwTxt();
		earth[0].startEarthquake(4f,1f,12f);
	}

	public void  earthquakeAvatarEffectP2 ()
	{
		earthP2[0].startEarthquake(4f,7f);
	}

	public void chooseALetter()
	{
		GLOBALS.Singleton.PUCHOOSELETTER = 1;
		Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];

		
		int i;
		bool verify;
		
		//Verifica se todas letras foram mostradas
		for(i=0;i<myTiles.Length;i++)
		{
			verify = verifyLetterInWhiteSquares(myTiles[i]._myLetter.ToString());
			
			//Caso todas foram mostradas pinta de cinza e bloqueia o botao
			if(verify == false)
			{
				myTiles[i].GetComponentInChildren<SpriteRenderer> ().color = Color.gray;
				myTiles[i].PUNotClicable = 1;
			}
			//Senao pinta de vermelho
			else
			{
				pwTxt.GetComponent<Animator>().Play("choose");
				moveTxtChoose();
				myTiles[i].GetComponentInChildren<SpriteRenderer> ().color = Color.red;
				myTiles[i].transform.DOShakePosition(10000f,3f,3);
			}
		}
	}

	void movePwTxt()
	{
		pwTxt.transform.DOMoveY(PwTxtFinalPos,PwTxtTime).OnComplete(waitPwTxt);
		pwTxt.SetActive(true);
		pwTxt.GetComponent<SpriteRenderer>().DOFade(1f,0.6f);
	}

	void waitPwTxt()
	{
		pwTxt.GetComponent<SpriteRenderer>().DOFade(1f,1f).OnComplete(dessapearPwTxt);
	}
	
	void dessapearPwTxt()
	{
		pwTxt.transform.DOKill();
		pwTxt.GetComponent<SpriteRenderer>().DOKill();
		pwTxt.transform.DOMoveY(pwTxtPos.y,0f);
		pwTxt.GetComponent<SpriteRenderer>().DOFade(0f,0f);
		pwTxt.SetActive(false);
	}

	void moveTxtChoose()
	{
		pwChooseTxt.transform.DOMoveY(PwTxtFinalPos,PwTxtTime).OnComplete(waitChooseTxt);
		pwChooseTxt.SetActive(true);
		pwChooseTxt.GetComponent<SpriteRenderer>().DOFade(1f,0.6f);



	}
	void waitChooseTxt()
	{
		pwChooseTxt.GetComponent<SpriteRenderer>().DOFade(1f,1f);
	}

	void dessapearChooseTxt()
	{
		pwChooseTxt.transform.DOKill();
		pwChooseTxt.GetComponent<SpriteRenderer>().DOKill();
		pwChooseTxt.transform.DOMoveY(pwTxtPos.y,PwTxtTime);
		pwChooseTxt.GetComponent<SpriteRenderer>().DOFade(0f,0.3f);
		pwChooseTxt.SetActive(false);
	}
}
