using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SatanHand : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.DOScaleX(1f, 1f).OnComplete(move);

    }
	
	// Update is called once per frame
	void Update () {
	
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
        MenusController.s.destroyMenu("SatanHand",null);
        
    }

}
