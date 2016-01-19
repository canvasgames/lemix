﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class DialogsTexts : MonoBehaviour
{
    public GameObject upPart, downPart, myText;
    float xPos, myTextHeight;
    string text_displaying = "";
    string text_final = "";
    int k = 0;
    string cur_color = "";
    Text text_component;
    // Use this for initialization
    void Start()
    {
        myTextHeight = myText.GetComponent<RectTransform>().rect.height;
        transform.localScale = new Vector3(1, 0, 1);
        Invoke("open", 0.3f);

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
  
        MenusController.s.destroyMenu("", transform.parent.gameObject);
    }
    public void changeText()
    {
        string text_to_display = "";

        if (GLOBALS.s.TUTORIAL_PHASE == 1)
            text_to_display = "Hi, I'm <color=#fe2323>Satan!</color>\n You've been promoted to \n<color=#fe2323>DEMON LORD!</color>";
            //text_to_display = "Hi, I'm Satan!  \n You've been promoted to DEMON LORD!";
        else if (GLOBALS.s.TUTORIAL_PHASE == 2)
            text_to_display = "You're now in charge of\nthis area of the Hell.\nThat's your Palace.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 3)
            text_to_display = "This is your personal\n<color=#fe2323>Hell's Gate</color>. It brings\ndead souls from earth.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 4)
            text_to_display = "Tap to Collect Souls.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 5)
            text_to_display = "It seems we have nowhere to place the souls..\nWhat's your suggestion?";
        else if (GLOBALS.s.TUTORIAL_PHASE == 6)
            text_to_display = "For Antichrist sake!!!\nWhy did I promoted you?\nThey deserve only  <color=#fe2323>ETERNAL PUNISHMENT!</color>";
        //
        else if (GLOBALS.s.TUTORIAL_PHASE == 7)
            text_to_display = "Now lets punish this sinner souls.\nTap the <color=green>Build Button</color>.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 8)
            text_to_display = "Select one of the <color=#fe2323>Punisher Buldings!</color>";
        // text_to_display = "Hold the finger over the building and drag to replace it and Confirm.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 9)
            text_to_display = "Place it and confirm";
        // text_to_display = "Tap to Collect Sadness.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 10)
            text_to_display = "<color=#fe2323>Punisher Buildings</color> increases your <color=#fe2323>Souls Capacity</color>";
        // text_to_display = "Great! Now your Hell Gate can Generate Souls! As higher your sadness meter, more souls will be sent to your Hell!";
        else if (GLOBALS.s.TUTORIAL_PHASE == 11)
            text_to_display = "Tap to Collect Souls.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 12)
            text_to_display = "Why the <color=blue>heaven</color> are you celebrating?\n You barely started!\n There is still a long path to get to be someone other than this <color=blue>bag of holiness</color> you are!";
        //text_to_display = "Aquire more souls to Level Up and be respected.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 14)
            text_to_display = "To construct more Punisher Buildings you need <color=#fe2323>HELLFIRE!</color> Tap to construct a Fire Mine.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 15)
            text_to_display = "Now Select the <color=#fe2323>Fire Mine</color>";
        else if (GLOBALS.s.TUTORIAL_PHASE == 16)
            text_to_display = "Place it and confirm";
        else if (GLOBALS.s.TUTORIAL_PHASE == 17)
            text_to_display = "Tap to collect Hellfire";
        else if (GLOBALS.s.TUTORIAL_PHASE == 18)
            text_to_display = "That's it <color=blue>Mr. Holiness</color>!\n\n Keep building and upgrading your buildings! Acquire souls to Level Up and be respected. \n\n  I will check your progress later.";
        else if (GLOBALS.s.TUTORIAL_PHASE == 21)
            text_to_display = "Select the Resources Tab";
        else if (GLOBALS.s.TUTORIAL_PHASE == 25)
            text_to_display = "Click on your Rank to see the Demons Rank List";
        // myText.GetComponentInChildren<Text>().text = "Good job building yourself an Army! Now give me 50% of them to my next Heaven's Raid.";
        //letter by letter code
        k = 0;
        text_displaying = "";
        text_final = text_to_display;
        myText.GetComponentInChildren<Text>().text = "";
        cur_color = "";
        text_component = myText.GetComponentInChildren<Text>();
        Invoke("display_text", 0.2f);
    }

    public void display_text()
    {
         
        string str = "";
        
        int i = 0;
        //if (string.Equals("a", "a") )Debug.Log("AAAAAAAAAAAAAAA");
        if (k < text_final.Length)
        {
            if (string.Equals(text_final[k].ToString(), "<") == false)
            {
                //Debug.Log("CUR CHAR: " + text_final[k]);
                text_displaying += text_final[k++];
            }
            else
            {
                i = 0;
                if (string.Equals(cur_color, "")) {
                    //Debug.Log(" < SYMBOL!!!!!!!! ");
                    do
                    {
                        cur_color += text_final[k + i];
                        i++;
                       // Debug.Log("< CUR CHAR: " + text_final[k+i] + " CUR COLOR: " +cur_color);
                    } while (!string.Equals(text_final[k+i].ToString(), ">"));

                    k += i;
                    if(k < text_final.Length)
                        text_displaying += cur_color + text_final[k++];
                }
                else
                {
                    //Debug.Log(" > SYMBOL!");

                    do
                    {
                        i++;
                    } while (!string.Equals(text_final[k+i].ToString(), ">"));

                    k += i+1;
                    text_displaying += "</color>";

                    if (k < text_final.Length)
                        text_displaying += text_final[k++];
                    cur_color = "";
                }
            }

            if(string.Equals(cur_color, ""))
                text_component.text = text_displaying;
            else
                text_component.text = text_displaying + "</color>";

            if (k < text_final.Length)
            {
                if (string.Equals(text_final[k-1].ToString(), "!") || string.Equals(text_final[k-1].ToString(), ".") || string.Equals(text_final[k-1].ToString(), "?"))
                    Invoke("display_text", 0.25f);
                else
                    Invoke("display_text", 0.02f);
            }
            //Invoke("display_text", 0.1f);


        }
    }


}