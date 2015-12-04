using UnityEngine;
using System.Collections;

public class GLOBALS : MonoBehaviour {
    public static GLOBALS s;

    //GAME STATUS
    [HideInInspector] public bool GAME_RUNNING = false;

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
