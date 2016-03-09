using UnityEngine;
using System.Collections;

public class activate_pw_button : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        ball_hero[] bolas = GameObject.FindObjectsOfType(typeof(ball_hero)) as ball_hero[];
        foreach (ball_hero b in bolas)
        {
            //Destroy(b.gameObject);
            b.send_actual_balls();
            break;
        }

        hud_controller.si.HUD_BUTTON_CLICKED = true;
        hud_controller.si.show_video_pw();
        USER.s.TOTAL_VIDEOS_WATCHED++;
        PlayerPrefs.SetInt("total_videos_watched", USER.s.TOTAL_VIDEOS_WATCHED);

        AnalyticController.s.ReportVideoWatchedForPowerUps();
    }
}
