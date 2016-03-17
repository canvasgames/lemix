using UnityEngine;
using System.Collections;


public class scenario_objects : MonoBehaviour {
    public int my_floor;
    int count_blink = 16;
    public bool already_blinked = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void try_blink(int floor)
    {
        Debug.Log(floor +" sdffsdfsdf " + " meu floor   " + my_floor);
        if (floor == my_floor)
        {
            already_blinked = true;
            blink_color();
        }
    }
    void blink_color()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        Invoke("blink_back", 0.1f);
    }
    void blink_back()
    {
        count_blink -= 1;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        if (count_blink > 0)
            Invoke("blink_color", 0.1f);
    }

    public void unactivate_collider()
    {
        if(transform.GetComponent<Collider2D>() != null)
            transform.GetComponent<Collider2D>().isTrigger = true;
    }
}
