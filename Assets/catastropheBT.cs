using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class catastropheBT : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
        if (GLOBALS.s.TUTORIAL_OCCURING == false && GLOBALS.s.DIALOG_ALREADY_OPENED == false)
        {
            //Application.LoadLevelAdditive("CATastrophe");
            SceneManager.LoadSceneAsync("CATastrophe");
        }
        
    }
}
