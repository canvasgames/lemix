using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ball_hero : MonoBehaviour
{
    #region ==== Variables Declaration =====

	public GameObject superJumpEffect;
	public GameObject jetpack;
    public bool is_destroyed = false;

    public GameObject my_trail;
    float target_y = 0;
    bool target_y_reached;

    [HideInInspector]
    public int my_id;
    private bool grounded = false;
    [HideInInspector]
    public int my_floor = 0;
    public bool first_ball = false;
    //private int porro = 1111;

    public Camera camerda;

    private bool son_created = false;
    [HideInInspector]
    public GameObject my_son;
    public GameObject my_alert;
    public GameObject bola;
    public GameObject heart, super, sight;
    public GameObject symbols;
    public note_trail_behavior my_note_trail;
    bool sight_active = false;
    bool heart_active = false;
    bool facing_right = false;

    public GameObject my_skin;

    public GameObject my_light;

    public float time_dif;
    //public GameObject

    // Use this for initialization
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 vetor;

    public GameObject explosion, spike_explosion;

    bool hitted_wall = false;

    public float cam_fall_dist = 0;

    #endregion

    #region ====== Init ========

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        my_alert.SetActive(false);
    }

    // START THE DANCE
    void Start()
    {
        my_id = globals.s.BALL_ID; globals.s.BALL_ID++;
        //Debug.Log("BALL CREATED! TIME: " + Time.time);
        time_dif = Time.time;


        //camerda = FindObjectOfType<Camera>;
        //Debug.Log(" SPEED X: " + globals.s.FLOOR_HEIGHT);


        //if(first_ball == true) grounded = true;
        son_created = false;
        //Debug.Log ("INIT NEW BALL !!! MY X SPEED: " + rb.velocity.x);

        // INITIALIZE SKIN AND MUSIC HERE!!!
        create_note_trail();
        changeSkinChar();

    }

    public void changeSkinChar()
    {
        if (globals.s.ACTUAL_CHAR == "pop")
        {
            my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/PopAnimator") as RuntimeAnimatorController;
        }
        else if (globals.s.ACTUAL_CHAR == "rock")
        {
            my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/RockAnimator") as RuntimeAnimatorController;
        }
        else if (globals.s.ACTUAL_CHAR == "eletronic")
        {
            my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/EletronicHero") as RuntimeAnimatorController;
        }
		else if (globals.s.ACTUAL_CHAR == "popGaga")
		{
			my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/PopGagaAnimator") as RuntimeAnimatorController;
		}
		else if (globals.s.ACTUAL_CHAR == "reggae")
		{
			my_skin.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Sprites/Animations/ReggaeAnimator") as RuntimeAnimatorController;
		}
    }
    public void Init_first_ball()
    {
        if (first_ball == true)
        {
            if (transform.position.x < 0)
            {
                rb.velocity = new Vector2(globals.s.BALL_SPEED_X, 0);
            }
            else
            {
                rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, 0);
            }
            init_my_skin();
        } 
    }
    
    public void init_my_skin() {
        if (transform.position.x < 0 ) {
            my_skin.transform.localScale = new Vector2(-3, my_skin.transform.localScale.y);
        }
        else if (transform.position.x > 0) {
            my_skin.transform.localScale = new Vector2(3, my_skin.transform.localScale.y);
        }
    }
    
    #endregion

    void create_note_trail() {
        //note_trail_behavior obj = (note_trail_behavior)Instantiate(my_note_trail, 
            //new Vector3(transform.position.x, transform.position.y + Random.Range(-0.2f, 0.2f)), transform.rotation);

		objects_pool_controller.s.reposite_note_trail (transform.position.x, transform.position.y + Random.Range (-0.2f, 0.2f));
			

        if(!is_destroyed) Invoke("create_note_trail",0.07f);
    }

    #region ======= UPDATE ==========
    void Update()
    {
		if (globals.s.ALERT_BALL == true && son_created == false && game_controller.s.alertDebug == true) {
            globals.s.ALERT_BALL = false;
			//Debug.Log ("!!!!!!!!!!!!!!!!!!! SHOW ALERT NOW !! ");
			Invoke("show_alert", 0.1f);
            //show_alert();
        }

        if (globals.s.GAME_STARTED == true)
        {
            if ((Input.GetMouseButton(0) || Input.GetKey("space")) && globals.s.GAME_STARTED == true)
            {
                jump();
            }
            else if (Input.GetMouseButtonUp(0) && hud_controller.si.HUD_BUTTON_CLICKED == false)
            {
                jump();
            }
        }

        symbols_PW_activate();
            
        // SET X SPEED TO MAX EVERY FRAME
        if (!globals.s.PW_SUPER_JUMP && !globals.s.REVIVING)
        {
            if (rb.velocity.x > 0)
                rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);
            else if (rb.velocity.x < 0)
                rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
        }
        //Vector3 abc = new Vector3(0, 0, 90);
        //if (rb.velocity.y != 0)
         //  my_trail.transform.rotation = new Quaternion(0, 0, 110, 0);
        //else
        //   my_trail.transform.rotation = new Quaternion(0, 0, 0, 0);


    }

    void show_alert() {
        Debug.Log("SHOWING ALERT!! MY FLOOR: " + my_floor);
        my_alert.SetActive(true);
        my_alert.transform.localScale = new Vector2(2.3f, 0);
        my_alert.transform.DOScaleY(2.3f, 0.12f);
        //sound_controller.s.play_alert();
    }

    public void activate_pos_revive()
    {
        if (transform.position.x > 0)
            rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
        else 
            rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);
    }
   
    void FixedUpdate()
    {
        //Debug.Log (" MY X SPEED: " + rb.velocity.x);
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

        if (globals.s.PW_SUPER_JUMP == true && target_y_reached == false && target_y > 0) {
            // main_camera.s.PW_super_jump(transform.position.y);
            if (transform.position.y >= target_y) {
                hud_controller.si.update_floor(my_floor+4);
                stop_go_up_PW();
            }
        }

        // falling case
        if (rb.velocity.y < -0.02f) grounded = false; //else grounded = true;

       
        #region ================ Ball Up ====================

        if (son_created == false && ((transform.position.x <= globals.s.LIMIT_LEFT + globals.s.BALL_R + 0.3f && rb.velocity.x < 0) ||
                                     (transform.position.x >= globals.s.LIMIT_RIGHT - globals.s.BALL_R - 0.3f && rb.velocity.x > 0))) {
            // my_light.SetActive(false);
            // Destroy(my_light);
            //Debug.Log ("END REACHED!!!!!!! MY POS: " + transform.position.x + " LEFT: " + globals.s.LIMIT_LEFT + " RIGHT: "  + globals.s.LIMIT_RIGHT);
            son_created = true;
            float x_new_pos = 0f;

            // define the relative new pos
            if (transform.position.x < 0) {
                x_new_pos = globals.s.LIMIT_LEFT - Mathf.Abs(globals.s.LIMIT_LEFT - transform.position.x);
                //x_new_pos = 0;
            }
            else {
                x_new_pos = globals.s.LIMIT_RIGHT + Mathf.Abs(globals.s.LIMIT_RIGHT - transform.position.x);
            }

            //Debug.Log("BALL DESTROYED TIME: " + Time.time + " .. TIME DIF: " + (Time.time - time_dif));


            //instantiating my son at the other side of the screen
            my_son = (GameObject)Instantiate(bola,
                                              new Vector3(x_new_pos,
                                                            // (transform.position.y + globals.s.FLOOR_HEIGHT+1), 
                                                            transform.position.y + globals.s.FLOOR_HEIGHT,
                                                              0),
                                              transform.rotation);

            PW_controller.s.add_ball(my_son.GetComponent<ball_hero>());

            my_son.GetComponent<Rigidbody2D>().velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            //Debug.Log("MMMMMM MY SON VY " + my_son.GetComponent<Rigidbody2D>().velocity.y + " | MY VY: " + rb.velocity.y);
            my_son.GetComponent<ball_hero>().my_floor = my_floor + 1;
            my_son.GetComponent<ball_hero>().grounded = grounded;


            globals.s.BALL_Y = my_son.transform.position.y;
            globals.s.BALL_X = my_son.transform.position.x;
            globals.s.CUR_BALL_SPEED = my_son.GetComponent<Rigidbody2D>().velocity.x;

            my_son.GetComponent<ball_hero>().init_my_skin();
            if (grounded == false) { 
                //my_son.GetComponent<ball_hero>().my_skin.GetComponent<Animator>().Play("Jumping", 0,
                //my_skin.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
                my_skin.GetComponent<Animator>().SetBool("Jumping", true);
                my_son.GetComponent<ball_hero>().my_skin.GetComponent<Animator>().SetBool("Jumping", true);
                //Debug.Log("aaaaaaanimator: " + my_skin.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
            }


            // CALL GAME CONTROLLER
            game_controller.s.ball_up(my_floor);
            // CALL MAIN CAMERA
            float pos = ((globals.s.BASE_Y + (my_floor+1) * globals.s.FLOOR_HEIGHT) ) + 1f;
            //Debug.Log("================ CALCULATED POS: " + pos + " MYSON POS: " + globals.s.BALL_Y + "my pos: " + transform.position.y);
            if (my_floor >= 1) main_camera.s.on_ball_up(pos);

            if(hitted_wall) main_camera.s.hitted_on_wall = false;

            // MAKE WALLS POSITION THEMSELVES
            wall[] paredez = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
            foreach(wall p in paredez){
                p.place_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 2);
            }

			if(USER.s.BEST_SCORE <= 5){
//				floor[] floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
//				foreach(floor p in floors){
//					p.reposite_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 2);
//				}

				hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
				foreach(hole_behaviour p in holes){
					p.reposite_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 3);
				}

				spike[] spks = GameObject.FindObjectsOfType(typeof(spike)) as spike[];
				foreach(spike p in spks){
					p.reposite_me_for_FTU(-my_son.transform.position.x, my_floor + 3);
				}
			}

			/*
			hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
			foreach(hole_behaviour p in holes){
				p.place_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 2);
			}*/

            //my_son = (GameObject)Instantiate (ball_hero, new Vector3 (0, 0, 0), transform.rotation);
            //Debug.Log("------------ NEW BALL CREATED! MY ID: " +my_id +" time: " + Time.time + " | pos y: " +my_son.transform.position.y + " CAMERA Y: " + main_camera.s.transform.position.y);

        }
        #endregion

        else if (son_created == true && (transform.position.x < globals.s.LIMIT_LEFT - globals.s.BALL_D ||
                                     transform.position.x > globals.s.LIMIT_RIGHT + globals.s.BALL_D))
        {
           // Debug.Log("Destroy me !!!! my pos:" + transform.position.x);
            Destroy(gameObject);
            //my_light.SetActive(false);
            
        }

        if (my_id == globals.s.BALL_ID - 1) {
            globals.s.BALL_Y = transform.position.y;
            globals.s.BALL_X = transform.position.x;
            globals.s.CUR_BALL_SPEED = rb.velocity.x;
            globals.s.BALL_GROUNDED = grounded;
            //Debug.Log("XY UPDATED | MY ID: " + my_id + " time: " + Time.time + " MY FLOOR " + my_floor + " CUR BALL SPEED: " + globals.s.CUR_BALL_SPEED);
            //globals.s.BALL_FLOOR = my_floor;
        }
    }


    #endregion

    void jump()
    {
        //Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        //GetComponent<EdgeCollider2D>().enabled = false;
        if (grounded == true)
        {
            // my_trail.transform.localRotation = new Quaternion(0, 0, 110, 0);
            my_trail.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.01f, RotateMode.Fast);
            sound_controller.s.PlayJump();
            grounded = false;
            //rb.AddForce (new Vector2 (0, y_jump));
            rb.velocity = new Vector2(rb.velocity.x, globals.s.BALL_SPEED_Y);

           // my_skin.GetComponent<Animator>().Play("Jumping");
            my_skin.GetComponent<Animator>().SetBool("Jumping", true);
        }
        //else Debug.Log("ÇÇÇÇÇÇÇÇÇÇÇÇÇÇÇ CANT JUMP! I AM NOT GROUNDED");
    }

    void Land() {
        my_trail.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.01f, RotateMode.Fast);
    }


