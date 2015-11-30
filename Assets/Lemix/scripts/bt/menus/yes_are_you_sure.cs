using UnityEngine;
using System.Collections;

public class yes_are_you_sure : BtsMenuClassCollider
{
	mp_controller[] mp;
	// Use this for initialization
	void Start () {
		mp = FindObjectsOfType(typeof(mp_controller)) as mp_controller[];
	}
	
	// Update is called once per frame
	void Update () {

	}

    public override void ActBT()
    {
        if (GLOBALS.Singleton.LVL_UP_MENU == false)
        {
            mp[0].send_accept_rematch();
            Destroy(transform.parent.gameObject);
        }
    }

}
