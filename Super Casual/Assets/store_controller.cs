using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class store_controller : MonoBehaviour {

	#region === Vars ===
    public static store_controller s;

	[SerializeField]Button myBackBt;

	[SerializeField]GameObject myU, mYTitle, myBgLights;
	[Space (10)] 

	public RewardScreen myRewardScreen;

	public int jukeboxCurrentPrice;

	[SerializeField] GameObject[] myChars;

	public GameObject[] lightLeftLines, lighttRightLines;
	public ScrollSnapRect ScrollSnap;

	public Text title;
	public Button jukeboxBt;
	public Text jukeboxBtNotesLow, jukeboxBtNotesHigh;

	public GameObject[] chars;

    [HideInInspector]public int popAlreadyBuyed ;
    [HideInInspector]public int eletronicAlreadyBuyed;
	[HideInInspector]public int rockAlreadyBuyed;
	[HideInInspector]public int popGagaAlreadyBuyed;
	[HideInInspector]public int reggaeAlreadyBuyed;
	[HideInInspector]public int rapAlreadyBuyed;

	int[] alreadyBuyed;
	int nCharsBuyed = 0;

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
	MusicStyle actualStyle, lastSortedStyle = MusicStyle.Eletro;

	#endregion
    // Use this for initialization

	#region === Init ===
    void Start () {
        s = this;
//				USER.s.AddNotes (150);

		PlayerPrefs.SetInt(MusicStyle.Eletro.ToString()+"AlreadyBuyed", 1);
//		PlayerPrefs.SetInt(MusicStyle.Rap.ToString()+"AlreadyBuyed", 1);

//        popAlreadyBuyed = PlayerPrefs.GetInt("popAlreadyBuyed", 0);
//		rockAlreadyBuyed = PlayerPrefs.GetInt("rockAlreadyBuyed", 0);
//		popGagaAlreadyBuyed = PlayerPrefs.GetInt("popGagaAlreadyBuyed", 0);
//		reggaeAlreadyBuyed = PlayerPrefs.GetInt("reggaeAlreadyBuyed", 0);
//		eletronicAlreadyBuyed = PlayerPrefs.GetInt("eletronicAlreadyBuyed", 1);
//		rapAlreadyBuyed = PlayerPrefs.GetInt("rapAlreadyBuyed", 1);

		alreadyBuyed = new int[(int)MusicStyle.Lenght];

//		alreadyBuyed[(int)MusicStyle.Pop] = PlayerPrefs.GetInt("PopAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Rock] = PlayerPrefs.GetInt("RockAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.PopGaga] = PlayerPrefs.GetInt("PopGagaAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Reggae] = PlayerPrefs.GetInt("ReggaeAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Eletro] = PlayerPrefs.GetInt("EletronicAlreadyBuyed", 1);
//		alreadyBuyed[(int)MusicStyle.Rap] = PlayerPrefs.GetInt("rapAlreadyBuyed", 1);
//		alreadyBuyed[(int)MusicStyle.Classic] = PlayerPrefs.GetInt("classicAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Latina] = PlayerPrefs.GetInt("latinacAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.Samba] = PlayerPrefs.GetInt("sambaAlreadyBuyed", 0);
//		alreadyBuyed[(int)MusicStyle.DingoBells] = PlayerPrefs.GetInt("dingoBellscAlreadyBuyed", 0);

		for (int i = 0; i < alreadyBuyed.Length; i++) {
//			PlayerPrefs.SetInt(((MusicStyle)i).ToString()+"AlreadyBuyed", 0);
			int unlocked = 0;
			if (QA.s.ALL_UNLOCKED)
				unlocked = 1;
			alreadyBuyed[i] = PlayerPrefs.GetInt(((MusicStyle)i).ToString()+"AlreadyBuyed", unlocked);
//			Debug.Log("ALREADY BUYED I: "+ i + " TRUE: " + alreadyBuyed[i]);
			if (alreadyBuyed [i] == 1)
				nCharsBuyed++;
		}

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
		actualCoins.text = USER.s.NOTES.ToString("00");
		DisplayNotes ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
    
	public void equipCharacterNew(bool dontChangeMusic = false){
		USER.s.SetCurrentSelectedMusic ((MusicStyle)actualCharInScreen);
		changeActualCharSkin ();

//		changeAnimationEquipButton("eletronic");
		changeAnimationEquipButtonNew((MusicStyle)actualCharInScreen);
		if(dontChangeMusic == false) sound_controller.s.change_music((MusicStyle)actualCharInScreen);
	}


	public void OnCharacterChangedNew(MusicStyle style, bool dontPlayMusic = false) {

		Debug.Log ("Character changed new: " + style.ToString());
		actualCharInScreen = (int)style;
		actualStyle = style;

		title.text = GD.s.GetStyleName (style);

		if(globals.s.JUKEBOX_SORT_ANIMATION == false && dontPlayMusic == false) 
//			sound_controller.s.change_music(style);
			sound_controller.s.ChangeMusicForStore(style);

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

//	public void OnCharacterChanged(int type)
//	{
//		actualCharInScreen = type;
//		if (type == 0)
//		{
//			title.text = "Electro";
//			sound_controller.s.change_music(MusicStyle.Eletro);
//			if(eletronicAlreadyBuyed == 0)
//			{
//				equipButton.GetComponent<Animator>().Play("select off");
//				equipButton.GetComponent<Button> ().interactable = false;
//			}
//			else
//			{
//				equipButton.GetComponent<Button> ().interactable = true;
//				changeAnimationEquipButton("eletronic");
//			}
//		}
//		else if (type == 1)
//		{
//			title.text = "Rock";
//			sound_controller.s.change_music(MusicStyle.Rock);
//
//			if (rockAlreadyBuyed == 0)
//			{
//				equipButton.GetComponent<Animator>().Play("select off");
//				equipButton.GetComponent<Button> ().interactable = false;
//			}
//			else
//			{
//				equipButton.GetComponent<Button> ().interactable = true;
//				changeAnimationEquipButton("rock");
//			}
//		}
//		else if (type == 2)
//		{
//			title.text = "Classic Pop";
//			sound_controller.s.change_music(MusicStyle.Pop);
//
//			if (popAlreadyBuyed == 0)
//			{
//				equipButton.GetComponent<Animator>().Play("select off");
//				equipButton.GetComponent<Button> ().interactable = false;
//			}
//			else
//			{
//				equipButton.GetComponent<Button> ().interactable = true;
//				changeAnimationEquipButton("pop");
//			}
//		}
//		else if (type == 3)
//		{
//			title.text = "Modern Pop";
//			sound_controller.s.change_music(MusicStyle.PopGaga);
//
//			if (popAlreadyBuyed == 0)
//			{
//				equipButton.GetComponent<Animator>().Play("select off");
//				equipButton.GetComponent<Button> ().interactable = false;
//			}
//			else
//			{
//				equipButton.GetComponent<Button> ().interactable = true;
//				changeAnimationEquipButton("popGaga");
//			}
//		}
//
//		else if (type == 4)
//		{
//			title.text = "Reggae";
//			sound_controller.s.change_music(MusicStyle.Reggae);
//
//			if (reggaeAlreadyBuyed == 0)
//			{
//				equipButton.GetComponent<Animator>().Play("select off");
//				equipButton.GetComponent<Button> ().interactable = false;
//			}
//			else
//			{
//				equipButton.GetComponent<Button> ().interactable = true;
//				changeAnimationEquipButton("reggae");
//			}
//		}
//
//
//		else if (type == (int)MusicStyle.Rap)
//		{
//			title.text = "Rap";
//			sound_controller.s.change_music(MusicStyle.Rap);
//
//			if (reggaeAlreadyBuyed == 0)
//			{
//				equipButton.GetComponent<Animator>().Play("select off");
//				equipButton.GetComponent<Button> ().interactable = false;
//			}
//			else
//			{
//				equipButton.GetComponent<Button> ().interactable = true;
//				changeAnimationEquipButton("rap");
//			}
//		}
//	}

	public void changeAnimationEquipButtonNew(MusicStyle style){
		if (globals.s.ACTUAL_STYLE == style) {
			equipButton.GetComponent<Button> ().interactable = false;
			equipButton.GetComponent<Animator>().Play("selected");
		}
		else
			equipButton.GetComponent<Animator>().Play("select");
	}

//
//    public void changeAnimationEquipButton(string inShopType)
//    {
//
//        if (globals.s.ACTUAL_CHAR == inShopType)
//        {
//			equipButton.GetComponent<Button> ().interactable = false;
//            equipButton.GetComponent<Animator>().Play("selected");
//        }
//        else
//        {
//            equipButton.GetComponent<Animator>().Play("select");
//        }
//    }

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
		
		if (globals.s.JUKEBOX_CURRENT_PRICE == GD.s.JUKEBOX_FTU_PRICE) {
			jukeboxBtNotesLow.gameObject.SetActive (true);
			jukeboxBtNotesHigh.gameObject.SetActive (false);
			jukeboxBtNotesLow.text = globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		} else {
			jukeboxBtNotesLow.gameObject.SetActive (false);
			jukeboxBtNotesHigh.gameObject.SetActive (true);
			jukeboxBtNotesHigh.text = globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}


		// CHECK IF IS FULL
		if (nCharsBuyed >= GD.s.N_MUSIC) {
			jukeboxBtNotesLow.gameObject.SetActive (false);
			jukeboxBtNotesHigh.gameObject.SetActive (true);
			jukeboxBt.interactable = false;
			jukeboxBtNotesHigh.text = "full";
		}
		else if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {
			jukeboxBt.interactable = true;
//			jukeboxBtNotes.text = USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}
		else {
			jukeboxBt.interactable = false;
			if (USER.s.NOTES < 10) {
//				jukeboxBtNotes.text = "0" + USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
			}
			else{
//				jukeboxBtNotes.text = USER.s.NOTES.ToString () + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
			}
		}
	}

   
    #endregion

	#region ==== Buying and Animation ====

	public void SetBuyButtonState(){
		if (USER.s.NOTES >= globals.s.JUKEBOX_CURRENT_PRICE) {
			jukeboxBt.interactable = true;
		}
		else
			jukeboxBt.interactable = false;
	}


	public void BuyRandomCharacter(){
		if (nCharsBuyed < GD.s.N_MUSIC) {
			StartCoroutine (StartRoulleteAnimation ());

			USER.s.NOTES -= globals.s.JUKEBOX_CURRENT_PRICE; 
			PlayerPrefs.SetInt("notes", USER.s.NOTES);

			FTUController.s.SetFirstSongPurchased ();

//			DisplayNotes ();
			UpdateUserNotes();
//			}
		} else {
			Debug.Log ("[JK] WOOOOOOOOOOOW! all chars purchaseD!!");
		}
	}

	/// <summary>
	/// Logic Of the sort animation
	/// </summary>
	/// <returns>The roullete animation.</returns>
	public IEnumerator StartRoulleteAnimation(){
		globals.s.JUKEBOX_SORT_ANIMATION = true;
		int rand = Random.Range (10, 20);

		myBackBt.interactable = false;
		jukeboxBt.interactable = false;

		//change animation speed
		myU.GetComponent<Animator>().speed = 3f;
		mYTitle.GetComponent<Animator>().speed = 3f;
		myBgLights.GetComponent<Animator>().speed = 3f;

		do {
			for (int i = 0; i < rand; i++) {
				//logica de não repetir
				ScrollSnap.NextScreen ();
				yield return new WaitForSeconds (0.1f);
				//		scrol
			}
			rand = Random.Range (1, 3);
			Debug.Log("STYLE FOUND: "+ (MusicStyle)actualStyle + " have " +CheckIfCharacterIsAlreadyPurchasedNew(actualStyle) );
//		} while (alreadyBuyed [(int)actualStyle] == 0);
		} while (CheckIfCharacterIsAlreadyPurchasedNew(actualStyle) == true || actualStyle == lastSortedStyle);

		globals.s.JUKEBOX_SORT_ANIMATION = false;
//		OnCharacterChangedNew (actualStyle);

		yield return new WaitForSeconds(0.1f);

		Debug.Log("STYLE FOUND 2222: "+ (MusicStyle)actualStyle + " have " +CheckIfCharacterIsAlreadyPurchasedNew(actualStyle));


		lastSortedStyle = actualStyle;

		StartCoroutine (GiveReward ());

//		hud_controller.si.GiftButtonClicked (actualStyle);

	}

	IEnumerator GiveReward(){

		OnCharacterChangedNew (actualStyle);
		globals.s.curGameScreen = GameScreen.RewardCharacter;

		GameObject curChar = null;
		foreach (GameObject character in myChars) {
			if (character.name == actualStyle.ToString ()) {
				curChar = character;
				break;
			}
		}

		myRewardScreen.SetMyRewardChar (curChar.GetComponent<Animator> ().runtimeAnimatorController, GD.s.GetStyleName(actualStyle));
//		myRewardScreen.myReward = curChar;
		yield return new WaitForSeconds(0.5f);

		myRewardScreen.gameObject.SetActive (true);

		//change animation speed back to normal
		myU.GetComponent<Animator>().speed = 1f;
		mYTitle.GetComponent<Animator>().speed = 1f;
		myBgLights.GetComponent<Animator>().speed = 1f;
	}


	public void WatchedVideoForResort(){
		globals.s.curGameScreen = GameScreen.Store;
		myRewardScreen.gameObject.SetActive (false);

		StartCoroutine (StartRoulleteAnimation ());
	}

	// Nice button pressed
	public void OnButtonRewardPressed(){
//		jukeboxBt.interactable = true; //TBD: FAZER LOGICA QUE TESTA SE TODOS FORAM COMPRADOS E POR UM IF AQUI
		UpdateUserNotes(); //TBD: FAZER LOGICA QUE TESTA SE TODOS FORAM COMPRADOS E POR UM IF AQUI
		globals.s.MENU_OPEN = false;

		globals.s.curGameScreen = GameScreen.Store;
		myRewardScreen.gameObject.SetActive (false);

		//give the reward for real
		equipCharacterNew(true);

		PlayerPrefs.SetInt (actualStyle.ToString () + "AlreadyBuyed", 1);
		alreadyBuyed [(int)actualStyle] = 1;
		nCharsBuyed++;

		myBackBt.interactable = true;

	}

	#endregion

	#region == Old ==

	public void watchedVideo()
	{
		USER.s.NOTES += 10;
		PlayerPrefs.SetInt("notes", USER.s.NOTES);
		actualCoins.text = USER.s.NOTES.ToString();
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


	//	public void OnCharacterChangedOLD(int type)
	//	{
	//		actualCharInScreen = type;
	//		if (type == 0)
	//		{
	//			title.text = "Electro";
	//			if(eletronicAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = eletronicPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("eletronic");
	//			}
	//		}
	//		else if (type == 1)
	//		{
	//			title.text = "Rock";
	//
	//			if (rockAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = rockPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("rock");
	//			}
	//		}
	//		else if (type == 2)
	//		{
	//			title.text = "Classic Pop";
	//
	//			if (popAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = popPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("pop");
	//			}
	//		}
	//		else if (type == 3)
	//		{
	//			title.text = "Modern Pop";
	//
	//			if (popAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = popPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("popGaga");
	//			}
	//		}
	//
	//		else if (type == 4)
	//		{
	//			title.text = "Reggae";
	//
	//			if (reggaeAlreadyBuyed == 0)
	//			{
	//				buyButton.SetActive(true);
	//				equipButton.SetActive(false);
	////				buyPrice.text = popPrice.ToString();
	//			}
	//			else
	//			{
	//				buyButton.SetActive(false);
	//				equipButton.SetActive(true);
	//				changeAnimationEquipButton("reggae");
	//			}
	//		}
	//	}

//
//	public void tryBuyCharacter(int musicStyle = -1)
//	{
//
//		if (musicStyle == -1)
//			musicStyle = actualCharInScreen;
//
//		Debug.Log (" TRYING TO BUY CHAR: " + musicStyle);
//
//		if(musicStyle == 0)
//		{
//			if (USER.s.NOTES >= eletronicPrice)
//			{
//				USER.s.NOTES -= eletronicPrice;
//				PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
//				eletronicAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 1)
//		{
//			if (USER.s.NOTES >= rockPrice)
//			{
//				USER.s.NOTES -= rockPrice;
//				PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
//				rockAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 2)
//		{
//			if (USER.s.NOTES >= popPrice)
//			{
//				USER.s.NOTES -= popPrice;
//				PlayerPrefs.SetInt("popAlreadyBuyed", 1);
//				popAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 3)
//		{
//			if (USER.s.NOTES >= popGagaPrice)
//			{
//				USER.s.NOTES -= popGagaPrice;
//				PlayerPrefs.SetInt("popGagaAlreadyBuyed", 1);
//				popGagaAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		else if (musicStyle == 4)
//		{
//			if (USER.s.NOTES >= reggaePrice)
//			{
//				USER.s.NOTES -= reggaePrice;
//				PlayerPrefs.SetInt("reggaeAlreadyBuyed", 1);
//				reggaeAlreadyBuyed = 1;
//				buyed();
//			}
//		}
//		actualCoins.text = USER.s.NOTES.ToString();
//		DisplayNotes ();
//
//	}

	#endregion
}