#region ========= COLLISIONS ================


	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log ("kkkkkkkkkkkkkkkkkCOLLISION IS HAPPENING!! ");
		if (coll.gameObject.CompareTag ("PW")) {
			
			PW_Collect temp = coll.gameObject.GetComponent<PW_Collect> ();
			pw_do_something (temp);
			sound_controller.s.play_collect_pw ();
		} 
		else if (coll.gameObject.CompareTag ("Spike")) {
			if (globals.s.PW_SUPER_JUMP == false && !QA.s.INVENCIBLE) {
				if (globals.s.PW_INVENCIBLE == false) {
					destroy_me (coll.gameObject.GetComponent<spike> ().wave_name);
					//coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
				} else {
					// Destroy(coll.gameObject);
					Instantiate (spike_explosion, new Vector3 (coll.transform.position.x, coll.transform.position.y, coll.transform.position.z), transform.rotation);
					PW_controller.s.invencible_end ();
					coll.gameObject.transform.position = new Vector3 (coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
				}
			}
		}
		else if (coll.gameObject.CompareTag("Wall"))
		{
			rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
			my_skin.transform.localScale = new Vector2(-my_skin.transform.localScale.x, my_skin.transform.localScale.y);

			if (transform.position.y < main_camera.s.transform.position.y - 10f) {
				hitted_wall = true;
				main_camera.s.hitted_on_wall = true;
			}   
		}

		else if (coll.gameObject.CompareTag("Note")) {
			USER.s.NOTES += 1;
			hud_controller.si.display_notes(USER.s.NOTES);
			coll.transform.position = new Vector2 (-9909,-9999);
			coll.GetComponent <note_behaviour> ().active = false;
			coll.GetComponent <note_behaviour> ().state = 0;
			//Destroy(coll.gameObject);
			//Debug.Log("COLLECTING NOTE !!!!!!! ");
			sound_controller.s.special_event();
		}

		else if (coll.gameObject.CompareTag("HoleFalling")) {
			//if (QA.s.TRACE_PROFUNDITY >= 3) Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
			if (QA.s.TRACE_PROFUNDITY >= 2) Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
			//coll.transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			//coll.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
			//grounded = false;
			//coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100f);
			if (transform.position.y < main_camera.s.transform.position.y + cam_fall_dist) { 
				main_camera.s.OnBallFalling();
				//coll.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

			}

		}


		
	}

	void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("xxxxxxxxxxxxxxxxxxxxx COLLIDING WITH SOMETHING!");


        if (coll.gameObject.CompareTag("Floor")) {
            
            if (coll.transform.position.y + coll.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2 <= transform.position.y - globals.s.BALL_R + 1f) {
                //rb.AddForce (new Vector2 (0, 0));
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //my_skin.GetComponent<Animator>().Play("Running");
                my_skin.GetComponent<Animator>().SetBool("Jumping", false);
                //transform.position = new Vector2(transform.position.x, coll.transform.position.y + coll.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2 + globals.s.BALL_R);
                my_floor = coll.gameObject.GetComponent<floor>().my_floor;
                //Debug.Log(my_id + " KKKKKKKKKKKKKKKKKK KOLLISION! MY NEW FLOOR: " + my_floor + " I AM GROUNDED ");

                if (globals.s.PW_SUPER_JUMP) {
                    pw_super_end_for_real();
                }

                grounded = true;
                Land();

                coll.gameObject.GetComponent<floor>().try_to_display_best_score();
            }
            else { Debug.Log("\n\n" + my_id + " ***************ERROR! THIS SHOULD NEVER HAPPEN ***************\n\n"); }
        }

