﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class wall : MonoBehaviour
{
    bool already_appeared = false;
    public bool spike_trigger = false;
    public int my_floor;
    public bool corner_wall = false;
    bool already_placed = false;
    public GameObject my_skin;
    public wall my_twin_wall;

    public bool wall_triggered_by_wall = false;
    public bool wall_trigger = false;
    public bool trigger_back = false;

    void Start()
    {
       // rb = transform.GetComponent<Rigidbody2D>();
        my_skin.transform.localScale = new Vector3(1,0,0);
        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            show_me_pw_sight();
        }
    }

    void Update()
    {
        // Destroy logic
        if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if ((!wall_triggered_by_wall || wall_triggered_by_wall && trigger_back) && already_appeared == false && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 2f)
        {
            if ((!corner_wall || (corner_wall && already_placed)) && Mathf.Abs(transform.position.x - globals.s.BALL_X) < 2f)
            {
                show_me();
            }
            /*else if (corner_wall && !already_placed)
            {
                if(globals.s.BALL_X > 0)
                    transform.position = new Vector2(globals.s.LIMIT_LEFT + globals.s.SLOT/2, transform.position.y);
                else
                    transform.position = new Vector2(globals.s.LIMIT_RIGHT - globals.s.SLOT/2, transform.position.y);
                already_placed = true;

                // Deactivate the manual trigger from spikes that are at the same side as I am
                spike[] spks = FindObjectsOfType(typeof(spike)) as spike[];

                foreach (spike spk in spks)
                {
                    //spk.manual_trigger_cancel(transform.position.x, my_floor);
                    spk.reposite_me_at_the_other_corner(transform.position.x, my_floor);
                }
            }*/
        }

        //Debug.Log(" WALL DIST: " + Mathf.Abs(transform.position.x - globals.s.BALL_X) + " BALL X: " + globals.s.BALL_X  + " MY X: "  +transform.position.x + " | MY Y-2f: " + (transform.position.y-2f)+ " BALL Y-R : "+ (globals.s.BALL_Y - globals.s.BALL_R) );
    }

    public void place_me_at_the_other_corner(float ball_x, int floor) {
        if (corner_wall && !already_placed && floor == my_floor) {

            if(!wall_triggered_by_wall) GetComponent<BoxCollider2D>().enabled = true;

            Debug.Log("WALL OF FLOOR " + floor + " REPOSITIONING!! BALL X: " + ball_x);
            if ((ball_x > 0 && !wall_triggered_by_wall) || (ball_x < 0 && wall_triggered_by_wall))
                transform.position = new Vector2(globals.s.LIMIT_LEFT + globals.s.SLOT / 2, transform.position.y);
            else
                transform.position = new Vector2(globals.s.LIMIT_RIGHT - globals.s.SLOT / 2, transform.position.y);
            already_placed = true;

            if (!wall_triggered_by_wall) { 
                // Deactivate the manual trigger from spikes that are at the same side as I am
                spike[] spks = FindObjectsOfType(typeof(spike)) as spike[];

                foreach (spike spk in spks) {
                    //spk.manual_trigger_cancel(transform.position.x, my_floor);
                    spk.reposite_me_at_the_other_corner(transform.position.x, my_floor);
                }
            }
        }
    }


    void show_me()
    {
        Debug.Log("\n NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN SCALE ME UP! DIST: " + Mathf.Abs(transform.position.x - globals.s.BALL_X) );
        transform.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 0);
        already_appeared = true;
        my_skin.transform.DOScaleY(1f, 0.2f);

        if (spike_trigger)
        {
            spike[] spks = FindObjectsOfType(typeof(spike)) as spike[];

            foreach(spike spk in spks)
            {
                spk.show_me_by_floor(my_floor);
            }
        }

        if (wall_trigger) {
            my_twin_wall.trigger_back = true;
            wall_trigger = false;
            my_twin_wall.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (trigger_back) {
            my_twin_wall.unshow_me();
        }
        /*
    gameObject spk = FindObjectOfType<spike>.GetComponent("spike");
    // Initialize Globals singleton
    globals[] single2 = FindObjectsOfType(typeof(globals)) as globals[];
    if (single2.Length == 0)
    {
        GameObject obj = (GameObject)Instantiate(single, new Vector3(0, 0, 0), transform.rotation);
        globals final = obj.GetComponent<globals>();
    }*/
    }

    public void unshow_me() {
        my_skin.transform.DOScaleY(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));
        
    }

    public void show_me_pw_sight()
    {
       //transform.GetComponent<SpriteRenderer>().color.a = 100;
      transform.GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    public void back_original_color_pw_sight()
    {

        transform.GetComponent<SpriteRenderer>().color = new Color (0,0,0,0);
    }
}
