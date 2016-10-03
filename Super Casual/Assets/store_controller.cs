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

        setPrice(0);

        buyPrice.text = eletronicPrice.ToString();

        actualCoins.text = USER.s.NOTES.ToString();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void tryBuyCharacter()
    {
        if(actualCharInScreen == 0)
        {
            USER.s.NOTES -= eletronicPrice;
            PlayerPrefs.SetInt("eletronicAlreadyBuyed", 1);
            
        }
        else if (actualCharInScreen == 1)
        {
            USER.s.NOTES -= rockPrice;
            PlayerPrefs.SetInt("rockAlreadyBuyed", 1);
        }
        else if (actualCharInScreen == 2)
        {
            USER.s.NOTES -= popPrice;
            PlayerPrefs.SetInt("popAlreadyBuyed", 1);
        }

        buyButton.SetActive(false);
        equipButton.SetActive(true);
        equipCharacter();

        actualCoins.text = USER.s.NOTES.ToString();

    }

    public void equipCharacter()
    {
        if (actualCharInScreen == 0)
        {
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "eletronic"); 
            PlayerPrefs.SetString("ACTUAL_CHAR", "eletronic");
        }
        else if (actualCharInScreen == 0)
        {
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "rock");
            PlayerPrefs.SetString("ACTUAL_CHAR", "rock");
        }
        else if (actualCharInScreen == 0)
        {
            globals.s.ACTUAL_CHAR = PlayerPrefs.GetString("ACTUAL_CHAR", "pop");
            PlayerPrefs.SetString("ACTUAL_CHAR", "pop");
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
            }
        }
    }
}