//        else if (coll.gameObject.CompareTag("Spike")) {
//            if (globals.s.PW_SUPER_JUMP == false && !QA.s.INVENCIBLE)
//            {
//                if (globals.s.PW_INVENCIBLE == false)
//                {
//                    destroy_me(coll.gameObject.GetComponent<spike>().wave_name);
//                    //coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
//                }
//                else
//                {
//                    // Destroy(coll.gameObject);
//                    Instantiate(spike_explosion, new Vector3(coll.transform.position.x, coll.transform.position.y, coll.transform.position.z), transform.rotation);
//                    PW_controller.s.invencible_end();
//                    coll.gameObject.transform.position = new Vector3(coll.gameObject.transform.position.x + 50, coll.gameObject.transform.position.y, coll.gameObject.transform.position.z);
//                }
//            }
//            else
//            {
//                Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
//                GetComponent<SpriteRenderer>().color = Color.blue;
//                Invoke("back_to_normal_color", 0.2f);
//
//            }
//        }
//

        /*else if (coll.gameObject.CompareTag("Hole")) {
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
            my_skin.GetComponent<Animator>().Play("Jumping");
        }*/

        
//        else if (coll.gameObject.CompareTag("Wall"))
//        {
//            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
//            my_skin.transform.localScale = new Vector2(-my_skin.transform.localScale.x, my_skin.transform.localScale.y);
//
//            if (transform.position.y < main_camera.s.transform.position.y - 10f) {
//                hitted_wall = true;
//                main_camera.s.hitted_on_wall = true;
//            }   
//        }
		
       /* else if (coll.gameObject.CompareTag("PW"))
        {
            PW_Collect temp = coll.gameObject.GetComponent<PW_Collect>();
            pw_do_something(temp);
            sound_controller.s.play_collect_pw();
			Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }*/
        else if (coll.gameObject.CompareTag("Revive"))
        {
            globals.s.CAN_REVIVE = true;
            Destroy(coll.gameObject);
        }
        if (globals.s.PW_SUPER_JUMP == true)
        {
            if (coll.gameObject.CompareTag("PW_Trigger"))
            {
                if (coll.gameObject.GetComponent<floor_pw_collider>() != null)
                    coll.gameObject.GetComponent<floor_pw_collider>().unactive_sprite_daddy();
            }
        }

    }

    #endregion

    void destroy_me(string killer_wave_name)
    {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];

        foreach (ball_hero b in bolas) {
            //Destroy(b.gameObject);
            b.gameObject.SetActive(false);
            b.is_destroyed = true;

        }

        bool with_high_score = false;

        if(my_floor > USER.s.BEST_SCORE)
        {
            with_high_score = true;
            floor[] chaozis = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
            int i;
            for (i = 0; i < chaozis.Length; i++)
            {
                chaozis[i].create_score_game_over(my_floor, 1);
            }
        }
        else if(my_floor > USER.s.DAY_SCORE)
        {
            with_high_score = true;
            floor[] chaozis = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
            int i;
            for (i = 0; i < chaozis.Length; i++)
            {
                chaozis[i].create_score_game_over(my_floor,2);
            }

        }

        sound_controller.s.PlayExplosion();
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        game_controller.s.game_over(killer_wave_name, bolas, with_high_score);


    }

    public void send_actual_balls()
    {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];

        game_controller.s.store_unactive_balls(bolas);

        foreach (ball_hero b in bolas)
        {
            //Destroy(b.gameObject);
            b.gameObject.SetActive(false);
        }
    }

    void pw_do_something(PW_Collect temp)
    {
        
        temp.collect();
        if(globals.s.PW_INVENCIBLE == true)
        {
            PW_controller.s.invencible_end();
        }
        else if(globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            PW_controller.s.sight_end();
        }

        Debug.Log("Tipodo PW q peguei " + temp.pw_type);
        if (temp.pw_type == (int) PW_Types.Invencible)
        {
            PW_controller.s.invencible_start();
        }
        else if (temp.pw_type == (int)PW_Types.Super)
        {
            go_up_pw_start();
        }
        else if((temp.pw_type == (int)PW_Types.Sight))
        {
            PW_controller.s.PW_sight_start();
        }
    }

    #region POWER UP -> GO UP
    void go_up_pw_start()
    {
        symbols.transform.GetComponent<SpriteRenderer>().DOFade(0, 0);
		super.SetActive(true);
		jetpack.SetActive(true);
        my_alert.SetActive(false);
        rb.velocity = new Vector2(0, 0);
       // my_skin.GetComponent<Animator>().Play("Jumping");
        my_skin.GetComponent<Animator>().SetBool("Jumping", true);

        rb.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        rb.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        //construct floors
        int i;
        int temp = my_floor;
        globals.s.PW_SUPER_JUMP = true;

        for (i = my_floor + 1; i <= temp + 5; i++) {
           game_controller.s.create_new_wave();
        }

        //activate squares of collisions
        activate_particles_floor();
        //main_camera.s.init_PW_super_jump(transform.position.y + 5 * globals.s.FLOOR_HEIGHT + 2f, 5 * (globals.s.FLOOR_HEIGHT / 20) + 0.5f);
        float pos = ((globals.s.BASE_Y + ((my_floor+1) * globals.s.FLOOR_HEIGHT) +  (5* globals.s.FLOOR_HEIGHT) + globals.s.FLOOR_HEIGHT / 2 ));
        main_camera.s.init_PW_super_jump( pos,  (pos-transform.position.y)/20  + 0.5f);

        Invoke("go_up_PW", 0.2f);
    }

    void go_up_PW() {
        
        //globals.s.PW_SUPER_JUMP = true;
        desactivate_pws_super();
		superJumpEffect.SetActive (true);

        int ball_speed = 20;
        target_y = (globals.s.BASE_Y + ((my_floor) * globals.s.FLOOR_HEIGHT) +  (5* globals.s.FLOOR_HEIGHT) + globals.s.FLOOR_HEIGHT / 2 - 0.6f );
        target_y_reached = false;

        rb.velocity = new Vector2(0, ball_speed);
        float dist = (target_y - transform.position.y);
        Debug.Log("[GO UP PW] Dist: " + dist + " OLD Dist: " + (5*globals.s.FLOOR_HEIGHT) + " dist/speed: " + (dist / ball_speed) + " | OLD dist/speed: " + ((5 *globals.s.FLOOR_HEIGHT) / ball_speed));
        Debug.Log("[GO UP PW] MY POS: " + transform.position.y + " BASE Y+FLOOR "+ (globals.s.BASE_Y + my_floor * globals.s.FLOOR_HEIGHT) + " BASE Y: " + globals.s.BASE_Y + " MY FLOOR: " + my_floor);
        //Invoke("stop_go_up_PW", ( dist / ball_speed));
        //Invoke("stop_go_up_PW", ((globals.s.BASE_Y + (my_floor * globals.s.FLOOR_HEIGHT) + 5* globals.s.FLOOR_HEIGHT + (globals.s.FLOOR_HEIGHT/2) ) - transform.position.y) / ball_speed);

        //transform.DOMoveY(target_y, 1.1f).SetEase(Ease.Linear).OnComplete(()=> stop_go_up_PW());

    }

    void stop_go_up_PW()
    {
        Debug.Log("[GOUPPW] FINISHED GOING UP ! MY Y: " + transform.position.y);
        rb.velocity = new Vector2(0.3f, globals.s.BALL_SPEED_Y/2);
        rb.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        rb.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        target_y = 0;

        target_y_reached = true; // bla

        unactivate_particles_floor();
        
        Invoke("create_floor", 0.2f);
    }
	

    void create_floor()
    {
		jetpack.SetActive(false);
        my_floor += 5;
        appear_floors();
        GameObject floor = game_controller.s.create_floor(12, my_floor);
        destroy_spikes();
        floor.transform.DOMoveX(0, 0.3f);//.OnComplete(pw_super_end);

		superJumpEffect.SetActive (false);

    }

   void pw_super_end_for_real() {
        globals.s.PW_SUPER_JUMP = false;
        squares_desappear();
        rb.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        main_camera.s.pw_super_jump_end();

        if (transform.position.x > 0)
            rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
        else
            rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);

        super.SetActive(false);

        Invoke("unactivate_squares", 0.3f);


    }

    void activate_particles_floor()
    {
        
        int i=0;
        floor[] floors = null;
       floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
        for (i = 0; i < floors.Length; i++)
          {
            floors[i].activate_colider_super_pw(my_floor);
          }

        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];

        for (i = 0; i < holes.Length; i++)
        {
            holes[i].activate_squares();
        }

        wall[] walls = GameObject.FindObjectsOfType(typeof(wall)) as wall[];

        for (i = 0; i < walls.Length; i++)
        {
            walls[i].destroy_me_PW_super();
        }

    }

    void appear_floors()
    {
        floor[] floors = null;
        int i;
        floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
        for (i = 0; i < floors.Length; i++)
        {
            floors[i].reaper_post_PW_super(my_floor);
        }
    }

    void unactivate_particles_floor()
    {
         //destroy_spikes();
         int i;
         floor[] floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
          for (i = 0; i < floors.Length; i++)
          {
              floors[i].unactivate_colider_super_pw();
             }

          hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];

          for (i = 0; i < holes.Length; i++)
          {
              holes[i].destroy_pw_super_under_floors(transform.position.y);
          }

    }

    //Called by invoke
    void unactivate_squares()
    {
        squares_desappear();
    }
    void squares_desappear()
    {
        int i;

        floor_square_pw_destruct[] squares = GameObject.FindObjectsOfType(typeof(floor_square_pw_destruct)) as floor_square_pw_destruct[];
        for (i = 0; i < squares.Length; i++)
        {
            squares[i].scale_down_to_dessapear();
        }

       objects_pool_controller.s.clear_squares_floor_particle();
    }


    void destroy_spikes()
    {
        int i;
        spike[] spikes = GameObject.FindObjectsOfType(typeof(spike)) as spike[];
        for (i = 0; i < spikes.Length; i++)
        {
            spikes[i].destroy_throwed_spikes(transform.position.y);
        }
    }


    void desactivate_pws_super()
    {
        int i;
        PW_Collect[] pws = GameObject.FindObjectsOfType(typeof(PW_Collect)) as PW_Collect[];
        for (i = 0; i < pws.Length; i++)
        {
            pws[i].destroy_by_floor_PW_Super(my_floor + 6);
        }
    }
    #endregion

    #region POWER UP -> SYMBOLS PW
    void symbols_PW_activate()
    {
        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true && sight_active == false)
        {
            sight_start();
        }
        else if ((globals.s.PW_SIGHT_BEYOND_SIGHT == false && sight_active == true))
        {
            sight_end();
        }

        if (globals.s.PW_INVENCIBLE == true && heart_active == false)
        {
            heart_start();
        }
        else if (globals.s.PW_INVENCIBLE == false && heart_active == true)
        {
            heart_end();
        }
    }
    #endregion

    #region POWER UP -> SIGHT BEYOND SIGHT 
    public void sight_start()
    {
        symbols.transform.GetComponent<SpriteRenderer>().DOFade(0, 0);
        sight_active = true;
        sight.SetActive(true);
        
    }

    void sight_end()
    {
        sight_active = false;
        sight.SetActive(false);

        
    }
    #endregion

    #region POWER UP -> LIFE
    void heart_start()
    {
        symbols.transform.GetComponent<SpriteRenderer>().DOFade(0, 0);
        heart_active = true;
        heart.SetActive(true);
    }


    void heart_end()
    {
        heart_active = false;
        heart.SetActive(false);
    }

    #endregion

    public void pw_ending_fade_symbol_mask(float alpha)
    {
        if (symbols != null)
        {
            symbols.transform.GetComponent<SpriteRenderer>().DOFade(alpha, 0);
        }

    }


    public void set_symbols_alpha(float alpha)
    {
        if(symbols != null)
        {
            symbols.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
        }
        
    }

    public void set_symbols_active(bool active)
    {
        symbols.SetActive(active);
    }
    #region =========== DEBUG ================
    void back_to_normal_color() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    #endregion

}
