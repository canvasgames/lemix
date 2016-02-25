using UnityEngine;
using System.Collections;
using DG.Tweening;
public class PW_controller : MonoBehaviour {

    public static PW_controller s;

    public ball_hero[] balls; 
    int balls_i = 0;

    float actual_mask_symbol_alpha = 0;
    float PW_symbol_startTime = 0;
    float PW_symbol_ending_duration = 3f;

    bool fade_in_mask = true;
    // Use this for initialization
    void Start () {
        s = this;

        balls = new ball_hero[2];

        globals.s.PW_INVENCIBLE = false;
        globals.s.PW_SIGHT_BEYOND_SIGHT = false;
        globals.s.PW_SUPER_JUMP = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (globals.s.PW_ENDING == true)
        {
            change_alpha_signs();
        }
	}

    //Called by invoke
    public void pw_ending()
    {
        globals.s.PW_ENDING = true;
        PW_symbol_startTime = Time.time;
    }


    #region INVENCIBLE HEART
    public void invencible_start()
    {
        globals.s.PW_INVENCIBLE = true;
        Invoke("invencible_end", GD.s.GD_PW_HEARTH_TIME);
        Invoke("pw_ending", GD.s.GD_PW_HEARTH_TIME - PW_symbol_ending_duration);
        symbols_start();
    }

    public void invencible_end()
    {
        CancelInvoke("invencible_end");
        CancelInvoke("pw_ending");
        globals.s.PW_INVENCIBLE = false;
        globals.s.PW_ENDING = false;

        actual_mask_symbol_alpha = 0;
        change_balls_symbol_mask_alpha();
    }
    #endregion
    #region SIGHT
    public void PW_sight_start()
    {
        globals.s.PW_SIGHT_BEYOND_SIGHT = true;
        change_color_sight_pw();
        
        Invoke("sight_end", GD.s.GD_PW_SIGHT_TIME);
        Invoke("pw_ending", GD.s.GD_PW_SIGHT_TIME - PW_symbol_ending_duration);
        symbols_start();
    }


    public void sight_end()
    {
        CancelInvoke("pw_ending");
        CancelInvoke("sight_end");
        globals.s.PW_ENDING = false;
        globals.s.PW_SIGHT_BEYOND_SIGHT = false;
        back_color_sight_pw();

        actual_mask_symbol_alpha = 0;
        change_balls_symbol_mask_alpha();

    }
    #endregion

    void symbols_start()
    {
        actual_mask_symbol_alpha = 0;
        change_balls_symbol_mask_alpha();
    }
    #region SIGHT CHANGE COLOR
    void change_color_sight_pw()
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

    void back_color_sight_pw()
    {
        int i;

        wall[] walls = GameObject.FindObjectsOfType(typeof(wall)) as wall[];
        for (i = 0; i < walls.Length; i++)
        {
            walls[i].back_original_color_pw_sight();
        }

        spike[] spikes = GameObject.FindObjectsOfType(typeof(spike)) as spike[];
        for (i = 0; i < spikes.Length; i++)
        {
            spikes[i].back_original_color_pw_sight();
        }


        hole_behaviour[] holes = GameObject.FindObjectsOfType(typeof(hole_behaviour)) as hole_behaviour[];
        for (i = 0; i < holes.Length; i++)
        {
            holes[i].back_original_color_pw_sight();
        }
    }
    #endregion

    public void add_ball(ball_hero new_ball)
    {
        balls[balls_i] = new_ball;

        if(balls_i == 0)
        {
            balls_i++;
        }
        else
        {
            balls_i = 0;
        }
    }

    void change_alpha_signs()
    {
        float t = (Time.time - PW_symbol_startTime) / PW_symbol_ending_duration;
        actual_mask_symbol_alpha = Mathf.SmoothStep(0, 1, t);
        /*
        if (fade_in_mask == true)
        {
            
            if (actual_mask_symbol_alpha >= 1)
            {
                fade_in_mask = false;
                PW_symbol_startTime = Time.time;
                PW_symbol_duration = PW_symbol_duration - 0.2f;
            } 
        }
        else
        {        
            actual_mask_symbol_alpha = Mathf.SmoothStep(1, 0, t);
            if(actual_mask_symbol_alpha <=0)
            {
                fade_in_mask = true;
                PW_symbol_startTime = Time.time;
                PW_symbol_duration = PW_symbol_duration - 0.2f;
            }
        }
        */
        change_balls_symbol_mask_alpha();
    }

    void change_balls_symbol_mask_alpha()
    {
        if (balls[0] != null)
        {
            balls[0].set_symbols_alpha(actual_mask_symbol_alpha);
        }
        if (balls[1] != null)
        {
            balls[1].set_symbols_alpha(actual_mask_symbol_alpha);
        }
    }
}