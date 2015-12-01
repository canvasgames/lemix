using UnityEngine;
using System.Collections;

public class cancel_bt : BtsGuiClick
{
	public GameObject lobbymaster,multiplayer, botbt, avatar_bt, statistics_txt, txt_connection, avatar_conection, lang_bt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override void ActBT()
    {
		GLOBALS.Singleton.MM_SEARCHING_MATCH = false;
		PhotonNetwork.LeaveLobby();
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.Disconnect();
        lang_bt.GetComponent<mm_choose_lang>().ActivateBt();

        txt_connection.SetActive(false);
		avatar_conection.SetActive(false);
		lobbymaster.SetActive(false);

		statistics_txt.SetActive(true);
		multiplayer.SetActive(true);
        multiplayer.GetComponent<bt_multiplayer>().OnMouseExit();
        botbt.SetActive(true);
		avatar_bt.SetActive(true);
        avatar_bt.GetComponent<avatar_main_menu>().changeAvatar(GLOBALS.Singleton.AVATAR_TYPE);

        this.OnMouseExit();
        gameObject.SetActive(false);
		//sair do lobby no photon
	}

    public void disconected()
    {
        GLOBALS.Singleton.MM_SEARCHING_MATCH = false;
        lang_bt.GetComponent<mm_choose_lang>().ActivateBt();
        Debug.Log("lalalala");
        txt_connection.SetActive(false);
        avatar_conection.SetActive(false);
        

        statistics_txt.SetActive(true);
        multiplayer.SetActive(true);
        multiplayer.GetComponent<bt_multiplayer>().OnMouseExit();
        botbt.SetActive(true);
        avatar_bt.SetActive(true);
        avatar_bt.GetComponent<avatar_main_menu>().changeAvatar(GLOBALS.Singleton.AVATAR_TYPE);

        this.OnMouseExit();
        gameObject.SetActive(false);

        lobbymaster.SetActive(false);
    }
}
