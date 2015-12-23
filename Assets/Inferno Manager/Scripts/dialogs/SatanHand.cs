using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SatanHand : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GLOBALS.s.TUTORIAL_PHASE == 4)
            tutorialCollect();
        if (GLOBALS.s.TUTORIAL_PHASE == 10)
        {
            
        }
            
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initHandSoulsTutorial()
    {
        Debug.Log("bbbbbb");
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.localPosition = new Vector3(-437, 214, 0);
        tutorialSouls();
    }
    void tutorialCollect()
    {
        transform.DOLocalMove(new Vector3(128, -112, 52), 0.7f).OnComplete(moveback);
    }
    void moveback()
    {
        transform.DOLocalMove(new Vector3(110, -87, 8.6f), 0.7f).OnComplete(tutorialCollect);
    }


    void tutorialSouls()
    {
        transform.DOLocalMoveY(160, 0.7f).OnComplete(tutorialsSoulsBack);
    }
    void tutorialsSoulsBack()
    {
        transform.DOLocalMoveY(214, 0.7f).OnComplete(tutorialSouls);
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
