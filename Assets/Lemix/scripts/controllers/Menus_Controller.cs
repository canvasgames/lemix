using UnityEngine;
using System.Collections;

public class Menus_Controller : MonoBehaviour {
	public GameObject rematch_menu, wait_title, op_disconnected;
	GameObject wait, disco;
	GameController[] gctrller;
	int disconect_state;
	float disconect_time;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		disconnect_menu();
	}

	public void rematchMenu()
	{
		GameObject rematch = (GameObject)Instantiate (rematch_menu, new Vector3 (0,0 , 100), transform.rotation);

	}

	public void waiting ()
	{
		wait = (GameObject)Instantiate (wait_title, new Vector3 (0,0 , 100), transform.rotation);

	}

	void disconnect_menu()
	{
		if(disconect_state !=0 )
		{
			disconect_time -= Time.unscaledDeltaTime;
			if(disconect_time <= 0)
			{
				if(disconect_state == 1)
				{
					Destroy(disco);
					gctrller = FindObjectsOfType(typeof(GameController)) as GameController[];
					GLOBALS.Singleton.WIN = true;
					GLOBALS.Singleton.GAME_RUNNING = false;
					gctrller[0].win_case_statistics();
				}
				else
					PhotonNetwork.LoadLevel ("Lobby");
				disconect_state = 0;
			}
		}
	}
	public void disconnected(bool game_state)
	{
		disco = (GameObject)Instantiate (op_disconnected, new Vector3 (0,0 , 100), transform.rotation);
		if(game_state == true)
			disconect_state =1;
		else
			disconect_state =2;
		disconect_time = 3f;
	}
	public void destructWaiting()
	{
		Destroy(wait,3f);
	}
}
