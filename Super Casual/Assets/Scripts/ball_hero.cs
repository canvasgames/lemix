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
    public GameObject heart;

    //public GameObject

    // Use this for initialization
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Vector2 vetor;

    public GameObject explosion;

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
        else rb.velocity = new Vector2(-globals.s.BALL_SPEED_X, rb.velocity.y);
    }

    void FixedUpdate()
    {
        //Debug.Log (" MY X SPEED: " + rb.velocity.x);
        //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Floor"));

        //


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
            if(globals.s.PW_INVENCIBLE == false)
            {
                destroy_me();
            }
            else
            {
                Destroy(coll.gameObject);
                heart_end();
            }
            
        }

        else if (coll.gameObject.CompareTag("HoleFalling"))
        {
            //grounded = false;
            //coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -100f);
            main_camera.s.OnBallFalling();
            Debug.Log(" ~~~~~~~~~~~~~~~~~~~~~~~~~COLLIDING WITH HOLE FALLING TAG!!!");
            

        }
        else if (coll.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        else if (coll.gameObject.CompareTag("PW"))
        {
            PW_Collect temp = coll.gameObject.GetComponent<PW_Collect>();
            pw_do_something(temp);
        }
    }

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
        Debug.Log(temp.pw_type + "meu tipinho");
        if (temp.pw_type == 1)
        {
            heart_start();
        }
    }

    void heart_start()
    {
        globals.s.PW_INVENCIBLE = true;
        heart.SetActive(true);
        //heart.transform.GetComponent<SpriteRenderer>().
        
    }


    void heart_end()
    {
        globals.s.PW_INVENCIBLE = false;
        //heart.transform.GetComponent<SpriteRenderer>().DOFade(1, 0);
        heart.SetActive(false);
    }
}