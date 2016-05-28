﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_controller : MonoBehaviour {

    #region ============= Variables Declaration ================
    public static game_controller s;
    
    float starting_time, match_time;

    // TYPES
    [HideInInspector] public GameObject floor;
    public GameObject floor_type;
    public GameObject spike_type;
    public GameObject hole_type;
    public GameObject triple_spike_type;
    public GameObject wall_type;
    public GameObject pw_icon;
    public GameObject QA_wave_name;

    ball_hero[] temp_ball;

    private int n_floor = 5;
    private int cur_floor = -1;

    public int wave_id;
    public string wave_name = "";
    public ball_hero[] ball;

	public GameObject single;

    public Camera camerda;

    int last_bg = 0;


    // WAVE CONTROLLER VARIABLES
    bool wave_found;

  
    float hole_size;
    float hole_dist = 1.35f;
    float screen_w = 9.4f;
    float corner_left = -4.35f;
    float corner_right = 4.35f;
    float mid_area = 2.1f;
    float center_mid_area = 1f;
    float min_spk_dist = 2.5f;

    float corner_limit_right = 2.7f;
    float corner_limit_left = -2.7f;

    bool last_spike_left;
    bool last_spike_right;
    bool last_hole;
    bool last_wall;
    int hole_creation_failed = 0;

    float last_hole_x;

    public wave_controller[] wave_ctrl;

    public int there_was_revive = 0;
    public int n_games_without_revive = 0;

    //PW VARIABLES
    int pw_dont_create_for_n_floors = 5;
    int pw_floors_not_created = 6;
    bool first_pw_created = false;

    string killer_wave_to_report = "";
    int time_to_report = 0;

    bool temp_flag_high_score_game_over;

    bool first_hole_already_created = false;
    bool first_wall_already_created = false;


    void Awake (){
        
        s = this;
        hole_size = hole_type.transform.GetComponent<SpriteRenderer>().bounds.size.x;

        // Initialize Globals singleton
        globals[] single2 = FindObjectsOfType(typeof(globals)) as globals[];
        if (single2.Length == 0)
        {
            GameObject obj = (GameObject)Instantiate(single, new Vector3(0, 0, 0), transform.rotation);
            globals final = obj.GetComponent<globals>();
        }

        wave_ctrl = FindObjectsOfType(typeof(wave_controller)) as wave_controller[];
        ball = FindObjectsOfType(typeof(ball_hero)) as ball_hero[];

        // init variables

        cur_floor = -1;
        last_spike_left = false;
        last_spike_right = false;
        last_hole = false;
        last_wall = false;


    }

    #endregion

    #region ================== GAME START ==================

    void Start () {

        
        globals.s.GAME_OVER = 0;
        globals.s.CAN_RESTART = false;
        globals.s.GAME_STARTED = false;
        sound_controller.s.play_music();
       // print("AHHHHHHHHH CORNER LIMIT RIGHT: " + corner_limit_right);
        //Time.timeScale = 0.3f;
        last_hole = false;
        last_spike_left = false;
        last_spike_right = false;
        last_wall = false;
        first_hole_already_created = false;
        first_wall_already_created = false;

		// Calculate ball speed(SLOT*4)/((480+25)/CASUAL_SPEED_X)
		globals.s.CAMERA_SPEED = globals.s.FLOOR_HEIGHT / ((globals.s.LIMIT_RIGHT*2 )/ globals.s.BALL_SPEED_X);
		Debug.Log ("\n============= NEW GAME !!!!!!!!!! =============== USER TOTAL GAMES: " + USER.s.TOTAL_GAMES);

        int count = 0;
        n_floor = 0;
        // create initial platforms
        for (int i = 0; i < 5; i++)
        {
            //Debug.Log (" base y is... : " + globals.s.BASE_Y);
            wave_found = false;
            while (wave_found == false && count < 50) {
                count++;
                switch (i)
                {
                    case 0:
                        GameObject b = create_floor(0, i);
                        if (b == null) Debug.Log("ERROR CREATING THE FIRST FLOOR!!!");
                        wave_found = true;
                        //create_spike_wave(i, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i);
                        break;
                    case 1:
                        //create_floor(0, i);
                       // create_corner_wall(i);
                        wave_found = true;
                        wave_found = create_just_hole(i, 0);
                        last_hole = true;
                        //Debug.Log(" CREATING 1ST EASY:");
                        //create_wave_easy(i);
                        //create_hole(i);
                        // create_floor(0,i);
                        //create_spike_wave(i, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i);
                        break;
                   case 2:
                        //create_hole(i,true);
                        create_floor(0, i);

                        if (USER.s.BEST_SCORE <= 3) 
                            create_spike(0, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i, i);
                        else
                            create_spike(Random.Range(-mid_area, mid_area), globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i, i);

                            //create_hidden_spike(0, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i, i);
                            wave_found = true;

                        break;
                     case 3:
                        
                        //create_wall_corner(i);
                        if (1==2 && USER.s.BEST_SCORE <= 3) {
                            create_floor(0, i);
                            create_spike(corner_left, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i, i);
                            create_spike(corner_right, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i, i);
                            wave_found = true;
                        }
                        else
                            wave_found = create_wave_easy(i);
                        break;
                     case 4:
                        //create_floor(0, i);
                        //create_hole(i);
                        wave_found = create_wave_easy(i);

                        //create_spike(0, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i, i);
                        //wave_found = true;
                        break;

                    default:
                        // Debug.Log(" DEFAULT FIRST WAVE:");
                        wave_found = create_wave_easy(i);
                       // wave_found = create_wave_very_hard(i);
                        //create_corner_wall(i);
                        break;
                }
            }

            if (globals.s.PW_ACTIVE == true && n_floor >= 2 && USER.s.TOTAL_GAMES >= 2) {
                //Debug.Log(" n floor: " + n_floor + " CREATE PW!! ");
                create_power_up_logic();
            }

            n_floor = i+1;
            

            // if (i > 0) { create_spike_wave(i, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i); }
        }

        ball[0].Init_first_ball();
        //wave_ctrl[0].create_new_wave(globals.s.BASE__Y + globals.s.FLOOR_HEIGHT * n_floor);

    }

    #endregion

    #region ================ GAME RUNNING ================

    public void game_running() {
        AnalyticController.s.ReportGameStarted();
        starting_time = Time.time;
    }

    #endregion

    #region ================= GAME END ===================

    bool revive_logic() {
        globals.s.CAN_REVIVE = false;
        there_was_revive = PlayerPrefs.GetInt("there_was_revive", 0); 
        n_games_without_revive = PlayerPrefs.GetInt("n_games_without_revive", 0);
        if ( USER.s.DAY_SCORE > 6 && globals.s.BALL_FLOOR > 6 && ( n_floor > 20 || globals.s.BALL_FLOOR > USER.s.DAY_SCORE - 5)  && there_was_revive == 0) {

            int rand = Random.Range(1,100);
            int dif = 0;
            if (globals.s.BALL_FLOOR < USER.s.DAY_SCORE) dif = 0;
            else dif = USER.s.DAY_SCORE - globals.s.BALL_FLOOR;

            Debug.Log("~~~~~~~~ REVIVE LOGIC ~~~~~~~ RAND: " + rand + " CHANCE TOTAL:  "+ (15 + 5 * dif + n_games_without_revive * 5) + " CHANCE: " + (20 + 5 * dif) + " N games: " + n_games_without_revive);
            if (rand < 15 + 5 * dif + n_games_without_revive * 5)
                globals.s.CAN_REVIVE = true;
        }

        if (globals.s.CAN_REVIVE == true) {
            PlayerPrefs.SetInt("there_was_revive", 1);
        }
        else {
            PlayerPrefs.SetInt("there_was_revive", 0);
            n_games_without_revive++;
            PlayerPrefs.SetInt("n_games_without_revive", n_games_without_revive);
        }

        return globals.s.CAN_REVIVE;
    }

    public void game_over(string killer_wave_name, ball_hero[] ball_hero, bool with_high_score)
    {
        temp_flag_high_score_game_over = with_high_score;
        //Time.timeScale = 0;
        Debug.Log("[GM] GAME OVER");
        killer_wave_to_report = killer_wave_name;
        time_to_report = (int)(Time.time - starting_time);

        globals.s.GAME_OVER = 1;

        temp_ball = ball_hero;
        Invoke("show_game_over", 1f);
    }

    void show_game_over() {

        sound_controller.s.stop_music();
        hud_controller.si.show_game_over(cur_floor + 1, temp_flag_high_score_game_over);

        if (globals.s.SHOW_VIDEO_AFTER == false)
        {
            revive_logic();
            
            if (globals.s.CAN_REVIVE == true)
            {
                hud_controller.si.show_revive_menu();
                globals.s.CAN_REVIVE = false;
            }
            else {
                globals.s.CAN_RESTART = true;
                game_over_for_real();
            }
        }
        else
        {
            hud_controller.si.show_video_revive();
            AnalyticController.s.ReportGameEnded(killer_wave_to_report, time_to_report);
            //globals.s.SHOW_VIDEO_AFTER = false;
            //globals.s.CAN_RESTART = true;
        }
    }

    public void game_over_for_real() {
        AnalyticController.s.ReportGameEnded(killer_wave_to_report, time_to_report);
        PlayerPrefs.SetInt("total_games", USER.s.TOTAL_GAMES + 1);
        PlayerPrefs.SetInt("notes", USER.s.NOTES);

    }

    
    #endregion

    #region ================ ACTIVATE/UNA LOGIC REVIVE ================ 
    public void store_unactive_balls(ball_hero[] ball_hero)
    {
        temp_ball = ball_hero;
        foreach (ball_hero b in temp_ball)
        {
            //Destroy(b.gameObject);
            b.gameObject.SetActive(true);
            break;
        }
    }

    public void activate_logic()
    {
        foreach (ball_hero b in temp_ball)
        {
            //Destroy(b.gameObject);
            b.gameObject.SetActive(true);
            if (b.gameObject.transform.position.x < -3)
            {
                b.gameObject.transform.position = new Vector3(-3, b.gameObject.transform.position.y, b.gameObject.transform.position.z);
            }
            else if (b.gameObject.transform.position.x > 3)
            {
                b.gameObject.transform.position = new Vector3(3, b.gameObject.transform.position.y, b.gameObject.transform.position.z);
            }
            //b.gameObject.rigidbody2D.

            break;
        }
    }
    public void anda_bolinha_fdd()
    {
        foreach (ball_hero b in temp_ball)
        {
            //Destroy(b.gameObject);
            b.activate_pos_revive();
            //b.gameObject.rigidbody2D.

            break;
        }
    }
    public void destroy_spikes_2_floors()
    {
        int i;
        spike[] spikes = GameObject.FindObjectsOfType(typeof(spike)) as spike[];
        for (i = 0; i < spikes.Length; i++)
        {
            spikes[i].remove_spikes_revive(cur_floor);
        }

        wall[] wallss = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
        for (i = 0; i < wallss.Length; i++)
        {
            wallss[i].destroy_me_PW_super();
        }
    }
    #endregion

    #region ================= POWER UPS =========================
    void create_power_up_logic() {
        int rand = Random.Range(0, 100);
        //rand = Random.Range(0, 10);
        // create chance check
        Debug.Log(" CREATE POWER UPS CHANCE: " + rand + " .. CONDITION: " + ((pw_floors_not_created - pw_dont_create_for_n_floors) * 7));
        // if (!QA.s.NO_PWS && pw_floors_not_created > pw_dont_create_for_n_floors && rand <= 15 && globals.s.PW_ACTIVE == true) {
        if (!QA.s.NO_PWS && USER.s.TOTAL_GAMES >= 2 && ((pw_floors_not_created > pw_dont_create_for_n_floors &&
            rand <= (pw_floors_not_created - pw_dont_create_for_n_floors) * 7) || (USER.s.FIRST_PW_CREATED == 0 && !first_pw_created))) {

            int my_type = 0;
            rand = Random.Range(0, 100);
            if (rand < 20 || (USER.s.TOTAL_GAMES >= 2 && USER.s.FIRST_PW_CREATED == 0 && !first_pw_created)) my_type = (int)PW_Types.Super;
            else if (rand < 60) my_type = (int)PW_Types.Invencible;
            else if (rand < 100) my_type = (int)PW_Types.Sight;

            first_pw_created = true;
            // int my_type = Random.Range((int)PW_Types.Invencible, (int)PW_Types.Sight + 1);

            create_pw_icon(Random.Range(corner_limit_left + 0.7f, corner_limit_right - 0.7f), n_floor, my_type);

            pw_floors_not_created = 0;
        }
        else
            pw_floors_not_created++;
    }

    void create_pw_icon(float x, int n, int type) {
        if (globals.s.PW_INVENCIBLE == false || globals.s.PW_SIGHT_BEYOND_SIGHT == false || globals.s.PW_SUPER_JUMP == false) {
            if (QA.s.TRACE_PROFUNDITY >= 3) Debug.Log("[GM] CREATING POWER UP!");
            Instantiate(pw_icon, new Vector3(x, 2 + globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
            Debug.Log("PW type " + type);
            pw_icon.GetComponent<PW_Collect>().my_floor = n;
            pw_icon.GetComponent<PW_Collect>().pw_type = type;
            pw_icon.GetComponent<PW_Collect>().init_my_icon();

        }
    }

    #endregion

    #region ================ GAME LOGIC ================ 

    public void ball_up(int ball_floor)
    {
        if (ball_floor > cur_floor) {
            // if (ball_floor >= 1) camerda.GetComponent<Rigidbody2D>().velocity = new Vector2(0, globals.s.CAMERA_SPEED);
            cur_floor = ball_floor;
            hud_controller.si.update_floor(cur_floor);
            globals.s.BALL_FLOOR = cur_floor;

            create_new_wave();
            if (ball_floor >= 5 && ball_floor % 5 == 0)
                sound_controller.s.update_music();
        }
        else if (ball_floor >= 1) {
            camerda.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //Debug.Log( "~~~~~~~~~~~~~~~~ DON'T CREATE FLOOR!!!! BALL FLOOR: "+ ball_floor + " CUR FLOOR: " + cur_floor);
        }

    }

    public void create_new_wave (){
		Debug.Log(" \n::::::::::::::::::::::: CREATING NEW FLOOR: " +n_floor);

        int rand;
        int count = 0;
        //rand = 1;
        wave_found = false;

        //PW Creation
        if(globals.s.PW_ACTIVE == true && globals.s.PW_SUPER_JUMP == false){
            create_power_up_logic();
        }
       
        while (wave_found == false && count < 50)
        {
            //hole_creation_failed = 9;
            count++;

            // ======== SORT INITIAL WAVES! ========
            if (n_floor <= 9) {
                if (USER.s.TOTAL_GAMES > 4) rand = Random.Range(1, 3);
                else rand = 1;

                switch (rand) {
                    case 1:
                        wave_found = create_wave_easy(n_floor);
                        //wave_found = create_wave_super_hard(n_floor);
                        break;
                    case 2:
                        wave_found = create_wave_medium(n_floor);
                        break;
                }
            }

            // USER HAD SOME PROGRESS
            else if (n_floor <= 15) {
                rand = Random.Range(1, 4);

                switch (rand) {
                    case 1:
                        wave_found = create_wave_easy(n_floor);
                        break;
                    case 2:
                        wave_found = create_wave_medium(n_floor);
                        break;
                    case 3:
                        wave_found = create_wave_hard(n_floor);
                        break;
                }
            }

            // LETS GET SERIOUS!
            else if (n_floor <= 24) {
                rand = Random.Range(1, 100);

                if (rand <= 10)
                    wave_found = create_wave_easy(n_floor);
                else if (rand <= 25)
                     wave_found = create_wave_medium(n_floor);
                else if (rand <= 65)
                    wave_found = create_wave_hard(n_floor);
                else
                    wave_found = create_wave_very_hard(n_floor);
            }
            

            // LETS GET SERIOUS!
            else {
                rand = Random.Range(1, 100);

                if (rand <= 10)
                    wave_found = create_wave_easy(n_floor);
                else if (rand <= 20)
                    wave_found = create_wave_medium(n_floor);
                else if (rand <= 55)
                    wave_found = create_wave_hard(n_floor);
                else if (rand <= 80)
                    wave_found = create_wave_very_hard(n_floor);
                else
                    wave_found = create_wave_super_hard(n_floor);
            }
        }

        if (wave_found == false) Debug.Log("\n******* ERROR! WAVE NOT FOUND!! ********");
        else if (QA.s.TRACE_PROFUNDITY >= 1) Debug.Log("\n " + n_floor + " - " + wave_name);

        n_floor++;

    }

    #endregion

    #region ============== WAVES CREATION LOGIC ==================

    int define_hole_chance() {
        int hole_chance;
        if(USER.s.FIRST_HOLE_CREATED == 0 && first_hole_already_created == false && USER.s.TOTAL_GAMES >=1 && n_floor >=4 ) {
            hole_chance = 90;
        }
        else {
            hole_chance = 30 + 10 * hole_creation_failed;
            if (hole_chance > 85) hole_chance = 85;
        }
        return hole_chance;
    }

    //SINGLE SPIKE SOMEWHERE
    bool create_wave_easy(int n)
    {
        float actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;
        int rand = Random.Range(1, 100);
        int hole_chance = define_hole_chance();
        if (QA.s.TRACE_PROFUNDITY >=2) Debug.Log("\n " + n + " ~~~~~~~~~~~~ TRY CREATE EASY HOLE! ~~~~~~~~~~~~ | rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);


        // HOLE + SPIKE -> 
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            rand = Random.Range(1, 100);
            bool not_hidden;
            if (rand < 60) not_hidden = true;
            else not_hidden = false;
            bool success = create_hole(n, not_hidden, screen_w / 4 - screen_w / 8);

            if (success)
            {
                wave_name = "easy_hole";
                if(QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                last_spike_right = false;
                last_spike_left = false;
                last_hole = true;
                last_wall = false;
            }

            return success;
        }
        else
        {
            rand = Random.Range(1, 100);
            if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " ======= CREATE WAVE EASY! ========== | rand: " + rand);

            // WALL EXCEPTION
            if (1==2 && !last_wall &&  ((USER.s.FIRST_WALL_CREATED == 0 && first_wall_already_created == false && USER.s.TOTAL_GAMES >= 1 && n_floor > 4 && rand < 95))) {
                wave_name = "medium_wall_corner_1_spk";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                // Sort between normal spike, hidden spike or manual hidden spike
                // float rand_x = Random.Range(-mid_area + 0.5f, mid_area - 0.5f);
                float rand_x = Random.Range(-0.35f, 0.35f);
                rand = Random.Range(1, 100);

                if (rand < 60) // Normal spike
                {
                    create_wall_corner(n);
                    create_spike(rand_x, actual_y, n);
                }
                else if (rand < 80) // Hidden Spike
                {
                    create_wall_corner(n);
                    create_hidden_spike(rand_x, actual_y, n);
                }
                else // Hidden spike manual trigger
                {
                    create_wall_corner(n, true);
                    create_hidden_spike(rand_x, actual_y, n, true);
                }

                last_spike_right = true;
                last_spike_left = true;
                last_wall = true;
                last_hole = false;
                return true;
            }


            // 1 SPK MIDDLE |___^___|
            if (rand > 0 && rand <= 30)
            {
                wave_name = "easy_spk_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_spike(Random.Range(corner_limit_left + 1.4f, corner_limit_right - 1.4f), actual_y, n);
                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;
                return true;
            }
            // 1 TRIPLE SPK MIDDLE |___/\___|
            if (rand > 30 && rand <= 60)
            {
                wave_name = "easy_triple_spk_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_triple_spike(Random.Range(-screen_w / 3 + 0.8f, screen_w / 3 - 0.8f), actual_y, n);
                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // 1 SPK CORNER LEFT |^_____|
            else if (last_spike_left == false && rand > 60 && rand <= 80)
            {
                wave_name = "easy_spike_left";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_spike(corner_left, actual_y, n);
                last_spike_right = false;
                last_spike_left = true;
                last_hole = false;
                last_wall = false;

                return true;
            }
            // 1 SPK CORNER RIGHT |____^|
            else if (last_spike_right == false && rand > 80)
            {
                wave_name = "easy_spk_right";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_spike(corner_right, actual_y, n);
                last_spike_right = true;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;

            }
            else return false;
        }
    }

    // 2 SPIKES, HOLE AND 1 HIDDEN SPK
    bool create_wave_medium(int n)
    {
        float actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;
        int rand = Random.Range(1, 100);
        int hole_chance = define_hole_chance();

        if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n "+ n+ " ~~~~~~~~~~~~ TRY CREATE MEDIUM HOLE! ~~~~~~~~~~~~ | rand " + rand + " HOLE CHANCE: "+ hole_chance + " N FAILED: " + hole_creation_failed);
   
        
        // FIRST, LET'S TRY TO CREATE A HOLE
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            bool success = create_hole(n);

            if (success)
            {
                wave_name = "medium_hole_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                float spk_pos;
                if (last_hole_x < 0) {
                    spk_pos = Random.Range(last_hole_x + min_spk_dist+0.2f, corner_limit_right -1f);
                }
                else {
                    spk_pos = Random.Range(corner_limit_left + 1f, last_hole_x - min_spk_dist-0.2f);

                }
                create_spike(spk_pos, actual_y, n);

                hole_creation_failed = 0;
                last_spike_right = false;
                last_spike_left = false;
                last_wall = false;
                last_hole = true;
            }

            return success;
        }

        else
        {
            hole_creation_failed++;

            //rand = Random.Range(44, 68);
            if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " ========= CREATE WAVE MEDIUM! ========== | rand " + rand);
            //rand = 50;

            // WALL CORNER + 1 SPK ||___^__|
            if (!last_wall && ((rand > 0 && rand <= 25) || (USER.s.FIRST_WALL_CREATED == 0 && first_wall_already_created == false && USER.s.TOTAL_GAMES >= 1 && n_floor > 4 && rand < 95 ) )) {
                wave_name = "medium_wall_corner_1_spk";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);

                // Sort between normal spike, hidden spike or manual hidden spike
                // float rand_x = Random.Range(-mid_area + 0.5f, mid_area - 0.5f);
                float rand_x = Random.Range(-0.35f, 0.35f);
                rand = Random.Range(1, 100);

                if (rand < 60) // Normal spike
                {
                    create_wall_corner(n);
                    create_spike(rand_x, actual_y, n);
                }
                else if (rand < 80) // Hidden Spike
                {
                    create_wall_corner(n);
                    create_hidden_spike(rand_x, actual_y, n);
                }
                else // Hidden spike manual trigger
                {
                    create_wall_corner(n, true);
                    create_hidden_spike(rand_x, actual_y, n, true);
                }

                last_spike_right = true;
                last_spike_left = true;
                last_wall = true;
                last_hole = false;
                return true;
            }
            
            // 2 SPK CRONER |^______^|
            else if (!last_spike_left && !last_spike_right && rand <= 40)
            {
                wave_name = "medium_2_spks_corners";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_spike(corner_left, actual_y, n);
                create_spike(corner_right, actual_y, n);
                last_spike_right = true;
                last_spike_left = true;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // 1 SPK CORNER LEFT, 1 SPK MID |^___^____|
            else if (!last_spike_left && rand > 40 && rand <= 48)
            {
                wave_name = "medium_1_spk_mid_1_spk_corner";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_spike(corner_left, actual_y, n);
                create_spike(Random.Range(-mid_area + 0.30f, mid_area - 0.30f), actual_y, n);

                last_spike_right = false;
                last_spike_left = true;
                last_wall = false;
                last_hole = false;
                return true;
            } 
            // 1 SPK CORNER RIGHT, 1 SPK MID |____^___^|
            else if (!last_spike_right && rand > 48 && rand <= 56)
            {
                wave_name = "medium_1_spk_mid_1_spk_corner";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_spike(corner_right, actual_y, n);
                create_spike(Random.Range(-mid_area  +0.30f, mid_area - 0.30f), actual_y, n);

                last_spike_right = true;
                last_spike_left = false;
                last_wall = false;
                last_hole = false;
                return true;
            }

            // 2 SPK MIDDLE |__^_^__|
            if (rand > 56 && rand <= 80) {
                wave_name = "medium_2_spks_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float rand_x = Random.Range(-screen_w / 4, 0 - 1f);
                //first spike
                create_spike(rand_x, actual_y, n);
                if (rand_x <= corner_limit_left) last_spike_left = true;
                else last_spike_left = false;

                //second spike
                rand_x = Random.Range(rand_x + min_spk_dist + 0.5f, rand_x + min_spk_dist + 1.5f);
                create_spike(rand_x, actual_y, n);
                if (rand_x <= corner_limit_right) last_spike_right = true;
                else last_spike_right = false;

                last_hole = false;
                last_wall = false;

                return true;
            }


            // 1 HIDDEN SPK |____v____|
            else if (rand > 80)
            {
                wave_name = "medium_1_hidden_spk";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                create_hidden_spike(Random.Range(-mid_area + 0.2f, mid_area - 0.2f), actual_y, n);

                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

          
            else return false;
        }
        
    }

    // 3 SPIKES, HOLE + SPK, 2 HIDDEN, ...
    bool create_wave_hard(int n)
    {
        float actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;
        int rand = Random.Range(1, 100);
        int hole_chance = define_hole_chance();
        if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " ^^^^^^^^^^^^^^^^^^ TRY CREATE HARD HOLE! ^^^^^^^^^^^^^^^^^^ | rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);


        // HOLE + 2 SPIKES 
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            //bool success = create_hole(n, false, screen_w / 4 - screen_w / 8);
            bool success = create_hole(n, false, 1.5f);
            //success = true;

            if (success) {
                last_hole = true;
                hole_creation_failed = 0;

                wave_name = "hard_hole";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                float spk_pos = 0;
                if (last_hole_x > corner_left + (min_spk_dist + 0.6f)) { // can create spike left check
                    spk_pos = Random.Range(corner_left, last_hole_x - min_spk_dist - 0.6f);
                    if (spk_pos <= corner_limit_left) {
                        if (!last_spike_left) {
                            last_spike_left = true;
                            //spk_pos = corner_left;
                            //Debug.Log("SPK LEFT: " + spk_pos);
                            create_spike(spk_pos, actual_y, n);
                        }
                        else last_spike_left = false;
                    }
                    else {
                        last_spike_left = false;
                        create_spike(spk_pos, actual_y, n);
                    }
                }

                if (last_hole_x < corner_right - (min_spk_dist + 0.6f)) { // can create spike right check
                    spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.6f, corner_right);
                    if (spk_pos >= corner_limit_right) {
                        if (!last_spike_right) {
                            last_spike_right = true;
                            create_spike(spk_pos, actual_y, n);
                        }
                        else last_spike_right = false;
                    }
                    else {
                        last_spike_right = false;
                        create_spike(spk_pos, actual_y, n);
                    }
                }
            }



            return success;
        }
        else {
            hole_creation_failed++;
            rand = Random.Range(1, 100);
            if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " ========= CREATE WAVE HARD! ========== | rand " + rand);

            // CORNERS AND 1 SPK MIDDLE |^__^__^|
            if (!last_spike_right && !last_spike_left && rand > 0 && rand <= 25)
            {
                wave_name = "hard_3_spks";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                //first spike
                create_spike(corner_left, actual_y, n);
                create_spike(corner_right, actual_y, n);
                create_spike(Random.Range(-mid_area + 0.35f, mid_area - 0.35f), actual_y, n);

                last_spike_right = true;
                last_spike_left = true;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // 1 HIDDEN SPK  + 1 SPK MID |__ v_^__|
            else if (rand > 25 && rand <= 45)
            {
                wave_name = "hard_1_hidden_spk_1_spk_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                int is_left = Random.Range(0, 2);
                float rand_x = Random.Range(-0.5f, 0.5f);
                float dist = Random.Range(min_spk_dist/2 + 0.2f, min_spk_dist/2 + 0.45f);

                if (is_left == 1)
                {
                    //first spike
                    create_hidden_spike(rand_x - dist, actual_y, n);
                    create_spike(rand_x + dist, actual_y, n);
                }
                else
                {
                    create_spike(rand_x - dist, actual_y, n);
                    create_hidden_spike(rand_x + dist, actual_y, n);
                }

                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // WALL MID-LEFT + SPIKE (12%) |__|____^_|
            else if (!last_wall && !last_spike_left && rand > 45 && rand <= 58)
            {
                wave_name = "hard_wall_mid_left";
                if (QA.s.SHOW_WAVE_TYPE == true)
                    create_wave_name(0, actual_y, wave_name);

                float wall_pos = Random.Range(-screen_w / 4 + 0.5f, 0 - 0.5f);
                float spk_pos = Random.Range(wall_pos + min_spk_dist + 1.1f, corner_right);

                if (spk_pos >= corner_limit_right && last_spike_right)
                    return false;
                else
                {
                    create_floor(0, n);
                    create_wall(wall_pos, n);
                    create_spike(spk_pos, actual_y, n);

                    if (spk_pos >= corner_limit_right) last_spike_right = true;
                    else last_spike_right = false;
                    last_spike_left = false;
                    last_wall = true;
                    last_hole = false;
                    return true;
                }
            }

            // WALL MID-RIGHT + SPIKE (12%) |_^____|__|
            else if (!last_wall && !last_spike_right && rand > 58 && rand <= 73)
            {
                wave_name = "hard_wall_mid_right";
                if (QA.s.SHOW_WAVE_TYPE == true)
                    create_wave_name(0, actual_y, wave_name);

                float wall_pos = Random.Range(0 + 0.5f, screen_w / 4 - 0.5f);
                float spk_pos = Random.Range(corner_left, wall_pos - min_spk_dist - 1.1f);

                if (spk_pos <= corner_limit_left && last_spike_left)
                    return false;
                else
                {
                    create_floor(0, n);
                    create_wall(wall_pos, n);
                    create_spike(spk_pos, actual_y, n);

                    last_spike_right = false;
                    if (spk_pos <= corner_limit_left) last_spike_left = true;
                    else last_spike_left = false;
                    last_wall = true;
                    last_hole = false;
                    return true;
                }
            }

            // 2 HIDDEN SPIKE MID |___v__v___|
            if (rand > 73 && rand <= 86)
            {
                wave_name = "hard_2_hidden_spk_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                //float rand_x = Random.Range(-screen_w / 4+0.2f, 0 - 1.00f);
                float dist = Random.Range(min_spk_dist/2 + 0.2f, min_spk_dist/2 + 0.4f);
                float rand_x = Random.Range(-0.5f, 0.5f);
                //first spike
                create_hidden_spike(rand_x - dist, actual_y, n);
                create_hidden_spike(rand_x + dist, actual_y, n);

                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // 2 TRIPLE SPK MID |___/\__/\___|
            if (rand > 86) {
                wave_name = "medium_2_triple_spk_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float dist = Random.Range(min_spk_dist/2 + 0.2f, min_spk_dist/2 + 0.4f);
                float rand_x = Random.Range(-0.5f, 0.5f);
                //first spike
                create_triple_spike(rand_x - dist, actual_y, n);
                create_triple_spike(rand_x + dist, actual_y, n);

                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

            else return false;
        }
    }

    // YOU DON'T WANNA KNOW
    bool create_wave_very_hard(int n)
    {
        float actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;
        int rand = Random.Range(1, 100);
        //rand = 1;
        int hole_chance = define_hole_chance();
        if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " &&&&&&&&&&&&&& TRY CREATE VERY HARD HOLE! &&&&&&&&&&| rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);

        // rand = 80;

        //  HOLE WITH SPK AT ITS SIDE AND ANOTHER CONDITIONAL SPIKE |_^\/__^_|
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            bool success = create_hole(n, false, 1.5f);

            if (success)
            {
                wave_name = "vhard_hole";
                if (QA.s.SHOW_WAVE_TYPE == true)
                    create_wave_name(0, actual_y, wave_name);

                hole_creation_failed = 0;
                last_hole = true;
                last_wall = false;

                int left_or_right = Random.Range(0, 2);
                if(left_or_right == 0)  
                {
                    Debug.Log(" ^_ 1 SPIKE NEXT AT LEFT FROM THE HOLE");
                    float spk_pos = last_hole_x - 1.168f;
                    create_spike(spk_pos, actual_y, n); // create the 1st spike, next to the hole at its left
                     
                    if (last_hole_x < 1f) {// HOLE IS LEFT FROM THE CENTER |__^\/___^|
                        if (spk_pos < corner_limit_left) last_spike_left = true;
                        
                        // TRY TO SORT A POSSIBLE SPIKE
                        if(!last_spike_right)
                            spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.6f, corner_right);
                        else {
                            if (last_hole_x + min_spk_dist + 0.6f > corner_limit_right) // CAN SORT A SPIKE AT RIGHT...
                                return true;
                            else
                                spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.6f, corner_limit_right);
                        }

                        if (spk_pos >= corner_limit_right) {
                            last_spike_right = true;
                            create_spike(spk_pos, actual_y, n); // create the 1st spike, next to the hole at its left
                        }

                        else { //try to make it hidden
                            int hidden_chance = Random.Range(0,100);
                            if (hidden_chance < 45 && spk_pos <= corner_limit_right - 1f)
                                create_hidden_spike(spk_pos, actual_y, n);
                            else
                                create_spike(spk_pos, actual_y, n);

                        }
                    }
                    else { // HOLE IS RIGHT FROM THE CENTER: |^___^\/_|

                        if (!last_spike_left)
                            spk_pos = Random.Range(corner_left, last_hole_x - min_spk_dist - 0.6f);
                        else {
                            if (last_hole_x - min_spk_dist - 0.6f < corner_limit_left) // CAN SORT A SPIKE AT LEFT..
                                return true;
                            else
                                spk_pos = Random.Range(corner_limit_left, last_hole_x - min_spk_dist - 0.6f);

                        }

                        create_spike(spk_pos, actual_y, n); 
                        if (spk_pos < corner_limit_left) last_spike_left = true;
                    }
                }
                // AT RIGHT OF THE ROLE
                else  {
                    if (QA.s.TRACE_PROFUNDITY >= 3) Debug.Log(" _^ 1 SPIKE NEXT AT RIGHT FROM THE HOLE");
                    float spk_pos = last_hole_x + 1.168f;
                    create_spike(spk_pos, actual_y, n); // create the 1st spike, next to the hole at its left

                    if (last_hole_x > -1f) {// HOLE IS LEFT FROM THE CENTER |__^\/___^|
                        if (spk_pos > corner_limit_right) last_spike_right = true;

                        // TRY TO SORT A POSSIBLE SPIKE
                        if (!last_spike_left)
                            spk_pos = Random.Range(corner_left, last_hole_x - min_spk_dist - 0.6f);
                        else {
                            if (last_hole_x - min_spk_dist - 0.6f < corner_limit_left) // CANT SORT A SPIKE AT RIGHT...
                                return true;
                            else
                                spk_pos = Random.Range(corner_limit_left, last_hole_x - min_spk_dist - 0.6f);
                        }

                        if (spk_pos <= corner_limit_left) {
                            last_spike_left = true;
                            create_spike(spk_pos, actual_y, n); // create the 1st spike, next to the hole at its right
                        }

                        else { //try to make it hidden
                            int hidden_chance = Random.Range(0,100);
                            if (hidden_chance < 45 && spk_pos > corner_limit_left + 1f)
                                create_hidden_spike(spk_pos, actual_y, n);

                            else
                                create_spike(spk_pos, actual_y, n);


                        }
                    }
                    else { // HOLE IS RIGHT FROM THE CENTER: |^___^\/_|

                        if (!last_spike_right)
                            spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.6f, corner_right);
                        else {
                            if (last_hole_x + min_spk_dist + 0.6f > corner_limit_right) // CANT SORT A SPIKE AT RIGHT..
                                return true;
                            else
                                spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.6f, corner_limit_right);
                        }

                        create_spike(spk_pos, actual_y, n);
                        if (spk_pos > corner_limit_right) last_spike_right = true;
                    }
                }
            }

            return success;
        }
        else
        {
            hole_creation_failed++;
            rand = Random.Range(1, 100);
            if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " ========== CREATE WAVE VERY HARD! ========== | rand " + rand);
            //rand = 1;
            // rand = Random.Range(1, 25);
            
            if(rand == 0) {
                create_floor(0, n);
                last_wall = false;
                last_hole = false;
                return true;
            }

            // 3 SPIKES MID (LEFT priority) |__^_^_^__|
            else if (!last_spike_left && rand > 0 && rand <= 15)
            {
                wave_name = "vhard_3_spks";
                if (QA.s.SHOW_WAVE_TYPE == true)
                    create_wave_name(0, actual_y, wave_name);

                create_floor(0, n);
                float triple_range = min_spk_dist + Random.Range(0.45f, 0.6f);
                float spk_pos = Random.Range(corner_left, corner_limit_left + 0.35f);
                create_spike(spk_pos, actual_y, n);

                if (spk_pos <= corner_limit_left) last_spike_left = true;
                else last_spike_left = false;

                //TRY TO HIDE THE MIDDLE SPIKE
                spk_pos += triple_range;
                rand = Random.Range(1, 100);
                if (rand < 75)
                    create_spike(spk_pos, actual_y, n);
                else
                    create_hidden_spike(spk_pos, actual_y, n);

                spk_pos += triple_range;
                create_spike(spk_pos, actual_y, n);

                if (spk_pos >= corner_limit_right) last_spike_right = true;
                else last_spike_right = false;

                last_hole = false;
                last_wall = false;

                return true;
            }
            // 3 SPIKES MID (RIGHT priority |__^_^_^__|)
            else if (!last_spike_right && rand>15 && rand <= 30)
            {
                wave_name = "vhard_3_spks";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float triple_range = min_spk_dist + Random.Range(0.45f, 0.6f);

                float spk_pos = Random.Range(corner_limit_right - 0.35f, corner_right);
                create_spike(spk_pos, actual_y, n);
                if (spk_pos >= corner_limit_right) last_spike_right= true;
                else last_spike_right = false;

                //TRY TO HIDE THE MIDDLE SPIKE
                spk_pos -= triple_range;
                rand = Random.Range(1, 100);
                if(rand < 75)
                    create_spike(spk_pos, actual_y, n);
                else
                    create_hidden_spike(spk_pos, actual_y, n);

                spk_pos -= triple_range;
                create_spike(spk_pos, actual_y, n);

                if (spk_pos <= corner_limit_left) last_spike_left = true;
                else last_spike_left = false;

                last_hole = false;
                last_wall = false;

                return true;
            }


            // WALL CORNER + 2 spks (normal, hidden or manual_trigger)  ||__v_v__|
            else if (!last_wall && rand > 30 && rand <= 70)
            {
                wave_name = "vhard_wall";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                bool there_is_manual = false;

                create_floor(0, n);

                float rand_x;
                rand_x = Random.Range(-center_mid_area, center_mid_area);
                //first spike, located at middle
                rand = Random.Range(1, 100);
                if (rand < 40)
                    create_spike(rand_x, actual_y, n);

                else if (rand < 65)
                    create_hidden_spike(rand_x, actual_y, n);
                else
                {
                    create_hidden_spike(rand_x, actual_y, n, true);
                    there_is_manual = true;
                }

                //second spike, manually triggered located at the opposite corner of the wall
                float rand_x2 = Random.Range(corner_right - 1.3f, corner_right);

                create_hidden_spike(rand_x2, actual_y, n, true, true);
                there_is_manual = true;

                create_wall_corner(n, there_is_manual);

                last_spike_right = true;
                last_spike_left = true;
                last_wall = true;
                last_hole = false;
                return true;
            }

            // 2 HIDDEN TRIPLE SPIKE MID |___v__v___|
            else if (rand > 70)
            {
                wave_name = "vhard_2_hidden_triple_spk_mid";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float rand_x = Random.Range(min_spk_dist/2+0.3f, min_spk_dist/2 + 0.6f );
                float pos = Random.Range(-0.45f,0.45f);
                //first spike
                create_triple_hidden_spike(pos -rand_x, actual_y, n);
                create_triple_hidden_spike(pos + rand_x, actual_y, n);

                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }
            else return false;
        }
    }

    // MADE FOR YOU TO DIE
    bool create_wave_super_hard(int n) {
        float actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;
        int rand = Random.Range(1, 100);
        //rand = 1;
        int hole_chance = define_hole_chance();
        if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " SSSSSSSSSSSSSS TRY CREATE SUPER HARD HOLE! SSSSSSSSSSSSS| rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);

        //rand = 80;

        // HOLE + 2 TRIPLE SPIKES |_^__\/__^_|
        if (!last_wall && !last_hole && !last_spike_left && !last_spike_right && rand > 0 && rand <= hole_chance) {
            bool success = create_hole(n, false, 1.5f);

            if (success) {

                wave_name = "shard_hole";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                hole_creation_failed = 0;
                last_hole = true;
                last_wall = false;

                rand = Random.Range(1, 100);
                if (QA.s.TRACE_PROFUNDITY >= 2)
                    Debug.Log("RAND IS :" + rand);
                float spk_pos = 0;
                //rand = 68;
                // SPIKES FAR FROM HOLE
                if (rand < 65) {

                    if (last_hole_x - min_spk_dist - 0.8f > corner_left) {
                        spk_pos = Random.Range(corner_left, last_hole_x - min_spk_dist - 0.8f);

                        //spike left
                        if (spk_pos <= corner_limit_left) last_spike_left = true;
                        else last_spike_left = false;
                        create_triple_spike(spk_pos+0.2f, actual_y, n);
                    }

                    if (last_hole_x + min_spk_dist + 0.8f < corner_right) {
                        spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.8f, corner_right);
                        if (spk_pos >= corner_limit_right) last_spike_right = true;
                        else last_spike_right = false;
                        create_triple_spike(spk_pos-0.2f, actual_y, n);
                    }
                    
                }
                else // 1 HIDDEN SPIKE OR TRIPLE SPIKE AT CORNER + 1 TRIPLE SPIKE AT HOLE BORDER! 
                {
                    int left_or_right = Random.Range(0, 2);
                    if (left_or_right == 0) {
                        if (last_hole_x - min_spk_dist - 0.5f > corner_left) {
                            spk_pos = Random.Range(corner_left + 0.2f, last_hole_x - min_spk_dist - 0.5f);

                            if (spk_pos >= corner_limit_left - 0.5f) {
                                last_spike_left = true;
                                create_triple_spike(spk_pos, actual_y, n);


                            }
                            else {
                                create_hidden_spike(spk_pos, actual_y, n);
                                last_spike_left = false;
                            }
                        }
                        else
                            last_spike_left = false;
                        spk_pos = last_hole_x + 1.168f + 0.17f;
                        create_triple_spike(spk_pos, actual_y, n);
                        last_spike_right = false;

                    }
                    else {
                        if (last_hole_x + min_spk_dist + 0.6f < corner_right) {
                            spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.6f, corner_right);

                            if (spk_pos <= corner_limit_right + 0.5f) {
                                last_spike_right = true;
                                create_triple_spike(spk_pos, actual_y, n);

                            }

                            else {
                                create_hidden_spike(spk_pos, actual_y, n);
                                last_spike_right = false;
                            }
                        }
                        else
                            last_spike_right = false;

                        spk_pos = last_hole_x - 1.168f - 0.17f;
                        create_hidden_spike(spk_pos, actual_y, n);
                        last_spike_left = false;
                    }

                }
                
            }

            return success;
        }
        else {
            hole_creation_failed++;
            rand = Random.Range(1, 100);
            if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("\n " + n + " ========== CREATE WAVE SUPER HARD! ========== | rand " + rand);
            //rand = 35;
            // rand = Random.Range(1, 25);


            // 3 TRIPLE SPIKES MID (LEFT priority) |__^_^_^__|
            if (!last_spike_left && rand > 0 && rand <= 15) {
                wave_name = "shard_3_triple_spks";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float triple_range = min_spk_dist + Random.Range(0.5f, 0.6f);
                float spk_pos = Random.Range(corner_left, corner_limit_left + 0.35f);

                //first spike
                create_triple_spike(spk_pos, actual_y, n);

                if (spk_pos <= corner_limit_left + 0.5f) last_spike_left = true;
                else last_spike_left = false;

                //TRY TO HIDE THE MIDDLE SPIKE
                spk_pos += triple_range;
                rand = Random.Range(1, 100);
                if (rand < 65)
                    create_triple_spike(spk_pos, actual_y, n);
                else
                    create_triple_hidden_spike(spk_pos, actual_y, n);

                spk_pos += triple_range;
                create_triple_spike(spk_pos, actual_y, n);

                if (spk_pos >= corner_limit_right - 0.5f) last_spike_right = true;
                else last_spike_right = false;

                last_hole = false;
                last_wall = false;

                return true;
            }
           
            // 3 TRIPLE SPIKES MID (RIGHT priority |__^_^_^__|)
            else if (!last_spike_right && rand > 15 && rand <= 30) {
                wave_name = "shard_3_triple_spks";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float triple_range = min_spk_dist + Random.Range(0.5f, 0.6f);

                float spk_pos = Random.Range(corner_limit_right - 0.45f, corner_right);

                create_triple_spike(spk_pos, actual_y, n);
                if (spk_pos >= corner_limit_right - 0.5f) last_spike_right = true;
                else last_spike_right = false;

                //TRY TO HIDE THE MIDDLE SPIKE
                spk_pos -= triple_range;
                rand = Random.Range(1, 100);
                if (rand < 75)
                    create_triple_spike(spk_pos, actual_y, n);
                else
                    create_triple_hidden_spike(spk_pos, actual_y, n);

                spk_pos -= triple_range;
                create_triple_spike(spk_pos, actual_y, n);

                if (spk_pos <= corner_limit_left + 0.5f) last_spike_left = true;
                else last_spike_left = false;

                last_hole = false;
                last_wall = false;

                return true;
            }

            // DOUBLE WALL CORNER + 1 spk (normal, hidden or manual_trigger)  ||__v_v__|
            else if (!last_wall && rand > 30 && rand <= 55) {
                wave_name = "shard_double_wall";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                bool there_is_manual = false;

                create_floor(0, n);
                
                float rand_x = Random.Range(-center_mid_area, center_mid_area);
                //spike, located at middle
                rand = Random.Range(1, 100);
                if (rand < 40)
                    create_spike(rand_x, actual_y, n);
                else if (rand < 65)
                    create_hidden_spike(rand_x, actual_y, n);
                else {
                    create_hidden_spike(rand_x, actual_y, n, true);
                    there_is_manual = true;
                }

                //second spike, manually triggered located at the opposite corner of the wall
                float rand_x2 = Random.Range(corner_right - 1.3f,corner_right);

                // WALL TWEEN LOGIC
                wall w1 = create_wall_corner(n, there_is_manual);

                wall w2 = create_wall_corner(n, false);

                w1.my_twin_wall = w2;
                w2.my_twin_wall = w1;

                w1.wall_trigger = true;
                w2.wall_triggered_by_wall = true;
                w2.GetComponent<BoxCollider2D>().enabled = false;

                last_spike_right = false;
                last_spike_left = false;
                last_wall = true;
                last_hole = false;
                return true;
            }

            // 2 TRIPLE SPIKE CLOSE to each other AT MID
            else if (rand > 55 && rand <= 80) {
                wave_name = "shard_2_triple_spikes";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float pair_range = 1.45f;


                create_triple_spike(0 - pair_range, actual_y, n);
                create_triple_spike(0 + pair_range, actual_y, n);

                last_hole = false;
                last_wall = false;
                last_spike_left = false;
                last_spike_right = false;

                return true;
            }


            // 4 SPIKES IN PAIRS |___v_v___v_v___|
            else if (rand > 80 && !last_spike_left && !last_spike_right) {
                wave_name = "shard_4_spikes";
                if (QA.s.SHOW_WAVE_TYPE == true)
                {
                    create_wave_name(0, actual_y, wave_name);
                }
                create_floor(0, n);
                float pair_range = Random.Range(2.42f, 2.50f);

                // left spike
                float spk_pos = corner_left;
                create_spike(spk_pos, actual_y, n);

                spk_pos += pair_range;
                create_spike(spk_pos, actual_y, n);

                spk_pos = corner_right;
                create_spike(spk_pos, actual_y, n);

                spk_pos -= pair_range;
                create_spike(spk_pos, actual_y, n);

                if (spk_pos >= corner_limit_right) last_spike_right = true;
                else last_spike_right = false;

                last_hole = false;
                last_wall = false;
                last_spike_left = true;
                last_spike_right = true;

                return true;
            }
            else return false;
        }
    }

    #endregion

    #region ============== WAVE ELEMENTS =================

    void create_wall(float x, int n)
    {
        GameObject obj = (GameObject)Instantiate(wall_type, new Vector3(x, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n +  globals.s.SLOT/2, 0), transform.rotation);
        obj.GetComponent<wall>().my_floor = n;
    }

    wall create_wall_corner(int n, bool spk_trigger = false){
        GameObject obj = (GameObject)Instantiate(wall_type, new Vector3(corner_right, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n  + globals.s.SLOT / 2, 0), transform.rotation);
        wall temp_wall = obj.GetComponent<wall>();
        temp_wall.my_floor = n;
        temp_wall.corner_wall = true;
        temp_wall.spike_trigger = spk_trigger;
        temp_wall.GetComponent<BoxCollider2D>().enabled = false;

        return obj.GetComponent<wall>();
    }

    void create_spike(float x, float y, int n, bool corner_repositionable = false)
    {

        //GameObject obj = (GameObject)Instantiate(spike_type, new Vector3(x, y + globals.s.SLOT/2, 0), transform.rotation);
        GameObject obj = objects_pool_controller.s.reposite_double_spikes(x, y + globals.s.SLOT / 2);
        spike spk = obj.GetComponent<spike>();

        if(spk != null)
        {
            spk.my_floor = n;
            spk.corner_repositionable = corner_repositionable;
            spk.wave_name = wave_name;
        }

        ///////////////////////// CREATE NOTES OR NOT

        int rand = Random.Range(1,100);
        if(rand <= 10) {
            GameObject instance = Instantiate(Resources.Load("Prefabs/Note",
            typeof(GameObject)), new Vector3(x, y + globals.s.SLOT / 2 + 1.85f), transform.rotation) as GameObject;

        }
    }

    void create_hidden_spike(float x, float y, int n, bool manual_trigger = false, bool corner_repositionable = false)
    {
        //GameObject obj = (GameObject)Instantiate(spike_type, new Vector3(x, y + globals.s.SLOT/2 - spike_type.transform.GetComponent<SpriteRenderer>().bounds.size.y, 0), transform.rotation);
        //GameObject obj = objects_pool_controller.s.reposite_double_spikes(x, y + globals.s.SLOT / 2 - spike_type.transform.GetComponent<SpriteRenderer>().bounds.size.y);
        GameObject obj = objects_pool_controller.s.reposite_double_spikes(x, y + globals.s.SLOT / 2 - 0.5f);
        obj.transform.localScale = new Vector3(globals.s.SPK_SCALE, 0.5f, globals.s.SPK_SCALE);
        
        spike spk = obj.GetComponent<spike>();
       // spk.hidden_scale_0();
        if (spk != null)
        {
            spk.hidden = true;
            spk.my_floor = n;
            spk.manual_trigger = manual_trigger;
            spk.my_collider.enabled = false;
            spk.corner_repositionable = corner_repositionable;
            spk.wave_name = wave_name;
            if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
            {
                spk.show_me_pw_sight();
            }
        }
    }

    void create_triple_spike(float x, float y, int n)
    {

       // GameObject obj = (GameObject)Instantiate(triple_spike_type, new Vector3(x, y + globals.s.SLOT / 2, 0), transform.rotation);
        GameObject obj = objects_pool_controller.s.reposite_triple_spikes(x, y + globals.s.SLOT / 2);

        if (obj.GetComponent<spike>() != null)
        {
            obj.GetComponent<spike>().my_floor = n;
            obj.GetComponent<spike>().wave_name = wave_name;

        }
    }

    void create_triple_hidden_spike(float x, float y, int n, bool manual_trigger = false)
    {
       // GameObject obj = (GameObject)Instantiate(triple_spike_type, new Vector3(x, y + globals.s.SLOT / 2 - triple_spike_type.transform.GetComponent<SpriteRenderer>().bounds.size.y, 0), transform.rotation);
        //GameObject obj = objects_pool_controller.s.reposite_triple_spikes(x, y + globals.s.SLOT / 2 - triple_spike_type.transform.GetComponent<SpriteRenderer>().bounds.size.y);
        GameObject obj = objects_pool_controller.s.reposite_triple_spikes(x, y + globals.s.SLOT / 2 - 0.5f);
        obj.transform.localScale = new Vector3(globals.s.SPK_SCALE, 0.5f, globals.s.SPK_SCALE);
        spike spk = obj.GetComponent<spike>();

        if (spk != null)
        {
            spk.hidden = true;
            spk.my_floor = n;
            spk.manual_trigger = manual_trigger;
            spk.my_collider.enabled = false;
            spk.wave_name = wave_name;
            if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
            {
                spk.show_me_pw_sight();
            }
        }
    }

    public void create_bg(int n) {
        int rand;
        if (n <= 5) {
            rand = Random.Range(1, 5);
            while (rand == last_bg) rand = Random.Range(1, 5);
            last_bg = rand;

            GameObject instance = Instantiate(Resources.Load("Prefabs/Bgs/Scenario1/floor_"+rand,
                typeof(GameObject)), new Vector3(0, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n  + 2.45f), transform.rotation) as GameObject;
        }
        else if (n <= 1000) {
            rand = Random.Range(1, 4);
            while (rand == last_bg) rand = Random.Range(1, 4);
            last_bg = rand;

            GameObject instance = Instantiate(Resources.Load("Prefabs/Bgs/Scenario2/bg_"+rand,
                typeof(GameObject)), new Vector3(0, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n  + 2.45f), transform.rotation) as GameObject;
        }   
        //(GameObject)Instantiate(, new Vector3(0, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
    }

    public GameObject create_floor(float x, int n)
    {
       // GameObject obj = (GameObject)Instantiate(floor_type, new Vector3(x, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
        GameObject obj = objects_pool_controller.s.reposite_floor(x, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n);
        obj.GetComponent<floor>().my_floor = n;
        obj.GetComponent<floor>().check_if_have_score();
        //obj.GetComponentInChildren<TextMesh>().text = n.ToString();

        create_bg(n);
        return obj;
    }

    bool create_hole(int n, bool not_hidden = false, float custom_rand = 0)
    {
        // Debug.Log("tttttttttttttttttttttt TRYING TO CREATE HOLE AT FLOOR: " + n);
        float rand;
        if (custom_rand == 0) rand = Random.Range(-screen_w / 4, screen_w / 4);
        else rand = Random.Range(-custom_rand, custom_rand);
        int i, j = 0, count = 0;
        bool can_create = false;
        spike[] spks = FindObjectsOfType(typeof(spike)) as spike[];

        // chef if there is any spikes
        if (spks.Length > 0) {
            List<spike> spks_below = new List<spike>();
            //Debug.Log("THERE IS SPIKES! " + spks.Length);
            // INITIALIZE THE SPIKES FROM FLOOR BELOW
            for (i = 0; i < spks.Length; i++){
                if (spks[i].my_floor == n - 1) {
                    spks_below.Add(spks[i]);
                    j++;
                }
            }
           // Debug.Log("spikes below: " + spks_below.Count);
            if (spks_below.Count > 0) {
                while (count < 50 && can_create == false)
                {
                    can_create = true;
                    rand = Random.Range(-screen_w / 4, screen_w / 4);
                    foreach (spike spk in spks_below)
                    {
                        can_create = spk.check_range_for_hole(n-1, rand);
                        if (can_create == false)
                        {
                            //Debug.Log("NO WAY.. RANDX: " + rand + " SPIKE POS: " + spk.transform.position.x); 
                            break;
                        }
                    }
                    count++;
                }
            }
            else
            {
                can_create = true;
                //Debug.Log("[HOLE] THERE IS NO SPIKES BELOW! MY FLOOR: " + n + " JUST CREATE!");
            }
        }
        else
        {
            can_create = true;
            //Debug.Log("[HOLE] THERE IS NO SPIKES AT ALL! JUST CREATE!"); 
        }


        // SUCCESS! LETS CREATE A HOLE!
        if (can_create)
        {
            if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log("SUCCESSFULLY CREATING HOLE!!");
            // GameObject obj = (GameObject)Instantiate(floor_type, new Vector3(rand - hole_size / 2 - floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
            GameObject obj = objects_pool_controller.s.reposite_floor(rand - hole_size / 2 - floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n);
            obj.GetComponent<floor>().my_floor = n;
            obj.GetComponent<floor>().check_if_have_score();

            //obj = (GameObject)Instantiate(floor_type, new Vector3(rand + hole_size / 2 + floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
            obj = objects_pool_controller.s.reposite_floor(rand + hole_size / 2 + floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n);
            obj.GetComponent<floor>().my_floor = n;


            if (not_hidden == false) obj = (GameObject)Instantiate(hole_type, new Vector3(rand, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
            obj.GetComponent<hole_behaviour>().my_floor = n;

            //return obj;
            last_hole_x = rand;

            if(USER.s.FIRST_HOLE_CREATED == 0) {
                USER.s.FIRST_HOLE_CREATED = 1;
                PlayerPrefs.SetInt("first_hole_created", 1);
            }

            create_bg(n);
            return true;
        }
        else { if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log(" FffffffffffffAILED TO CREATE HOLE..."); return false;  }
    }

    bool create_just_hole(int n, float x)
    {
        //GameObject obj = (GameObject)Instantiate(floor_type, new Vector3(x - hole_size / 2 - floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
        GameObject obj = objects_pool_controller.s.reposite_floor(x - hole_size / 2 - floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n);
        obj.GetComponent<floor>().my_floor = n;
        obj.GetComponent<floor>().check_if_have_score();

        // obj = (GameObject)Instantiate(floor_type, new Vector3(x + hole_size / 2 + floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
        obj = objects_pool_controller.s.reposite_floor(x + hole_size / 2 + floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n);
        obj.GetComponent<floor>().my_floor = n;

        create_bg(n);
        return true;
    }
#endregion

    void create_wave_name(float x_pos, float y_pos, string wave_name)
    {
        GameObject my_qa_wave;
        my_qa_wave = (GameObject)Instantiate(QA_wave_name, new Vector3(0, y_pos - 0.6f, transform.position.z), transform.rotation);
        my_qa_wave.GetComponentInChildren<TextMesh>().text = wave_name;
    }
}

