﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class spike : scenario_objects {
    public string wave_name;
    public bool hidden = false;
    public bool manual_trigger = false;
	public bool corner_repositionable = false;
    public bool repositionable = false;
    bool already_appeared = false;
    bool already_alerted = false;
	public bool triple_spk = false;

    public GameObject my_glow;

    float timer = 0;
    float target_y;
    [HideInInspector] public PolygonCollider2D my_collider;
  



	// Use this for initialization
    void Awake()
    {
        my_collider = GetComponent<PolygonCollider2D>();
    }
	void Start () {

        //GetComponent<SpriteRenderer>().color = Color.green;
        //if(QA.s.INVENCIBLE == true)
        //my_collider.enabled = false;
        glow_animation_wait();
    }

    public void glow_animation_start() {
		if(my_glow != null) my_glow.GetComponent<SpriteRenderer>().DOFade(1f, GD.s.GlowInTime).OnComplete(glow_animation_wait);
    }
	public void glow_animation_wait(){
		Invoke ("glow_animation_end", GD.s.GlowStaticTime);
		//glow_animation_end ();

	}
    public void glow_animation_end() {
		if (my_glow != null) my_glow.GetComponent<SpriteRenderer>().DOFade(0, GD.s.GlowOutTime).OnComplete(glow_animation_start);
    }

    // Update is called once per frame
    void Update()
    {
        //alert ball check
        if (!manual_trigger && !already_alerted && hidden == true && already_appeared == false
            && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f && my_floor <= globals.s.BALL_FLOOR + 1) {
            already_alerted = true;
            globals.s.ALERT_BALL = true;
        }

        //hidden spike distance check (no manual trigger)
        if (!manual_trigger && hidden == true && already_appeared == false
            && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f
            && Mathf.Abs(globals.s.BALL_X - transform.position.x) < 4.5f)
            show_me();

        //Destroy check
        if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
        {
            if(gameObject!=null)
            {
               // Destroy(gameObject);
            }
        }
            
    }
    
    public void show_me_by_floor(int n)
    {
        if (my_floor == n && hidden && manual_trigger) show_me();
    }

    // method for hidden spikess
    void show_me()
    {
        Debug.Log("[SPK HIDDEN] show me called !!!! ");
        //transform.GetComponent<SpriteRenderer>().color = Color.white;
        transform.GetComponent<SpriteRenderer>().color = Color.black;
        //target_y = transform.position.y + transform.GetComponent<SpriteRenderer>().bounds.size.y;
        target_y = transform.position.y + 0.5f;
        transform.DOMoveY(target_y, 0.14f);
        transform.DOScaleY(globals.s.SPK_SCALE, 0.14f);
        already_appeared = true;
        my_collider.enabled = true;
        hidden = false;

        //if(QA.s.INVENCIBLE == false)
        //my_collider.enabled = true;
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
			if ((!triple_spk && Mathf.Abs(x - transform.position.x) < globals.s.HOLE_SPK_DIST) ||
				(triple_spk && Mathf.Abs(x - transform.position.x) < globals.s.HOLE_SPK_DIST + 0.15f)) return false;
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

	public void reposite_me_for_FTU(float wall_position, int floor)
	{
		if (repositionable && floor == my_floor)
		{
			if (wall_position > 0)
				transform.position = new Vector2(0 - Mathf.Abs(0 - transform.position.x), transform.position.y);
			else
				transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
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
            /// transform.DOScale(0, 0.3f);//.OnComplete(destroy_me_baby);
            transform.position = new Vector3(transform.position.x - Random.Range(50,150), transform.position.y - Random.Range(50, 150), transform.position.z);
        }
    }


    public void remove_spikes_revive(int floor)
    {
        if (floor == my_floor || floor+1 == my_floor || floor+2 == my_floor)
        {
            transform.position = new Vector3(transform.position.x - Random.Range(50, 150), transform.position.y - Random.Range(50, 150), transform.position.z);

        }
    }

    void destroy_me_baby()
    {
        //Destroy(gameObject);
    }

    public void show_me_pw_sight()
    {
        if ( hidden == true) {
            transform.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
            
                //transform.GetComponent<SpriteRenderer>().color = Color.magenta;
        //transform.GetComponent<Animator>().Play("red");
    }

    public void back_original_color_pw_sight()
    {
        //transform.GetComponent<Animator>().Play("blue");
        //transform.GetComponent<SpriteRenderer>().color = Color.white;
       transform.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void clear_flags_reposite()
    {
        hidden = false;
        manual_trigger = false;
		corner_repositionable = false;
        repositionable = false;
        already_appeared = false;

        timer = 0;
        GetComponent<SpriteRenderer>().color = Color.black;
        transform.localScale = new Vector3(globals.s.SPK_SCALE, globals.s.SPK_SCALE, globals.s.SPK_SCALE);
        count_blink = 16;
        //transform.DOScale(0.7f, 0.1f);
    }
}
