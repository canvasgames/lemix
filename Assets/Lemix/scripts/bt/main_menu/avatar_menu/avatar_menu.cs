using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class avatar_menu : MonoBehaviour {
	avatar_menu[] avatars;
	avatar_main_menu[] mm_avatar;
	public int my_type;
	bool locked = true;
	bool selected = false;

	// Use this for initialization
	void OnEnable(){
		//transform.position = new Vector3(transform.position.x,transform.position.y,-44);
		if(GLOBALS.Singleton.AVATAR_TYPE == my_type)
		{
			this.transform.GetComponent<Image> ().color = Color.white;
			selected = true;
		}
		else
		{

			this.transform.GetComponent<Image> ().color = Color.gray;
		}

		if(GLOBALS.Singleton.MY_LVL >= my_type)
		{
			locked = false;
			if(my_type>1)
			{
				string tempName = "lvl_" +my_type.ToString() +"_unlocked";
				transform.GetComponent<Animator>().Play(tempName);
			}

		}
		else
		{
			if(my_type>1)
			{
				string tempName = "lvl_"+my_type.ToString() +"_locked";
				transform.GetComponent<Animator>().Play(tempName);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {


	}

	public void mouseDown()
	{
	
		if(locked == false)
		{

			int i;
			avatars  = FindObjectsOfType(typeof(avatar_menu)) as avatar_menu[];
			for(i=0; i<avatars.Length; i++)
			{
				avatars[i].unselect_me();
			}
			selected = true;
			this.transform.GetComponent<Image> ().color = Color.white;
			GLOBALS.Singleton.AVATAR_TYPE = my_type;
			mm_avatar =  FindObjectsOfType(typeof(avatar_main_menu)) as avatar_main_menu[];
			mm_avatar[0].changeAvatar(my_type);
		}

		
	}
	public void mouseOver() {

		this.transform.GetComponent<Image> ().color = Color.green;
		
	}
	
	public void mouseExits() {
		if(selected == true)
		{
			this.transform.GetComponent<Image> ().color = Color.white;
		}
		else
		{
			this.transform.GetComponent<Image> ().color = Color.gray;
		}
	}

	public void unselect_me()
	{
		this.transform.GetComponent<Image> ().color = Color.gray;
		selected = false;
	}

}
