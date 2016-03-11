using UnityEngine;
using System.Collections;

public class USER : MonoBehaviour {

    public static USER s;

    [HideInInspector]   public int BEST_SCORE, LAST_SCORE, DAY_SCORE;
    [HideInInspector]   public int TOTAL_GAMES, TOTAL_VIDEOS_WATCHED;
    [HideInInspector]   public int FIRST_PW_CREATED;

    void Awake() {
        //PlayerPrefs.SetInt("total_games", 2);
       // PlayerPrefs.SetInt("best", 7);
        BEST_SCORE = PlayerPrefs.GetInt("best", 0);
        LAST_SCORE = PlayerPrefs.GetInt("last_score", 0);
        DAY_SCORE = PlayerPrefs.GetInt("day_best", 0);
        TOTAL_GAMES = PlayerPrefs.GetInt("total_games", 0);
        TOTAL_VIDEOS_WATCHED =  PlayerPrefs.GetInt("total_videos_watched", 0);
        FIRST_PW_CREATED =  PlayerPrefs.GetInt("first_pw_created", 0);

        s = this;
    } 

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
