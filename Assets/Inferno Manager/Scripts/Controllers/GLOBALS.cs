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
