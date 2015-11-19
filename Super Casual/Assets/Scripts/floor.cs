using UnityEngine;
using System.Collections;



public class floor : MonoBehaviour {

    public int my_floor;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT*4){
			Destroy(gameObject);
		}

    }
}
