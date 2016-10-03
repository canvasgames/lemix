﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class hole_behaviour : MonoBehaviour
{
    public GameObject my_skin;
    public int my_floor;
    public GameObject colliderPW;
    bool hidden = true;
    bool already_alerted = false;
    public GameObject my_glow;
	public bool repositionable = false;
	public bool already_placed = false;
	public GameObject floor_left, floor_right;
    // Use this for initialization
    void Start()
    {
        my_skin.GetComponent<hole_skin_behaviour>().my_floor = my_floor;
        //glow_animation_start();

        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            show_me_pw_sight();
        }

    }
    
    public void glow_animation_start() {
		my_glow.GetComponent<SpriteRenderer>().DOFade(1f, GD.s.GlowInTime).OnComplete(glow_animation_wait);
    }
	public void glow_animation_wait(){
		Invoke("glow_animation_end", GD.s.GlowStaticTime);
	}
    public void glow_animation_end() {
		if (my_glow != null) my_glow.GetComponent<SpriteRenderer>().DOFade(0, GD.s.GlowOutTime).OnComplete(glow_animation_start);
    }

    // Update is called once per frame
    void Update()
    {
        if (hidden && !already_alerted && 
            globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f) {
            already_alerted = true;
            globals.s.ALERT_BALL = true;
        }

        if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
            Destroy(gameObject);
    }

	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            //my_skin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -15f);
            Invoke("start_falling", 0.01f);

            // gameObject.SetActive(false);

        }
    }

    void start_falling() {
        my_skin.transform.DOMoveY(my_skin.transform.position.y - 8f, 0.8f).OnComplete(() => Destroy(my_skin));
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void activate_squares()
    {
        if (colliderPW) colliderPW.SetActive(true);
    }


    public void destroy_pw_super_under_floors(float y_pos_ball)
    {
        if(colliderPW != null)
            colliderPW.SetActive(false);

        if (y_pos_ball > transform.position.y)
        {
            transform.DOScaleZ(1, 0.3f).OnComplete(destroy_me_baby); 
        }
    }

    void destroy_me_baby()
    {
        Destroy(gameObject);
    }

    public void show_me_pw_sight()
    {
        if (my_skin != null)
            my_skin.transform.GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    public void back_original_color_pw_sight()
    {
        if(my_skin != null)
            //my_skin.transform.GetComponent<SpriteRenderer>().color = Color.white;
            my_skin.transform.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void colidded_super_pw()
    {
        my_skin.transform.GetComponent<SpriteRenderer>().enabled = false;
    }

	public void reposite_me_at_the_other_corner(float ball_position, int floor)
	{
		if (repositionable && floor == my_floor)
		{
			if (ball_position > 0) {
				transform.position = new Vector2 (0 - Mathf.Abs (0 - transform.position.x), transform.position.y);
			}
			else
				transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
//			if (floor_left != null) Debug.Log("REPOSITIONING FLOOR LEFT.." + floor_left.transform.position.x );
//			else Debug.Log("FLOOR LEFT IS NUL!! ");
			floor_left.transform.position = new Vector2 (transform.position.x - game_controller.s.hole_size/2 - + floor_left.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, floor_left.transform.position.y);
			floor_right.transform.position = new Vector2 (transform.position.x + game_controller.s.hole_size/2 + floor_left.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2, floor_left.transform.position.y);
//			if (floor_left != null)
//				Debug.Log ("REPOSITIONED.." + floor_left.transform.position.x);

		}
		//if (my_hole != null)
		//my_hole.transform.position = transform.position;
	}
}

