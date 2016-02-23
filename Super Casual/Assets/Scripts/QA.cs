using UnityEngine;
using System.Collections;

public class QA : MonoBehaviour {

    public static QA s;

    public bool INVENCIBLE ;
    public float TIMESCALE = 1;
    public float TRACE_PROFUNDITY = 1;
    // 1 = Just Main info
    // 2 = All floor excential creation information
    // 3 = More detailed info of creation floor process and physics

	// Use this for initialization
	void Awake() {
        s = this;
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = TIMESCALE;
    }
}
