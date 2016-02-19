using UnityEngine;
using System.Collections;
using DG.Tweening;

public class hole_behaviour : MonoBehaviour
{
    public GameObject my_skin;
    public int my_floor;
    public GameObject squares, colliderPW;
    // Use this for initialization
    void Start()
    {
        my_skin.GetComponent<hole_skin_behaviour>().my_floor = my_floor;

        if (globals.s.PW_SIGHT_BEYOND_SIGHT == true)
        {
            show_me_pw_sight();
        }

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
            //my_skin.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -15f);
            my_skin.transform.DOMoveY(my_skin.transform.position.y - 8f, 0.6f).OnComplete ( ()=>Destroy(my_skin));
            GetComponent<BoxCollider2D>().enabled = false;

            // gameObject.SetActive(false);

        }
    }

    public void activate_squares()
    {
        squares.SetActive(true);
        colliderPW.SetActive(true);
    }

    public void unactivate_squares()
    {
        squares.SetActive(false);
    }

    public void destroy_pw_super_under_floors(float y_pos_ball)
    {
        if(colliderPW != null)
            colliderPW.SetActive(false);

        if (y_pos_ball > transform.position.y)
        {
            transform.DOScaleZ(1, 0.3f).OnComplete(destroy_me_baby); 
        }
    }

    void destroy_me_baby()
    {
        Destroy(gameObject);
    }

    public void show_me_pw_sight()
    {
        if (my_skin != null)
            my_skin.transform.GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    public void back_original_color_pw_sight()
    {
        if(my_skin != null)
            my_skin.transform.GetComponent<SpriteRenderer>().color = Color.white;
    }
}