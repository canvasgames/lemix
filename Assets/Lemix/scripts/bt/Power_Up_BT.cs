using UnityEngine;
using System.Collections;

public class Power_Up_BT : MonoBehaviour {

	int my_type_Attack;
	int my_type_Defense;
	int turn_0def_1att;
	int isOut;
	int isIn;
	int moveIn1moveOut2;
	mp_controller[] mp;
	PowerUpCtrl[] pwctrl;

	public Sprite[] frozen;
	public Sprite[] firstletter;
	public Sprite[] secondletter;
	public Sprite[] lastletter;
	public Sprite[] thistletter;
	public Sprite[] moretime;
	public Sprite[] goldletter;
	public Sprite[] dark;
	public Sprite[] erase;
	public Sprite[] earthquake;

	public SpriteRenderer btSprite;
	public int myType;

	Vector3 originalPos;
	Vector3 outofScreenPos;
	private int speed = 10;
	float appearTimer = 0f;
	int sortID;

	// Use this for initialization
	void Start () {
		pwctrl = FindObjectsOfType(typeof(PowerUpCtrl)) as PowerUpCtrl[];
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];

		originalPos = transform.position;
		outofScreenPos = new Vector3(970,-387,0);
		isOut = 0;
		isIn = 1;
		moveIn1moveOut2 = 0;

		isIn = 0;
		moveIn1moveOut2 = 2;
		appearTimer =10f;
		//appearTimer =2f;



		sortID =  Random.Range(3,7);
		my_type_Defense = sortID;

		sortID =  Random.Range(7,11);
		my_type_Attack = sortID;
		//my_type_Attack = 10;

		changeSprite(my_type_Defense);
	}
	
	// Update is called once per frame
	void Update () {

		if(moveIn1moveOut2 == 2)
		{
			transform.position = Vector3.Lerp (transform.position, outofScreenPos, Time.deltaTime * speed);
			if(transform.position.x > outofScreenPos.x - 5 && transform.position.x < outofScreenPos.x + 5)
			{
				transform.position = outofScreenPos;
				moveIn1moveOut2 =0;
				isOut = 1;
			/*	if(turn_0def_1att == 0)
				{
					changeSprite(my_type_Defense);
				}
				else
				{
					changeSprite(my_type_Attack);
				}*/
				if(myType == 0)
				{
					changeSprite(my_type_Defense);
				}
				else
				{
					changeSprite(my_type_Attack);
				}
			}
		}
		if(isOut == 1 || moveIn1moveOut2 == 2 )
		{
			appearTimer -= Time.deltaTime;
		
			if(appearTimer <=0)
			{
				moveIn1moveOut2 = 1;
				isOut = 0;
				//outofScreenPos
			}
		}

		if(moveIn1moveOut2 == 1)
		{
			transform.position = Vector3.Lerp (transform.position, originalPos, Time.deltaTime * speed);
			//Debug.Log("entrando");
			if(transform.position.x > originalPos.x - 5 && transform.position.x < originalPos.x + 5)
			{
				transform.position = originalPos;
				//Debug.Log("dentro");
					moveIn1moveOut2 =0;
					isIn = 1;
			}
		}
	}

	void OnMouseDown()
	{
		clickOrKeyboard ();

	}

	public void clickOrKeyboard ()
	{
		if(isIn == 1)
		{
			isIn = 0;
			moveIn1moveOut2 = 2;
			appearTimer =25f;
			//appearTimer =2f;
			/*if(turn_0def_1att == 0)
			{
				actBT(my_type_Defense);
				turn_0def_1att = 1;
				sortID =  Random.Range(3,7);
				my_type_Defense = sortID;
				

			}
			else
			{
				actBT(my_type_Attack);
				turn_0def_1att = 0;
				sortID =  Random.Range(7,10);
				my_type_Attack = sortID;
			}*/
			if(myType == 0)
			{
				actBT(my_type_Defense);
				sortID =  Random.Range(3,7);
				my_type_Defense = sortID;
				

			}
			else
			{
				actBT(my_type_Attack);

				sortID =  Random.Range(7,10);
				my_type_Attack = sortID;
			}
		}
	}

	void letterCase ()
	{
		GLOBALS.Singleton.PUCHOOSELETTER = 1;
		Tile[] myTiles = FindObjectsOfType(typeof(Tile)) as Tile[];
		PowerUpCtrl[] pwctrl = FindObjectsOfType(typeof(PowerUpCtrl)) as PowerUpCtrl[];
		
		int i;
		bool verify;

		//Verifica se todas letras foram mostradas
		for(i=0;i<myTiles.Length;i++)
		{
			verify = pwctrl[0].verifyLetterInWhiteSquares(myTiles[i]._myLetter.ToString());
			
			//Caso todas foram mostradas pinta de cinza e bloqueia o botao
			if(verify == false)
			{
				myTiles[i].GetComponentInChildren<SpriteRenderer> ().color = Color.gray;
				myTiles[i].PUNotClicable = 1;
			}
			//Senao pinta de vermelho
			else
				myTiles[i].GetComponentInChildren<SpriteRenderer> ().color = Color.red;
		}
	}

	void changeSprite(int btType)
	{
		switch (btType)
		{
		case 1:
			btSprite.sprite = goldletter[0];
			break;
		case 2:
			btSprite.sprite = moretime[0];
			break;
		case 3:
			btSprite.sprite = firstletter[0];
			break;	
		case 4:
			btSprite.sprite = secondletter[0];
			break;
		case 5:
			btSprite.sprite = lastletter[0];
			break;
		case 6:
			btSprite.sprite = thistletter[0];
			break;
		case 7:
			btSprite.sprite = frozen[0];
			break;
		case 8:
			btSprite.sprite = dark[0];
			break;
		case 9:
			btSprite.sprite = erase[0];
			break;
		case 10:
			btSprite.sprite = earthquake[0];
			break;
		}
	}

	void actBT(int btType)
	{
		switch (btType)
		{
			case 1:
				pwctrl[0].goldLetter();
				break;
			case 2:
				pwctrl[0].moreTime();
				break;
			case 3:
				pwctrl[0].showLetter(0);
				break; 
			case 4:
				pwctrl[0].showLetter(1);
				break;
			case 5:
				pwctrl[0].showLastLetter();
				break;
			case 6:
				letterCase();
				break;
			case 7:
				pwctrl[0].freezeOPSprite();
				if(GLOBALS.Singleton.MP_MODE == 1)
					mp[0].send_frozen_letter();
				break;
			case 8:
				pwctrl[0].curtainOPSprite();
				if(GLOBALS.Singleton.MP_MODE == 1)
					mp[0].send_dark();
				break;
			case 9:
				pwctrl[0].eraseWord(GLOBALS.Singleton.OP_PLAYER );
				break;
			case 10:
				pwctrl[0].earthquakeAvatarEffectP2();
				if(GLOBALS.Singleton.MP_MODE == 1)
					mp[0].send_earthquake();
				break;
		}
	}
}
