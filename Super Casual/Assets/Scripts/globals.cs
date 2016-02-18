using UnityEngine;
using System.Collections;

public class globals : MonoBehaviour {
	public static globals s;

    public int GAME_OVER = 0;
    public bool CAN_RESTART = false;

    public int BALL_ID = 0;
    public float BALL_Y = -8f;
    public float BALL_X;
    public float BALL_FLOOR = 0;
    public bool BALL_GROUNDED = true;
    public float BALL_SPEED_X = 4f;
    public float BALL_SPEED_Y;
	public float CAMERA_SPEED = 0;
    [HideInInspector] public float HOLE_SPK_DIST = 1.25f;
	[HideInInspector] public float LIMIT_LEFT = -4.8f;
    [HideInInspector] public float LIMIT_RIGHT = 4.8f;
	[HideInInspector] public float SCREEN_MID =  0;
	[HideInInspector] public float FLOOR_HEIGHT = 4f;
    [HideInInspector] public float SLOT = 1f;
	[HideInInspector] public float BASE_Y = -7.5f;
	public float BALL_R = 0.5f;
	public float BALL_D = 1f;
	public int SCREEN_WIDTH = 980;

    public int BEST_RECORD;
    public int LAST_RECORD;
    public int BEST_RECORD_TODAY;

    [HideInInspector]
    public bool PW_INVENCIBLE = false;

    [HideInInspector]
    public bool PW_SUPER_JUMP = false;
    // Use this for initialization

    void Awake () {
		s = this;

        HOLE_SPK_DIST = 2.6f;

        DontDestroyOnLoad (transform.gameObject);
		Debug.Log ("Globals initialized !!");
	}
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
