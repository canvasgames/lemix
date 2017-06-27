using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class QA : MonoBehaviour {

    public static QA s;

	public bool DONT_START_THE_GAME = false;

	public bool OLD_PLAYER;

	bool sqMode = true;
    public bool INVENCIBLE ;
    public float TIMESCALE = 1;
    public float TRACE_PROFUNDITY = 1;

    // 1 = Just Main info
    // 2 = All floor excential creation information
    // 3 = More detailed info of creation floor process and physics
    public bool NO_PWS = false;
    public bool SHOW_WAVE_TYPE = false;
	public bool CREATE_NOTE_TRAIL = true;

	public Ease ease1;

	public GameObject SqBt;
	// Use this for initialization
	void Awake() {
		if (sqMode == false) {
			SqBt.GetComponent<Image> ().color = Color.white;
		} else {
			SqBt.GetComponent<Image> ().color = Color.green;
		}
        s = this;
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != TIMESCALE) Time.timeScale = TIMESCALE;
    }

	public void SwitchSquaresMode(){
		if (sqMode == true) {
			sqMode = false;
			SqBt.GetComponent<Image> ().color = Color.white;
		} else {
			sqMode = true;
			SqBt.GetComponent<Image> ().color = Color.green;
		}

	}
}
