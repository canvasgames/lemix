using UnityEngine;
using System.Collections;

public class mp_controller : Photon.MonoBehaviour {

	// Use this for initialization
	public WController w_controller;
	public GameObject ss;
	private static PhotonView ScenePhotonView;
	PowerUpCtrl[] pwctrl;
	Menus_Controller[] menusctrl;
	Waiting_scrpit[] waitingMenu;

	void Awake () {
		ScenePhotonView = this.GetComponent<PhotonView>();
	}
	
	void Start () {
		pwctrl = FindObjectsOfType(typeof(PowerUpCtrl)) as PowerUpCtrl[];
		menusctrl = FindObjectsOfType(typeof(Menus_Controller)) as Menus_Controller[];

	//	WController[] wordCTRL = FindObjectsOfType(typeof(WController)) as WController[];

		if (GLOBALS.Singleton.MP_PLAYER == 0){
			GLOBALS.Singleton.MP_PLAYER = 1;
			GLOBALS.Singleton.OP_PLAYER = 2;
			Debug.Log ("CONNECTION STATE: " +PhotonNetwork.connectionState);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		// Make a background box
		//GUI.Box (new Rect (10, 10, 100, 90), "Loader Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		//if (GUI.Button (new Rect (20, 40, 80, 20), "SEND")) {
			//ScenePhotonView.RPC ("ChatMessage", PhotonTargets.Others, "OH MY GOD THEY KILLED KENNY");
		//}
	}



	//====================== SEND RPCS =============================

	public void send_player_info(int anagram_id){
		ScenePhotonView.RPC("get_player_info", PhotonTargets.Others , anagram_id);
	}

	public void send_lvl(int level){
		ScenePhotonView.RPC("receive_lvl", PhotonTargets.Others ,level);
		Debug.Log ("MANDANDO");
	}
	
	[PunRPC]
	public void receive_lvl(int lvl){
		Debug.Log ("recebendo");
		GameObject P2lvl = GameObject.Find ("hud_p2_level"); 
		P2lvl.GetComponent<TextMesh> ().text = "LVL " + lvl.ToString ();
	}



	//======================= GET RPCS =============================

	[PunRPC]
	public void ChatMessage(string a)
	{
		Debug.Log("ChatMessage: " + a);

		GameObject objj = (GameObject)Instantiate (ss, new Vector3 (0, 0, 0), transform.rotation);
//		Bt_restart abc = objj.GetComponent<Bt_restart> ();
	}

	//====================== WORD FOUND RPCS =============================
	[PunRPC]
	public void WordFoundByOP(int word_id, int goldLetterActive)
	{
		Debug.Log("Receveid Word Id " + word_id);

		//wordCTRL[0].wordfound(GLOBALS.Singleton.OP_PLAYER, word_id);
		w_controller.wordfound(GLOBALS.Singleton.OP_PLAYER, word_id, goldLetterActive);
	}

	public void send_word_found(int word_id){
		ScenePhotonView.RPC("WordFoundByOP", PhotonTargets.Others , word_id, GLOBALS.Singleton.PUGOLDLETTERACTIVE);
	}
	
	//====================== POWER UP RPCS =============================
	
	public void send_frozen_letter(){
		ScenePhotonView.RPC("receive_frozen_letter", PhotonTargets.Others ,0);
		Debug.Log ("MANDANDO");
	}
	
	[PunRPC]
	public void receive_frozen_letter(int a){
		Debug.Log ("recebendo");
		pwctrl[0].freezeLetter();
	}


	public void send_end_of_freeze_time()
	{
		ScenePhotonView.RPC("receive_end_frozen_letter", PhotonTargets.Others ,0);
		Debug.Log ("MANDANDO");
	}

	[PunRPC]
	public void receive_end_frozen_letter(){
		Debug.Log ("recebendo");
		pwctrl[0].unfreezeOPSprite();
	}


	public void send_dark(){
		ScenePhotonView.RPC("receive_dark", PhotonTargets.Others ,0);
		Debug.Log ("MANDANDO DARK");
	}
	
	[PunRPC]
	public void receive_dark(int a){
		Debug.Log ("recebendo DARK");
		pwctrl[0].night();
	}

	public void send_erase(int IDWord){
		ScenePhotonView.RPC("receive_erase", PhotonTargets.Others ,IDWord);
		Debug.Log ("MANDANDO ERASE");
	}
	
	[PunRPC]
	public void receive_erase(int IDWord){
		Debug.Log ("recebendo");
		pwctrl[0].eraseWordReceive(IDWord);
	}

	public void send_earthquake(){
		ScenePhotonView.RPC("receive_earthquake", PhotonTargets.Others ,0);
		Debug.Log ("MANDANDO EARTHQUSKE");
	}
	
	[PunRPC]
	public void receive_earthquake(){
		Debug.Log ("recebendo earthquake");
		pwctrl[0].earthquakeReceive();
	}
#region Rematch
//============================ REMATCH ==============================
	public void send_ask_for_rematch()
	{
		if (GLOBALS.Singleton.REMATCH_RECEIVED == 0 && PhotonNetwork.connected && PhotonNetwork.connected != false ){
			GLOBALS.Singleton.REMATCH_SENT = 1;

			// sorting anagram id
			int numberOfFiles = GLOBALS.Singleton.NumberOfWordFiles;
			int anagram_id = Random.Range (1, numberOfFiles);
			GLOBALS.Singleton.ANAGRAM_ID = anagram_id;

			ScenePhotonView.RPC("rematch_request_received", PhotonTargets.Others ,anagram_id);
			// DISPLAY WAITING DIALOG...

		}
	}

	public void send_accept_rematch(){
		if (PhotonNetwork.connected && PhotonNetwork.connected != false) {
			GLOBALS.Singleton.REMATCH_ACCEPT_STATUS = 1;
			ScenePhotonView.RPC("rematch_request_answered", PhotonTargets.Others ,1);

			if (GLOBALS.Singleton.REMATCH_SENT == 0 || (GLOBALS.Singleton.REMATCH_SENT == 1 && GLOBALS.Singleton.MP_PLAYER == 2 )){
				GLOBALS.Singleton.ANAGRAM_ID = GLOBALS.Singleton.RECEIVED_ANAGRAM_ID;
				rematch_begins();
			}
		}
	}

	public void send_reject_rematch(){
		//NEED TO DEACTIVATE THE REMATCH BUTTON IF THERE IS ONE
		if (PhotonNetwork.connected && PhotonNetwork.connected != false) {
				GLOBALS.Singleton.REMATCH_ACCEPT_STATUS = 2;
				ScenePhotonView.RPC ("rematch_request_answered", PhotonTargets.Others, 2);
		}
	}


	[PunRPC]
	public void rematch_request_received(int anagram_id){
		if (GLOBALS.Singleton.REMATCH_RECEIVED == 0 && PhotonNetwork.connected && PhotonNetwork.connected != false) {
			GLOBALS.Singleton.RECEIVED_ANAGRAM_ID = anagram_id;
			GLOBALS.Singleton.REMATCH_RECEIVED = 1;

			if (GLOBALS.Singleton.REMATCH_SENT == 0) {
				menusctrl[0].rematchMenu();
				Debug.Log ("REMATCH REQUEST RECEIVED");
					//DISPLAY ACCEPT AND REJECT BUTTONS

			} else {
				Debug.Log ("REMATCH INVITATION ALREADY SENT!!!");
				send_accept_rematch();
			}
		}
	}

	[PunRPC]
	public void rematch_request_answered(int accept_status){
		if (PhotonNetwork.connected && PhotonNetwork.connected != false) {
			Debug.Log ("REMATCH ANSWERED!!! STATUS:  "  +accept_status);
			GLOBALS.Singleton.REMATCH_RECEIVED = accept_status;
			waitingMenu = FindObjectsOfType(typeof(Waiting_scrpit)) as Waiting_scrpit[]; 
			if(accept_status == 1){
				waitingMenu[0].rematchAcepted();
			}
			else{
				// DESTROY DIALOG
				int zero =0;
				waitingMenu[0].rematchRejected();
			}
		}
	}

	public void rematch_begins(){
		Debug.Log ("REMATCH BEGINS NOW!!!!!!!");
		PhotonNetwork.LoadLevel ("Gameplay");
	}


#endregion
		
	

}
