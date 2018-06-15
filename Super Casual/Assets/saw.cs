using UnityEngine;
using System.Collections;

public class saw : scenario_objects
{
    public bool hidden = false;
    public bool manual_trigger = false;
    public bool corner_repositionable = false;
    public bool repositionable = false;
    bool already_appeared = false;
    bool already_alerted = false;
    public bool triple_spk = false;
    float timer = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clear_flags_reposite()
    {
        hidden = false;
        manual_trigger = false;
        corner_repositionable = false;
        repositionable = false;
        already_appeared = false;

        timer = 0;
        //        GetComponent<SpriteRenderer>().color = Color.black;
        transform.localScale = new Vector3(globals.s.SPK_SCALE, globals.s.SPK_SCALE, globals.s.SPK_SCALE);
        count_blink = 16;
        //transform.DOScale(0.7f, 0.1f);
    }
}
