using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game_controller : MonoBehaviour {

    #region ============= Variables Declaration ================
    public static game_controller s;

    // TYPES
    [HideInInspector] public GameObject floor;
    public GameObject floor_type;
    public GameObject spike_type;
    public GameObject hole_type;
    public GameObject triple_spike_type;
    public GameObject wall_type;
    public GameObject pw_icon;

    private int n_floor = 5;
    private int cur_floor = -1;

    public ball_hero[] ball;

	public GameObject single;

    public Camera camerda;


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

    float corner_limit_right = 3.2f;
    float corner_limit_left = -3.2f;

    bool last_spike_left;
    bool last_spike_right;
    bool last_hole;
    bool last_wall;
    int hole_creation_failed = 0;

    float last_hole_x;

    public wave_controller[] wave_ctrl;


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

       // print("AHHHHHHHHH CORNER LIMIT RIGHT: " + corner_limit_right);
        //Time.timeScale = 0.3f;
        last_hole = false;
        last_spike_left = false;
        last_spike_right = false;
        last_wall = false;

		// Calculate ball speed(SLOT*4)/((480+25)/CASUAL_SPEED_X)
		globals.s.CAMERA_SPEED = globals.s.FLOOR_HEIGHT / ((globals.s.LIMIT_RIGHT*2 )/ globals.s.BALL_SPEED_X);
		Debug.Log ("\n\n\n============= NEW GAME !!!!!!!!!! ===============");

        int count = 0;
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
                        create_floor(0, i);
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
                    /*case 2:
                        create_hole(i,true);
                        break;
                    case 3: create_wave_easy(i); break;
                    case 4:
                        create_hole(i,true);
                        break;
                    */
                    default:
                        // Debug.Log(" DEFAULT FIRST WAVE:");
                        wave_found = create_wave_easy(i);
                       // wave_found = create_wave_very_hard(i);
                        //create_corner_wall(i);
                        break;
                }
            }

            n_floor = i+1;

            // if (i > 0) { create_spike_wave(i, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * i); }
        }

        ball[0].Init_me();
        //wave_ctrl[0].create_new_wave(globals.s.BASE__Y + globals.s.FLOOR_HEIGHT * n_floor);

    }

    #endregion

    #region ================= GAME END ===================

    public void game_over()
    {
        //Time.timeScale = 0;

        globals.s.GAME_OVER = 1;

        Invoke("show_game_over", 1f);
    }

    void show_game_over()
    {
        globals.s.CAN_RESTART = true;


        hud_controller.si.show_game_over(cur_floor + 1);
    }

    #endregion

    #region ================ GAME LOGIC ================ 

    public void ball_up(int ball_floor)
    {
        if(ball_floor > cur_floor)
        {
           // if (ball_floor >= 1) camerda.GetComponent<Rigidbody2D>().velocity = new Vector2(0, globals.s.CAMERA_SPEED);
            cur_floor = ball_floor;
            hud_controller.si.update_floor(cur_floor);

            create_new_wave();
        }
        else if (ball_floor >=1) camerda.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }

    public void create_new_wave (){
		Debug.Log(" \n::::::::::::::::::::::: CREATING NEW FLOOR: " +n_floor);

        int rand;
        int count = 0;
        //rand = 1;
        wave_found = false;


        while(wave_found == false && count < 50)
        {
            count++;

            //PW Creation
            rand = Random.Range(0, 100);
           // rand = 100;
            if (rand <=10)
            {
                create_pw_icon(Random.Range(corner_limit_left, corner_limit_right), n_floor);
            }

            // SORT INITIAL WAVES!
            if (n_floor <= 6) {
                rand = Random.Range(1, 3);
            }

            // USER HAD SOME PROGRESS
            else if (n_floor <= 13) {
                rand = Random.Range(2, 4);
            }
            // LETS GET SERIOUS!
            else if (n_floor <= 27) {
                rand = Random.Range(1, 5);
            }

            // LETS GET SERIOUS!
            else {
                rand = Random.Range(1, 6);
            }

            //rand = 4;

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
                case 4:
                    wave_found = create_wave_very_hard(n_floor);
                    break;
                case 5:
                    wave_found = create_wave_super_hard(n_floor);
                    break;
            }

        }

        if (wave_found == false) Debug.Log("\n******* ERROR! WAVE NOT FOUND!! ********");
        n_floor++;



    }

    #endregion

    #region ============== WAVES CREATION LOGIC ==================

    //SINGLE SPIKE SOMEWHERE
    bool create_wave_easy(int n)
    {
        
        float actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;
        int rand = Random.Range(1, 100);
        int hole_chance = 30 + 10 * hole_creation_failed;
        if (hole_chance > 60) hole_chance = 60;
        Debug.Log("\n " + n + " ~~~~~~~~~~~~ TRY CREATE MEDIUM HOLE! ~~~~~~~~~~~~ | rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);


        // HOLE + SPIKE
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            bool success = create_hole(n, false, screen_w / 4 - screen_w / 8);

            if (success)
            {

                float spk_pos;
                if (last_hole_x < 0)
                {
                    spk_pos = last_hole_x + min_spk_dist + Random.Range(0.60f, 1f);
                }
                else
                {
                    spk_pos = last_hole_x - min_spk_dist - Random.Range(0.60f, 1f);
                }

                create_spike(spk_pos, actual_y, n);
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
            Debug.Log("\n " + n + " ======= CREATE WAVE EASY! ========== | rand: " + rand);

            // 1 SPK MIDDLE |___^___|
            if (rand > 0 && rand <= 30)
            {
                create_floor(0, n);
                create_spike(Random.Range(corner_limit_left, corner_limit_right), actual_y, n);
                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;
                return true;
            }
            // 1 TRIPLE SPK MIDDLE |___/\___|
            if (rand > 30 && rand <= 60)
            {
                create_floor(0, n);
                create_triple_spike(Random.Range(-screen_w / 3 + 0.5f, screen_w / 3 - 0.5f), actual_y, n);
                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // 1 SPK CORNER LEFT |^_____|
            else if (last_spike_left == false && rand > 60 && rand <= 80)
            {
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
        int hole_chance = 30 + 10 * hole_creation_failed;
        if (hole_chance > 85) hole_chance = 85;
        
        Debug.Log("\n "+ n+ " ~~~~~~~~~~~~ TRY CREATE MEDIUM HOLE! ~~~~~~~~~~~~ | rand " + rand + " HOLE CHANCE: "+ hole_chance + " N FAILED: " + hole_creation_failed);
   
        
        // FIRST, LET'S TRY TO CREATE A HOLE
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            bool success = create_hole(n);

            if (success)
            {
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
            Debug.Log("\n " + n + " ========= CREATE WAVE MEDIUM! ========== | rand " + rand);
            //rand = 50;

            // 2 SPK MIDDLE |__^_^__|
            if (rand > 0 && rand <= 20)
            {
                create_floor(0, n);
                float rand_x = Random.Range(-screen_w / 4, 0 - 1f);
                //first spike
                create_spike(rand_x, actual_y, n);
                if (rand_x <= corner_limit_left) last_spike_left = true;
                else last_spike_left = false;

                //second spike
                rand_x = Random.Range(rand_x + min_spk_dist+ 0.5f, rand_x + min_spk_dist + 1.5f);
                create_spike(rand_x, actual_y, n);
                if (rand_x <= corner_limit_right) last_spike_right = true;
                else last_spike_right = false;

                last_hole = false;
                last_wall = false;

                return true;
            }

            // 2 SPK CRONER |^______^|
            else if (!last_spike_left && !last_spike_right && rand > 20 && rand <= 30)
            {
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
            else if (!last_spike_left && rand > 30 && rand <= 37)
            {
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
            else if (!last_spike_right && rand > 37 && rand <= 44)
            {
                create_floor(0, n);
                create_spike(corner_right, actual_y, n);
                create_spike(Random.Range(-mid_area  +0.30f, mid_area - 0.30f), actual_y, n);

                last_spike_right = true;
                last_spike_left = false;
                last_wall = false;
                last_hole = false;
                return true;
            }

            // WALL CORNER + 1 SPK ||___^__|
            else if (!last_wall && rand > 44 && rand <= 68)
            {                   
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
                    create_wall_corner(n,true);
                    create_hidden_spike(rand_x, actual_y, n, true);
                }
                    

                last_spike_right = true;
                last_spike_left = true;
                last_wall = true;
                last_hole = false;
                return true;
            }

            // 1 HIDDEN SPK |____v____|
            else if (rand > 68 && rand <= 87)
            {
                create_floor(0, n);
                create_hidden_spike(Random.Range(-mid_area + 0.2f, mid_area - 0.2f), actual_y, n);

                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // 2 TRIPLE SPK MID |___/\__/\___|
            if ( rand > 87)
            {
                create_floor(0, n);
                float rand_x = Random.Range(-screen_w / 4 - 1f, 0 - 0.5f);
                //first spike
                create_spike(rand_x, actual_y, n);
                create_spike(Random.Range(rand_x + min_spk_dist + 0.35f, rand_x + min_spk_dist + 2f), actual_y, n);

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
        int hole_chance = 30 + 10 * hole_creation_failed;
        if (hole_chance > 85) hole_chance = 85;
        Debug.Log("\n " + n + " ^^^^^^^^^^^^^^^^^^ TRY CREATE HARD HOLE! ^^^^^^^^^^^^^^^^^^ | rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);


        // HOLE + 2 SPIKES 
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            //bool success = create_hole(n, false, screen_w / 4 - screen_w / 8);
            bool success = create_hole(n, false, 1.5f);
            //success = true;

            if (success) {
                last_hole = true;
                hole_creation_failed = 0;

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
            Debug.Log("\n " + n + " ========= CREATE WAVE HARD! ========== | rand " + rand);

            // CORNERS AND 1 SPK MIDDLE |^__^__^|
            if (!last_spike_right && !last_spike_left && rand > 0 && rand <= 25)
            {
                create_floor(0, n);
                //first spike
                create_spike(corner_left, actual_y, n);
                create_spike(corner_right, actual_y, n);
                create_spike(Random.Range(-mid_area, mid_area), actual_y, n);

                last_spike_right = true;
                last_spike_left = true;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // 1 HIDDEN SPK  + 1 SPK MID |__ v_^__|
            else if (rand > 25 && rand <= 50)
            {
                create_floor(0, n);
                int is_left = Random.Range(0, 2);
                float rand_x = Random.Range(-screen_w / 4, 0 - 1.20f);

                if (is_left == 1)
                {
                    //first spike
                    create_hidden_spike(rand_x, actual_y, n);
                    create_spike(Random.Range(rand_x + min_spk_dist + 0.5f, rand_x + min_spk_dist + 1.4f), actual_y, n);
                }
                else
                {
                    create_spike(rand_x, actual_y, n);
                    create_hidden_spike(Random.Range(rand_x + min_spk_dist + 0.5f, rand_x + min_spk_dist + 1.4f), actual_y, n);
                }

                last_spike_right = false;
                last_spike_left = false;
                last_hole = false;
                last_wall = false;

                return true;
            }

            // WALL MID-LEFT + SPIKE (12%) |__|____^_|
            else if (!last_wall && !last_spike_left && rand > 50 && rand <= 65)
            {
                float wall_pos = Random.Range(-screen_w / 4, 0 - 0.5f);
                float spk_pos = Random.Range(wall_pos + min_spk_dist + 0.8f, corner_right);

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
            else if (!last_wall && !last_spike_right && rand > 65 && rand <= 80)
            {
                float wall_pos = Random.Range(0 + 0.5f, screen_w / 4);
                float spk_pos = Random.Range(corner_left, wall_pos - min_spk_dist - 0.8f);

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
            if (rand > 80)
            {
                create_floor(0, n);
                //float rand_x = Random.Range(-screen_w / 4+0.2f, 0 - 1.00f);
                float dist = Random.Range(min_spk_dist/2 + 0.2f, min_spk_dist/2 + 0.5f);
                float rand_x = Random.Range(-0.6f, 0.6f);
                //first spike
                create_hidden_spike(rand_x - dist, actual_y, n);
                create_hidden_spike(rand_x + dist, actual_y, n);

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
        int hole_chance = 35 + 10 * hole_creation_failed;
        if (hole_chance > 85) hole_chance = 85;
        Debug.Log("\n " + n + " &&&&&&&&&&&&&& TRY CREATE VERY HARD HOLE! &&&&&&&&&&| rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);

        // rand = 80;

        //  HOLE WITH SPK AT ITS SIDE AND ANOTHER CONDITIONAL SPIKE |_^\/__^_|
        if (!last_wall && !last_hole && rand > 0 && rand <= hole_chance)
        {
            bool success = create_hole(n, false, 1.5f);

            if (success)
            {
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
                    Debug.Log(" _^ 1 SPIKE NEXT AT RIGHT FROM THE HOLE");
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
            Debug.Log("\n " + n + " ========== CREATE WAVE VERY HARD! ========== | rand " + rand);
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
            else if (!last_spike_right && rand >15 && rand <= 30)
            {
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
            else if (!last_wall && rand > 30 && rand <= 75)
            {
                bool there_is_manual = false;

                create_floor(0, n);
               
                float rand_x = Random.Range(-screen_w / 4, 0 - 0.8f);
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
                float rand_x2 = Random.Range(corner_right - 1.3f,corner_right);

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
            else if (rand > 75)
            {
                create_floor(0, n);
                float rand_x = Random.Range(min_spk_dist/2+0.2f, min_spk_dist/2 + 0.7f );
                float pos = Random.Range(-0.7f,0.7f);
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

    bool create_wave_super_hard(int n) {
        float actual_y = globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n;
        int rand = Random.Range(1, 100);
        //rand = 1;
        int hole_chance = 35 + 10 * hole_creation_failed;
        if (hole_chance > 85) hole_chance = 85;
        Debug.Log("\n " + n + " SSSSSSSSSSSSSS TRY CREATE SUPER HARD HOLE! SSSSSSSSSSSSS| rand " + rand + " HOLE CHANCE: " + hole_chance + " N FAILED: " + hole_creation_failed);

        // rand = 80;

        // HOLE + 2 TRIPLE SPIKES |_^__\/__^_|
        if (!last_wall && !last_hole && !last_spike_left && !last_spike_right && rand > 0 && rand <= hole_chance) {
            bool success = create_hole(n, false, 1.5f);

            if (success) {
                rand = Random.Range(1, 100);
                //rand = 68;
                // SPIKES FAR FROM HOLE
                if (rand < 65) {
                    float spk_pos = Random.Range(corner_left, last_hole_x - min_spk_dist - 0.8f);
                    //spike left
                    if (spk_pos <= corner_limit_left) last_spike_left = true;
                    else last_spike_left = false;
                    create_triple_spike(spk_pos, actual_y, n);

                    spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.8f, corner_right);
                    if (spk_pos >= corner_limit_right) last_spike_right = true;
                    else last_spike_right = false;
                    create_triple_spike(spk_pos, actual_y, n);
                }
                else // 1 HIDDEN SPIKE OR TRIPLE SPIKE AT CORNER + 1 TRIPLE SPIKE AT HOLE BORDER! 
                {
                    int left_or_right = Random.Range(0, 2);
                    if (left_or_right == 0) {
                        float spk_pos = Random.Range(corner_left+0.2f, last_hole_x - min_spk_dist - 0.5f);

                        if (spk_pos >= corner_limit_left) {
                            last_spike_left = true;
                            create_hidden_spike(spk_pos, actual_y, n);

                        }
                        else {
                            last_spike_left = false;
                            create_triple_spike(spk_pos, actual_y, n);
                        }

                        spk_pos = last_hole_x + 1.168f + 0.17f;
                        create_triple_spike(spk_pos, actual_y, n);
                        last_spike_right = false;
                    }
                    else {
                        float spk_pos = Random.Range(last_hole_x + min_spk_dist + 0.6f, corner_right);

                        if (spk_pos <= corner_limit_right) {
                            last_spike_right = true;
                            create_hidden_spike(spk_pos, actual_y, n);
                        }

                        else {
                            last_spike_right = false;
                            create_triple_spike(spk_pos, actual_y, n);
                        }

                        spk_pos = last_hole_x - 1.168f - 0.17f;
                        create_hidden_spike(spk_pos, actual_y, n);
                        last_spike_left = false;
                    }

                }
                hole_creation_failed = 0;
                last_hole = true;
                last_wall = false;
            }

            return success;
        }
        else {
            hole_creation_failed++;
            rand = Random.Range(1, 100);
            Debug.Log("\n " + n + " ========== CREATE WAVE VERY HARD! ========== | rand " + rand);
            //rand = 35;
            // rand = Random.Range(1, 25);


            // 3 TRIPLE SPIKES MID (LEFT priority) |__^_^_^__|
            if (!last_spike_left && rand > 0 && rand <= 15) {
                create_floor(0, n);
                float triple_range = min_spk_dist + Random.Range(0.5f, 0.6f);
                float spk_pos = Random.Range(corner_left, corner_limit_left + 0.35f);

                //first spike
                create_triple_spike(spk_pos, actual_y, n);

                if (spk_pos <= corner_limit_left) last_spike_left = true;
                else last_spike_left = false;

                //TRY TO HIDE THE MIDDLE SPIKE
                spk_pos += triple_range;
                rand = Random.Range(1, 100);
                if (rand < 75)
                    create_triple_spike(spk_pos, actual_y, n);
                else
                    create_triple_hidden_spike(spk_pos, actual_y, n);

                spk_pos += triple_range;
                create_triple_spike(spk_pos, actual_y, n);

                if (spk_pos >= corner_limit_right) last_spike_right = true;
                else last_spike_right = false;

                last_hole = false;
                last_wall = false;

                return true;
            }
           
            // 3 TRIPLE SPIKES MID (RIGHT priority |__^_^_^__|)
            if (!last_spike_right && rand > 15 && rand <= 30) {
                create_floor(0, n);
                float triple_range = min_spk_dist + Random.Range(0.5f, 0.6f);

                float spk_pos = Random.Range(corner_limit_right - 0.45f, corner_right);

                create_triple_spike(spk_pos, actual_y, n);
                if (spk_pos >= corner_limit_right) last_spike_right = true;
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

                if (spk_pos <= corner_limit_left) last_spike_left = true;
                else last_spike_left = false;

                last_hole = false;
                last_wall = false;

                return true;
            }

            // DOUBLE WALL CORNER + 1 spk (normal, hidden or manual_trigger)  ||__v_v__|
            else if (!last_wall && rand > 30 && rand <= 65) {
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

            // 4 SPIKES IN PAIRS |___v_v___v_v___|
            if (rand > 65 && !last_spike_left && !last_spike_right) {
                create_floor(0, n);
                float pair_range = Random.Range(2.2f, 2.35f);

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
        GameObject obj = (GameObject)Instantiate(wall_type, new Vector3(0, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n  + globals.s.SLOT / 2, 0), transform.rotation);
        wall temp_wall = obj.GetComponent<wall>();
        temp_wall.my_floor = n;
        temp_wall.corner_wall = true;
        temp_wall.spike_trigger = spk_trigger;
        temp_wall.GetComponent<BoxCollider2D>().enabled = false;

        return obj.GetComponent<wall>();
    }

    void create_spike(float x, float y, int n, bool corner_repositionable = false)
    {
        GameObject obj = (GameObject)Instantiate(spike_type, new Vector3(x, y + globals.s.SLOT/2, 0), transform.rotation);
        spike spk = obj.GetComponent<spike>();
        spk.my_floor = n;
        spk.corner_repositionable = corner_repositionable;
    }

    void create_triple_spike(float x, float y, int n)
    {
        GameObject obj = (GameObject)Instantiate(triple_spike_type, new Vector3(x, y + globals.s.SLOT / 2, 0), transform.rotation);
        obj.GetComponent<spike>().my_floor = n;
    }

    void create_hidden_spike(float x, float y, int n, bool manual_trigger = false, bool corner_repositionable = false)
    {
        GameObject obj = (GameObject)Instantiate(spike_type, new Vector3(x, y + globals.s.SLOT/2 - spike_type.transform.GetComponent<SpriteRenderer>().bounds.size.y, 0), transform.rotation);
        spike spk = obj.GetComponent<spike>();
        spk.hidden = true;
        spk.my_floor = n;
        spk.manual_trigger = manual_trigger;
        spk.my_collider.enabled = false;
        spk.corner_repositionable = corner_repositionable;
    }

    void create_triple_hidden_spike(float x, float y, int n, bool manual_trigger = false)
    {
        GameObject obj = (GameObject)Instantiate(triple_spike_type, new Vector3(x, y + globals.s.SLOT / 2 - spike_type.transform.GetComponent<SpriteRenderer>().bounds.size.y, 0), transform.rotation);
        spike spk = obj.GetComponent<spike>();
        spk.hidden = true;
        spk.my_floor = n;
        spk.manual_trigger = manual_trigger;
        spk.my_collider.enabled = false;
    }


    public GameObject create_floor(float x, int n)
    {
        GameObject obj = (GameObject)Instantiate(floor_type, new Vector3(x, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
        obj.GetComponent<floor>().my_floor = n;
        obj.GetComponentInChildren<TextMesh>().text = n.ToString();
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
                        if (can_create == false) { break; Debug.Log("NO WAY.. RANDX: " + rand + " SPIKE POS: " + spk.transform.position.x); }
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
            Debug.Log("SUCCESSFULLY CREATING HOLE!!");
            GameObject obj = (GameObject)Instantiate(floor_type, new Vector3(rand - hole_size / 2 - floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
            obj.GetComponent<floor>().my_floor = n;
            obj = (GameObject)Instantiate(floor_type, new Vector3(rand + hole_size / 2 + floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
            obj.GetComponent<floor>().my_floor = n;
            
            if (not_hidden == false) obj = (GameObject)Instantiate(hole_type, new Vector3(rand, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
            obj.GetComponent<hole_behaviour>().my_floor = n;
            //return obj;
            last_hole_x = rand;

            return true;
        }
        else { return false; Debug.Log(" FffffffffffffAILED TO CREATE HOLE..."); }
    }

    bool create_just_hole(int n, float x)
    {
        GameObject obj = (GameObject)Instantiate(floor_type, new Vector3(x - hole_size / 2 - floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
        obj.GetComponent<floor>().my_floor = n;
        obj = (GameObject)Instantiate(floor_type, new Vector3(x + hole_size / 2 + floor_type.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
        obj.GetComponent<floor>().my_floor = n;
        return true;
    }
#endregion

    void create_pw_icon(float x, int n)
    {
        Debug.Log("[GM] CREATING POWER UP!");
        Instantiate(pw_icon, new Vector3(x, 2 + globals.s.BASE_Y + globals.s.FLOOR_HEIGHT * n, 0), transform.rotation);
    }

}
