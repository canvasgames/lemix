using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class hud_controller : MonoBehaviour {
	#region ======== Variables Declaration ========

    public static hud_controller si;

    [HideInInspector]
    public bool HUD_BUTTON_CLICKED = false;

	public GameObject store_label;

    public GameObject game_over_text;
	public GameObject game_over_score;
	public GameObject game_over_best;

	public GameObject gameplah_hud_label;
    public GameObject floor;
    public GameObject best;
    public GameObject notes;

    public GameObject intro_label;

	public GameObject header;
	public GameObject game_title;
	public GameObject pw_info;

    public GameObject activate_pw_bt;

    public GameObject revive;
    public GameObject video;
    public GameObject ready;
    public GameObject v_pw_on;
	public GameObject pw_time_bar;
	public GameObject pw_time_left_title_on;
	public GameObject pw_time_left_title_off;
    public Text PW_time_text;


	public GameObject start_game_bt;

    string PW_date;
    DateTime tempDate;
    DateTime tempcurDate;

    int temp_cur_floor;
    int temp_best_floor;
    // Use this for initialization
    void Awake()
    {
        si = this;
		start_game_bt.SetActive (true);
    }

	#endregion

	#region ======= INIT ==========

    void Start () {
        display_best(USER.s.BEST_SCORE);
        display_notes(USER.s.NOTES);

        //PlayerPrefs.DeleteAll();
        PW_date = PlayerPrefs.GetString("PWDate2ChangeState");
        Debug.Log("pw date: " + PW_date);

        //SETTING  FIRST_GAME GLOBAL
        int tmp_first = PlayerPrefs.GetInt("first_game", 1); ;
        if(tmp_first == 1)
        {
            globals.s.FIRST_GAME = true;
            PlayerPrefs.SetInt("first_game", 0); ;
        }
        else
        {
            globals.s.FIRST_GAME = false;
        }

        //SETTING PW STATE
        int temp_state = PlayerPrefs.GetInt("PWState", 1);
		if(temp_state == 1)
        {
            globals.s.PW_ACTIVE = true;
			//pw_info.SetActive (true);
        }
        else
        {
            globals.s.PW_ACTIVE = false;
			//pw_info.SetActive (false);
			Debug.Log(" SETTING PW ACTIVE TO FALSE");
           // v_pw_on.SetActive(false);
			//pw_time_left_title_on.SetActive (true);
			//pw_time_left_title_off.SetActive (false);
        }


        //Debug.Log(PW_date);
        if (PW_date != "")
        {
           
            tempDate = Convert.ToDateTime(PW_date);
        }
        
        if(globals.s.FIRST_GAME == true)
        {
            activate_pw_bt.SetActive(false);
            pw_info.SetActive(false);
			Debug.Log ("------ FIRST GAME.. " );
        }
        else
        {
            if (globals.s.PW_ACTIVE == false)
            {
                activate_pw_bt.SetActive(true);
				pw_time_left_title_on.SetActive (false);
				pw_time_left_title_off.SetActive (true);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        //GAME OVER GAME CASE
        //if(Input.GetMouseButtonDown(0))
         //   Debug.Log("ueeeeeeeeeeeeeeeeee epaaaaaaaaaaaaaaaaaaa epa, veja la como fala sua " + globals.s.CAN_RESTART);

        if (globals.s.CAN_RESTART && Input.GetMouseButtonDown(0))
        {
            //Application.LoadLevel("Gameplay");
            //Application.LoadLevel()
            
            SceneManager.LoadScene("Gameplay");
        }

        if(globals.s.GAME_STARTED == false && globals.s.MENU_OPEN == false)
        {
            if (globals.s.FIRST_GAME == false)
            {
                show_pw_time();
            }

            if (Input.GetMouseButtonUp(0) && HUD_BUTTON_CLICKED == false)
            {
                //globals.s.GAME_STARTED = true;
               // start_game();
                
            }
            else if(Input.GetMouseButtonDown(0) && HUD_BUTTON_CLICKED == true)
            {
                HUD_BUTTON_CLICKED = false;
            }
        }
    }

	public void start_game_coroutine(){
		StartCoroutine (start_game ());
	}

	public IEnumerator start_game()
    {
        globals.s.FIRST_GAME = false;
        //floor.SetActive(true);
        //best.SetActive(true);
        //notes.SetActive(true);
		if (globals.s.AT_STORE == false) {
//			pw_info.transform.DOLocalMoveY (pw_info.transform.localPosition.y + 700
//				, 0.5f).SetEase (Ease.OutQuart);

			
			//yield return new WaitForSeconds (0.15f);
			pw_info.transform.DOLocalMoveY (-GetComponent <RectTransform>().rect.height/2 - pw_info.GetComponent <RectTransform>().rect.height/2
				, 0.5f).SetEase (Ease.OutQuad).OnComplete (store_entrance);

			game_title.transform.DOLocalMoveY (game_title.transform.localPosition.y + 700
					, 0.5f).SetEase (Ease.OutQuart);
			yield return new WaitForSeconds (0.15f);

			header.transform.DOLocalMoveY (game_title.transform.localPosition.y + 200
				, 0.5f).SetEase (Ease.OutQuart);
			yield return new WaitForSeconds (0.2f);




			hud_entrance ();

		} else {
			
			store_label.transform.DOLocalMoveY(store_label.GetComponent <RectTransform> ().rect.height
				, 0.5f).SetEase (Ease.OutQuart);
			yield return new WaitForSeconds (0.14f);

			header.transform.DOLocalMoveY (game_title.transform.localPosition.y + 500
				, 0.5f).SetEase (Ease.OutQuart);
			yield return new WaitForSeconds (0.2f);
			hud_entrance ();
		}

        //Destroy(intro_label);
		//store_label.SetActive (false);

        game_controller.s.game_running();
		//return true;
    }

	void hud_entrance(){
		intro_label.SetActive (false);
		store_label.SetActive (false);
		gameplah_hud_label.SetActive (true);
		float y_start = gameplah_hud_label.transform.localPosition.y;
		gameplah_hud_label.transform.localPosition = new Vector2 (gameplah_hud_label.transform.localPosition.x, 
			gameplah_hud_label.transform.localPosition.y + 250);
		gameplah_hud_label.transform.DOLocalMoveY (y_start, 0.3f).SetEase (Ease.OutQuad).OnComplete (show_floor_intro);
	}

	void show_floor_intro(){
		stage_intro.s.StartEntering (1);
	}
		
	public void display_best(int value)
	{
		best.GetComponent<Text>().text = "BEST " + value;
	}

	public void display_notes(int n) {
		notes.GetComponent<Text>().text = (n).ToString();
	}

	#endregion
    
	#region ========== Store ==========
	float pw_info_y, game_title_y;
	public void store_bt_act(){
		//pw_info.transform.DOLocalMoveY(pw_info.transform.localPosition.y + pw_info.GetComponent <RectTransform>().rect.height
		if (globals.s.AT_STORE == false) {
			Debug.Log (" MENU HEIGHT: " + game_title.GetComponent <RectTransform>().rect.height + " POS: " + game_title.transform.position.y + " LOCAL Y: " + game_title.transform.localPosition.y);
			globals.s.AT_STORE = true;
			pw_info_y = pw_info.transform.position.y;
			pw_info.transform.DOLocalMoveY (-GetComponent <RectTransform>().rect.height/2 - pw_info.GetComponent <RectTransform>().rect.height/2
				, 0.5f).SetEase (Ease.OutQuad);
			Invoke ("store_entrance", 0.2f);

			game_title_y = game_title.transform.position.y;
			game_title.transform.DOLocalMoveY (GetComponent <RectTransform>().rect.height/2 + game_title.GetComponent <RectTransform>().rect.height/2
			//game_title.transform.DOLocalMoveY (GetComponent <Rect>().height - game_title.transform.localPosition.y + 500
				, 0.5f).SetEase (Ease.OutQuad);
			
		} else { // close store
			globals.s.AT_STORE = false;
			store_label.transform.DOLocalMoveY(store_label.GetComponent <RectTransform> ().rect.height
				, 0.5f).SetEase (Ease.OutQuad);
			Invoke ("store_closing", 0.35f);
		}
	}

	void store_entrance(){
        Debug.Log("aaa");
		store_label.SetActive (true);
		store_label.transform.localPosition = new Vector3 (0, store_label.transform.localPosition.y + store_label.GetComponent <RectTransform> ().rect.height , store_label.transform.localPosition.z);
		store_label.transform.DOLocalMoveY(-70
			, 0.5f).SetEase (Ease.OutQuad);
	
	}

	void store_closing(){
		pw_info.transform.DOMoveY (pw_info_y, 0.5f).SetEase (Ease.OutQuad);
		game_title.transform.DOMoveY (game_title_y, 0.5f).SetEase (Ease.OutQuad);
		store_label.SetActive (false);

	}


	#endregion

	public void update_floor(int n)
    {
        if (QA.s.TRACE_PROFUNDITY >= 3) Debug.Log(" NEW FLOOR!!!!!! ");
        //GetComponentInChildren<TextMesh>().text =  "Floor " + (n+1).ToString();
        floor.GetComponent<Text>().text = "Floor " + (n + 1).ToString();
    }

    

    #region ======================== GAME OVER ==============================

    public void show_game_over(int currentFloor, bool with_high_score)
    {

        int last_score = PlayerPrefs.GetInt("last_score", currentFloor);
        int bestFloor = get_and_set_best_score(currentFloor);
        int dayFloor = get_and_set_day_score(currentFloor);

        PlayerPrefs.SetInt("last_score", currentFloor);

        temp_cur_floor = currentFloor;
        temp_best_floor = bestFloor;

        if(with_high_score == false)
            Invoke("appear_game_over", 0.7f);
        else
            Invoke("appear_game_over", 1.6f);


    }

    void appear_game_over()
    {
        if(globals.s.GAME_OVER == 1)
        {
            game_over_text.SetActive(true);

            //if (game_over_text.GetComponent<Text>().IsActive()) print(" IS GAME OVER ACTIVE ");
			//game_over_text.GetComponent<Text>().text = "GAME OVER\n\nSCORE: " + temp_cur_floor + "\n BEST: " + temp_best_floor;
			game_over_score.GetComponent<Text>().text =  temp_cur_floor.ToString () ;
			game_over_best.GetComponent<Text>().text =  temp_best_floor.ToString ();
        }

    }

    public void hide_game_over()
    {
        globals.s.GAME_OVER = 0;
        game_over_text.SetActive(false);
    }




    #endregion

    #region ================== PLAYER PREFS ============================
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

    int get_and_set_notes(int n) {
        int cur_notes = PlayerPrefs.GetInt("notes", 0);
        return cur_notes;
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

    #endregion

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
        if (difference.Days >= 1)
        {
           // Debug.Log("Day passed");
            PlayerPrefs.SetString("PlayDate", newDate.ToString());
            return true;

        }

        return false;
    }

    #region ========= LIFE SYSTEM ============

    void show_pw_time()
    {
        tempcurDate = System.DateTime.Now;
        
        //NO DATE CASE, TRIGGER 5 MINUTES
        if (PW_date == "")
        {
            PW_time_set_new_date_and_state(true);
        }
        else
        {
            TimeSpan diff = tempDate.Subtract(tempcurDate);
            if(diff.Minutes > 30)
            {
                PW_time_set_new_date_and_state(true);
            }
            else
            {
                if (tempDate < tempcurDate)
                {
                    PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
                }
            }

        }



        TimeSpan difference = tempDate.Subtract(tempcurDate);

        if(globals.s.PW_ACTIVE == true)
        {
			float fill =  ((float) difference.Minutes + (float) difference.Seconds/60) / 5;
			pw_time_bar.GetComponent<Image> ().fillAmount =  fill;
            PW_time_text.text =  difference.Minutes + "m " + difference.Seconds + "s ";
        }
        else
        {
			float fill =  ((float) difference.Minutes + (float) difference.Seconds/60) / 30;
			pw_time_bar.GetComponent<Image> ().fillAmount =  1f - fill;
            PW_time_text.text = difference.Minutes + "m " + difference.Seconds + "s ";
        }  
    }

    public void PW_time_set_new_date_and_state(bool PW_active_state)
    {
        if(PW_active_state == true)
        {
            v_pw_on.SetActive(true);
			pw_time_left_title_on.SetActive (true);
			pw_time_left_title_off.SetActive (false);
			//pw_text.SetActive (true);
            globals.s.PW_ACTIVE = true;
            tempDate = tempcurDate;
            tempDate = tempDate.AddMinutes(GD.s.GD_WITH_PW_TIME);
            //tempDate = tempDate.AddSeconds(6);
            PW_date = tempDate.ToString();
            activate_pw_bt.SetActive(false);

            PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
            PlayerPrefs.SetInt("PWState", 1);
        }
        else
        {
            v_pw_on.SetActive(false);
			pw_time_left_title_on.SetActive (false);
			pw_time_left_title_off.SetActive (true);
            globals.s.PW_ACTIVE = false;
            tempDate = tempcurDate;
            tempDate = tempDate.AddMinutes(GD.s.GD_WITHOUT_PW_TIME);
            //tempDate = tempDate.AddSeconds(6);

            PW_date = tempDate.ToString();

            activate_pw_bt.SetActive(true);
            PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
            PlayerPrefs.SetInt("PWState", 0);
        }
    }
    #endregion

    #region =========== REVIVE ================
    public void show_revive_menu()
    {
        revive.SetActive(true);
    }

    public void close_revive_menu()
    {
        revive.SetActive(false);
        AnalyticController.s.ReportRevive(false);
        game_controller.s.game_over_for_real();
    }

    public void revive_menu_start()
    {
        globals.s.SHOW_VIDEO_AFTER = true;
        globals.s.CAN_RESTART = false;
        globals.s.REVIVING = true;

        hide_game_over();
        revive.SetActive(false);
        ready.SetActive(true);

        game_controller.s.activate_logic();
        game_controller.s.destroy_spikes_2_floors();
        sound_controller.s.play_music();
        AnalyticController.s.ReportRevive(true);

        Invoke("partiu", 1);
    }

    void partiu()
    {
        ready.GetComponent<Text>().text = "GO!";
        Invoke("vaivaivai", 1);
    }

    void vaivaivai()
    {
        game_controller.s.anda_bolinha_fdd();
        globals.s.REVIVING = false;
        ready.SetActive(false);
       
    }

    public void show_video_revive()
    {
        globals.s.MENU_OPEN = true;
        Invoke("appear_video", 1.7f);
    }

    void appear_video()
    {
        video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(true,false);
        video.GetComponent<new_external_link_bt>().set_variables(true, false);
    }

    public void watched_the_video_revive()
    {
        video.SetActive(false);
       
        globals.s.SHOW_VIDEO_AFTER = false;
        Invoke("change_menu_open_state_revive", 1f);
    }

    void change_menu_open_state_revive()
    {
        globals.s.MENU_OPEN = false;
        globals.s.CAN_RESTART = true;
    }

    #endregion

	#region ======== Video =========
    public void show_video_pw()
    {
        globals.s.MENU_OPEN = true;
        video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(false, true);
        video.GetComponent<new_external_link_bt>().set_variables(false, true);
    }

    public void watched_the_video_pw()
    {
        hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
        game_controller.s.activate_logic();
        video.SetActive(false);
        Invoke("change_menu_open_state", 1f);
    }

    public void show_video_coins()
    {
        globals.s.MENU_OPEN = true;
        video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(false, true);
        video.GetComponent<new_external_link_bt>().set_variables(false, true);
    }

    public void watched_the_video_coins()
    {
        hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
        game_controller.s.activate_logic();
        video.SetActive(false);
        Invoke("change_menu_open_state", 1f);
    }

    #endregion

    void change_menu_open_state()
    {
        globals.s.MENU_OPEN = false;
    }

}
