using UnityEngine;
using System.Collections;
using DG.Tweening;

public class spike : MonoBehaviour {

    public bool hidden = false;
    public bool manual_trigger = false;
    bool already_appeared = false;
    public int my_floor;
    Rigidbody2D rb;
    float timer = 0;
    float target_y;

	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manual_trigger && hidden == true && already_appeared == false
            && globals.s.BALL_Y - globals.s.BALL_R > transform.position.y + 0.5f
            && Mathf.Abs(globals.s.BALL_X - transform.position.x) < 3f)
            show_me();

        //Destroy check
        if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
            Destroy(gameObject);
    }
    
    public void show_me_by_floor(int n)
    {
        if (my_floor == n && hidden && manual_trigger) show_me();
    }

    // method for hidden spikess
    void show_me()
    {
        target_y = transform.position.y + transform.GetComponent<SpriteRenderer>().bounds.size.y;
        transform.DOMoveY(target_y, 0.17f);
        already_appeared = true;
        /*rb.velocity = new Vector2(0, 20f);
        timer = (transform.GetComponent<SpriteRenderer>().bounds.size.y ) / 20f ;
        Debug.Log(" MMMMMMMMMMMM MOVE SPIKE! TIMER: " + timer);
        ;*/

    }

    // Check if a hole can be created on top of me
    public bool check_range_for_hole(int floor_n, float x)
    {
        if (floor_n == my_floor)
        {
            if (Mathf.Abs(x - transform.position.x) < globals.s.HOLE_SPK_DIST) return false;
            else return true;
        }
        else
        {
            Debug.Log(" TTTTTTTTTTTT THIS SHOULD NEVER HAPPEN! M FLOOR: " + my_floor + " FLOOR PARAM: " + floor_n); return true;
        }
    }
}
