using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ball_hero : MonoBehaviour
{

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

    //public GameObject

    // Use this for initialization
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 vetor;

    public GameObject explosion;

    bool hitted_wall = false;

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

    void Update()
    {

        if (Input.GetMouseButton(0) || Input.GetKey("space"))
        {
            jump();
        }

        // SET X SPEED TO MAX EVERY FRAME
        if (rb.velocity.x > 0) rb.velocity = new Vector2(globals.s.BALL_SPEED_X, rb.velocity.y);
        else if (rb.velocity.x < 0) rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);

    }

    void FixedUpdate()
    {
        //Debug.Log (" MY X SPEED: " + rb.velocity.x);
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

        //

        if (globals.s.PW_SUPER_JUMP == true)
        {
            main_camera.s.PW_super_jump(transform.position.y);
        }
        // falling case
        if (rb.velocity.y < -0.02f) grounded = false; //else grounded = true;

        if (my_id == globals.s.BALL_ID - 1)
        {
            globals.s.BALL_Y = transform.position.y;
            globals.s.BALL_X = transform.position.x;
            globals.s.BALL_GROUNDED = grounded;

            //globals.s.BALL_FLOOR = my_floor;
        }
        #region ================ Ball Up ====================
        if (son_created == false && ((transform.position.x <= globals.s.LIMIT_LEFT + globals.s.BALL_R + 0.3f && rb.velocity.x < 0) ||
                                     (transform.position.x >= globals.s.LIMIT_RIGHT - globals.s.BALL_R - 0.3f && rb.velocity.x > 0)))
        {
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
            my_son.GetComponent<Rigidbody2D>().velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            //Debug.Log("MMMMMM MY SON VY " + my_son.GetComponent<Rigidbody2D>().velocity.y + " | MY VY: " + rb.velocity.y);
            my_son.GetComponent<ball_hero>().my_floor = my_floor + 1;
            my_son.GetComponent<ball_hero>().grounded = grounded;

            /*1st camera movement check
            if (my_floor == 1){
				camerda.GetComponent<Rigidbody2D>().velocity = new Vector2(0,globals.s.CAMERA_SPEED);
		    }*/


            // CALL GAME CONTROLLER
            //game_controller.s.create_new_wave()   ;
            game_controller.s.ball_up(my_floor);

            if(hitted_wall) main_camera.s.hitted_on_wall = false;

            wall[] paredez = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
            foreach(wall p in paredez){
                p.place_me_at_the_other_corner(my_son.transform.position.x, my_floor + 1);
            }


            //my_son = (GameObject)Instantiate (ball_hero, new Vector3 (0, 0, 0), transform.rotation);

        }
        #endregion

        else if (son_created == true && (transform.position.x < globals.s.LIMIT_LEFT - globals.s.BALL_D ||
                                     transform.position.x > globals.s.LIMIT_RIGHT + globals.s.BALL_D))
        {
            //Debug.Log("Destroy me !!!! my pos:" + transform.position.x);
            Destroy(gameObject);
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


        if (coll.gameObject.CompareTag("Floor"))
        {
            
            if (coll.transform.position.y + coll.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2 <= transform.position.y - globals.s.BALL_R + 1f)
            {
                //rb.AddForce (new Vector2 (0, 0));
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //transform.position = new Vector2(transform.position.x, coll.transform.position.y + coll.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2 + globals.s.BALL_R);
                my_floor = coll.gameObject.GetComponent<floor>().my_floor;
                //Debug.Log(my_id + " KKKKKKKKKKKKKKKKKK KOLLISION! MY NEW FLOOR: " + my_floor + " I AM GROUNDED ");

                grounded = true;

                coll.gameObject.GetComponent<floor>().blink();
            }
            else { Debug.Log("\n\n" + my_id + " ***************ERROR! THIS SHOULD NEVER HAPPEN ***************\n\n"); }
        }

        else if (coll.gameObject.CompareTag("Spike"))
        {
            if(globals.s.PW_SUPER_JUMP == false)
            {
                if (globals.s.PW_INVENCIBLE == false)
                {
                    destroy_me();
                }
                else
                {
                    Destroy(coll.gameObject);
                    heart_end();
                }
            }

        }

        else if (coll.gameObject.CompareTag("HoleFalling"))
        {
            //grounded = false;
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100f);
            if(transform.position.y < main_camera.s.transform.position.y - 10f)
                main_camera.s.OnBallFalling();
            Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
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


    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (globals.s.PW_SUPER_JUMP == true )
        {
            if (coll.gameObject.CompareTag("PW_Trigger"))
            {
                if (coll.gameObject.GetComponent<floor_pw_collider>() != null)
                    coll.gameObject.GetComponent<floor_pw_collider>().unactive_sprite_daddy();
            }
        }
    }
        #endregion

        void destroy_me()
    {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];

        foreach (ball_hero b in bolas) {
            Destroy(b.gameObject);
        }

        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        game_controller.s.game_over();
    }

    void pw_do_something(PW_Collect temp)
    {
        
        temp.collect();

        if (temp.pw_type == 1)
        {
            // heart_start();
            //go_up_pw_start();
            sight_start();
        }
    }

    #region POWER UP -> LIFE
    void heart_start()
    {
        globals.s.PW_INVENCIBLE = true;
        heart.SetActive(true);
        

        //chMotor.movement.gravity = g;
        //heart.transform.GetComponent<SpriteRenderer>().

    }


    void heart_end()
    {
       globals.s.PW_INVENCIBLE = false;
        //heart.transform.GetComponent<SpriteRenderer>().DOFade(1, 0);
        heart.SetActive(false);
    }
    #endregion

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

        for (i = my_floor + 1; i < temp + 5; i++)
        {
            my_son.GetComponent<ball_hero>().my_floor = i;
            game_controller.s.ball_up(i);
        }

        //activate squares of collisions
        activate_particles_floor();

        main_camera.s.init_PW_super_jump(transform.position.y);

        Invoke("go_up_PW", 0.1f);
    }
    void go_up_PW()
    {
        globals.s.PW_SUPER_JUMP = true;
        // rb.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        int ball_speed = 20;
        rb.velocity = new Vector2(0, ball_speed);

        Invoke("stop_go_up_PW", 5*( globals.s.FLOOR_HEIGHT/ ball_speed));
    }

    void stop_go_up_PW()
    {
        rb.velocity = new Vector2(2, 0);
        unactivate_particles_floor();
        Invoke("create_floor", 0.1f);
    }

    void create_floor()
    {
       GameObject floor = game_controller.s.create_floor(12, my_son.GetComponent<ball_hero>().my_floor + 1);
        destroy_spikes();
        floor.transform.DOMoveX(0, 0.3f);//.OnComplete(pw_super_end);
        pw_super_end();
    }
   
    void pw_super_end()
    {
        super.SetActive(false);
        globals.s.PW_SUPER_JUMP = false;
        rb.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        rb.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        main_camera.s.pw_super_jump_end();
    }

    void activate_particles_floor()
    {
        floor[] floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
        int i;
        for (i = 0; i < floors.Length; i++)
        {
            floors[i].activate_squares();
        }

        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];

        for (i = 0; i < holes.Length; i++)
        {
            holes[i].activate_squares();
        }
    }

    void unactivate_particles_floor()
    {
        int i;

        floor_square_pw_destruct[] squares = GameObject.FindObjectsOfType(typeof(floor_square_pw_destruct)) as floor_square_pw_destruct[];
        for (i = 0; i < squares.Length; i++)
        {
            squares[i].scale_down_to_dessapear();
        }

        destroy_spikes();

        floor[] floors = GameObject.FindObjectsOfType(typeof(floor)) as floor[];
        for (i = 0; i < floors.Length; i++)
        {
            floors[i].destroy_pw_super_under_floors(transform.position.y);
        }

        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];

        for (i = 0; i < holes.Length; i++)
        {
            holes[i].destroy_pw_super_under_floors(transform.position.y);
        }
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
    #endregion

    #region POWER UP -> SIGHT BEYOND SIGHT 
    void sight_start()
    {
        globals.s.PW_SIGHT_BEYOND_SIGHT = true;
        sight.SetActive(true);
        change_color_pw();
        Invoke("sight_end", 10);
    }

    void sight_end()
    {
        globals.s.PW_SIGHT_BEYOND_SIGHT = false;
        sight.SetActive(false);
    }

    void change_color_pw()
    {
        int i;

        wall[] walls = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
        for (i = 0; i < walls.Length; i++)
        {
            walls[i].show_me_pw_sight();
        }

        spike[] spikes = GameObject.FindObjectsOfType(typeof(spike)) as spike[];
        for (i = 0; i < spikes.Length; i++)
        {
            spikes[i].show_me_pw_sight();
        }


        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
        for (i = 0; i < holes.Length; i++)
        {
            holes[i].show_me_pw_sight();
        }
    }
    #endregion
}