using UnityEngine;
using System.Collections;
using DG.Tweening;

public class bt_menu : BtsMenuClassCollider
{

	GameController[] gc;
	// Use this for initialization
	void Start () {
		gc = FindObjectsOfType(typeof(GameController)) as GameController[];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    {
		if(GLOBALS.Singleton.LVL_UP_MENU == false)
		{
			Debug.Log ("GO TO LOBBY NOW !");
			Time.timeScale = 1;
			gc [0].go_to_lobby ();
		}

	}

}
