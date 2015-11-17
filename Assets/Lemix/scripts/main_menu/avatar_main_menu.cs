using UnityEngine;
using System.Collections;

public class avatar_main_menu : MonoBehaviour {
	public GameObject avatarMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clicked()
	{

		
		avatarMenu.SetActive(true);


	}

	public void changeAvatar(int type)
	{
		transform.GetComponent<Animator>().Play("lvl_"+type);
	}
}
