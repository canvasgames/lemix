using UnityEngine;
using System.Collections;

public class hole_behaviour : MonoBehaviour
{
    public GameObject my_skin;
    public int my_floor;

    // Use this for initialization
    void Start()
    {
        my_skin.GetComponent<hole_skin_behaviour>().my_floor = my_floor;
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
            my_skin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -15f);
            GetComponent<BoxCollider2D>().enabled = false;

           // gameObject.SetActive(false);

        }
    }
}