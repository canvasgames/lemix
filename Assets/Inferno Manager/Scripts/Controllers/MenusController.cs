using UnityEngine;
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
        menu.transform.SetParent(bigDaddy);

        //Set again the local position, now in the GUI
        menu.transform.localPosition = new Vector3(xPos, yPos, 0f);

    }
    #endregion

    public void destroyMenu(string name, GameObject closeClicked)
    {

        menusList menu2Destroy;

        if (name != "")
        {
            menu2Destroy = forEachFindName(name);
            if(menu2Destroy != null)
                Destroy(menu2Destroy.menuObj);
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
        else
        {
            Debug.Log("ERROR, NO NAME OR CLOSER ADDED");
        }
    }
    #region EnterFromLeftWithPunch
    public void enterFromLeft(GameObject menu, string name, GameObject myClose)
    {
        float xPos;
        addToGUIAndRepositeObject(menu, name, myClose);
        xPos = menu.transform.position.x;
        menu.transform.position = new Vector3((xPos - Screen.width/2), menu.transform.position.y, 0f);
        menu.transform.DOMoveX(xPos, 0.5f).OnComplete(() => punchLeft(menu));
    }

    void punchLeft(GameObject menu)
    {
        transform.DOPunchPosition(new Vector3(2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion

    #region EnterFromRightWithPunch

    /// <summary>
    /// New X and New Y if you want 2 change the original position
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="name"></param>
    /// <param name="myClose"></param>
    /// <param name="newX"></param>
    /// <param name="newY"></param>
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
            xPos = menu.transform.position.x;
        }

        if (newY != 0)
        {
            yPos = newY;
        }
        else
        {
            yPos = menu.transform.position.y;
        }
        menu.transform.position = new Vector3((xPos + Screen.width / 2), yPos, 0f);
        menu.transform.DOMoveX(xPos, 0.5f).OnComplete(() => punchRight(menu));
    }

    public void repositeMenu(string name, GameObject closeClicked, float newXpos, float NewYpos)
    {
        menusList menu2Move;
        Debug.Log("called");
        if (name != "")
        {
            Debug.Log("moving");
            menu2Move = forEachFindName(name);
            menu2Move.menuObj.transform.DOMove(new Vector3(newXpos, NewYpos, 0f), 0.5f);
        }
        else
        {

        }
        
    }

    void punchRight(GameObject menu)
    {
        transform.DOPunchPosition(new Vector3(-2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion

    void desappear(GameObject desappear)
    {

        Image[] images = desappear.GetComponentsInChildren<Image>();

        foreach (Image myImg in images)
        {
            myImg.DOFade(0f, 0f);

        }
        appear(desappear, 1f);

    }

    void appear(GameObject appear, float time)
    {

        Image[] images = appear.GetComponentsInChildren<Image>();

        foreach (Image myImg in images)
        {
            myImg.DOFade(1f, 2f);

        }

    }

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
                        theMenu.menuObj.transform.SetParent(police);
                        theMenu.menuObj.transform.SetParent(bigDaddy);
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

    menusList forEachFindName(string name2Find)
    {
        Debug.Log("Chamando de novo");
        foreach (menusList theMenu in menusOpened)
        {
            // Debug.Log(theMenu.menuName + " Meu nominho");
            if (theMenu.menuName == name2Find)
            {
                //Find empty references and destroy
                if (theMenu.menuObj != null)
                {
                    //Destroy
                    Debug.Log(theMenu);
                    return theMenu;
                }
                else
                {
                    Debug.Log("Achei o safado vazio");
                    menusOpened.Remove(theMenu);
                    forEachFindName (name2Find);
                    break;
                    Debug.Log("ALALLALA de novo");
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