using UnityEngine;
using System.Collections;
using DG.Tweening;

public class collectSouls : MonoBehaviour {
    public GameObject allObjects;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
        
        BE.SceneTown.instance.CapacityCheck();
        BE.SceneTown.Elixir.ChangeDelta((double)200);
        allObjects.transform.DOMoveY(-450,1f).OnComplete(destroy);
       
    }

    void destroy()
    {
        GLOBALS.s.TUTORIAL_OCCURING = false;
        MenusController.s.destroyMenu("WarningEnteringAnimation", null);
        MenusController.s.destroyMenu("TvStaticEnteringAnimation", null);
        MenusController.s.destroyMenu("TvEnteringAnimation", null);
        MenusController.s.destroyMenu("WarningEnteringAnimation", null);
        MenusController.s.destroyMenu("WarningEnteringAnimation", null);
        Application.UnloadLevel("CATastrophe");
    }
}
