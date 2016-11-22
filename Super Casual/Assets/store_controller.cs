using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class store_controller : MonoBehaviour {
    public static store_controller s;


    [HideInInspector]public int popAlreadyBuyed ;
    [HideInInspector]public int eletronicAlreadyBuyed;
	[HideInInspector]public int rockAlreadyBuyed;
	[HideInInspector]public int popGagaAlreadyBuyed;
    [HideInInspector]public int reggaeAlreadyBuyed;

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
    // Use this for initialization
    void Start () {
        s = this;

        popAlreadyBuyed = PlayerPrefs.GetInt("popAlreadyBuyed", 0);
		rockAlreadyBuyed = PlayerPrefs.GetInt("rockAlreadyBuyed", 0);
		popGagaAlreadyBuyed = PlayerPrefs.GetInt("popGagaAlreadyBuyed", 0);
		reggaeAlreadyBuyed = PlayerPrefs.GetInt("reggaeAlreadyBuyed", 0);
		eletronicAlreadyBuyed = PlayerPrefs.GetInt("eletronicAlreadyBuyed", 1);

        changeAnimationEquipButton("eletronic");

        setPrice(0);

        buyPrice.text = eletronicPrice.ToString();

        actualCoins.text = USER.s.NOTES.ToString();

		if (globals.s.FIRST_GAME) {
			actualCharInScreen = 0;
			equipCharacter ();
		}
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
	}


	public void tryBuyCharacter(int musicStyle = -1)
    {
		if (musicStyle == -1)
			musicStyle = actualCharInScreen;
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
    }

    public void setPrice(int type)
    {
        actualCharInScreen = type;
        if (type == 0)
        {
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
    }

    public void changeAnimationEquipButton(string inShopType)
    {

        if (globals.s.ACTUAL_CHAR == inShopType)
        {

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

    public void watchedVideo()
    {
        USER.s.NOTES += 10;
        PlayerPrefs.SetInt("notes", USER.s.NOTES);
        actualCoins.text = USER.s.NOTES.ToString();
    }
    #endregion
}
