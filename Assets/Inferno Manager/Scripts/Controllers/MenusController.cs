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

    //Class to keep the menu list in the List menusOpened
    public class menusList
    {
        public GameObject menuObj;
        public string menuName;
    }


    #region Awake/Start/Update
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
    #endregion

    #region AddToGuiAndMenuList
    //Add to menu List and add to the GUI
    public void addToGUIAndRepositeObject(GameObject menu, string name)
    {
        
        float xPos, yPos;

        menusList tempLecture = new menusList();
        tempLecture.menuObj = menu;

        if(name != null)
            tempLecture.menuName = name;

        menusOpened.Add(tempLecture);
        GLOBALS.s.DIALOG_ALREADY_OPENED = true;

        //Copy the readed local position
        xPos = menu.transform.localPosition.x;
        yPos = menu.transform.localPosition.y;

        //Set the Canvas of GUI was parent
        menu.transform.SetParent(bigDaddy, false);

        //Set again the local position, now in the GUI
        menu.transform.localPosition = new Vector3(xPos, yPos, 0f);
    }
    #endregion

    #region DestroyMenu
    //Destroy the menu
    //Put a name if you want to destroy a menu by name
    //or the reference to menu if you want use a close button with the reference or something like that
    public void destroyMenu(string name , GameObject myMenu)
    {

        menusList menu2Destroy = null;

        if (name != "")
        {
            menu2Destroy = forEachFindName(name);
            if(menu2Destroy != null)
            { 
               //Destroy the Gameobject
               Destroy(menu2Destroy.menuObj);
               //Remove from list
               menusOpened.Remove(menu2Destroy);
            }
            else
            {
                Debug.Log("ERROR! CANT FIND THE MENU");
            }
        }

        else if(myMenu != null)
        {
            menu2Destroy = forEachFindTheMenuItself(myMenu);
            if (menu2Destroy.menuObj != null)
            {
                Debug.Log("Destroing the menu");
                
                Destroy(menu2Destroy.menuObj);

                menusOpened.Remove(menu2Destroy);
            }
            else
            {
                Debug.Log("ERROR! CANT FIND THE MENU");
            }
        }
        else
        {
            Debug.Log("ERROR! NO NAME OR MENU ADDED");
        }

        if(menusOpened.Count <= 0)
        {

            GLOBALS.s.DIALOG_ALREADY_OPENED = false;
        }
    }
    #endregion

    #region EnterFromLeftWithPunch
    //Make the menu enter from left
    //If newX and New Y is equals to 0, the menu will use the original position of prefab 
    public void enterFromLeft(GameObject menu, string name, float newX, float newY)
    {
        float xPos, yPos;
        addToGUIAndRepositeObject(menu, name);

        //Use the passed position
        if (newX != 0)
        {
            xPos = newX;
        }
        //Use the original position
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

        //Put out of the screen
        menu.transform.localPosition = new Vector3((xPos - Screen.width), yPos, 0f);
        //Move back to the screen and call punch at the end
        menu.transform.DOLocalMoveX(xPos, 0.5f).OnComplete(() => punchLeft(menu));
    }

    void punchLeft(GameObject menu)
    {
        transform.DOPunchPosition(new Vector3(2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion

    #region EnterFromRightWithPunch
    //Make the menu enter from right
    //If newX and New Y is equals to 0, the menu will use the original position of prefab 
    public void enterFromRight(GameObject menu, string name, float newX, float newY)
    {
        float xPos, yPos;
        addToGUIAndRepositeObject(menu, name);

        //Use the passed position
        if (newX != 0)
        {
            xPos = newX;
        }
        //Use the original position
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

        //Put out of the screen
        menu.transform.localPosition = new Vector3((xPos + Screen.width), yPos, 0f);
        //Move back to the screen and call punch at the end
        menu.transform.DOLocalMoveX(xPos, 0.5f).OnComplete(() => punchRight(menu));
    }

    void punchRight(GameObject menu)
    {
         transform.DOPunchPosition(new Vector3(-2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion

    #region EnterFromUpWithPunch
    //Make the menu enter from up
    //If newX and New Y is equals to 0, the menu will use the original position of prefab 
    public void enterFromUp(GameObject menu, string name, float newX, float newY, bool dont_screw_canvas = false)
    {
        float xPos, yPos;
        if (!dont_screw_canvas)
        {
            Debug.Log(" PUTTING OUT OF SCREEN");
            addToGUIAndRepositeObject(menu, name);
        }

        //Use the passed position
        if (newX != 0)
        {
            xPos = newX;
        }
        //Use the original position
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

       
        //Put out of the screen 
        menu.transform.localPosition = new Vector3(xPos, yPos + Screen.height, 0f);
        //Move back to the screen and call punch at the end
        menu.transform.DOLocalMoveY(yPos, 0.5f).OnComplete(() => punchUp(menu));
        
    }

    void punchUp(GameObject menu)
    {
        transform.DOPunchPosition(new Vector3(0f, 2f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion

    #region EnterFromDownWithPunch
    //Make the menu enter from up
    //If newX and New Y is equals to 0, the menu will use the original position of prefab 
    public void enterFromDown(GameObject menu, string name, float newX, float newY, bool dont_screw_canvas = false)
    {
        float xPos, yPos;
        if (!dont_screw_canvas)
        {
            addToGUIAndRepositeObject(menu, name);
        }
        //Use the passed position
        if (newX != 0)
        {
            xPos = newX;
        }
        //Use the original position
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

        if (!dont_screw_canvas) {
            //Put out of the screen
            menu.transform.localPosition = new Vector3(xPos, yPos - Screen.height, 0f);
            //Move back to the screen and call punch at the end
            menu.transform.DOLocalMoveY(yPos, 0.5f).OnComplete(() => punchDown(menu));
        }
    }

    void punchDown(GameObject menu)
    {
        transform.DOPunchPosition(new Vector3(0f, 2f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion        

    #region AdjustZOrder 
    //Just change the zorder of the menu. Call if you create a menu over the object
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
        else
        {
            Debug.Log("ERROR, NO NAME OR CLOSER ADDED");
        }
    }
    #endregion

    #region MoveRepositeTheMenuToNewPosition
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
    #endregion

    #region ForEachFindInTheListofMenus
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
    #endregion
}
