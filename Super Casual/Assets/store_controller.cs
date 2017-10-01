﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class store_controller : MonoBehaviour {

	#region === Vars ===
    public static store_controller s;

	public int jukeboxCurrentPrice;


	public GameObject[] lightLeftLines, lighttRightLines;
	public ScrollSnapRect ScrollSnap;

	public Text title;
	public Button jukeboxBt;
	public Text jukeboxBtNotes;

	public GameObject[] chars;

    [HideInInspector]public int popAlreadyBuyed ;
    [HideInInspector]public int eletronicAlreadyBuyed;
	[HideInInspector]public int rockAlreadyBuyed;
	[HideInInspector]public int popGagaAlreadyBuyed;
	[HideInInspector]public int reggaeAlreadyBuyed;
	[HideInInspector]public int rapAlreadyBuyed;

	int[] alreadyBuyed;

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
	MusicStyle actualStyle;

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

		alreadyBuyed = new int[(int)MusicStyle.Lenght];

		alreadyBuyed[(int)MusicStyle.Pop] = PlayerPrefs.GetInt("popAlreadyBuyed", 0);
		alreadyBuyed[(int)MusicStyle.Rock] = PlayerPrefs.GetInt("rockAlreadyBuyed", 0);
		alreadyBuyed[(int)MusicStyle.PopGaga] = PlayerPrefs.GetInt("popGagaAlreadyBuyed", 0);
		alreadyBuyed[(int)MusicStyle.Reggae] = PlayerPrefs.GetInt("reggaeAlreadyBuyed", 0);
		alreadyBuyed[(int)MusicStyle.Eletro] = PlayerPrefs.GetInt("eletronicAlreadyBuyed", 1);
		alreadyBuyed[(int)MusicStyle.Rap] = PlayerPrefs.GetInt("rapAlreadyBuyed", 1);
		alreadyBuyed[(int)MusicStyle.Classic] = PlayerPrefs.GetInt("classicAlreadyBuyed", 0);
		alreadyBuyed[(int)MusicStyle.Latina] = PlayerPrefs.GetInt("latinacAlreadyBuyed", 0);
		alreadyBuyed[(int)MusicStyle.Samba] = PlayerPrefs.GetInt("sambacAlreadyBuyed", 0);
		alreadyBuyed[(int)MusicStyle.DingoBells] = PlayerPrefs.GetInt("dingoBellscAlreadyBuyed", 0);

//		changeAnimationEquipButton("eletronic");
//		changeAnimationEquipButtonNew(MusicStyle.Eletro);

//		DefineActualCharOnTheScreen ();
		DefineActualCharOnTheScreenNew ();
//		OnCharacterChanged(actualCharInScreen);
		OnCharacterChangedNew((MusicStyle)actualCharInScreen);

//        buyPrice.text = eletronicPrice.ToString();

        actualCoins.text = USER.s.NOTES.ToString();

		if (globals.s.FIRST_GAME) {
			actualCharInScreen = 0;
//			equipCharacter ();

			equipCharacterNew ();
		}

		StartCoroutine (LightAnimations ());
    }

	void DefineActualCharOnTheScreenNew(){
		actualCharInScreen = (int)globals.s.ACTUAL_STYLE;
		actualStyle = globals.s.ACTUAL_STYLE;
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

//		else if(globalscu
	
	}

	public void OpenStore(){
		Invoke ("OpenStore2", 0.1f); 
		DisplayNotes ();
//		changeAnimationEquipButtonNew(MusicStyle.Eletro);
//		OnCharacterChangedNew((MusicStyle)actualCharInScreen);
//		OnCharacterChangedNew((MusicStyle)actualCharInScreen);
	}

	void OpenStore2(){
		changeAnimationEquipButtonNew((MusicStyle)actualCharInScreen);
		ScrollSnap.SetCurrentPage((MusicStyle)actualCharInScreen);
	}


	IEnumerator LightAnimations(){
		int curLine = 0;
		int max = 0;
		for (int i = 0; ;i++) {
			if (1==1 || globals.s.curGameScreen == GameScreen.Store) {
				lightLeftLines [i].SetActive (false);
				lighttRightLines [i].SetActive (false);
				yield return new WaitForSeconds (0.15f);

				lightLeftLines [i].SetActive (true);
				lighttRightLines [i].SetActive (true);


				if (i == lightLeftLines.Length-1) {
					i = -1;
				}



			} else
				break;

			max++;
			if (max > 100)
				break;
		}

	}


	#endregion

	#region === Update ===
	public void UpdateUserNotes(){
		actualCoins.text = USER.s.NOTES.ToString();
		DisplayNotes ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	#endregion

	#region === GiveChar===

	public IEnumerator StartRoulleteAnimation(){
		yield return new WaitForSeconds (0.1f);
	}

	#endregion

    #region === CHAR ===
	public bool CheckIfCharacterIsAlreadyPurchasedNew(MusicStyle style){
		if (alreadyBuyed [(int)style] == 1)
			return true;
		else
			return false;
	}

//
//	public bool CheckIfCharacterIsAlreadyPurchased(int musicStyle){
//		if (musicStyle == 0) {
//			if (eletronicAlreadyBuyed == 1)
//				return true;
//			else
//				return false;
//		} else if (musicStyle == 1) {
//			if (rockAlreadyBuyed == 1)
//				return true;
//			else
//				return false;
//		} else if (musicStyle == 2) {
//			if (popAlreadyBuyed == 1)
//				return true;
//			else
//				return false;
//		} else if (musicStyle == 3) {
//			if (popGagaAlreadyBuyed == 1)
//				return true;
//			else
//				return false;
//		} else if (musicStyle == 4) {
//			if (reggaeAlreadyBuyed == 1)
//				return true;
//			else
//				return false;
//
//		} else if (musicStyle == (int)MusicStyle.Rap) {
//			if (rapAlreadyBuyed == 1)
//				return true;
//			else
//				return false;
//		} else
//			return true;
//	}

	public void GiveCharacterForFreeNew(MusicStyle style){
		actualCharInScreen = (int)style;
		actualStyle = style;

//		equipCharacter();
		equipCharacterNew();

		PlayerPrefs.SetInt (style.ToString () + "AlreadyBuyed", 1);
		alreadyBuyed [(int)style] = 1;

	}

//	public void GiveCharacterForFree(int musicStyle = -1)
//	{
//		actualCharInScreen = musicStyle;
//		equipCharacter();
//
//		if(musicStyle == 0)
//		{
//			PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
//			eletronicAlreadyBuyed = 1;
//		}
//		else if (musicStyle == 1)
//		{
//			PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
//			rockAlreadyBuyed = 1;
//		}
//		else if (musicStyle == 2)
//		{
//			PlayerPrefs.SetInt("popAlreadyBuyed", 1);
//			popAlreadyBuyed = 1;
//		}
//
//		else if (musicStyle == 3)
//		{
//			PlayerPrefs.SetInt("popGagaAlreadyBuyed", 1);
//			popGagaAlreadyBuyed = 1;
//		}
//		else if (musicStyle == 4)
//		{
//			PlayerPrefs.SetInt("reggaeAlreadyBuyed", 1);
//			reggaeAlreadyBuyed = 1;
//		}
//
//		else if (musicStyle == 5)
//		{
//			PlayerPrefs.SetInt("rapAlreadyBuyed", 1);
//			reggaeAlreadyBuyed = 1;
//		}
//	}
//

    void buyed()
    {
        buyButton.SetActive(false);
        equipButton.SetActive(true);
//		equipCharacter();
        equipCharacterNew();
    }
    
	public void equipCharacterNew(){
		USER.s.SetCurrentSelectedMusic ((MusicStyle)actualCharInScreen);
		changeActualCharSkin ();

//		changeAnimationEquipButton("eletronic");
		changeAnimationEquipButtonNew((MusicStyle)actualCharInScreen);
		sound_controller.s.change_music((MusicStyle)actualCharInScreen);
	}

//    public void equipCharacter()
//    {
//		Debug.Log ("ACTUAL CHAR IN THE SCREEN: " + actualCharInScreen);
//        if (actualCharInScreen == 0)
//        {
////            Debug.Log(" igona flee gonna be gonna flow");
//            PlayerPrefs.SetString("ACTUAL_CHAR", "eletronic");
//            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "eletronic");
//            changeActualCharSkin();
//
//            changeAnimationEquipButton("eletronic");
//
//            sound_controller.s.change_music(MusicStyle.Eletro);
//
//        }
//        else if (actualCharInScreen == 1)
//        {
//            PlayerPrefs.SetString("ACTUAL_CHAR", "rock");
//            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rock");
//            changeActualCharSkin();
//
//            changeAnimationEquipButton("rock");
//
//            sound_controller.s.change_music(MusicStyle.Rock);
//
//        }
//        else if (actualCharInScreen == 2)
//        {
//            PlayerPrefs.SetString("ACTUAL_CHAR", "pop");
//            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "pop");
//            changeActualCharSkin();
//
//            changeAnimationEquipButton("pop");
//
//            sound_controller.s.change_music(MusicStyle.Pop);
//        }
//		else if (actualCharInScreen == 3)
//		{
//			PlayerPrefs.SetString("ACTUAL_CHAR", "popGaga");
//			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "popGaga");
//			changeActualCharSkin();
//
//			changeAnimationEquipButton("popGaga");
//
//			sound_controller.s.change_music(MusicStyle.PopGaga);
//		}
//		else if (actualCharInScreen == 4)
//		{
//			PlayerPrefs.SetString("ACTUAL_CHAR", "reggae");
//			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "reggae");
//			changeActualCharSkin();
//
//			changeAnimationEquipButton("reggae");
//
//			sound_controller.s.change_music(MusicStyle.Reggae);
//		}
//
//		else if (actualCharInScreen == (int)MusicStyle.Rap)
//		{
//			PlayerPrefs.SetString("ACTUAL_CHAR", "rap");
//			globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rap");
//			changeActualCharSkin();
//
//			changeAnimationEquipButton("rap");
//
//			sound_controller.s.change_music(MusicStyle.Rap);
//		}
//    }
//
//


	public void OnCharacterChangedNew(MusicStyle style) {

		Debug.Log ("Character changed new: " + style.ToString());
		actualCharInScreen = (int)style;
		actualStyle = style;

		title.text = GD.s.GetStyleName (style);

		sound_controller.s.change_music(style);

		if (alreadyBuyed [(int)style] == 0) {
			equipButton.GetComponent<Animator> ().Play ("select off");
			equipButton.GetComponent<Button> ().interactable = false;
		}
		else
		{
			equipButton.GetComponent<Button> ().interactable = true;
			changeAnimationEquipButtonNew(style);
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

	public void changeAnimationEquipButtonNew(MusicStyle style){
		if (globals.s.ACTUAL_STYLE == style) {
			equipButton.GetComponent<Button> ().interactable = false;
			equipButton.GetComponent<Animator>().Play("selected");
		}
		else
			equipButton.GetComponent<Animator>().Play("select");

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

    void changeActualCharSkin()
    {
        ball_hero[] balls = FindObjectsOfType(typeof(ball_hero)) as ball_hero[];
        for(int i=0;i<balls.Length;i++)
        {
//			balls[i].changeSkinChar();
			balls[i].UpdateMySkin();
        }
    }
    #endregion

    #region === COINS ===

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
//				buyPrice.text = eletronicPrice.ToString();
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
//				buyPrice.text = rockPrice.ToString();
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
//				buyPrice.text = popPrice.ToString();
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
//				buyPrice.text = popPrice.ToString();
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
//				buyPrice.text = popPrice.ToString();
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
