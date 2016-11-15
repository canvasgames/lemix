using UnityEngine;
using System.Collections;

public class GD : MonoBehaviour {

    public static GD s;

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
    // Use this for initialization
    void Awake()
    {
        s = this;
    }

    // Update is called once per frame
    void Update () {
	
	}

}
