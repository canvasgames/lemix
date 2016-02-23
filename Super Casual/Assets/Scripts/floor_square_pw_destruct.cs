using UnityEngine;
using System.Collections;
using DG.Tweening;
public class floor_square_pw_destruct : MonoBehaviour {
    bool can_disappear = false;
    bool voe_pinta = false;
    float initial_y;
	// Use this for initialization
	void Start () {
        initial_y = transform.position.y;
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
        transform.DOScale(0, 0.3f).OnComplete(destroy_me_baby); ;
    }

    void destroy_me_baby()
    {
        Destroy(gameObject);
    }

}
