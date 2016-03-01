using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ball_hero : MonoBehaviour
{
    #region Variables Declaration
    float target_y;
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
    public GameObject bola;
    public GameObject heart, super, sight;
    public GameObject symbols;
    bool sight_active = false;
    bool heart_active = false;

    public GameObject my_light;

    //public GameObject

    // Use this for initialization
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 vetor;

    public GameObject explosion;

    bool hitted_wall = false;

    public float cam_fall_dist = 0;

    #endregion

    #region Init

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // START THE DANCE
    void Start()
    {
        my_id = globals.s.BALL_ID; globals.s.BALL_ID++;
        

        //camerda = FindObjectOfType<Camera>;
        //Debug.Log(" SPEED X: " + globals.s.FLOOR_HEIGHT);


        //if(first_ball == true) grounded = true;
        son_created = false;
        //Debug.Log ("INIT NEW BALL !!! MY X SPEED: " + rb.velocity.x);
    }

    public void Init_me()
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
        }
    }

    #endregion

    void Update()
    {

        if ((Input.GetMouseButton(0) || Input.GetKey("space")) && globals.s.GAME_STARTED == true)
        {
            jump(); 
        }
        else if(Input.GetMouseButtonUp(0) && hud_controller.si.HUD_BUTTON_CLICKED == false)
        {
            jump();
        }
        symbols_PW_activate();
            
        // SET X SPEED TO MAX EVERY FRAME
        if (!globals.s.PW_SUPER_JUMP) {
            if (rb.velocity.x > 0) rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);
            else if (rb.velocity.x < 0) rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
        }

    }

    void FixedUpdate()
    {
        //Debug.Log (" MY X SPEED: " + rb.velocity.x);
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

        //

        if (globals.s.PW_SUPER_JUMP == true && target_y_reached == false) {
            // main_camera.s.PW_super_jump(transform.position.y);
            if (transform.position.y >= target_y) { 
                stop_go_up_PW();
            }
        }

        // falling case
        if (rb.velocity.y < -0.02f) grounded = false; //else grounded = true;

        if (my_id == globals.s.BALL_ID - 1)
        {
            globals.s.BALL_Y = transform.position.y;
            globals.s.BALL_X = transform.position.x;
            globals.s.BALL_GROUNDED = grounded;
            //Debug.Log("XY UPDATED | MY ID: " + my_id  + " time: " + Time.time);
            //globals.s.BALL_FLOOR = my_floor;
        }
        #region ================ Ball Up ====================
        if (son_created == false && ((transform.position.x <= globals.s.LIMIT_LEFT + globals.s.BALL_R + 0.3f && rb.velocity.x < 0) ||
                                     (transform.position.x >= globals.s.LIMIT_RIGHT - globals.s.BALL_R - 0.3f && rb.velocity.x > 0)))
        {
            // my_light.SetActive(false);
           // Destroy(my_light);
            //Debug.Log ("END REACHED!!!!!!! MY POS: " + transform.position.x + " LEFT: " + globals.s.LIMIT_LEFT + " RIGHT: "  + globals.s.LIMIT_RIGHT);
            son_created = true;
            float x_new_pos = 0f;

            // define the relative new pos
            if (transform.position.x < 0)
            {
                x_new_pos = globals.s.LIMIT_LEFT - Mathf.Abs(globals.s.LIMIT_LEFT - transform.position.x);
                //x_new_pos = 0;
            }
            else
            {
                x_new_pos = globals.s.LIMIT_RIGHT + Mathf.Abs(globals.s.LIMIT_RIGHT - transform.position.x);
            }

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

            // CALL GAME CONTROLLER
            game_controller.s.ball_up(my_floor);
            if (my_floor >= 1) main_camera.s.on_ball_up();

            if(hitted_wall) main_camera.s.hitted_on_wall = false;

            // MAKE WALLS POSITION THEMSELVES
            wall[] paredez = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
            foreach(wall p in paredez){
                p.place_me_at_the_other_corner(-my_son.transform.position.x, my_floor + 2);
            }

            //my_son = (GameObject)Instantiate (ball_hero, new Vector3 (0, 0, 0), transform.rotation);
            //Debug.Log("------------ NEW BALL CREATED! MY ID: " +my_id +" time: " + Time.time);

        }
        #endregion

        else if (son_created == true && (transform.position.x < globals.s.LIMIT_LEFT - globals.s.BALL_D ||
                                     transform.position.x > globals.s.LIMIT_RIGHT + globals.s.BALL_D))
        {
           // Debug.Log("Destroy me !!!! my pos:" + transform.position.x);
            Destroy(gameObject);
            //my_light.SetActive(false);
            
        }
    }

    void jump()
    {
        //Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        //GetComponent<EdgeCollider2D>().enabled = false;
        if (grounded == true)
        {
            grounded = false;
            //rb.AddForce (new Vector2 (0, y_jump));
            rb.velocity = new Vector2(rb.velocity.x, globals.s.BALL_SPEED_Y);
        }
        //else Debug.Log("ÇÇÇÇÇÇÇÇÇÇÇÇÇÇÇ CANT JUMP! I AM NOT GROUNDED");
    }


