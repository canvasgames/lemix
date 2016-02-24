using UnityEngine;
using System.Collections;
using DG.Tweening;
public class PW_controller : MonoBehaviour {

    public static PW_controller s;

    // Use this for initialization
    void Start () {
        s = this;
        globals.s.PW_INVENCIBLE = false;
        globals.s.PW_SIGHT_BEYOND_SIGHT = true;
        globals.s.PW_SUPER_JUMP = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void pw_ending()
    {
        globals.s.PW_ENDING = true;
        globals.s.PW_ENDING_TIME = 5f;
    }

    public void invencible_end()
    {
        CancelInvoke("invencible_end");
        globals.s.PW_INVENCIBLE = false;
        globals.s.PW_ENDING = false;
    }

    #region HEART
    public void invencible_start()
    {
        globals.s.PW_INVENCIBLE = true;
        Invoke("invencible_end", GD.s.GD_PW_HEARTH_TIME);
        Invoke("pw_ending", GD.s.GD_PW_HEARTH_TIME - 5);
    }
    #endregion

    #region SIGHT
    public void PW_sight_start()
    {
        globals.s.PW_SIGHT_BEYOND_SIGHT = true;
        change_color_sight_pw();

        Invoke("sight_end", GD.s.GD_PW_SIGHT_TIME);
    }

    public void sight_end()
    {
        CancelInvoke("sight_end");
        globals.s.PW_SIGHT_BEYOND_SIGHT = false;
        back_color_sight_pw();
    }

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
}