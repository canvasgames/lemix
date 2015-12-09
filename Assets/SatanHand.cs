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
        transform.DOLocalMoveY(131, 2.6f);
    }
}
