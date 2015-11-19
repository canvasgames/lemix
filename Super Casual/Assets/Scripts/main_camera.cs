using UnityEngine;
using System.Collections;

public class main_camera : MonoBehaviour {

    private Rigidbody2D rb;
    bool initiated= false;
    public bool moving = false;

    // Use this for initialization
    void Start () {
        rb = transform.GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3 (0, 0,0);
        if (initiated == false)
        {
            if (globals.s.BALL_Y > transform.position.y)
            {
                rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
                initiated = true;
                moving = true;
            }
        }
        else
        {
            if (moving && globals.s.BALL_Y < transform.position.y - globals.s.FLOOR_HEIGHT)
            {
                rb.velocity = new Vector2(0, 0);
                moving = false;
            }

            else if (globals.s.BALL_Y > transform.position.y - globals.s.FLOOR_HEIGHT/4  && globals.s.BALL_GROUNDED == true)//Debug.Log("MY Y POS: " + transform.position.y);  if (globals.s.BALL_Y > transform.position.y)
            {
                rb.velocity = new Vector2(0, globals.s.CAMERA_SPEED);
                moving = true;
            }

        }
	}
}
