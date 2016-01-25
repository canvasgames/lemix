﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public static TutorialController s;
    
    GameObject tempObject;
    public GameObject HUD, explosion;

    BE.Building[] buildings;
    DialogsTexts[] fscreen;

    float tutorial1Timer;

    void Awake()
    {
        s = this;
    }

    #region Start And First Tutorial Flag
    void Start()
    {

        int firstGame = PlayerPrefs.GetInt("firstGame");
		
        if (QA.s.NoTutorial == true) firstGame = 2;

 
        if (firstGame == 0)
        {
            HUD.SetActive(false);
            GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;
            GLOBALS.s.TUTORIAL_OCCURING     = true;
            SatanController.s.start_entering(1.4f);
            //tutorial1Timer = 2f;
            // 

        }
        else
        {
            BE.SceneTown.instance.createTownHownTutorial();
            createGate();
            BE.SceneTown.instance.CapacityCheck();
            BE.SceneTown.Gold.ChangeDelta((double)200);

        }
        //
        #endregion
    }

    #region Update and Timers
    // Update is called once per frame
    void Update()
    {
    }
    #endregion

    #region Tutorial Phase 1 Welcome

    public void startTutorial()
    {
        Invoke("tutorial1", 1f);
    }
    //Hi i'm Satan msg
    public void tutorial1()
    {
        //createBuilding();
    
            GLOBALS.s.TUTORIAL_PHASE = 1;
        Debug.Log("TUTORIAL PHASE 1");
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Welcome"));
        MenusController.s.enterFromUp(tempObject, "Welcome", 0, 0);
        Invoke("tutorial1EnterOtherStuff", 0.5f);
       // */
    }

    public void tutorial1EnterOtherStuff()
    {
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", 0, 0);

        //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SatanProud"));
        MenusController.s.enterFromLeft(tempObject, "Satan", 0, 0);
        Invoke("createNextButton", 2f);
    }

    public void tutorial1Clicked101()
    {
        Debug.Log("TUTORIAL PHASE 101");
        GLOBALS.s.TUTORIAL_PHASE = 101;
        MenusController.s.destroyMenu("Welcome", null);

        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
    
        Invoke("createNextButton", 2);
    }
    #endregion

    #region Tutorial Phase 2 Create Town Hall
    //Constrcut the Town Hall
    public void tutorial1Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 2;;

        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
        
        Invoke("createBuilding", 0.3f);
        Invoke("createNextButton", 2);
    }

    void createBuilding()
    {
        BE.SceneTown.instance.createTownHownTutorial();
    }
    #endregion

    #region Tutorial Phase 3 Create Town Hall
    //Create Hells Gate
    public void tutorial1Phase2Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 3;
        BE.SceneTown.instance.CapacityCheck();
        BE.SceneTown.Gold.ChangeDelta((double)200);
        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();

        Invoke("createGate", 0.3f);
        Invoke("createNextButton", 2);
        
    }

    void createGate()
    {
        BE.SceneTown.instance.createHellGateTutorial();
    }
    #endregion

    #region Tutorial Phase 4 Collect
    //Click to collect
    public void tutorial1Phase3Clicked()
    {
        Debug.Log("Tutorial phase 4: Tap to collect souls");
        GLOBALS.s.TUTORIAL_PHASE = 4;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
        HUD.SetActive(true);
        
        buildings = GameObject.FindObjectsOfType(typeof(BE.Building)) as BE.Building[];
        foreach (BE.Building element in buildings)
        {
            element.activateHandTutorialUI(4);
        }
    }
    #endregion

    #region Tutorial Phase 5 Question
    //Full of souls question, what to do?
    public void tutorial1Phase4Clicked()
    {
        Debug.Log("Tut phase 5: Display the question");

        GLOBALS.s.TUTORIAL_PHASE = 5;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;

        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
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
    #endregion

    #region Tutorial Phase 6 Blablabla
    //Chicachicabum, começa e não para. Avisa todo mundo que meu nome é Sara
    public void questionAnswered()
    {
        Debug.Log("[TUT] 6 - BLA BLA ");
        GLOBALS.s.TUTORIAL_PHASE = 6;
        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndDestroy();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", 0, 0);
        Invoke("createNextButton", 2);

    }
    #endregion

    #region Tutorial Phase 7 Indicate The Build Bt
    //Indicate the build Bt
    public void indicateBuildBT()
    {
        Debug.Log("[TUT] 7 TOUCH THE BUILD BUTTON | tutorial occuring: " + GLOBALS.s.TUTORIAL_OCCURING);

        GLOBALS.s.TUTORIAL_PHASE = 7;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
        MenusController.s.repositeMenu("SmallScroll", null, 252, 224);
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DownArrow"));
        MenusController.s.enterFromRight(tempObject, "DownArrow", 0, 0);
        HUD.SetActive(true);
         
    }
    #endregion

    #region Tutorial Phase 8 Choose a Building
    //Clicked build bt
    public void clickedBuildBt()
    {
        Debug.Log("[TUT] 8 SELECT A BUILDING");
        GLOBALS.s.TUTORIAL_PHASE = 8;
        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("DownArrow", null);
        MenusController.s.repositeMenu("SmallScroll", null, 0f, 240f);
        //Invoke("createNextButton", 2);

    }
    #endregion

    #region Tutorial Phase 9 Place The Building
    //Place it building msg
    public void destroySelectPunisher()
    {
        GLOBALS.s.TUTORIAL_PHASE = 9;
        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();

    }

    //Destroy small scroll
    public void destructSmallScroll()
    {
        MenusController.s.destroyMenu("SmallScroll", null);
    }
    #endregion

    #region Tutorial Phase 10 Punisher Capacity Explanation (blablabla)
    //Indicate souls HUD
    public void punisherCapacityExplanation()
    {
        GLOBALS.s.TUTORIAL_PHASE = 10;
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SatanHand"));
        MenusController.s.addToGUIAndRepositeObject(tempObject,"SatanHand");
        
        SatanHand script;
        script = (SatanHand)tempObject.GetComponent(typeof(SatanHand));
        script.initHandSoulsTutorial();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", -292f, -185f);

        //fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        // fscreen[0].closeAndReopen();
        // MenusController.s.repositeMenu("SmallScroll", null, -394f, -118f);

        Invoke("createNextButton", 2);

    }
    #endregion

    #region Chicken 

    public void start_chicken_tutorial()
    {
        GLOBALS.s.TUTORIAL_PHASE = -1;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;

        MenusController.s.destroyMenu("SatanHand", null);

        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
        MenusController.s.repositeMenu("SmallScroll", null, 0f, 240f);

        ChickenController.s.start_animation();
    }

    public void after_chicken_kicked()
    {
        Debug.Log("[TUT] -2 AFTER CHICKEN KICKED");
        GLOBALS.s.TUTORIAL_PHASE = -2;

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromLeft(tempObject, "SmallScroll", 0, 0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromRight(tempObject, "Satan", 0, 0);

        Invoke("createNextButton", 2);
    }

    #endregion

    #region Tutorial Phase 11 Collect Souls Again
    //Indicate to collect souls again
    public void collectSoulsAgain()
    {
        GLOBALS.s.TUTORIAL_PHASE = 11;
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;

        MenusController.s.destroyMenu("SatanHand", null);

        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
        //MenusController.s.repositeMenu("SmallScroll", null, -18f, -250f);

        buildings = GameObject.FindObjectsOfType(typeof(BE.Building)) as BE.Building[];
        foreach (BE.Building element in buildings)
        {
            element.activateHandTutorialUI(4);
        }
    }
    #endregion

    #region Tutorial Phase 11 (11 again, because of reasons) Souls Collected Level UP
    //Souls Collected
    public void soulReallyCollected()
    {
        GLOBALS.s.TUTORIAL_PHASE = 11;
        MenusController.s.destroyMenu("SmallScroll", null);
        MenusController.s.destroyMenu("Satan", null);

        GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;
       
        buildings = GameObject.FindObjectsOfType(typeof(BE.Building)) as BE.Building[];
        foreach (BE.Building element in buildings)
        {
            element.unactivateHandTutorialUI(4);
        }

        Invoke("StartLevelUpAnimation", 1.5f);
    }

    void StartLevelUpAnimation()
    {
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/LevelUp"));
        MenusController.s.apearAlphaCanvasGroup(tempObject, "LevelUp");
        BE.BEAudioManager.SoundPlay(10);

        Invoke("createNextButton", 2);
    }

    #endregion

    #region Tutorial Phase 12 Satan Talking Shit About You
    //Satan talking shit about you (voce é uma merda garoto)
    public void blablaQuemEhVcNaFilaDoPao()
    {
        MenusController.s.destroyMenu("LevelUp", null);
        MenusController.s.destroyMenu("ArowNext", null);
        GLOBALS.s.TUTORIAL_PHASE = 12;

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/BigScroll"));
        MenusController.s.enterFromLeft(tempObject, "BigScroll", 0, 0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromRight(tempObject, "Satan", 0, 0);

        Invoke("createNextButton", 2);
    }
    #endregion

    #region Tutorial Phase 25 Click Rank HUD
    public void clickRankHUD()
    {
        HUD.SetActive(true);
        GLOBALS.s.TUTORIAL_PHASE = 25;
        MenusController.s.destroyMenu("BigScroll", null);
        
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", 238f, 80f);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SatanHand"));
        MenusController.s.addToGUIAndRepositeObject(tempObject, "SatanHand");

        SatanHand script;
        script = (SatanHand)tempObject.GetComponent(typeof(SatanHand));
        script.initRankTutorial();
    }
    #endregion

    #region Tutorial Phase 13 Show Rank List
    public void showRankList()
    {
        GLOBALS.s.TUTORIAL_PHASE = 13;
        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("SmallScroll", null);
        MenusController.s.destroyMenu("SatanHand", null);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DemonList/DemonList"));
        MenusController.s.enterFromLeft(tempObject, "DemonList", 0, 0);

        Invoke("autoMoveList", 2);

    }


    public void autoMoveList()
    {
        CreateDemonsScrollView[] list;
        list = GameObject.FindObjectsOfType(typeof(CreateDemonsScrollView)) as CreateDemonsScrollView[];
        list[0].moveList();
        Invoke("createNextButton", 3);


    }
    #endregion

    #region Tutorial Phase 14 Indicate Build Bt
    //Construct imp pit msg, indicate to press build
    public void pressBuildBtConstructImp()
    {
        MenusController.s.destroyMenu("SatanHand", null);
        MenusController.s.destroyMenu("DemonList", null);

        GLOBALS.s.TUTORIAL_PHASE = 14;
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", 252f, 224f);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", 0, 0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DownArrow"));
        MenusController.s.enterFromRight(tempObject, "DownArrow", 0, 0);
    }
    #endregion

    #region Tutorial Phase 21 Indicate Tab of Fire Mine
    //Indicate tab of fire mine
    public void indicateTabFireMine()
    {

        MenusController.s.destroyMenu("Satan", null);

        MenusController.s.repositeMenu("SmallScroll", null, 0f, 240f);
        MenusController.s.repositeMenu("DownArrow", null, -190f, -215f);

        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();

        GLOBALS.s.TUTORIAL_PHASE = 21;
    }
    #endregion

    #region Tutorial Phase 15 Choose Fire Mine
    //Clicked tab
    public void pressBuildImpCasePressed()
    {
   
        GLOBALS.s.TUTORIAL_PHASE = 15;

        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
        MenusController.s.destroyMenu("DownArrow", null);
    }
    #endregion

    #region Tutorial Phase 16 Place The Building
    //Imp pit pressed, place it msg
    public void impClicked()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        GLOBALS.s.TUTORIAL_PHASE = 16;
        fscreen = GameObject.FindObjectsOfType(typeof(DialogsTexts)) as DialogsTexts[];
        fscreen[0].closeAndReopen();
    }
    #endregion

    #region Tutorial Phase 17 Collect Fire
    //Collect fire
    public void collectDemonsPhase()
    {
       
        GLOBALS.s.TUTORIAL_PHASE = 17;
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", -18f, -250f);


        buildings = GameObject.FindObjectsOfType(typeof(BE.Building)) as BE.Building[];
        foreach (BE.Building element in buildings)
        {
            element.activateHandTutorialUI(3);
        }
    }
    #endregion

    #region Tutorial Phase 18 Satan Goodbye
    //That's it, end of tutorial
    public void endOfTutorial()
    {
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = true;
        GLOBALS.s.TUTORIAL_PHASE = 18;

        MenusController.s.destroyMenu("SmallScroll", null);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/BigScroll"));
        MenusController.s.enterFromRight(tempObject, "BigScroll", 0, 0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", 0, 0);

        buildings = GameObject.FindObjectsOfType(typeof(BE.Building)) as BE.Building[];
        foreach (BE.Building element in buildings)
        {
            element.unactivateHandTutorialUI(3);
        }

        BE.UIShop.instance.activateTabs();

        Invoke("createNextButton", 1);
    }
    #endregion

    #region Tutorial 1 End
    public void realEndTutorial()
    {
        Debug.Log("[TUT] REAL END TUTORIAL! ");
        GLOBALS.s.LOCK_CAMERA_TUTORIAL = false;
        GLOBALS.s.TUTORIAL_PHASE = 0;
        GLOBALS.s.TUTORIAL_OCCURING = false;

        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("BigScroll", null);

        // PlayerPrefs.SetInt("firstGame", 1);
    }
    #endregion

    #region Create Next Bt for Tutorial
    //Create the arrow
    void createNextButton()
    {

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/ArowNext"));
        MenusController.s.enterFromRight(tempObject, "ArowNext", 0, 0);
        if (GLOBALS.s.TUTORIAL_PHASE == 13)
        {
            Invoke("SatanHand", 0.7f);
        }
    }
    void SatanHand()
    {

            tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SatanHand"));
            MenusController.s.addToGUIAndRepositeObject(tempObject, "SatanHand");

            SatanHand script;
            script = (SatanHand)tempObject.GetComponent(typeof(SatanHand));
            script.tutorialList();
        
    }

#endregion
}
