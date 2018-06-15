﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class saw : scenario_objects
{
    public GameObject my_vision_effect;
    public GameObject my_skin;

    public bool hidden = false;
    public bool manual_trigger = false;
    bool already_placed = false;
    public bool corner_repositionable = false;
    public bool repositionable = false;
    bool already_appeared = false;
    bool already_alerted = false;
    public bool triple_spk = false;
    float timer = 0;
    internal string wave_name;

    // Use this for initialization
    void Start () {
        //my_skin.transform.localScale = new Vector3(1, 0, 0);
        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            show_me_pw_sight();
        }
        else
            my_vision_effect.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (already_appeared == false && !already_alerted &&
        globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 2f && my_floor <= globals.s.BALL_FLOOR + 1)
        {
            already_alerted = true;
            globals.s.ALERT_BALL = true;
        }

        if ( already_appeared == false &&
           globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 2f && my_floor <= globals.s.BALL_FLOOR + 1)
        {
            if (already_placed && Mathf.Abs(transform.position.x - globals.s.BALL_X) < 12f)
            {
                show_me();
            }
        }
        }

    public void place_me_at_the_other_corner(float ball_x, int floor)
    {
        if (floor == my_floor)
        {
            //GetComponent<CircleCollider2D>().enabled = true;

            if ((ball_x > 0 ) )
            {
                transform.position = new Vector2(0 - Mathf.Abs(0 - transform.position.x), transform.position.y);
            }
            else
            {
                transform.position = new Vector2(0 + Mathf.Abs(0 - transform.position.x), transform.position.y);
            }

            already_placed = true;
        }
    }


    void show_me()
    {
        //Debug.Log("\n NNNNNNNNNNNNNNNNNNNNNNNNNNNNNN SCALE ME UP! DIST: " + Mathf.Abs(transform.position.x - globals.s.BALL_X) );
        already_appeared = true;
        transform.GetComponent<CircleCollider2D>().enabled = true;

        transform.DOLocalMoveY((transform.localPosition.y + 0.64f), 0.1f).SetEase(Ease.Linear); ;
        if(transform.position.x < 0)
            transform.DOMoveX((transform.position.x + 11f), QA.s.jokerf).SetEase(Ease.Linear);
        else
            transform.DOMoveX((transform.position.x - 11f), QA.s.jokerf).SetEase(Ease.Linear);

        /*if (wall_trigger)
       {
           my_twin_wall.trigger_back = true;
           wall_trigger = false;
           my_twin_wall.GetComponent<BoxCollider2D>().enabled = true;
       }


   gameObject spk = FindObjectOfType<spike>.GetComponent("spike");
   // Initialize Globals singleton
   globals[] single2 = FindObjectsOfType(typeof(globals)) as globals[];
   if (single2.Length == 0)
   {
       GameObject obj = (GameObject)Instantiate(single, new Vector3(0, 0, 0), transform.rotation);
       globals final = obj.GetComponent<globals>();
   }*/
    }

    public void unshow_me()
    {
        my_skin.transform.DOScaleY(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));

    }

    public void show_me_pw_sight()
    {
        //transform.GetComponent<SpriteRenderer>().color.a = 100;
        // transform.GetComponent<SpriteRenderer>().color = Color.magenta;
        //GameObject instance = Instantiate(Resources.Load("Prefabs/Warning",
        //typeof(GameObject)), new Vector3(transform.position.x, transform.position.y +2f), transform.rotation) as GameObject;
        if (!already_appeared)
        {
            my_vision_effect.SetActive(true);
            my_vision_effect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            my_vision_effect.GetComponent<SpriteRenderer>().DOFade(1, 0.25f);
        }

    }

    public void back_original_color_pw_sight()
    {
        //transform.GetComponent<SpriteRenderer>().color = new Color (0,0,0,0);
        my_vision_effect.SetActive(false);
    }

    public void clear_flags_reposite()
    {
        transform.GetComponent<CircleCollider2D>().enabled = false;

        hidden = false;
        manual_trigger = false;
        corner_repositionable = false;
        repositionable = false;
        already_appeared = false;

        timer = 0;
        count_blink = 16;
    }
}