#region ========= COLLISIONS ================

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("xxxxxxxxxxxxxxxxxxxxx COLLIDING WITH SOMETHING!");


        if (coll.gameObject.CompareTag("Floor")) {

            if (coll.transform.position.y + coll.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2 <= transform.position.y - globals.s.BALL_R + 1f) {
                //rb.AddForce (new Vector2 (0, 0));
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //transform.position = new Vector2(transform.position.x, coll.transform.position.y + coll.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2 + globals.s.BALL_R);
                my_floor = coll.gameObject.GetComponent<floor>().my_floor;
                //Debug.Log(my_id + " KKKKKKKKKKKKKKKKKK KOLLISION! MY NEW FLOOR: " + my_floor + " I AM GROUNDED ");

                if (globals.s.PW_SUPER_JUMP) {
                    pw_super_end_for_real();
                }

                grounded = true;

                coll.gameObject.GetComponent<floor>().try_to_display_best_score();
            }
            else { Debug.Log("\n\n" + my_id + " ***************ERROR! THIS SHOULD NEVER HAPPEN ***************\n\n"); }
        }

        else if (coll.gameObject.CompareTag("Spike")) {
            if (globals.s.PW_SUPER_JUMP == false && !QA.s.INVENCIBLE) {
                if (globals.s.PW_INVENCIBLE == false) {
                    destroy_me(coll.gameObject.GetComponent<spike>().wave_name);
                }
                else {
                   // Destroy(coll.gameObject);
                    PW_controller.s.invencible_end();
                }
            }
            else {
                Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
                GetComponent<SpriteRenderer>().color = Color.blue;
                Invoke("back_to_normal_color", 0.2f);

            }
        }

        else if (coll.gameObject.CompareTag("HoleFalling")) {
            //if (QA.s.TRACE_PROFUNDITY >= 3) Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
            Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
            //coll.transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //coll.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            //grounded = false;
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100f);
            if (transform.position.y < main_camera.s.transform.position.y + cam_fall_dist) { 
                main_camera.s.OnBallFalling();
                //coll.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            }
            
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }

        else if (coll.gameObject.CompareTag("Hole")) {
            Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
        }


        else if (coll.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            if (transform.position.y < main_camera.s.transform.position.y - 10f) {
                hitted_wall = true;
                main_camera.s.hitted_on_wall = true;
            }   
        }
		
        else if (coll.gameObject.CompareTag("PW"))
        {
            PW_Collect temp = coll.gameObject.GetComponent<PW_Collect>();
            pw_do_something(temp);
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
    void OnCollisionStay2D(Collision2D coll)
    {

    }
    
    #endregion

    void destroy_me(string killer_wave_name)
    {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];

        foreach (ball_hero b in bolas) {
            Destroy(b.gameObject);
        }


        if(my_floor > hud_controller.si.BEST_SCORE)
        {
            floor[] chaozis = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
            int i;
            for (i = 0; i < chaozis.Length; i++)
            {
                chaozis[i].create_score_game_over(my_floor, 1);
            }
        }
        else if(my_floor > hud_controller.si.DAY_SCORE)
        {

            floor[] chaozis = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
            int i;
            for (i = 0; i < chaozis.Length; i++)
            {
                chaozis[i].create_score_game_over(my_floor,2);
            }

        }
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        game_controller.s.game_over(killer_wave_name);


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
        super.SetActive(true);
        rb.velocity = new Vector2(0, 0);

        rb.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        rb.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

        //construct floors
        int i;
        int temp = my_floor;
        globals.s.PW_SUPER_JUMP = true;

        for (i = my_floor + 1; i <= temp + 5; i++)
        {
           game_controller.s.create_new_wave();
        }

        //activate squares of collisions
        activate_particles_floor();
        //main_camera.s.init_PW_super_jump(transform.position.y + 5 * globals.s.FLOOR_HEIGHT + 2f, 5 * (globals.s.FLOOR_HEIGHT / 20) + 0.5f);
        float pos = ((globals.s.BASE_Y + ((my_floor+1) * globals.s.FLOOR_HEIGHT) +  (5* globals.s.FLOOR_HEIGHT) + globals.s.FLOOR_HEIGHT / 2 ));
        main_camera.s.init_PW_super_jump( pos,  (pos-transform.position.y)/20  + 0.5f);

        Invoke("go_up_PW", 0.1f);
    }

    void go_up_PW() {
        
        //globals.s.PW_SUPER_JUMP = true;
        desactivate_pws_super();
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

        target_y_reached = true;

        unactivate_particles_floor();
        
        Invoke("create_floor", 0.2f);
    }
	

    void create_floor()
    {
        my_floor += 5;
        appear_floors();
        GameObject floor = game_controller.s.create_floor(12, my_floor);
        destroy_spikes();
        floor.transform.DOMoveX(0, 0.3f);//.OnComplete(pw_super_end);
        

    }
   void pw_super_end_for_real() {
        globals.s.PW_SUPER_JUMP = false;
        squares_desappear();
        rb.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        main_camera.s.pw_super_jump_end();
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
        int i;
        /*floor[] floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
        for (i = 0; i < floors.Length; i++)
        {
            floors[i].unactivate_squares();
        }*/

        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];

        for (i = 0; i < holes.Length; i++)
        {
            holes[i].unactivate_squares();
        }
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

       // objects_pool_controller.s.clear_squares_floor_particle();
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
        heart_active = true;
        heart.SetActive(true);
    }


    void heart_end()
    {
        heart_active = false;
        heart.SetActive(false);
    }

    #endregion

    public void set_symbols_alpha(float alpha)
    {
        symbols.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
    }

    #region =========== DEBUG ================
    void back_to_normal_color() {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    #endregion

}
