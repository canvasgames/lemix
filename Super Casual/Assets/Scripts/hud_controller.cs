using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Advertisements;

public class hud_controller : MonoBehaviour {
	#region ======== Variables Declaration ========

	public PW_Collect firstPw;
	public GameObject giftBt;
	public GameObject giftAnimation;
	public GameObject giftChar;
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
    //public GameObject v_pw_on;
	public GameObject pw_time_bar;
	public GameObject pw_time_left_title_on, roullete_time_left, gift_time_left;
	public PwWheelMaster roda_a_roda;

	public Text pw_Text_Header;
    public Text PW_time_text;

	public bool giftAnimationEnded = false;


	public GameObject start_game_bt;

    string PW_date, roullete_date, gift_date;
    DateTime tempDate;
    DateTime tempDateRoulette;
    DateTime tempDateGift;
    public bool CAN_ROTATE_ROULETTE = true;
    public bool CAN_GET_GIFT = true;
    DateTime tempcurDate;

    int temp_cur_floor;
    int temp_best_floor;

    public bool runningRoullete;

    bool flagVideoPower = false, flagVideoRevive = false, flagVideoCoins = false;
    // Use this for initialization
    void Awake()
    {
        flagVideoPower = false; flagVideoRevive = false; flagVideoCoins = false;
        si = this;
		start_game_bt.SetActive (true);
    }

	#endregion

	#region ======= INIT ========

	public void ActivateFirstPw(){
		if (USER.s.FIRST_PW_CREATED == 0) {
			firstPw.gameObject.SetActive (true);
			firstPw.pw_type = 2;
			firstPw.init_my_icon ();
			USER.s.FIRST_PW_CREATED = 1;
			PlayerPrefs.SetInt("first_pw_created", 1);
			//PlayerPrefs.SetInt(
		}
			
	}

    void Start () {
		//Invoke ("GiftButtonClicked", 1f);

        display_best(USER.s.BEST_SCORE);
        display_notes(USER.s.NOTES);

        //PlayerPrefs.DeleteAll();
        PW_date = PlayerPrefs.GetString("PWDate2ChangeState");
        roullete_date = PlayerPrefs.GetString("RouletteDate2ChangeState");
        gift_date = PlayerPrefs.GetString("GiftDate2ChangeState");

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

		if (USER.s.NEWBIE_PLAYER == 0 || QA.s.OLD_PLAYER) {
			activate_pw_bt.SetActive (true);
			activate_pw_bt.GetComponent<activate_pw_button> ().HandTutLogic ();
			//SETTING PW STATE
			int temp_state = PlayerPrefs.GetInt ("PWState", 0);
			if (temp_state == 1) {
				globals.s.PW_ACTIVE = true;
				activate_pw_bt.GetComponent<activate_pw_button> ().SetCountownState ();
			} else {
				globals.s.PW_ACTIVE = false;
				activate_pw_bt.GetComponent<activate_pw_button> ().SetSPinNowState ();
			}
		} else {
			activate_pw_bt.SetActive (false);
		}


        //Debug.Log(PW_date);
        if (PW_date != "")
        {
            tempDate = Convert.ToDateTime(PW_date);
        }

        if(roullete_date != "")
        {
            tempDateRoulette = Convert.ToDateTime(roullete_date);
            PlayerPrefs.SetString("RouletteDate2ChangeState", tempDateRoulette.ToString());
            int canRotate = PlayerPrefs.GetInt("CanRotate", 1);
            if (canRotate == 1)
            {
                CAN_ROTATE_ROULETTE = true;

                Debug.Log("can rotate init");
            }
            else
            {

                CAN_ROTATE_ROULETTE = false;

                Debug.Log("sem rotate init");

            }
        }
        else
        {
            CAN_ROTATE_ROULETTE = true;
            Debug.Log("vazio can rotate init");
        }


		if (USER.s.GIFT_INTRODUCED == 1 ||  QA.s.OLD_PLAYER) {
			giftBt.SetActive (true);
			if (gift_date != "") {
				tempDateGift = Convert.ToDateTime (gift_date);
				PlayerPrefs.SetString ("GiftDate2ChangeState", tempDateGift.ToString ());
				int canGet = PlayerPrefs.GetInt ("CanGetGift", 1);
				if (canGet == 1) {
					CAN_GET_GIFT = true;
					giftBt.GetComponent<GiftButton> ().SetGetNowState ();
				} else {
					CAN_GET_GIFT = false;
					giftBt.GetComponent<GiftButton> ().SetCountownState ();
				}
			} else {
				CAN_GET_GIFT = true;
				giftBt.GetComponent<GiftButton> ().SetGetNowState ();
			}
		} else {
			giftBt.SetActive (false);
		}


		// FIRST GAME LOGIC FOR THE POWER UPS BUTTON AND GIFT BUTTON
        if (globals.s.FIRST_GAME == true)
        {
            //activate_pw_bt.SetActive(false);
         //   pw_info.SetActive(false);
			activate_pw_bt.SetActive(false);
			//giftBt.SetActive(false);
			Debug.Log ("------ FIRST GAME.. " );
        }
//        else
//        {
//            if (globals.s.PW_ACTIVE == false)
//            {
//                activate_pw_bt.SetActive(true);
//				//pw_time_left_title_on.SetActive (false);
////                pw_time_bar.GetComponent<Animator>().Play("PowerUpsChargingBarAnimation");
////                pw_Text_Header.text = "Power Up Status";
//            }
//        }
    }
	
