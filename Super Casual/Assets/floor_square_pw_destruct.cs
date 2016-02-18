using UnityEngine;
using System.Collections;
using DG.Tweening;
public class floor_square_pw_destruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStart2D(Collision2D coll)
    {
        if (globals.s.PW_SUPER_JUMP == true)
        {
            if (coll.gameObject.CompareTag("PW_Trigger"))
            {
                if (coll.gameObject.GetComponent<floor_pw_collider>() != null)
                    coll.gameObject.GetComponent<floor_pw_collider>().unactive_sprite_daddy();
            }
                
        }
    }

    public void scale_down_to_dessapear()
    {
        transform.DOScale(0, 0.3f).OnComplete(destroy_me_baby); ;
    }

    void destroy_me_baby()
    {
        Destroy(gameObject);
    }
}
