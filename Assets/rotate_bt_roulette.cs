using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class rotate_bt_roulette : MonoBehaviour {
    public GameObject pizza_chart, dialog_no_fire, fire_icon, gem_icon;
    bool running = false;
    bool re_spin = false;
    int re_spin_cost = 1;
    // Use this for initialization
    void Start () {
        gem_icon.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        if(running == false)
        {
            running = true;
            if(re_spin == false)
            {
                if (BE.SceneTown.Gold.Target() >= 200)
                {
                    BE.SceneTown.Gold.ChangeDelta(-200);
                    pizza_chart.GetComponent<pizza_char>().rotate();
                }
                else
                {
                    dialog_no_fire.SetActive(true);
                }
            }
            else
            {
                if (BE.SceneTown.Gem.Target() >= re_spin_cost)
                {
                    BE.SceneTown.Gem.ChangeDelta(-re_spin_cost);
                    pizza_chart.GetComponent<pizza_char>().rotate();
                }
                else
                {
                    dialog_no_fire.SetActive(true);
                }
            }

        }
 
        //
    }

    public void running_state_false()
    {
        running = false;
        re_spin = true;

        re_spin_cost++;
        fire_icon.SetActive(false);
        gem_icon.SetActive(true);
        transform.GetComponentInChildren<Text>().text = "Re-Spin the Wheel \nCost: "+ re_spin_cost+"  ";


    }

    bool day_passed()
    {
        DateTime newDate = System.DateTime.Now;
        string stringDate = PlayerPrefs.GetString("PlayDate");
        DateTime oldDate;

        if (stringDate == "")
        {
            oldDate = newDate;
            PlayerPrefs.SetString("PlayDate", newDate.ToString());
        }
        else
        {
            oldDate = Convert.ToDateTime(stringDate);
        }

        // Debug.Log("LastDay: " + oldDate);
        // Debug.Log("CurrDay: " + newDate);

        TimeSpan difference = newDate.Subtract(oldDate);

        // Debug.Log("Dif Houras: " + difference);
        if (difference.Minutes >= 10)
        {
            // Debug.Log("Day passed");
            PlayerPrefs.SetString("PlayDate", newDate.ToString());
            return true;

        }

        return false;
    }
}
