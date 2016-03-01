using UnityEngine;
using System.Collections;

public class USER : MonoBehaviour {

    public static USER s;

    [HideInInspector]   public int BEST_SCORE, LAST_SCORE, DAY_SCORE;
    [HideInInspector]   public int TOTAL_GAMES, TOTAL_VIDEOS_WATCHED;



    void Awake() { 

        s = this;
    } 

	// Use this for initialization
	void Start () {
        BEST_SCORE = PlayerPrefs.GetInt("best", 0);
        LAST_SCORE = PlayerPrefs.GetInt("last_score", 0);
        DAY_SCORE = PlayerPrefs.GetInt("day_best", 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
