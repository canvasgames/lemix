using UnityEngine;
using System.Collections;

public class yes_are_you_sure : BtsMenuClassCollider
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
        mp_controller.access.send_accept_rematch();
            Destroy(transform.parent.gameObject);
    }

    public override void clicked()
    {
        if (GLOBALS.Singleton.LVL_UP_MENU == false)
        {
            base.clicked();
        }
    }
}
