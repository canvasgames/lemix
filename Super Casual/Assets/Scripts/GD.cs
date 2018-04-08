using UnityEngine;
using System.Collections;

public enum MusicStyle{
	Eletro = 0,
	Rock = 1,
	Pop= 2,
	PopGaga = 3,
	Reggae= 4,
	Rap = 5,
	Samba = 6,
	Latina = 7,
	Classic = 8,
	DingoBells = 9,
	Lenght = 10
}


public class GD : MonoBehaviour {

    public static GD s;

	public float FOLLOWER_DELAY = 0.7f, FOLLOWER_DELAY_BASE = 0.1f;

	public int JUKEBOX_PRICE = 50, JUKEBOX_FTU_PRICE = 5;

	public int[] SCENERY_FLOOR_VALUES;

	public int FTU_NEWBIE_SCORE = 5;
	public int FTU_MATCHES_TO_UNLOCK_PW = 2;
	public int FTU_MATCHES_TO_UNLOCK_GIFT = 4;
	public int CUR_MATCHES_TO_UNLOCK_STUFF = 0;

    public int GD_PW_SIGHT_TIME;
    public int GD_PW_HEARTH_TIME;

    public int GD_WITH_PW_TIME;
    public int GD_WITHOUT_PW_TIME;

    public int GD_JUMPS_PW_BAR_FULL;

    public int GD_ROULLETE_WAIT_MINUTES;
    public int GD_GIFT_WAIT_MINUTES;

    public float GlowInTime = 0.15f;
	public float GlowStaticTime = 0f;
	public float GlowOutTime = 0.83f;

    public bool AnalyticsLive = false;

	public int N_MUSIC = 6;
    // Use this for initialization
    void Awake()
    {
        s = this;
    }

    // Update is called once per frame
    void Update () {
	
	}

	public string GetStyleName(MusicStyle style){
		if (style == MusicStyle.Classic)
			return "Classic";
		else if (style == MusicStyle.Eletro)
			return "Eletronic";
		else if (style == MusicStyle.Latina)
			return "Latina";
		else if (style == MusicStyle.Reggae)
			return "Reggae";
		else if (style == MusicStyle.Pop)
			return "Classic Pop";
		else if (style == MusicStyle.Rap)
			return "Rap";
		else if (style == MusicStyle.PopGaga)
			return "Modern Pop";
		else if (style == MusicStyle.Rock)
			return "Rock";
		else if (style == MusicStyle.Samba)
			return "Samba";
		else if (style == MusicStyle.DingoBells)
			return "Dingo Bells";
		else return "";
	}

}
