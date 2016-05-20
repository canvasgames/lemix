using UnityEngine;
using System.Collections;
using DG.Tweening;

public class note_behaviour : MonoBehaviour {

    float init_y;
	// Use this for initialization
	void Start () {
        init_y = transform.position.y;
       // Debug.Log(" MY COLOR : " + transform.GetComponent<SpriteRenderer>().color);
       // new_color();
        floating_animation_up();
    }
	
	// Update is called once per frame
	void Update () {
       // transform.GetComponent<SpriteRenderer>().color = Color.black;

    }

    void floating_animation_up() {
        transform.DOMoveY(transform.position.y + 0.5f, 1f).SetEase(Ease.InOutCubic).OnComplete(() => floating_animation_down());
    }

    void floating_animation_down() {
        transform.DOMoveY(transform.position.y - 0.5f, 1f).SetEase(Ease.InOutCubic).OnComplete(() => floating_animation_up());
    }

    void new_color() {
        float r = Random.Range(0f, 0.8f);
        float g = Random.Range(0f, 0.8f);
        float b = Random.Range(0f, 0.8f);

        //transform.GetComponent<SpriteRenderer>().color = new Color(r, g, b);

        //Invoke("new_color", 0.2f);
        transform.GetComponent<SpriteRenderer>().DOColor(new Color(r, g, b), 0.5f).OnComplete(() => new_color());
        //Debug.Log(" MY COLOR : " + transform.GetComponent<SpriteRenderer>().color);
    }
}
