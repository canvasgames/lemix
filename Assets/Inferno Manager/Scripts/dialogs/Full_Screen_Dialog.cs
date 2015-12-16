﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class Full_Screen_Dialog : MonoBehaviour
{
    public GameObject upPart, downPart, myText;
    float xPos, myTextHeight;
    // Use this for initialization
    void Start()
    {
        myTextHeight = myText.GetComponent<RectTransform>().rect.height;
        transform.localScale = new Vector3(1, 0, 1);
        Invoke("open", 1);

    }

    // Update is called once per frame
    void Update()
    {
        upPart.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y , transform.localPosition.z);
        downPart.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (GetComponent<RectTransform>().rect.height * transform.localScale.y), transform.localPosition.z);
        myText.GetComponent<RectTransform>().sizeDelta = new Vector2 (myText.GetComponent<RectTransform>().sizeDelta.x, myTextHeight * transform.localScale.y);
    }

    public void open()
    {
        changeText();
        transform.DOScaleY(1f, 0.3f);

    }

    public void closeAndReopen()
    {
        transform.DOScaleY(0f, 0.3f).OnComplete(open); 
    }

    public void closeAndDestroy()
    {
        transform.DOScaleY(0f, 0.3f).OnComplete(destroy);
        
    }
    void destroy()
    {
  
        MenusController.s.destroyMenu("", null, transform.parent.gameObject);
    }
    public void changeText()
    {
        if (GLOBALS.s.TUTORIAL_PHASE == 3)
            myText.GetComponentInChildren<Text>().text = "This is your personal HELL'S GATE. It brings dead souls from earth.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 4)
            myText.GetComponentInChildren<Text>().text = "Aquire more souls to Level Up and be respected.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 6)
            myText.GetComponentInChildren<Text>().text = "Now let's punish this sinner souls. Tap the BUILD Button.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 7)
            myText.GetComponentInChildren<Text>().text = "Select a PUNISHER BUILDING.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 8)
            myText.GetComponentInChildren<Text>().text = "Hold the finger over the building to replace it and Confirm.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 9)
            myText.GetComponentInChildren<Text>().text = "Tap to Collect Sadness.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 10)
            myText.GetComponentInChildren<Text>().text = "Great! Now your Hell Gate can Generate Souls! As higher your sadness meter, more souls will be sent to your Hell!";
        else if (GLOBALS.s.TUTORIAL_PHASE == 11)
            myText.GetComponentInChildren<Text>().text = "Tap to Collect Souls.";

        // myText.GetComponentInChildren<Text>().text = "That's it! Now keep building and upgrading your buildings!";

            // myText.GetComponentInChildren<Text>().text = "Good job building yourself an Army! Now give me 50% of them to my next Heaven's Raid.";

            // myText.GetComponentInChildren<Text>().text = "I dont't think you understand who I Am! Just give me 60% instead 3=)";
    }


}