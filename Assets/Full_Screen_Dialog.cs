using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Full_Screen_Dialog : MonoBehaviour
{

    float xPos;
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void open()
    {

    }

    public void closeAndReopen()
    {

    }

    public void closeAndDestroy()
    {

        Destroy(gameObject);
    }

    public void changeText()
    {
        if (GLOBALS.s.TUTORIAL_PHASE == 3)
            GetComponentInChildren<Text>().text = "This is your personal HELL'S GATE. It brings dead souls from earth.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 4)
            GetComponentInChildren<Text>().text = "Aquire more souls to Level Up and be respected.";
    }
}