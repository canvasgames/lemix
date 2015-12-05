using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Full_Screen_Dialog : MonoBehaviour {

    float xPos;
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void act()
    {
        xPos = transform.position.x;
        transform.position = new Vector3((xPos + 500f), transform.position.y, 0f);
        gogogogogo();
    }

    void gogogogogo()
    {
        // 
        transform.DOMoveX(xPos, 1f).OnComplete(punch);
    }

    void punch()
    {
        transform.DOPunchPosition(new Vector3(-2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
}