	// Update is called once per frame
	void Update () {

        //GAME OVER GAME CASE
        //if(Input.GetMouseButtonDown(0))
        //   Debug.Log("ueeeeeeeeeeeeeeeeee epaaaaaaaaaaaaaaaaaaa epa, veja la como fala sua " + globals.s.CAN_RESTART);
       // Debug.Log(globals.s.PW_ACTIVE);
        if (globals.s.CAN_RESTART && Input.GetMouseButtonDown(0))
        {
            //Application.LoadLevel("Gameplay");
            //Application.LoadLevel()
            
            SceneManager.LoadScene("Gameplay 1");
        }
		if (USER.s.NEWBIE_PLAYER == 0 && globals.s.FIRST_GAME == false && globals.s.GAME_STARTED == false)
        {
			if (USER.s.NEWBIE_PLAYER == 0) {
				show_pw_time ();
				show_roullete_time ();
			}
			if ( USER.s.GIFT_INTRODUCED == 1)
            	show_gift_time();
        }
        if (globals.s.GAME_STARTED == false && globals.s.MENU_OPEN == false)
        {


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
		if (globals.s.GIFT_ANIMATION == true && giftAnimationEnded == true && Input.GetMouseButtonDown (0)) {
			giftChar.SetActive (false);
			giftAnimation.SetActive (false);
			globals.s.GIFT_ANIMATION = false;
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
		if (globals.s.AT_STORE == false && globals.s.MENU_OPEN == false) {
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
			
		} else if(globals.s.AT_STORE == true && globals.s.MENU_OPEN == false) { // close store
			globals.s.AT_STORE = false;
			store_label.transform.DOLocalMoveY(store_label.GetComponent <RectTransform> ().rect.height
				, 0.5f).SetEase (Ease.OutQuad);
			Invoke ("store_closing", 0.35f);
		}
	}

	void store_entrance(){
		store_label.SetActive (true);
		store_label.transform.localPosition = new Vector3 (0, store_label.transform.localPosition.y + store_label.GetComponent <RectTransform> ().rect.height , store_label.transform.localPosition.z);
		store_label.transform.DOLocalMoveY(-70
			, 0.5f).SetEase (Ease.OutQuad);
        store_controller.s.changeAnimationEquipButton("eletronic");

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

    

    #region ============== GAME OVER ================

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

	void show_pw_time() // atualiza o tempo do power ups
    {
        tempcurDate = System.DateTime.Now;
        
        //NO DATE CASE, TRIGGER 5 MINUTES
        if (PW_date == "")
        {
            Debug.Log("no date");
            PW_time_set_new_date_and_state(false);
        }
        else
        {
            TimeSpan diff = tempDate.Subtract(tempcurDate);
            //Debug.Log(diff + "  TimeDif " + globals.s.PW_ACTIVE);
            if (diff.Minutes > GD.s.GD_WITHOUT_PW_TIME && globals.s.PW_ACTIVE == false)
            {
                Debug.Log("new date");

                PW_time_set_new_date_and_state(true);
            }
            else
            {
                if (tempDate < tempcurDate)
                {
                    Debug.Log("new date");

                    PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
                }
            }
        }

        TimeSpan difference = tempDate.Subtract(tempcurDate);
        //Debug.Log(tempDate + " sadsad " + tempcurDate);
        if(globals.s.PW_ACTIVE == true)
        {

			float fill =  ((float) difference.Minutes + (float) difference.Seconds/60) / GD.s.GD_WITH_PW_TIME;
//            pw_Text_Header.text = "Power Ups On";
            //pw_time_left_title_on.SetActive(true);
            //activate_pw_bt.SetActive(false);
//			if(pw_time_bar.GetComponent<Animator>()) pw_time_bar.GetComponent<Animator>().Play("PowerUpsCharginBarGreen");
//            pw_time_bar.GetComponent<Image> ().fillAmount =  fill;
            pw_time_left_title_on.GetComponent<Text>().text =  difference.Minutes + ":" + difference.Seconds + "";

			//activate_pw_bt.GetComponent<activate_pw_button> ().SetCountownState ();
        }
        else
        {
            float fill =  ((float) difference.Minutes + (float) difference.Seconds/60) / GD.s.GD_WITHOUT_PW_TIME;
//            pw_Text_Header.text = "Power Up Status";
//            pw_time_left_title_on.SetActive(false);
           // pw_time_bar.GetComponent<Animator>().Play("PowerUpsChargingBarAnimation");
           // pw_time_bar.GetComponent<Image> ().fillAmount =  1f - fill;

//			activate_pw_bt.SetActive(true);
//			activate_pw_bt.GetComponent<activate_pw_button> ().SetSPinNowState();
			PW_time_text.text = difference.Minutes + ":" + difference.Seconds + "";

        }  
    }

	// mostra o tempo pros power ups
    public void PW_time_set_new_date_and_state(bool PW_active_state)
    {
        if(PW_active_state == true) // 
        {
			Debug.Log ("!!!!!!! PW ACTIVE STATE = TRUE! COUNTOWN");
            //v_pw_on.SetActive(true);
//			pw_time_left_title_on.SetActive (true);
//            pw_time_bar.GetComponent<Animator>().Play("PowerUpsCharginBarGreen");
//
//            pw_Text_Header.text = "Power Ups On";
            //pw_text.SetActive (true);
            globals.s.PW_ACTIVE = true;
            tempDate = tempcurDate;
            tempDate = tempDate.AddMinutes(GD.s.GD_WITH_PW_TIME);
            //tempDate = tempDate.AddSeconds(6);
            PW_date = tempDate.ToString();
            //activate_pw_bt.SetActive(false);

            PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
            PlayerPrefs.SetInt("PWState", 1);

			activate_pw_bt.GetComponent<activate_pw_button> ().SetCountownState ();
        }
        else
        {
			Debug.Log ("!!!!!!! PW ACTIVE STATE = FALSE! SPIN NOW!");

            //v_pw_on.SetActive(false);
//			pw_time_left_title_on.SetActive (false);

//            pw_Text_Header.text = "Power Up Status";
//            pw_time_bar.GetComponent<Animator>().Play("PowerUpsChargingBarAnimation");
            globals.s.PW_ACTIVE = false;
            tempDate = tempcurDate;
            tempDate = tempDate.AddMinutes(GD.s.GD_WITHOUT_PW_TIME);
            //tempDate = tempDate.AddSeconds(6);

            PW_date = tempDate.ToString();

            PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
            PlayerPrefs.SetInt("PWState", 0);

			activate_pw_bt.SetActive(true);
			activate_pw_bt.GetComponent<activate_pw_button> ().SetSPinNowState ();

        }
    }

	//called by the Roullete
    public void add_pw_time(float time)
    {
        globals.s.PW_ACTIVE = true;
        tempDate = tempcurDate;
        tempDate = tempDate.AddMinutes(time);

        //tempDate = tempDate.AddSeconds(6);
        PW_date = tempDate.ToString();
        //activate_pw_bt.SetActive(false);

        PlayerPrefs.SetString("PWDate2ChangeState", PW_date);
        PlayerPrefs.SetInt("PWState", 1);

    }

    void show_roullete_time()
    {
        tempcurDate = System.DateTime.Now;

        //NO DATE CASE=
        if (roullete_date == "")
        {
            CAN_ROTATE_ROULETTE = true;
            PlayerPrefs.SetInt("CanRotate", 1);
        }
        else
        {


            TimeSpan diff = tempDateRoulette.Subtract(tempcurDate);
            if (CAN_ROTATE_ROULETTE == false)
            {
                
                if (diff.TotalSeconds <= 0)
                {
                    Debug.Log("o tempo passou e eu sofri calado");
                    CAN_ROTATE_ROULETTE = true;
                    PlayerPrefs.SetInt("CanRotate", 1);
                }
            }
            if(diff.TotalSeconds > 0 && CAN_ROTATE_ROULETTE == false)
            {
                roullete_time_left.GetComponent<Text>().text = diff.Minutes + "m " + diff.Seconds + "s ";
            }

        }
        if(CAN_ROTATE_ROULETTE == true)
        {
            roullete_time_left.GetComponent<Text>().text = " ROTATE NOW ";

        }
        //TimeSpan difference = tempDate.Subtract(tempcurDate);
        //Debug.Log(tempDate + " sadsad " + tempcurDate);

    }
    
	public void addRoulleteTime()
    {
        PlayerPrefs.SetInt("CanRotate", 0);
        //Debug.Log("rodou efalsificou");
        CAN_ROTATE_ROULETTE = false;
        tempDateRoulette = System.DateTime.Now;
        tempDateRoulette = tempDateRoulette.AddMinutes(GD.s.GD_ROULLETE_WAIT_MINUTES);
        roullete_date = tempDateRoulette.ToString();

        PlayerPrefs.SetString("RouletteDate2ChangeState", roullete_date);

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
        //video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(true,false);
        //video.GetComponent<new_external_link_bt>().set_variables(true, false);
        flagVideoPower = false;
        flagVideoCoins = false;
        flagVideoRevive = true;
        ShowAd();
    }

    public void watched_the_video_revive()
    {
        //video.SetActive(false);
       
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
        //globals.s.MENU_OPEN = true;
        //video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(false, true);
        //video.GetComponent<new_external_link_bt>().set_variables(false, true);
        if(CAN_ROTATE_ROULETTE == false)
        {
            flagVideoRevive = false;
            flagVideoCoins = false;
            flagVideoPower = true;

            ShowAd();
        }
    }

    public void watched_the_video_pw()
    {
        hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
                Debug.Log("new date");
        game_controller.s.activate_logic();
        video.SetActive(false);
        Invoke("change_menu_open_state", 1f);
    }

    public void show_video_coins()
    {
        globals.s.MENU_OPEN = true;
        //video.SetActive(true);
        //video.GetComponentInChildren<Play_Video>().solta_a_vinheta_sombra(false, true);
        //video.GetComponent<new_external_link_bt>().set_variables(false, true);
        flagVideoRevive = false;
        flagVideoCoins = true;
        flagVideoPower = false;
        ShowAd();

    }

    public void watched_the_video_coins()
    {
        //hud_controller.si.PW_time_set_new_date_and_state(!globals.s.PW_ACTIVE);
        game_controller.s.activate_logic();
        video.SetActive(false);
        Debug.Log("new date");

        Invoke("change_menu_open_state", 1f);
    }

    #endregion

    void change_menu_open_state()
    {
        globals.s.MENU_OPEN = false;
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions {resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

	#if UNITY_ANDROID || UNITY_EDITOR
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                if(flagVideoPower == true)
                {
                    CAN_ROTATE_ROULETTE = true;
                    PlayerPrefs.SetInt("CanRotate", 1);
                    StartCoroutine (openTampa());
					//Invoke("activeRodaaRoda", 1);
                    flagVideoPower = false;
                }
                else if(flagVideoRevive == true)
                {
                    watched_the_video_revive();
                }
                else if(flagVideoCoins == true)
                {
                    store_controller.s.watchedVideo();
                }
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
	#endif

	#region ======== Menu Power Ups ============
	public void RodaMenu(){
		//StartCoroutine(activeRodaaRoda());
		roda_a_roda.gameObject.SetActive(true);

		roda_a_roda.Entrance ();
	}

	public void PowerUpsMenuClose(){
		roda_a_roda.gameObject.SetActive (false);
	}

	IEnumerator openTampa()
    {
        yield return new WaitForSeconds(0.15f);
        //roda_a_roda.gameObject.SetActive(true);
        //roda_a_roda.Entrance ();
        roda_a_roda.openTampa();
    }

    IEnumerator closeTampa()
    {
        yield return new WaitForSeconds(0.15f);
        //roda_a_roda.gameObject.SetActive(true);
        //roda_a_roda.Entrance ();
        roda_a_roda.openTampa();
    }
    #endregion

    #region ======== Gift ============
    void show_gift_time()
    {
        tempcurDate = System.DateTime.Now;

        //NO DATE CASE
        if (gift_date == "")
        {
            CAN_GET_GIFT = true;
			//Debug.Log("asd");
            PlayerPrefs.SetInt("CanGetGift", 1);
			//giftBt.GetComponent<GiftButton> ().SetGetNowState ();
        }

        else
        {
            TimeSpan diff = tempDateGift.Subtract(tempcurDate);
            if (CAN_GET_GIFT == false)
            {
                if (diff.TotalSeconds <= 0)
                {
                    Debug.Log("o tempo passou e eu sofri calado");
                    CAN_GET_GIFT = true;
                    PlayerPrefs.SetInt("CanGetGift", 1);
					giftBt.GetComponent<GiftButton> ().SetGetNowState ();

                }
            }

            if (diff.TotalSeconds > 0 && CAN_GET_GIFT == false)
            {
				//gift_time_left.GetComponent<Text>().text = diff.Days + "d " + diff.Hours + "h " + diff.Minutes + "m " + diff.Seconds + "s ";
				if( diff.Hours >= 1)
					gift_time_left.GetComponent<Text>().text = diff.Hours + "h\n" + diff.Minutes + "m";
				else
					gift_time_left.GetComponent<Text>().text = diff.Minutes + "m\n" + diff.Seconds + "s";

            }

        }

//        if(CAN_GET_GIFT == true)
//        {
//            gift_time_left.GetComponent<Text>().text = " GET NOW";
//			giftBt.GetComponent<GiftButton> ().SetGetNowState ();
//
//        }

        //TimeSpan difference = tempDate.Subtract(tempcurDate);
        //Debug.Log(tempDate + " sadsad " + tempcurDate);


    }
    public void getGift()
    {
        PlayerPrefs.SetInt("CanGetGift", 0);
        CAN_GET_GIFT = false;
        tempDateGift = System.DateTime.Now;
        tempDateGift = tempDateGift.AddMinutes(GD.s.GD_GIFT_WAIT_MINUTES);
        gift_date = tempDateGift.ToString();

        PlayerPrefs.SetString("GiftDate2ChangeState", gift_date);
    }
  
	public void GiftButtonClicked(){
		if (CAN_GET_GIFT || 1==1) { //codigo que testa se pode dar o presente,
			


			bool rand_found;
			int rand, count = 0;

			do {
				rand = UnityEngine.Random.Range (1, 5);
				rand_found = store_controller.s.CheckIfCharacterIsAlreadyPurchased (rand);
				count++;
			} while (rand_found == true && count < 50);
				
			if (count < 50) {
				giftAnimationEnded = false;
				globals.s.GIFT_ANIMATION = true;
				giftAnimation.SetActive (true);
				
				StartCoroutine (GiveChar (rand));

				giftAnimation.GetComponent<GiftAnimationLogic> ().Init ();
			}
			else {
				Debug.Log ("ERROR!!");
			}
			
		} else {//fazer alguma coisa ?

		}
	}	

	IEnumerator GiveChar(int musicType){
		yield return new WaitForSeconds (1f);

		giftChar.SetActive (true);
		giftChar.GetComponent<CharGift> ().InitAnimation ((MusicStyle)musicType);
		store_controller.s.GiveCharacterForFree (musicType);

		giftBt.GetComponent<GiftButton> ().SetCountownState ();
		getGift ();

		giftAnimation.GetComponent<GiftAnimationLogic> ().EnterTitle ((MusicStyle)musicType);
	}

	#endregion
}
