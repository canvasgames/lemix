
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

    mm_connect_status[] mm_status;
    Word_Sorter_Controller[] wSort;


    public static int playerWhoIsIt = 0;
    private static PhotonView ScenePhotonView;

    /// <summary>if we don't want to connect in Start(), we have to "remember" if we called ConnectUsingSettings()</summary>
    private bool ConnectInUpdate = true;
    private bool connected = false;

    public virtual void OnEnable()
    {

        wSort = FindObjectsOfType(typeof(Word_Sorter_Controller)) as Word_Sorter_Controller[];
        ScenePhotonView = this.GetComponent<PhotonView>();
        ConnectInUpdate = true;
        

        PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
    }

    public virtual void Update()
    {
        Debug.Log(GLOBALS.Singleton.MP_MODE + "connected");
        Debug.Log(PhotonNetwork.connected);
        if (connected == true && PhotonNetwork.connected == false)
        {
            connected = false;
            cancel_bt[] cancel;
            cancel = FindObjectsOfType(typeof(cancel_bt)) as cancel_bt[];
            cancel[0].disconected();
        }
    }

    public void Connect_to_photon()
    {

        if (ConnectInUpdate && AutoConnect && !PhotonNetwork.connected)
        {
            mm_status = FindObjectsOfType(typeof(mm_connect_status)) as mm_connect_status[];
            mm_status[0].connectionState1();
            Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");

            ConnectInUpdate = false;
            PhotonNetwork.ConnectUsingSettings(Version + "." + Application.loadedLevel);
        }
    }


    // to react to events "connected" and (expected) error "failed to join random room", we implement some methods. PhotonNetworkingMessage lists all available methods!

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("LOBBY - CONNECTION STATE: " + PhotonNetwork.connectionState);
        if (PhotonNetwork.networkingPeer.AvailableRegions != null) Debug.LogWarning("List of available regions counts " + PhotonNetwork.networkingPeer.AvailableRegions.Count + ". First: " + PhotonNetwork.networkingPeer.AvailableRegions[0] + " \t Current Region: " + PhotonNetwork.networkingPeer.CloudRegion);
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        //PhotonNetwork.JoinRandomRoom();

        // Trying to create or join a random room
        // documentation found at: https://doc.photonengine.com/en/realtime/current/reference/matchmaking-and-lobby
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(GLOBALS.Singleton.LANGUAGE.ToString(), roomOptions, TypedLobby.Default);
    }

    public virtual void OnPhotonRandomJoinFailed()
    {
        Debug.Log("LOBBY - CONNECTION STATE: " + PhotonNetwork.connectionState);
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
        connected = true;
        Debug.Log("LOBBY - CONNECTION STATE: " + PhotonNetwork.connectionStateDetailed);
        mm_status[0].connectionState2();
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

        //MASTER TEST
        if (PhotonNetwork.isMasterClient == true)
        {
            GameObject texto = GameObject.Find("mytext");
            //			texto.GetComponent<TextMesh> ().text = "I'M MASTER";
        }
        else
        {
            GameObject texto = GameObject.Find("mytext");
            //		texto.GetComponent<TextMesh> ().text = "NOT MASTER";
        }

        Debug.Log("PLAYER COUNT" + PhotonNetwork.room.playerCount);
        if (PhotonNetwork.room.playerCount == 1)
        {
            Debug.Log("OnJoinedRoom(): I AM THE HOST");
            GLOBALS.Singleton.MP_PLAYER = 1;
            GLOBALS.Singleton.OP_PLAYER = 2;
            GLOBALS.Singleton.MP_MODE = 1;
            GLOBALS.Singleton.CONNECTED = 1;

            string tempWords = PlayerPrefs.GetString("WordsAlreadySorted");

            int word_id;
            word_id = wSort[0].sortWordAndReturnAnagramID("");
        }
        else
        {
            Debug.Log("OnJoinedRoom(): I AM NOT THE HOST");
            GLOBALS.Singleton.MP_PLAYER = 2;
            GLOBALS.Singleton.OP_PLAYER = 1;
            GLOBALS.Singleton.MP_MODE = 1;
            GLOBALS.Singleton.CONNECTED = 1;
            mm_status[0].connectionState3Guest();
            //Application.LoadLevel("GamePlay");
            //PhotonNetwork.LoadLevel("GamePlay");

            //send_player_info();
        }

        if (PhotonNetwork.playerList.Length == 1)
        {
            playerWhoIsIt = PhotonNetwork.player.ID;
        }

        Debug.Log("playerWhoIsIt: " + playerWhoIsIt);

        if (OFFLINE_MODE == true)
            PhotonNetwork.LoadLevel("GamePlay");
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("OnPhotonPlayerConnected() ");

        if (PhotonNetwork.room.playerCount == 1)
        {
            Debug.Log("OnPhotonPlayerConnected(: I AM THE HOST");
            GLOBALS.Singleton.CONNECTED = 1;
            GLOBALS.Singleton.MP_PLAYER = 1;
            GLOBALS.Singleton.OP_PLAYER = 2;
            GLOBALS.Singleton.MP_MODE = 1;
        }
        else
        {
            Debug.Log("OnPhotonPlayerConnected(): ANOTHER PLAYER JOINING THE ROOM");
            //Application.LoadLevel("GamePlay");
            //PhotonNetwork.LoadLevel("GamePlay");
            mm_status[0].connectionState3();
            //send_player_info();
        }
    }

    //

    public void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby(). Use a GUI to show existing rooms available in PhotonNetwork.GetRoomList().");
    }

    public virtual void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        Debug.Log("OP Disconected");
        mm_status[0].connectionState2();

        if (PhotonNetwork.room.playerCount == 1)
        {
            Debug.Log("OnPhotonPlayerConnected(: NOW I AM THE HOST");
            GLOBALS.Singleton.CONNECTED = 1;
            GLOBALS.Singleton.MP_PLAYER = 1;
            GLOBALS.Singleton.OP_PLAYER = 2;
            GLOBALS.Singleton.MP_MODE = 1;
        }
        //Disconnect ()

    }

     


    //====================== SEND RPCS =============================
    public void send_my_words_already_sorted_list()
    {
        if (GLOBALS.Singleton.MP_PLAYER == 2)
        {
            Debug.Log("send_my_words(): I am not the host, sending my wordList");
            string tempWords = PlayerPrefs.GetString("WordsAlreadySorted");
            ScenePhotonView.RPC("get_word_list_OP_lobby", PhotonTargets.Others, tempWords);
        }

    }
    /*public void send_player_info_lobby()
    {
        Debug.Log("send_player_info()");
        
        if (GLOBALS.Singleton.MP_PLAYER == 1)
        {

    

            Debug.Log("send_player_info(): I am host, generating a anagram... | WORD ID SENT: ");

            ScenePhotonView.RPC("get_player_info_lobby", PhotonTargets.Others, word_id);
        }


    }*/


    //======================= GET RPCS =============================
    [PunRPC]
    public void get_word_list_OP_lobby(string words)
    {
        if (GLOBALS.Singleton.MP_PLAYER == 1)
        {
            int word_id = 0;
            Debug.Log("get_word_list(info): I am the host, receiving word list and sorting");
            //Sort the word id
          
            word_id = wSort[0].sortWordAndReturnAnagramID(words);
            Debug.Log("Nao deu pau");
            GLOBALS.Singleton.ANAGRAM_ID = word_id;
            //Send to infeliz
            ScenePhotonView.RPC("receive_sorted_word", PhotonTargets.Others, word_id);

        }
    }

    [PunRPC]
    public void receive_sorted_word(int word_id)
    {
        Debug.Log("get_player(info)");
        if (GLOBALS.Singleton.MP_PLAYER == 2)
        {
            //Add sorted word to the list of sorted words
            wSort[0].addSortedWordOP(word_id);

            Debug.Log("get_player(info): I am not the host, sending a confirmation and loading next scene | WORD ID RECEIVED: " + word_id);
            ScenePhotonView.RPC("confirmation_received", PhotonTargets.Others, 0);
            PhotonNetwork.LoadLevel("GamePlay");
        }
    }

    [PunRPC]
    public void confirmation_received(int n)
    {
        Debug.Log("confirmation_received(): loading next scene");
        PhotonNetwork.LoadLevel("GamePlay");
    }


}
