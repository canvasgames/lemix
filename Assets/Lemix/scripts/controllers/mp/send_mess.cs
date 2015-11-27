using UnityEngine;
using System.Collections;

public class send_mess : Photon.MonoBehaviour {

	// Use this for initialization
	//private static PhotonView ScenePhotonView;
	//private static NetworkViewID nView;

	void Awake () {

		//ScenePhotonView = this.GetComponent<PhotonView>();
	}

	void Start () {

	}
	// Update is called once per frame
	void Update () {
	
	
	}

	void OnMouseDown (){
		Debug.Log("V IS BEING PRESSED!!");
		//ConstructTable[] constTab = FindObjectsOfType(typeof(ConstructTable)) as ConstructTable[];

		//NetworkViewID viewID = Network.AllocateViewID();
    
		//ScenePhotonView.RPC("ChatMessage", PhotonTargets.All, viewID, "OH MY GOD THEY KILLED KENNY");
	}


	/*[PunRPC]
	public void ChatMessage(NetworkViewID viewID, string a)
	{
		Debug.Log("ChatMessage: " + a);

	}*/

}
