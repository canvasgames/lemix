using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Power_Up_BT : MonoBehaviour {

	int my_type_Attack, my_type_Defense;
	int turn_0def_1att;
	int isOut, isIn, moveIn1moveOut2;
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
	float appearTimer = 0f, setAppearTime = 25f;
	int sortID;

	// Use this for initialization
	void Start () {
		pwctrl = FindObjectsOfType(typeof(PowerUpCtrl)) as PowerUpCtrl[];
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
		pulse();
		originalPos = transform.position;
		outofScreenPos = new Vector3(970,-387,0);
		isOut = 0;
		isIn = 1;
		moveIn1moveOut2 = 0;

		isIn = 0;
		moveIn1moveOut2 = 2;
		appearTimer = setAppearTime;

		sortID =  Random.Range(3,7);
		//sortID =  6;
		my_type_Defense = sortID;

		sortID =  Random.Range(7,11);
		//sortID =  10;
		my_type_Attack = sortID;

		changeSprite(my_type_Defense);
	}
	
	// Update is called once per frame
	void Update () {
		if(GLOBALS.Singleton.GAME_RUNNING == true)
		{
			if(moveIn1moveOut2 == 2)
			{
				transform.position = Vector3.Lerp (transform.position, outofScreenPos, Time.deltaTime * speed);
				if(transform.position.x > outofScreenPos.x - 5 && transform.position.x < outofScreenPos.x + 5)
				{
					transform.position = outofScreenPos;
					moveIn1moveOut2 =0;
					isOut = 1;
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
	}
	void pulse()
	{
		transform.DOScale(new Vector3(1.3f,1.3f,transform.localScale.z),0.8f).OnComplete(repulse);
	}

	void repulse()
	{
		transform.DOScale(new Vector3(1.5f,1.5f,transform.localScale.z),0.8f).OnComplete(pulse);
	}
	void OnMouseDown()
	{
		if(GLOBALS.Singleton.GAME_RUNNING == true && GLOBALS.Singleton.GAME_QUIT_MENU == false)
		{
			clickOrKeyboard ();
		}
	}

	public void clickOrKeyboard ()
	{
		if(isIn == 1)
		{
			isIn = 0;
			moveIn1moveOut2 = 2;
			appearTimer = setAppearTime;
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
				pwctrl[0].chooseALetter();
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
