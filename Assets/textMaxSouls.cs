﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class textMaxSouls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pulse()
    {
        transform.DOScale(new Vector3 (1.5f,1.5f,1.5f),0.7f).OnComplete(repulse);
    }

    void repulse()
    {
        transform.DOScale(new Vector3 (1.5f, 1.5f, 1.5f), 0.7f).OnComplete(pulse);
    }
}
