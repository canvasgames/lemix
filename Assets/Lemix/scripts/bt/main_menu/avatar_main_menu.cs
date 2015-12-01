using UnityEngine;
using System.Collections;

public class avatar_main_menu : BtsGuiClick
{
	public GameObject avatarMenu;

    // Use this for initialization
    void Start() {

        if (GLOBALS.Singleton.AVATAR_TYPE != 0)
        {
            changeAvatar(GLOBALS.Singleton.AVATAR_TYPE);

        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void ActBT()
    {

		
		avatarMenu.SetActive(true);


	}

	public void changeAvatar(int type)
	{
		transform.GetComponent<Animator>().Play("lvl_"+type);
	}
}
