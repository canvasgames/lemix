using UnityEngine;
using System.Collections;

public class CloseBT : MonoBehaviour {

    public GameObject myFather;
    public bool waitToClose = false;

	// Use this for initialization
	void Start () {
	    if(waitToClose == true)
        {
            Invoke("canCloseNow", 2f);
        }
	}
	
    void canCloseNow()
    {
        waitToClose = false;
    }
	// Update is called once per frame
	void Update () {
	    
	}

    public void clicked()
    {
        if(waitToClose == false)
        {
            if (GLOBALS.s.TUTORIAL_PHASE == 11)
            {
                TutorialController.s.blablaQuemEhVcNaFilaDoPao();
            }
            destroyMenu();
        }

    }

    public void destroyMenu()
    {

        MenusController.s.destroyMenu("", myFather);
    }
}
