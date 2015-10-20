using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Conecta ao Photon
		PhotonNetwork.ConnectUsingSettings("v4.2");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//Coisas para conectar
	public string stringToEdit = "Hello World";
	public TextMesh textmesh;
	private const string roomName = "Astolfo";
	private RoomInfo[] roomsList;
	void OnGUI()
	{
		if(PhotonNetwork.playerList.Length > 0)
		{
			//stringToEdit = PhotonNetwork.playerList[0].name;
		GUI.TextField(new Rect(90, 30, 200, 20), PhotonNetwork.playerList[0].name, 25);
			if(PhotonNetwork.playerList.Length > 1)
			{
				GUI.TextField(new Rect(90, 50, 200, 20), PhotonNetwork.playerList[1].name, 25);
				if(PhotonNetwork.playerList.Length > 2)
					GUI.TextField(new Rect(90, 70, 200, 20), PhotonNetwork.playerList[2].name, 25);
			}
		}
		//textmesh.text = stringToEdit;
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

	}

	void OnJoinedLobby()
	{

		PhotonNetwork.JoinRandomRoom();
		PhotonNetwork.playerName = "JAOSOASJAJ";
	
	}
	void OnJoinedRoom()
	{
		Debug.Log("Connected to Room");
	}
	void OnPhotonJoinFailed()
	{
		Debug.Log("Failed to Connect");
	}

	void OnCreatedRoom()
	{


	}
	void OnPhotonRandomJoinFailed()
	{

		Debug.Log("Failed to Connect Random");
		PhotonNetwork.CreateRoom(null);

	}


}
