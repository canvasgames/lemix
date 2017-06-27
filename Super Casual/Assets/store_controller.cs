﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class store_controller : MonoBehaviour {

	#region === Vars ===
    public static store_controller s;

	public int jukeboxCurrentPrice;

	public Text title;
	public Button jukeboxBt;
	public Text jukeboxBtNotes;

    [HideInInspector]public int popAlreadyBuyed ;
    [HideInInspector]public int eletronicAlreadyBuyed;
	[HideInInspector]public int rockAlreadyBuyed;
	[HideInInspector]public int popGagaAlreadyBuyed;
	[HideInInspector]public int reggaeAlreadyBuyed;
	[HideInInspector]public int rapAlreadyBuyed;

    public int popPrice = 70;
    public int rockPrice = 50;
	public int popGagaPrice = 70;
	public int reggaePrice = 50;
    [HideInInspector]
    public int eletronicPrice = 30;

    public Text actualCoins;
    public Text buyPrice;

    public GameObject buyButton;
    public GameObject equipButton;

    int actualCharInScreen;

	#endregion
    // Use this for initialization

	#region === Init ===
    void Start () {
        s = this;

        popAlreadyBuyed = PlayerPrefs.GetInt("popAlreadyBuyed", 0);
		rockAlreadyBuyed = PlayerPrefs.GetInt("rockAlreadyBuyed", 0);
		popGagaAlreadyBuyed = PlayerPrefs.GetInt("popGagaAlreadyBuyed", 0);
		reggaeAlreadyBuyed = PlayerPrefs.GetInt("reggaeAlreadyBuyed", 0);
		eletronicAlreadyBuyed = PlayerPrefs.GetInt("eletronicAlreadyBuyed", 1);
		rapAlreadyBuyed = PlayerPrefs.GetInt("rapAlreadyBuyed", 1);

        changeAnimationEquipButton("eletronic");

		DefineActualCharOnTheScreen ();
		OnCharacterChanged(actualCharInScreen);

        buyPrice.text = eletronicPrice.ToString();

        actualCoins.text = USER.s.NOTES.ToString();

		if (globals.s.FIRST_GAME) {
			actualCharInScreen = 0;
			equipCharacter ();
		}
    }

	void DefineActualCharOnTheScreen(){
		if (globals.s.ACTUAL_CHAR.Equals ("electronic"))
			actualCharInScreen = 0;
		else if (globals.s.ACTUAL_CHAR.Equals ("rock"))
			actualCharInScreen = 1;
		else if (globals.s.ACTUAL_CHAR.Equals ("pop"))
			actualCharInScreen = 2;
		else if (globals.s.ACTUAL_CHAR.Equals ("popGaga"))
			actualCharInScreen = 3;
		else if (globals.s.ACTUAL_CHAR.Equals ("reggae"))
			actualCharInScreen = 4;
		else if (globals.s.ACTUAL_CHAR.Equals ("rap"))
			actualCharInScreen = 5;

//		else if(globals
	
	}

	public void OpenStore(){
		DisplayNotes ();
	}

	#endregion

	public void UpdateUserNotes(){
		actualCoins.text = USER.s.NOTES.ToString();
		DisplayNotes ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region === CHAR ===
	public bool CheckIfCharacterIsAlreadyPurchased(int musicStyle){
		if (musicStyle == 0) {
			if (eletronicAlreadyBuyed == 1)
				return true;
			else
				return false;
		} else if (musicStyle == 1) {
			if (rockAlreadyBuyed == 1)
				return true;
			else
				return false;
		} else if (musicStyle == 2) {
			if (popAlreadyBuyed == 1)
				return true;
			else
				return false;
		} else if (musicStyle == 3) {
			if (popGagaAlreadyBuyed == 1)
				return true;
			else
				return false;
		} else if (musicStyle == 4) {
			if (reggaeAlreadyBuyed == 1)
				return true;
			else
				return false;

		} else if (musicStyle == (int)MusicStyle.Rap) {
			if (rapAlreadyBuyed == 1)
				return true;
			else
				return false;
		} else
			return true;
	}

	public void GiveCharacterForFree(int musicStyle = -1)
	{
		actualCharInScreen = musicStyle;
		equipCharacter();

		if(musicStyle == 0)
		{
			PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
			eletronicAlreadyBuyed = 1;
		}
		else if (musicStyle == 1)
		{
			PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
			rockAlreadyBuyed = 1;
		}
		else if (musicStyle == 2)
		{
			PlayerPrefs.SetInt("popAlreadyBuyed", 1);
			popAlreadyBuyed = 1;
		}

		else if (musicStyle == 3)
		{
			PlayerPrefs.SetInt("popGagaAlreadyBuyed", 1);
			popGagaAlreadyBuyed = 1;
		}
		else if (musicStyle == 4)
		{
			PlayerPrefs.SetInt("reggaeAlreadyBuyed", 1);
			reggaeAlreadyBuyed = 1;
		}

		else if (musicStyle == 5)
		{
			PlayerPrefs.SetInt("rapAlreadyBuyed", 1);
			reggaeAlreadyBuyed = 1;
		}
	}



    void buyed()
    {
        buyButton.SetActive(false);
        equipButton.SetActive(true);
        equipCharacter();
    }
    

    public void equipCharacter()
    {
		Debug.Log ("ACTUAL CHAR IN THE SCREEN: " + actualCharInScreen);
        if (actualCharInScreen == 0)
        {
            Debug.Log(" igona flee gonna be gonna flow");
            PlayerPrefs.SetString("ACTUAL_CHAR", "eletronic");
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "eletronic");
            changeActualChar();

            changeAnimationEquipButton("eletronic");

            sound_controller.s.change_music(MusicStyle.Eletro);

        }
        else if (actualCharInScreen == 1)
        {
            PlayerPrefs.SetString("ACTUAL_CHAR", "rock");
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rock");
            changeActualChar();

            changeAnimationEquipButton("rock");

            sound_controller.s.change_music(MusicStyle.Rock);

        }
        else if (actualCharInScreen == 2)
        {
            PlayerPrefs.SetString("ACTUAL_CHAR", "pop");
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "pop");
            changeActualChar();

            changeAnimationEquipButton("pop");

            sound_controller.s.change_music(MusicStyle.Pop);
        }
		else if (actualCharInScreen == 3)
		{
			PlayerPrefs.SetString("ACTUAL_CHAR", "popGaga");
			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "popGaga");
			changeActualChar();

			changeAnimationEquipButton("popGaga");

			sound_controller.s.change_music(MusicStyle.PopGaga);
		}
		else if (actualCharInScreen == 4)
		{
			PlayerPrefs.SetString("ACTUAL_CHAR", "reggae");
			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "reggae");
			changeActualChar();

			changeAnimationEquipButton("reggae");

			sound_controller.s.change_music(MusicStyle.Reggae);
		}

		else if (actualCharInScreen == (int)MusicStyle.Rap)
		{
			PlayerPrefs.SetString("ACTUAL_CHAR", "rap");
			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rap");
			changeActualChar();

			changeAnimationEquipButton("rap");

			sound_controller.s.change_music(MusicStyle.Rap);
		}

    }


	public void OnCharacterChanged(int type)
	{
		actualCharInScreen = type;
		if (type == 0)
		{
			title.text = "Electro";
			sound_controller.s.change_music(MusicStyle.Eletro);
			if(eletronicAlreadyBuyed == 0)
			{
				equipButton.GetComponent<Animator>().Play("select off");
				equipButton.GetComponent<Button> ().interactable = false;
			}
			else
			{
				equipButton.GetComponent<Button> ().interactable = true;
				changeAnimationEquipButton("eletronic");
			}
		}
		else if (type == 1)
		{
			title.text = "Rock";
			sound_controller.s.change_music(MusicStyle.Rock);

			if (rockAlreadyBuyed == 0)
			{
				equipButton.GetComponent<Animator>().Play("select off");
				equipButton.GetComponent<Button> ().interactable = false;
			}
			else
			{
				equipButton.GetComponent<Button> ().interactable = true;
				changeAnimationEquipButton("rock");
			}
		}
		else if (type == 2)
		{
			title.text = "Classic Pop";
			sound_controller.s.change_music(MusicStyle.Pop);

			if (popAlreadyBuyed == 0)
			{
				equipButton.GetComponent<Animator>().Play("select off");
				equipButton.GetComponent<Button> ().interactable = false;
			}
			else
			{
				equipButton.GetComponent<Button> ().interactable = true;
				changeAnimationEquipButton("pop");
			}
		}
		else if (type == 3)
		{
			title.text = "Modern Pop";
			sound_controller.s.change_music(MusicStyle.PopGaga);

			if (popAlreadyBuyed == 0)
			{
				equipButton.GetComponent<Animator>().Play("select off");
				equipButton.GetComponent<Button> ().interactable = false;
			}
			else
			{
				equipButton.GetComponent<Button> ().interactable = true;
				changeAnimationEquipButton("popGaga");
			}
		}

		else if (type == 4)
		{
			title.text = "Reggae";
			sound_controller.s.change_music(MusicStyle.Reggae);

			if (reggaeAlreadyBuyed == 0)
			{
				equipButton.GetComponent<Animator>().Play("select off");
				equipButton.GetComponent<Button> ().interactable = false;
			}
			else
			{
				equipButton.GetComponent<Button> ().interactable = true;
				changeAnimationEquipButton("reggae");
			}
		}


		else if (type == (int)MusicStyle.Rap)
		{
			title.text = "Rap";
			sound_controller.s.change_music(MusicStyle.Rap);

			if (reggaeAlreadyBuyed == 0)
			{
				equipButton.GetComponent<Animator>().Play("select off");
				equipButton.GetComponent<Button> ().interactable = false;
			}
			else
			{
				equipButton.GetComponent<Button> ().interactable = true;
				changeAnimationEquipButton("rap");
			}
		}
	}
		
    public void changeAnimationEquipButton(string inShopType)
    {

        if (globals.s.ACTUAL_CHAR == inShopType)
        {
			equipButton.GetComponent<Button> ().interactable = false;
            equipButton.GetComponent<Animator>().Play("selected");
        }
        else
        {
            equipButton.GetComponent<Animator>().Play("select");
        }
    }

    void changeActualChar()
    {
        ball_hero[] balls = FindObjectsOfType(typeof(ball_hero)) as ball_hero[];
        for(int i=0;i<balls.Length;i++)
        {
            balls[i].changeSkinChar();
        }
    }
    #endregion

    #region COINS

	public void DisplayNotes(){
		if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {
			jukeboxBt.interactable = true;
			jukeboxBtNotes.text = USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}
		else {
			jukeboxBt.interactable = false;
			if (USER.s.NOTES < 10) {
				jukeboxBtNotes.text = "0" + USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
			}
			else
				jukeboxBtNotes.text = USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}
	}

   
    #endregion

	#region ==== Jukebox Animation ====


	public void OnCharacterChangedOLD(int type)
	{
		actualCharInScreen = type;
		if (type == 0)
		{
			title.text = "Electro";
			if(eletronicAlreadyBuyed == 0)
			{
				buyButton.SetActive(true);
				equipButton.SetActive(false);
				buyPrice.text = eletronicPrice.ToString();
			}
			else
			{
				buyButton.SetActive(false);
				equipButton.SetActive(true);
				changeAnimationEquipButton("eletronic");
			}
		}
		else if (type == 1)
		{
			title.text = "Rock";

			if (rockAlreadyBuyed == 0)
			{
				buyButton.SetActive(true);
				equipButton.SetActive(false);
				buyPrice.text = rockPrice.ToString();
			}
			else
			{
				buyButton.SetActive(false);
				equipButton.SetActive(true);
				changeAnimationEquipButton("rock");
			}
		}
		else if (type == 2)
		{
			title.text = "Classic Pop";

			if (popAlreadyBuyed == 0)
			{
				buyButton.SetActive(true);
				equipButton.SetActive(false);
				buyPrice.text = popPrice.ToString();
			}
			else
			{
				buyButton.SetActive(false);
				equipButton.SetActive(true);
				changeAnimationEquipButton("pop");
			}
		}
		else if (type == 3)
		{
			title.text = "Modern Pop";

			if (popAlreadyBuyed == 0)
			{
				buyButton.SetActive(true);
				equipButton.SetActive(false);
				buyPrice.text = popPrice.ToString();
			}
			else
			{
				buyButton.SetActive(false);
				equipButton.SetActive(true);
				changeAnimationEquipButton("popGaga");
			}
		}

		else if (type == 4)
		{
			title.text = "Reggae";

			if (reggaeAlreadyBuyed == 0)
			{
				buyButton.SetActive(true);
				equipButton.SetActive(false);
				buyPrice.text = popPrice.ToString();
			}
			else
			{
				buyButton.SetActive(false);
				equipButton.SetActive(true);
				changeAnimationEquipButton("reggae");
			}
		}


	}

	public void BuyRandomCharacter(){

		if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {

			USER.s.NOTES -= globals.s.JUKEBOX_CURRENT_PRICE; 
			PlayerPrefs.SetInt("notes", USER.s.NOTES);

			hud_controller.si.GiftButtonClicked ();

			FTUController.s.SetFirstSongPurchased ();

			DisplayNotes ();
		}
	}


	#endregion


	#region == Old ==

	public void watchedVideo()
	{
		USER.s.NOTES += 10;
		PlayerPrefs.SetInt("notes", USER.s.NOTES);
		actualCoins.text = USER.s.NOTES.ToString();
	}

	public void tryBuyCharacter(int musicStyle = -1)
	{

		if (musicStyle == -1)
			musicStyle = actualCharInScreen;

		Debug.Log (" TRYING TO BUY CHAR: " + musicStyle);

		if(musicStyle == 0)
		{
			if (USER.s.NOTES >= eletronicPrice)
			{
				USER.s.NOTES -= eletronicPrice;
				PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
				eletronicAlreadyBuyed = 1;
				buyed();
			}
		}
		else if (musicStyle == 1)
		{
			if (USER.s.NOTES >= rockPrice)
			{
				USER.s.NOTES -= rockPrice;
				PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
				rockAlreadyBuyed = 1;
				buyed();
			}
		}
		else if (musicStyle == 2)
		{
			if (USER.s.NOTES >= popPrice)
			{
				USER.s.NOTES -= popPrice;
				PlayerPrefs.SetInt("popAlreadyBuyed", 1);
				popAlreadyBuyed = 1;
				buyed();
			}
		}
		else if (musicStyle == 3)
		{
			if (USER.s.NOTES >= popGagaPrice)
			{
				USER.s.NOTES -= popGagaPrice;
				PlayerPrefs.SetInt("popGagaAlreadyBuyed", 1);
				popGagaAlreadyBuyed = 1;
				buyed();
			}
		}
		else if (musicStyle == 4)
		{
			if (USER.s.NOTES >= reggaePrice)
			{
				USER.s.NOTES -= reggaePrice;
				PlayerPrefs.SetInt("reggaeAlreadyBuyed", 1);
				reggaeAlreadyBuyed = 1;
				buyed();
			}
		}
		actualCoins.text = USER.s.NOTES.ToString();
		DisplayNotes ();

	}

	#endregion
}
