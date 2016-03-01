using UnityEngine;
using System.Collections;
using DG.Tweening;
public class floor_square_pw_destruct : MonoBehaviour {

    bool can_disappear = false;

    float initial_y, initial_x;

	// Use this for initialization
	void Start () {
        initial_y = transform.localPosition.y;
        initial_x = transform.localPosition.x;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (can_disappear == false)
        {
            if (transform.GetComponent<Rigidbody2D>().velocity != new Vector2(0, 0))
            {
                can_disappear = true;
            }
        }
    }



    public void scale_down_to_dessapear()
    {
     if (can_disappear == true)
        transform.DOScale(0, 0.3f) ;
    }

    public void reposite_squares_tururu()
    {
        transform.DOScale(1, 0.1f);
        transform.localPosition = new Vector3(initial_x, initial_y, 0);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

}
