using UnityEngine;
using System.Collections;
using DG.Tweening;

public class bt_menu : BtsMenuClassCollider
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
            Debug.Log ("GO TO LOBBY NOW !");
			Time.timeScale = 1;
            GameController.s.go_to_lobby ();
	}
    public override void clicked()
    {
        if (GLOBALS.Singleton.LVL_UP_MENU == false)
        {
            base.clicked();
        }
    }
}
