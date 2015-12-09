using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public static TutorialController s;
    
    GameObject tempObject;
    public GameObject HUD;
    

    Full_Screen_Dialog[] fscreen;

    float tutorial1Timer;

    void Awake()
    {
        s = this;
    }

    // Use this for initialization
    void Start()
    {
        int firstGame = PlayerPrefs.GetInt("firstGame");

        if (firstGame == 0)
        {
            // PlayerPrefs.SetInt("firstGame", 1);
            tutorial1Timer = 2f;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (tutorial1Timer > 0)
        {
            tutorial1Timer -= Time.deltaTime;
            if (tutorial1Timer <= 0)
            {
                tutorial1();
                tutorial1Timer = 0;
            }

        }
    }

    void tutorial1()
    {
        GLOBALS.s.TUTORIAL_PHASE = 1;
        HUD.SetActive(false);
        //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/FullScreenDialog"));

        //tempObject.GetComponent<Full_Screen_Dialog>().act();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/FullScreenDialog"));
        MenusController.s.enterFromRight(tempObject, "FullScreenDialog", null);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", null);
        Invoke("createNextButton", 2);

    }

    void createNextButton()
    {
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/ArowNext"));
        MenusController.s.enterFromRight(tempObject, "ArowNext", null);
    }

    public void tutorial1Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 2;
        
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndDestroy();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", null);

        Invoke("createNextButton", 2);
    }

    public void tutorial1Phase2Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 3;
       
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        // fscreen[0].changeText();
        fscreen[0].closeAndReopen();

        Invoke("createNextButton", 2);
    }

    public void tutorial1Phase3Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 4;
        
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DemonList/DemonList"));
        MenusController.s.enterFromRight(tempObject, "DemonList", null);

        MenusController.s.bring2FrontZOrder("Satan", null);
        MenusController.s.bring2FrontZOrder("SmallScroll", null);

        Invoke("createNextButton", 2);
    }
    public void tutorial1Phase4Clicked()
    {
        MenusController.s.destroyMenu("Satan", null);
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndDestroy();
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SatanHand"));
        MenusController.s.addToGUIAndRepositeObject(tempObject, "SatanHand", null);

        // transform.GetComponent<ScrollRect>.
        //HUD.transform.GetComponent<Scrollbar>
        //gameObject.GetComponent<UIScrollView>();
    }
}
