using UnityEngine;
using System.Collections;

public class USER : MonoBehaviour {

    public static USER s;

	[HideInInspector] 	public int PW_INTRODUCED, GIFT_INTRODUCED, NEWBIE_PLAYER;
    [HideInInspector]   public int BEST_SCORE, LAST_SCORE, DAY_SCORE;
    [HideInInspector]   public int TOTAL_GAMES, TOTAL_VIDEOS_WATCHED;
    [HideInInspector]   public int FIRST_PW_CREATED, FIRST_HOLE_CREATED, FIRST_WALL_CREATED;
    [HideInInspector]   public int NOTES;
    [HideInInspector]
    public int SOUND_MUTED;

    void Awake() {
		//PlayerPrefs.SetInt ("notes", 666);

        //PlayerPrefs.SetInt("total_games", 0);
		//PlayerPrefs.SetInt("best", 0);
		//PlayerPrefs.SetInt ("first_game", 0);
        SOUND_MUTED = PlayerPrefs.GetInt("sound_muted", 0);



        NOTES = PlayerPrefs.GetInt("notes", 180);

        BEST_SCORE = PlayerPrefs.GetInt("best", 0);
        LAST_SCORE = PlayerPrefs.GetInt("last_score", 0);
        DAY_SCORE = PlayerPrefs.GetInt("day_best", 0);
        TOTAL_GAMES = PlayerPrefs.GetInt("total_games", 0);
        TOTAL_VIDEOS_WATCHED =  PlayerPrefs.GetInt("total_videos_watched", 0);

        // new user variables
        FIRST_PW_CREATED =  PlayerPrefs.GetInt("first_pw_created", 0);
        FIRST_HOLE_CREATED =  PlayerPrefs.GetInt("first_hole_created", 0);
        FIRST_WALL_CREATED =  PlayerPrefs.GetInt("first_wall_created", 0);


		PW_INTRODUCED =  PlayerPrefs.GetInt("pw_introduced", 0);
		GIFT_INTRODUCED = PlayerPrefs.GetInt("gift_introduced", 0);
		NEWBIE_PLAYER = PlayerPrefs.GetInt("newbie_player", 1);

        s = this;

		if (TOTAL_GAMES > 4 && FIRST_PW_CREATED == 1) {
			GIFT_INTRODUCED = 1;
			PlayerPrefs.SetInt("gift_introduced", 1);
		}
    } 


	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
