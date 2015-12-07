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
        Debug.Log("Destroinggggggggggggg");
        Destroy(gameObject);
        TutorialController.s.blabla();
    }


}