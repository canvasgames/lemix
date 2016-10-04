using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class store_controller : MonoBehaviour {
    public static store_controller s;


    [HideInInspector]public int popAlreadyBuyed ;
    [HideInInspector]public int eletronicAlreadyBuyed;
    [HideInInspector]public int rockAlreadyBuyed;

    [HideInInspector]
    public int popPrice = 70;
    [HideInInspector]
    public int rockPrice = 50;
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

        eletronicAlreadyBuyed = PlayerPrefs.GetInt("eletronicAlreadyBuyed", 1);
        popAlreadyBuyed = PlayerPrefs.GetInt("popAlreadyBuyed", 0);
        rockAlreadyBuyed = PlayerPrefs.GetInt("rockAlreadyBuyed", 0);

        changeAnimationEquipButton("eletronic");

        setPrice(0);

        buyPrice.text = eletronicPrice.ToString();

        actualCoins.text = USER.s.NOTES.ToString();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    #region CHAR
    public void tryBuyCharacter()
    {
        
        if(actualCharInScreen == 0)
        {
            if (USER.s.NOTES >= eletronicPrice)
            {
                USER.s.NOTES -= eletronicPrice;
                PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
                eletronicAlreadyBuyed = 1;
                buyed();
            }
        }
        else if (actualCharInScreen == 1)
        {
            if (USER.s.NOTES >= rockPrice)
            {
                USER.s.NOTES -= rockPrice;
                PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
                rockAlreadyBuyed = 1;
                buyed();
            }
        }
        else if (actualCharInScreen == 2)
        {
            if (USER.s.NOTES >= popPrice)
            {
                USER.s.NOTES -= popPrice;
                PlayerPrefs.SetInt("popAlreadyBuyed", 1);
                popAlreadyBuyed = 1;
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
        if (actualCharInScreen == 0)
        {
            PlayerPrefs.SetString("ACTUAL_CHAR", "eletronic");
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "eletronic");
            changeActualChar();

            changeAnimationEquipButton("eletronic");
        }
        else if (actualCharInScreen == 1)
        {
            PlayerPrefs.SetString("ACTUAL_CHAR", "rock");
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rock");
            changeActualChar();

            changeAnimationEquipButton("rock");
        }
        else if (actualCharInScreen == 2)
        {
            PlayerPrefs.SetString("ACTUAL_CHAR", "pop");
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "pop");
            changeActualChar();

            changeAnimationEquipButton("pop");
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
    }

    void changeAnimationEquipButton(string inShopType)
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
