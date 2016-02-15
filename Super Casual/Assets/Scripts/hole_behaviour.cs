using UnityEngine;
using System.Collections;

public class hole_behaviour : scenario_objects
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT * 4)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -15f);

           // gameObject.SetActive(false);

        }
    }
}