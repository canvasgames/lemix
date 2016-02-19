using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class hud_controller : MonoBehaviour {

    public static hud_controller si;

    public GameObject game_over_text;
    public GameObject floor;
    public GameObject best;
    public GameObject intro;
    [HideInInspector]
    public int BEST_SCORE, LAST_SCORE, DAY_SCORE;
	// Use this for initialization
    void Awake()
    {
        si = this;
    }

    void Start () {
        display_best(PlayerPrefs.GetInt("best", 0));

        BEST_SCORE = PlayerPrefs.GetInt("best", 0);
        LAST_SCORE = PlayerPrefs.GetInt("last_score", 0);
        DAY_SCORE = PlayerPrefs.GetInt("day_best", 0);
    }
	
	// Update is called once per frame
	void Update () {
	
        if(globals.s.CAN_RESTART && Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel("Gameplay");
        }

        if(!globals.s.GAME_STARTED && Input.GetMouseButtonDown(0)) {
            globals.s.GAME_STARTED = true;
            Destroy(intro);
        }
    }

    public void update_floor(int n)
    {
        Debug.Log(" NEW FLOOR!!!!!! ");
        //GetComponentInChildren<TextMesh>().text =  "Floor " + (n+1).ToString();
        floor.GetComponent<Text>().text = "Floor " + (n + 1).ToString();

    }

    public void show_game_over(int currentFloor)
    {
        game_over_text.SetActive(true);

        if (game_over_text.GetComponent<Text>().IsActive()) print(" IS GAME OVER ACTIVE ");

        int last_score = PlayerPrefs.GetInt("last_score", currentFloor);
        int bestFloor = get_and_set_best_score(currentFloor);
        int dayFloor = get_and_set_day_score(currentFloor);

        game_over_text.GetComponent<Text>().text = "GAME OVER\n\nSCORE: " + currentFloor + "\n BEST: " + bestFloor ;
        
        PlayerPrefs.SetInt("last_score", currentFloor);

    }

    public void display_best(int value)
    {
        best.GetComponent<Text>().text = "BEST " + value;
    }

    int get_and_set_best_score(int cur_floor)
    {
        int cur_best = PlayerPrefs.GetInt("best", 0);
        

        if (cur_floor > cur_best)
        {
            PlayerPrefs.SetInt("best", cur_floor);
            cur_best = cur_floor;
        }

        return cur_best;
    }

    int get_and_set_day_score(int cur_floor)
    {
        int day_best = PlayerPrefs.GetInt("day_best", 0);
        bool day_gone = day_passed();

        if(day_gone == false)
        {
            if (cur_floor > day_best)
            {
                PlayerPrefs.SetInt("day_best", cur_floor);
                day_best = cur_floor;
            }
        }
        else
        {
            day_best = 0;
            PlayerPrefs.SetInt("day_best", 0);
        }

        return day_best;
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

        Debug.Log("LastDay: " + oldDate);
        Debug.Log("CurrDay: " + newDate);

        TimeSpan difference = newDate.Subtract(oldDate);

        Debug.Log("Dif Houras: " + difference);
        if (difference.Days >= 1)
        {
            Debug.Log("Day passed");
            PlayerPrefs.SetString("PlayDate", newDate.ToString());
            return true;

        }

        return false;
    }
}
