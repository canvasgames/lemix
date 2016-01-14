using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SatanHand : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GLOBALS.s.TUTORIAL_PHASE == 4 || GLOBALS.s.TUTORIAL_PHASE == 17)
            tutorialCollect();
            
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initHandSoulsTutorial()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localPosition = new Vector3(-316, 226, 0);

        tutorialSouls();
    }
    void tutorialCollect()
    {
       // transform.DOLocalMove(new Vector3(128, -112, 52), 0.7f).OnComplete(moveback);
        transform.DOLocalMove(new Vector3(112, -267, -200), 0.7f).OnComplete(moveback);
    }
    void moveback()
    {
        //transform.DOLocalMove(new Vector3(110, -87, 8.6f), 0.7f).OnComplete(tutorialCollect);
        transform.DOLocalMove(new Vector3(75, -227, -200), 0.7f).OnComplete(tutorialCollect);

    }


    void tutorialSouls()
    {
        transform.DOLocalMoveY(180, 0.7f).OnComplete(tutorialsSoulsBack);
    }
    void tutorialsSoulsBack()
    {
        transform.DOLocalMoveY(240, 0.7f).OnComplete(tutorialSouls);
    }

    void tutorialList()
    {
        transform.DOScaleX(1f, 1f).OnComplete(move);
    }
    void move()
    {

    }

    void destroyMe()
    {
        MenusController.s.destroyMenu("SatanHand", null);
        
    }

}
