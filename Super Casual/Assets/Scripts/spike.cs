using UnityEngine;
using System.Collections;
using DG.Tweening;

public class spike : scenario_objects {

    public bool hidden = false;
    public bool manual_trigger = false;
    public bool corner_repositionable = false;
    bool already_appeared = false;

   // public int my_floor;
    Rigidbody2D rb;
    float timer = 0;
    float target_y;
    public PolygonCollider2D my_collider;
  

	// Use this for initialization
    void Awake()
    {

        my_collider = GetComponent<PolygonCollider2D>();
    }
	void Start () {
        rb = transform.GetComponent<Rigidbody2D>();
        //GetComponent<SpriteRenderer>().color = Color.green;
        if(QA.s.INVENCIBLE == true)
            my_collider.enabled = false;
        if(globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            show_me_pw_sight();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        //hidden spike distance check (no manual trigger)
        if (!manual_trigger && hidden == true && already_appeared == false
            && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y + 0.5f
            && Mathf.Abs(globals.s.BALL_X - transform.position.x) < 4.5f)
            show_me();

        //Destroy check
        if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
            Destroy(gameObject);
    }
    
    public void show_me_by_floor(int n)
    {
        if (my_floor == n && hidden && manual_trigger) show_me();
    }

    // method for hidden spikess
    void show_me()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.white;
        target_y = transform.position.y + transform.GetComponent<SpriteRenderer>().bounds.size.y;
        transform.DOMoveY(target_y, 0.14f);
        already_appeared = true;
        if(QA.s.INVENCIBLE == false)
            my_collider.enabled = true;
        /*rb.velocity = new Vector2(0, 20f);
        timer = (transform.GetComponent<SpriteRenderer>().bounds.size.y ) / 20f ;
        Debug.Log(" MMMMMMMMMMMM MOVE SPIKE! TIMER: " + timer);
        ;*/

    }

    // Check if a hole can be created on top of me
    public bool check_range_for_hole(int floor_n, float x)
    {
        if (floor_n == my_floor)
        {
            if (Mathf.Abs(x - transform.position.x) < globals.s.HOLE_SPK_DIST) return false;
            else return true;
        }
        else
        {
            Debug.Log(" TTTTTTTTTTTT THIS SHOULD NEVER HAPPEN! M FLOOR: " + my_floor + " FLOOR PARAM: " + floor_n); return true;
        }
    }

    public void manual_trigger_cancel(float position, int floor)
    {
        if (manual_trigger && floor == my_floor && ((position > 0 && transform.position.x > 0) || (position < 0 && transform.position.x < 0)))
        {
            manual_trigger = false;
        }
    }


    public void reposite_me_at_the_other_corner(float wall_position, int floor)
    {
        if (corner_repositionable && floor == my_floor)
        {
            if (wall_position > 0)
                transform.position = new Vector2(0 - Mathf.Abs(0 - transform.position.x), transform.position.y);
            else
                transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
        }
    }


    public void destroy_throwed_spikes(float y_pos_ball)
    {
        if( y_pos_ball > transform.position.y)
        {
            transform.DOScale(0, 0.3f).OnComplete(destroy_me_baby);
        }
    }

    void destroy_me_baby()
    {
        Destroy(gameObject);
    }

    public void show_me_pw_sight()
    {
        if(hidden == true)
            transform.GetComponent<SpriteRenderer>().color = Color.magenta;
    }
}
