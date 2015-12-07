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
    public Transform bigDaddy;
    float xPos, yPos;
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
        Debug.Log("Adding " + name);
        menusList tempLecture = new menusList();
        tempLecture.menuObj = menu;

        if(name != null)
            tempLecture.menuName = name;

        if(close != null)
            tempLecture.myClose = close;

        menusOpened.Add(tempLecture);

        //Copy thereated local position
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
        if (name != "")
        {
            Debug.Log(menusOpened.Count() + " Tamanho menus controller");
            foreach (menusList theMenu in menusOpened)
            {
               // Debug.Log(theMenu.menuObj.name + " Eu existo, olia aqui");
               // Debug.Log(theMenu.menuName + " Meu nominho");
                if (theMenu.menuName == name)
                {
                    
                    if (theMenu.menuObj != null)
                    {
                        //Destroy
                        //menusOpened.Remove(theMenu);
                        Destroy(theMenu.menuObj);
                        Debug.Log("Menu Deleted");
                        //break;

                    }
                    else
                    {
                       // menusOpened.Remove(theMenu);
                        Debug.Log("Menu Already Deleted");
                        //break;
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
                        menusOpened.Remove(theMenu);

                    }
                    else
                    {
                        menusOpened.Remove(theMenu);
                        Debug.Log("Menu Already Deleted");
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
        addToGUIAndRepositeObject(menu, name, myClose);
        xPos = menu.transform.position.x;
        menu.transform.position = new Vector3((xPos - 500f), menu.transform.position.y, 0f);
        menu.transform.DOMoveX(xPos, 1f).OnComplete(() => punchLeft(menu));
    }

    void punchLeft(GameObject menu)
    {
        transform.DOPunchPosition(new Vector3(2f, 0f, 0f), 0.5f, 7, 0.9f);
    }
    #endregion

    #region EnterFromRightWithPunch

    public void enterFromRight(GameObject menu, string name, GameObject myClose)
    {
        addToGUIAndRepositeObject(menu, name, myClose);
        xPos = menu.transform.position.x;
        menu.transform.position = new Vector3((xPos + 500f), menu.transform.position.y, 0f);
        menu.transform.DOMoveX(xPos, 1f).OnComplete(() => punchRight(menu));
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
}
