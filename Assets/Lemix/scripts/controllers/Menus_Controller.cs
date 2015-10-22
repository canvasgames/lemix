using UnityEngine;
using System.Collections;

public class Menus_Controller : MonoBehaviour {
	public GameObject rematch_menu, wait_title;
	GameObject wait;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void rematchMenu()
	{
		GameObject rematch = (GameObject)Instantiate (rematch_menu, new Vector3 (0,0 , 100), transform.rotation);

	}

	public void waiting ()
	{
		wait = (GameObject)Instantiate (wait_title, new Vector3 (0,0 , 100), transform.rotation);

	}
	public void destructWaiting()
	{
		Destroy(wait,3f);
	}
}
