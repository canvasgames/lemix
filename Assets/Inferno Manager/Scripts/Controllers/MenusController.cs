﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

using DG.Tweening;
using UnityEngine.UI;

public class MenusController : MonoBehaviour {
    public static MenusController s;
    public Transform bigDaddy, police;
    public List<menusList> menusOpened = new List<menusList>();

    public class menusList
    {
        public GameObject menuObj;
        public string menuName;
        public GameObject myClose;
    }

    

    void Awake()
    {
        s = this;
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

    }

    #region AddToGuiAndMenuList
    //Add to menu List and GUI, Put a name if you want to destroy a menu by name 
    //or close to add a close button who is in charge to destroy the menu
    public void addToGUIAndRepositeObject(GameObject menu, string name, GameObject close)
    {
        float xPos, yPos;

        menusList tempLecture = new menusList();
        tempLecture.menuObj = menu;

        if(name != null)
            tempLecture.menuName = name;

        if(close != null)
            tempLecture.myClose = close;

        menusOpened.Add(tempLecture);

        //Copy the readed local position
        xPos = menu.transform.localPosition.x;
        yPos = menu.transform.localPosition.y;

        //Set the Canvas of GUI was parent
        menu.transform.SetParent(bigDaddy, false);

        //Set again the local position, now in the GUI
        menu.transform.localPosition = new Vector3(xPos, yPos, 0f);

    }
    #endregion

    public void destroyMenu(string name, GameObject closeClicked, GameObject myMenu)
    {

        menusList menu2Destroy = null;

        if (name != "")
        {
            menu2Destroy = forEachFindName(name);
            if(menu2Destroy != null)
            { 
               Destroy(menu2Destroy.menuObj);
                menusOpened.Remove(menu2Destroy);
            }
        }
        else if (closeClicked != null)
        {
            foreach (menusList theMenu in menusOpened)
            {
                if (theMenu.myClose == closeClicked)
                {
                    if (theMenu.menuObj != null)
                    {
                        //Destroy
                        Destroy(theMenu.menuObj);
                        menusOpened.Remove(theMenu);
                        break;

                    }
                    else
                    {
                        menusOpened.Remove(theMenu);
                        Debug.Log("Menu Already Deleted");
                        break;
                    }
                }
            }
        }
        else if(myMenu != null)
        {
            menu2Destroy = forEachFindTheMenuItself(myMenu);
            if (menu2Destroy.menuObj != null)
            {
                menusOpened.Remove(menu2Destroy);
                Destroy(menu2Destroy.menuObj);
            }
                
        }
        else
        {
            Debug.Log("ERROR, NO NAME OR CLOSER ADDED");
        }
    }

    #region EnterFromLeftWithPunch
    public void enterFromLeft(GameObject menu, string name, GameObject myClose, float newX, float newY)
    {
        float xPos, yPos;
        addToGUIAndRepositeObject(menu, name, myClose);



        if (newX != 0)
        {
            xPos = newX;
        }
        else
        {
            xPos = menu.transform.localPosition.x;
        }

        if (newY != 0)
        {
            yPos = newY;
        }
        else
        {
            yPos = menu.transform.localPosition.y;
        }


        menu.transform.localPosition = new Vector3((xPos - Screen.width), yPos, 0f);
        menu.transform.DOLocalMoveX(xPos, 0.5f).OnComplete(() => punchLeft(menu));
    }

    void punchLeft(GameObject menu)
    {
        transform.DOPunchPosition(new Vector3(2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion

    #region EnterFromRightWithPunch

    /// <summary>
    /// New X and New Y if you want 2 change the original position
    public void enterFromRight(GameObject menu, string name, GameObject myClose, float newX, float newY)
    {
        float xPos, yPos;
        addToGUIAndRepositeObject(menu, name, myClose);
        
        

        if(newX != 0)
        {
            xPos = newX;
        }
        else
        {
            xPos = menu.transform.localPosition.x;
        }

        if (newY != 0)
        {
            yPos = newY;
        }
        else
        {
            yPos = menu.transform.localPosition.y;
        }


        menu.transform.localPosition = new Vector3((xPos + Screen.width), yPos, 0f);
        menu.transform.DOLocalMoveX(xPos, 0.5f).OnComplete(() => punchRight(menu));
    }

    

    void punchRight(GameObject menu)
    {
         transform.DOPunchPosition(new Vector3(-2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion


    public void bring2FrontZOrder(string name, GameObject closeClicked)
    {
        if (name != "")
        {
            foreach (menusList theMenu in menusOpened)
            {
                if (theMenu.menuName == name)
                {

                    if (theMenu.menuObj != null)
                    {
                        //Bring to front
                        theMenu.menuObj.transform.SetParent(police, false);
                        theMenu.menuObj.transform.SetParent(bigDaddy, false);
                        break;

                    }
                }
            }
        }
        else if (closeClicked != null)
        {
            foreach (menusList theMenu in menusOpened)
            {
                if (theMenu.myClose == closeClicked)
                {
                    if (theMenu.menuObj != null)
                    {
                        //Destroy
                        theMenu.menuObj.transform.SetParent(bigDaddy);
                        break;

                    }
                }
            }
        }
        else
        {
            Debug.Log("ERROR, NO NAME OR CLOSER ADDED");
        }
    }

    public void repositeMenu(string name, GameObject closeClicked, float newXpos, float NewYpos)
    {
        menusList menu2Move;

        if (name != "")
        {
            menu2Move = forEachFindName(name);

            menu2Move.menuObj.transform.DOLocalMove(new Vector3(newXpos, NewYpos, 0f), 1.5f);
        }
        else
        {

        }

    }
    menusList forEachFindName(string name2Find)
    {
        foreach (menusList theMenu in menusOpened)
        {
            if (theMenu.menuName == name2Find)
            {
                if (theMenu.menuObj != null)
                {
                    return theMenu;
                }
            }
        }
        return null;
    }



    menusList forEachFindTheMenuItself(GameObject menu)
    {

        foreach (menusList theMenu in menusOpened)
        {
            // Debug.Log(theMenu.menuName + " Meu nominho");
            if (theMenu.menuObj == menu)
            {
                //Find empty references and destroy
                if (theMenu.menuObj != null)
                {
                    return theMenu;
                }

            }
        }
        return null;
    }
}


/*
            foreach (menusList theMenu in menusOpened)
            {
               // Debug.Log(theMenu.menuName + " Meu nominho");
                if (theMenu.menuName == name)
                {
                    
                    if (theMenu.menuObj != null)
                    {
                        //Destroy
                        menusOpened.Remove(theMenu);
                        Destroy(theMenu.menuObj);
                        break;

                    }
                    else
                    {
                       menusOpened.Remove(theMenu);
                        break;
                    }
                }*/