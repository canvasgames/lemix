using UnityEngine;
using System.Collections;

public class Menus_Controller : MonoBehaviour {
	public static Menus_Controller acesss;

	public GameObject rematch_menu, wait_title, op_disconnected, countdown, sync, quit_game;
	GameObject wait, disco, load, syncro, quit, rematch;

	int disconect_state;
	float disconect_time;

	// Use this for initialization
	void Awake () {
		acesss = this;
	}
	
	// Update is called once per frame
	void Update () {
		disconnect_menu();
	}

	public void quitGame ()
	{
		quit = (GameObject)Instantiate (quit_game, new Vector3 (0,0 , 100), transform.rotation);

    }

	public void destructQuitGame()
	{
        if(quit != null)
		    Destroy(quit);
	}

	// ======================= MATCH END =======================
	public void rematchMenu()
	{
        rematch = (GameObject)Instantiate(rematch_menu, new Vector3 (0,0 , 100), transform.rotation);

	}

    public void destructRematch()
    {
        if (rematch != null)
        {
            Destroy(rematch);
        }
           

    }

    public void waiting ()
	{
		wait = (GameObject)Instantiate (wait_title, new Vector3 (0,0 , 100), transform.rotation);

	}

	public void destructWaiting()
	{
		Destroy(wait,3f);
	}

	// ======================= LOADING GAME =======================
	public void syncronize_menu()
	{
		syncro = (GameObject)Instantiate (sync, new Vector3 (0,0 , 100), transform.rotation);
	}

	public void countdown_menu()
	{
		Destroy(syncro);
		load = (GameObject)Instantiate (countdown, new Vector3 (0,0 , 100), transform.rotation);
	}
	
	// ======================= DISCONNECTED =======================
	void disconnect_menu()
	{
		if(disconect_state !=0 )
		{
			disconect_time -= Time.unscaledDeltaTime;
			if(disconect_time <= 0)
			{
                //In game case
                if (disconect_state == 1)
				{
					Destroy(disco);
					GLOBALS.Singleton.WIN = true;
					GLOBALS.Singleton.GAME_RUNNING = false;
                    GameController.s.win_case_statistics();
				}
                //Match end case
                else if (disconect_state == 2)
                {
                    Destroy(disco);
                }
                //Before the game begins case
                else
                {
                    PhotonNetwork.LoadLevel("Lobby_GUI");
                }
				disconect_state = 0;
			}
		}
	}

	public void disconnected(bool game_state)
	{
		disco = (GameObject)Instantiate (op_disconnected, new Vector3 (0,0 , 100), transform.rotation);
        //In game case
		if(game_state == true && GLOBALS.Singleton.WIN == false && GLOBALS.Singleton.DRAW == false 
            && GLOBALS.Singleton.LOOSE == false)
            disconect_state =1;
        //Match end case
		else if(game_state == false && (GLOBALS.Singleton.WIN == true || GLOBALS.Singleton.DRAW == true
            || GLOBALS.Singleton.LOOSE == true))
		{
			Destroy(syncro);
			Destroy(load);

			disconect_state =2;
		}
        //Before the game begins case
        else
        {
            Destroy(syncro);
            Destroy(load);

            disconect_state = 3;
        }
		disconect_time = 3f;
	}


}
