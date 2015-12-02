using UnityEngine;
using System.Collections;

public class no_are_you_sure : BtsMenuClassCollider
{
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    {
        base.ActBT();
            mp_controller.access.send_reject_rematch ();
			
            bt_revenge[] revMenu;
            revMenu = FindObjectsOfType(typeof(bt_revenge)) as bt_revenge[];
            revMenu[0].DeactivateBt();
            Destroy(transform.parent.gameObject);
    }

    public override void clicked()
    {
        base.clicked();
    }
}
