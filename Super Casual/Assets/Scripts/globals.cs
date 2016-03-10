using UnityEngine;
using System.Collections;

public class globals : MonoBehaviour {
	public static globals s;

    public int GAME_OVER = 0;
    public bool CAN_RESTART = false;
    public bool GAME_STARTED = false;

    public int BALL_ID = 0;
    public float BALL_Y = -8f;
    public float BALL_X;
    public int BALL_FLOOR = 0;
    public bool BALL_GROUNDED = true;
    public float BALL_SPEED_X = 4f;
    public float BALL_SPEED_Y;
	public float CAMERA_SPEED = 0;
    [HideInInspector] public float HOLE_SPK_DIST = 1.29f;
	[HideInInspector] public float LIMIT_LEFT = -4.8f;
    [HideInInspector] public float LIMIT_RIGHT = 4.8f;
	[HideInInspector] public float SCREEN_MID =  0;
	[HideInInspector] public float FLOOR_HEIGHT = 4f;
    [HideInInspector] public float SLOT = 1f;
	[HideInInspector] public float BASE_Y = -7.5f;
	public float BALL_R = 0.5f;
	public float BALL_D = 1f;
	public int SCREEN_WIDTH = 980;

   

    [HideInInspector] public bool PW_ACTIVE;
    [HideInInspector] public bool PW_INVENCIBLE = false;
    [HideInInspector] public bool PW_SUPER_JUMP = false;
    [HideInInspector] public bool PW_SIGHT_BEYOND_SIGHT = false;

    [HideInInspector] public bool PW_ENDING = false;

    [HideInInspector] public bool FIRST_GAME = true;

    [HideInInspector]
    public bool CAN_REVIVE = false;
    [HideInInspector]
    public bool SHOW_VIDEO_AFTER = false;
    [HideInInspector]
    public bool REVIVING = false;

    [HideInInspector]
    public bool MENU_OPEN = false;
    // Use this for initialization

    void Awake () {
		s = this;

        BALL_Y = -8f;

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
