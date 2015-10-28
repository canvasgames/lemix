
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// This script automatically connects to Photon (using the settings file), 
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class Lobby_Master : Photon.MonoBehaviour
{
    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = true;

	public bool OFFLINE_MODE = false;

    public byte Version = 1;



	public static int playerWhoIsIt = 0;
	private static PhotonView ScenePhotonView;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;

    public virtual void Start()
    {
		ScenePhotonView = this.GetComponent<PhotonView>();


       

		PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
    }

    public virtual void Update()
    {    
	}

	public void Connect_to_photon(){
		if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
		{
			Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");
			
			ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings(Version + "."+Application.loadedLevel);
		}
	}

    // to react to events "connected" and (expected) error "failed to join random room", we implement some methods. PhotonNetworkingMessage lists all available methods!

    public virtual void OnConnectedToMaster()
    {
		Debug.Log ("LOBBY - CONNECTION STATE: " +PhotonNetwork.connectionState);
        if (PhotonNetwork.networkingPeer.AvailableRegions != null) Debug.LogWarning("List of available regions counts " + PhotonNetwork.networkingPeer.AvailableRegions.Count + ". First: " + PhotonNetwork.networkingPeer.AvailableRegions[0] + " \t Current Region: " + PhotonNetwork.networkingPeer.CloudRegion);
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        //PhotonNetwork.JoinRandomRoom();

		// Trying to create or join a random room
		// documentation found at: https://doc.photonengine.com/en/realtime/current/reference/matchmaking-and-lobby
		RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
		PhotonNetwork.JoinOrCreateRoom("babado", roomOptions, TypedLobby.Default);
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
		Debug.Log ("LOBBY - CONNECTION STATE: " +PhotonNetwork.connectionState);
        Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = 2 }, null);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

	public void OnJoinedRoom()
	{
		Debug.Log ("LOBBY - CONNECTION STATE: " +PhotonNetwork.connectionStateDetailed);
		Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
		
		//MASTER TEST
		if (PhotonNetwork.isMasterClient == true) {
			GameObject texto = GameObject.Find ("mytext");
//			texto.GetComponent<TextMesh> ().text = "I'M MASTER";
		} 
		else {
			GameObject texto = GameObject.Find ("mytext");
	//		texto.GetComponent<TextMesh> ().text = "NOT MASTER";
		}

		Debug.Log ("PLAYER COUNT" + PhotonNetwork.room.playerCount);
		if(PhotonNetwork.room.playerCount == 1) 
		{
			Debug.Log("OnJoinedRoom(): I AM THE HOST");
			GLOBALS.Singleton.MP_PLAYER = 1;
			GLOBALS.Singleton.OP_PLAYER = 2;
			GLOBALS.Singleton.MP_MODE = 1;
		}
		else{
			Debug.Log("OnJoinedRoom(): I AM NOT THE HOST");
			GLOBALS.Singleton.MP_PLAYER = 2;
			GLOBALS.Singleton.OP_PLAYER = 1;
			GLOBALS.Singleton.MP_MODE = 1;
			//Application.LoadLevel("GamePlay");
			//PhotonNetwork.LoadLevel("GamePlay");

			send_player_info();
		}

		if (PhotonNetwork.playerList.Length == 1)
		{
			playerWhoIsIt = PhotonNetwork.player.ID;
		}
		
		Debug.Log ("playerWhoIsIt: " + playerWhoIsIt);

		if(OFFLINE_MODE == true) 
				PhotonNetwork.LoadLevel("GamePlay");
	}
	
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer){
		Debug.Log( "OnPhotonPlayerConnected() " );

		if (PhotonNetwork.room.playerCount == 1) {
			Debug.Log("OnPhotonPlayerConnected(: I AM THE HOST");
			GLOBALS.Singleton.MP_PLAYER = 1;
			GLOBALS.Singleton.OP_PLAYER = 2;
			GLOBALS.Singleton.MP_MODE = 1;
		}
		else{
			Debug.Log("OnPhotonPlayerConnected(): ANOTHER PLAYER JOINING THE ROOM");
			//Application.LoadLevel("GamePlay");
			//PhotonNetwork.LoadLevel("GamePlay");

			send_player_info();
		}
	}

	//

    public void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). Use a GUI to show existing rooms available in PhotonNetwork.GetRoomList().");
    }

	//====================== SEND RPCS =============================

	void send_player_info(){
		Debug.Log("send_player_info()");
		int word_id = 0;
		if (GLOBALS.Singleton.MP_PLAYER == 1) {
			Debug.Log("send_player_info(): I am host, generating a anagram... | WORD ID SENT: "+ word_id);
			int numberOfFiles = GLOBALS.Singleton.NumberOfWordFiles;
			Debug.Log(numberOfFiles);
			//Sorteia um dos arquivos de palavras
			word_id = Random.Range(1,numberOfFiles);
			GLOBALS.Singleton.ANAGRAM_ID = word_id;
		}

		ScenePhotonView.RPC("get_player_info", PhotonTargets.Others , word_id);
	}
	
	//======================= GET RPCS =============================


	[PunRPC]
	public void get_player_info(int word_id){
		Debug.Log("get_player(info)");
		if (GLOBALS.Singleton.MP_PLAYER == 2) {
			Debug.Log("get_player(info): I am not the host, sending a confirmation and loading next scene | WORD ID RECEIVED: " + word_id);
			GLOBALS.Singleton.ANAGRAM_ID = word_id;

			ScenePhotonView.RPC("confirmation_received", PhotonTargets.Others,0);

			PhotonNetwork.LoadLevel("GamePlay");
		}
	}

	[PunRPC]
	public void confirmation_received(int n){
		Debug.Log("confirmation_received(): loading next scene");
		PhotonNetwork.LoadLevel("GamePlay");
	}

	
}
