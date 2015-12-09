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

    GameObject blueClash, redClash;
    // Use this for initialization
    void Start()
    {
        blueClash = GameObject.Find("hud_blue_bar_clash");
        redClash = GameObject.Find("hud_red_bar_clash");
    }

    // Update is called once per frame
    void Update()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        blueClash.transform.position = new Vector3((transform.position.x + renderer.bounds.size.x), transform.position.y, transform.position.z);
        redClash.transform.position = new Vector3((transform.position.x + renderer.bounds.size.x), transform.position.y, transform.position.z);
    }
}