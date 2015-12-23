using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public static TutorialController s;
    
    GameObject tempObject;
    public GameObject HUD, explosion;

    BE.Building[] buildings;
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
            GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;
            GLOBALS.s.TUTORIAL_OCCURING = true;
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

    #region Tutorial 01
    //Hi i'm Satan msg
    void tutorial1()
    {
        GLOBALS.s.TUTORIAL_PHASE = 1;
        HUD.SetActive(false);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", 0,0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Welcome"));
        MenusController.s.enterFromLeft(tempObject, "Welcome", 0, 0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan",0,0);
        Invoke("createNextButton", 2);

    }


    //Constrcut the Town Hall
    public void tutorial1Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 2;
        MenusController.s.destroyMenu("Welcome", null);
        // fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        //fscreen[0].closeAndDestroy();

        // tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        // MenusController.s.enterFromRight(tempObject, "SmallScroll",0,0);

        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        // fscreen[0].changeText();
        fscreen[0].closeAndReopen();

        tempObject = (GameObject)Instantiate(explosion);

        Invoke("createBuilding", 0.3f);
        Invoke("createNextButton", 2);
    }

    void createBuilding()
    {
        BE.SceneTown.instance.createTownHownTutorial();
    }

    //Create Hells Gate
    public void tutorial1Phase2Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 3;
       
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
 
        Invoke("createGate", 0.3f);
        Invoke("createNextButton", 2);
    }

    void createGate()
    {
        BE.SceneTown.instance.createHellGateTutorial();
    }

    //Old: Open the demon lords list
    //Click to collect
    public void tutorial1Phase3Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 4;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
        HUD.SetActive(true);

        buildings = GameObject.FindObjectsOfType(typeof(BE.Building)) as BE.Building[];
        foreach (BE.Building element in buildings)
        {
            element.activateHandTutorialUI(4);
        }

        //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DemonList/DemonList"));
        //MenusController.s.enterFromRight(tempObject, "DemonList",0,0);

        //MenusController.s.bring2FrontZOrder("Satan", null);
        // MenusController.s.bring2FrontZOrder("SmallScroll", null);

        // Invoke("createNextButton", 2);
    }

    //Full of souls question, what to do?
    public void tutorial1Phase4Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 5;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;

        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndDestroy();

        buildings = GameObject.FindObjectsOfType(typeof(BE.Building)) as BE.Building[];
        foreach (BE.Building element in buildings)
        {
            element.unactivateHandTutorialUI(4);
        }

        Invoke("tutorial1Phase4ClickedPart2", 2);

    }

    void tutorial1Phase4ClickedPart2()
    {
        HUD.SetActive(false);
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/BigScrollQuestion"));
        MenusController.s.enterFromRight(tempObject, "BigScrollQuestion", 0, 0);

    }

    public void questionAnswered()
    {
        GLOBALS.s.TUTORIAL_PHASE = 6;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndDestroy();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", 0, 0);
        Invoke("createNextButton", 2);

    }

    //Indicate the build Bt
    public void indicateBuildBT()
    {
        GLOBALS.s.TUTORIAL_PHASE = 7;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
        MenusController.s.repositeMenu("SmallScroll", null, 252, 224);
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DownArrow"));
        MenusController.s.enterFromRight(tempObject, "DownArrow", 0, 0);
        HUD.SetActive(true);
         
    }

    //Clicked build bt
    public void clickedBuildBt()
    {
        GLOBALS.s.TUTORIAL_PHASE = 8;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("DownArrow", null);
        MenusController.s.repositeMenu("SmallScroll", null, 0f, 240f);
        //Invoke("createNextButton", 2);

    }

    //Place it building msg
    public void destroySelectPunisher()
    {
        GLOBALS.s.TUTORIAL_PHASE = 9;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();

    }

    //Tell to collect sad
    public void collectSadnessPhase()
    {
        GLOBALS.s.TUTORIAL_PHASE = 9;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();

    }

    //Great now you can collect souls msg
    public void sadnessCollected()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;
        GLOBALS.s.TUTORIAL_PHASE = 10;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
        Invoke("createNextButton", 1);
    }

    //Tell to collect souls
    public void collectSoulPhase()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        GLOBALS.s.TUTORIAL_PHASE = 11;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
    }

    //Souls collected
    public void soulsCollected()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;
        GLOBALS.s.TUTORIAL_PHASE = 12;
        MenusController.s.destroyMenu("SmallScroll", null);
        //RANK UP

        Invoke("createNextButton", 1);
    }

    //Construct imp pit msg, indicate to press build
    public void pressBuildBtConstructImp()
    {
        GLOBALS.s.TUTORIAL_PHASE = 13;
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", 252f, 224f);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", 0, 0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DownArrow"));
        MenusController.s.enterFromRight(tempObject, "DownArrow", 0, 0);
    }

    //Choose the imp pit
    public void pressBuildImpCasePressed()
    {
        GLOBALS.s.TUTORIAL_PHASE = 14;

        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("DownArrow", null);
        MenusController.s.repositeMenu("SmallScroll", null, 0f, 240f);
    }

    //Imp pit pressed, place it msg
    public void impClicked()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        GLOBALS.s.TUTORIAL_PHASE = 15;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
    }

    //Collect demons
    public void collectDemonsPhase()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;
        GLOBALS.s.TUTORIAL_PHASE = 16;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
    }

    //That's it, end of tutorial
    public void endOfTutorial()
    {
        GLOBALS.s.TUTORIAL_PHASE = 17;

        MenusController.s.destroyMenu("SmallScroll", null);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/FullScreenDialog"));
        MenusController.s.enterFromRight(tempObject, "FullScreenDialog", 0, 0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", 0, 0);

        Invoke("createNextButton", 1);
    }


    public void realEndTutorial()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        GLOBALS.s.TUTORIAL_PHASE = 0;
        GLOBALS.s.TUTORIAL_OCCURING = false;

        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("FullScreenDialog", null);

        // PlayerPrefs.SetInt("firstGame", 1);
    }
    #endregion

    //Create the arrow
    void createNextButton()
    {

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/ArowNext"));
        MenusController.s.enterFromRight(tempObject, "ArowNext", 0, 0);
    }
}
