using UnityEngine;
using System.Collections;

public class QA : MonoBehaviour {

    public static QA s;

    public bool INVENCIBLE ;
    public int TIMESCALE = 1;
	// Use this for initialization
	void Start () {
        s = this;
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = TIMESCALE;
    }
}
