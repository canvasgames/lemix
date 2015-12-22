using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SatanHand : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GLOBALS.s.TUTORIAL_PHASE == 4)
            tutorialCollect();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void tutorialCollect()
    {
        transform.DOLocalMove(new Vector3(128, -112, 52), 0.7f).OnComplete(moveback);
    }
    void moveback()
    {
        transform.DOLocalMove(new Vector3(110, -87, 8.6f), 0.7f).OnComplete(tutorialCollect);
    }




    void tutorialList()
    {
        transform.DOScaleX(1f, 1f).OnComplete(move);
    }
    void move()
    {
        CreateDemonsScrollView[] list;
        list = GameObject.FindObjectsOfType(typeof(CreateDemonsScrollView)) as CreateDemonsScrollView[];
        list[0].moveList();
        transform.DOLocalMoveY(131, 1.5f).OnComplete(destroyMe);
    }

    void destroyMe()
    {
        MenusController.s.destroyMenu("SatanHand", null);
        
    }

}
