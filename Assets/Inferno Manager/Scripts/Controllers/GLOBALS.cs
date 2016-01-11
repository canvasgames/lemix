using UnityEngine;
using System.Collections;

public class GLOBALS : MonoBehaviour {
    public static GLOBALS s;

    //GAME STATUS
    [HideInInspector] public bool GAME_RUNNING = false;
    [HideInInspector] public int TUTORIAL_PHASE = 0;
    [HideInInspector] public bool TUTORIAL_OCCURING = false;
    [HideInInspector] public bool LOCK_CAMERA_TUTORIAL = false;
    [HideInInspector] public int USER_RANK = 0;
    [HideInInspector] public int BUILDING_MAX_LEVEL = 30;
    [HideInInspector] public int BUILDING_N_TYPES = 30;
    [HideInInspector] public string PUNISHER_COUNT_EVOLUTION = "1,2,3,3,4,4,5,5,6,6,7,7,7,8,8,8,9,9,9,10,10,10,11,11,11,12,12,12,13,13";

    void Awake()
    {
        s = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
