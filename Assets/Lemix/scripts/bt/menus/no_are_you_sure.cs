using UnityEngine;
using System.Collections;

public class no_are_you_sure : BtsMenuClassCollider
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
            mp [0].send_reject_rematch ();
			
            bt_revenge[] revMenu;
            revMenu = FindObjectsOfType(typeof(bt_revenge)) as bt_revenge[];
            revMenu[0].DeactivateBt();
            Destroy(transform.parent.gameObject);
    }
}
