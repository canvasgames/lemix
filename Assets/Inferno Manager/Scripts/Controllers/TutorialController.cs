using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public static TutorialController s;
    
    GameObject tempObject;
    public GameObject HUD, explosion;
    

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
        MenusController.s.enterFromRight(tempObject, "FullScreenDialog", null,0,0);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", null,0,0);
        Invoke("createNextButton", 2);

    }

    void createNextButton()
    {
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/ArowNext"));
        MenusController.s.enterFromRight(tempObject, "ArowNext", null,0,0);
    }

    public void tutorial1Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 2;
        
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndDestroy();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", null,0,0);
        
        tempObject = (GameObject)Instantiate(explosion);

        Invoke("createBuilding", 0.3f);
        Invoke("createNextButton", 2);
    }

    void createBuilding()
    {
        BE.SceneTown.instance.createTownHownTutorial();
    }

    public void tutorial1Phase2Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 3;
       
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        // fscreen[0].changeText();
        fscreen[0].closeAndReopen();
        tempObject = (GameObject)Instantiate(explosion,new Vector3(9f,0f,-1f),Quaternion.identity);
        Invoke("createGate", 0.3f);
        Invoke("createNextButton", 2);
    }

    void createGate()
    {
        BE.SceneTown.instance.createHellGateTutorial();
    }

    public void tutorial1Phase3Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 4;
        
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DemonList/DemonList"));
        MenusController.s.enterFromRight(tempObject, "DemonList", null,0,0);

        MenusController.s.bring2FrontZOrder("Satan", null);
        MenusController.s.bring2FrontZOrder("SmallScroll", null);

        Invoke("createNextButton", 2);
    }
    public void tutorial1Phase4Clicked()
    {
        GLOBALS.s.TUTORIAL_PHASE = 5;

        MenusController.s.destroyMenu("Satan", null, null);
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndDestroy();

        Invoke("tutorial1Phase4ClickedPart2", 1);

    }

    void tutorial1Phase4ClickedPart2()
    {
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SatanHand"));
        MenusController.s.addToGUIAndRepositeObject(tempObject, "SatanHand", null);
        Invoke("createNextButton", 3);

    }

    public void tutorialListOfDemosClosed()
    {
        GLOBALS.s.TUTORIAL_PHASE = 6;

        MenusController.s.destroyMenu("DemonList", null, null);
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", null, 252f, 224f);
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", null,0,0);
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DownArrow"));
        MenusController.s.enterFromRight(tempObject, "DownArrow", null, 0, 0);
        HUD.SetActive(true);
    }

    public void clickedBuildBt()
    {
        GLOBALS.s.TUTORIAL_PHASE = 7;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
        MenusController.s.destroyMenu("Satan", null, null);
        MenusController.s.destroyMenu("DownArrow", null, null);
        MenusController.s.repositeMenu("SmallScroll", null, 0f, 240f);
        //Invoke("createNextButton", 2);

    }

    public void destroySelectPunisher()
    {
        GLOBALS.s.TUTORIAL_PHASE = 8;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();

    }

    public void collectSadnessPhase()
    {
        GLOBALS.s.TUTORIAL_PHASE = 9;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();

    }

    public void sadnessCollected()
    {
        GLOBALS.s.TUTORIAL_PHASE = 10;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
        Invoke("createNextButton", 1);
    }

    public void collectSoulPhase()
    {
        GLOBALS.s.TUTORIAL_PHASE = 11;
        fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
        fscreen[0].closeAndReopen();
    }

    public void soulsCollected()
    {
        GLOBALS.s.TUTORIAL_PHASE = 12;
        MenusController.s.destroyMenu("SmallScroll", null, null);
        //RANK UP
    }
}
