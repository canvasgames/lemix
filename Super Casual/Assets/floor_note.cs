using UnityEngine;
using System.Collections;
using DG.Tweening;

public class floor_note : MonoBehaviour {
    public bool already_appeared = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(!already_appeared && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y - 1.5f && 
            ((globals.s.CUR_BALL_SPEED > 0 && globals.s.BALL_X > transform.position.x) ||
            ((globals.s.CUR_BALL_SPEED < 0 && globals.s.BALL_X < transform.position.x)))
            ) {
            already_appeared = true;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            GetComponent<SpriteRenderer>().DOFade(1, 0.3f);
            //Debug.Log("LET ME APPEAR! MY: " + transform.position.x + " BALL X: " + globals.s.BALL_X + "ball speed: " + globals.s.CUR_BALL_SPEED);
        }
	
	}
}
