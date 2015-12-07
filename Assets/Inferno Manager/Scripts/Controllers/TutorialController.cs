using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
    public static TutorialController s;
    GameObject tempObject; 
    public GameObject HUD;

    float tutorial1Timer;

    void Awake()
    {
        s = this;
    }

    // Use this for initialization
    void Start () {
        int firstGame = PlayerPrefs.GetInt("firstGame");

        if(firstGame == 0)
        {
           // PlayerPrefs.SetInt("firstGame", 1);
            tutorial1Timer = 2f;
        }
            
    }
	
	// Update is called once per frame
	void Update () {

        if (tutorial1Timer > 0)
        {
            tutorial1Timer -= Time.deltaTime;
            if(tutorial1Timer <= 0 )
            {
                tutorial1();
                tutorial1Timer = 0;
            }

        }
	}

    void tutorial1()
    {
        HUD.SetActive(false);
        //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/FullScreenDialog"));

        //tempObject.GetComponent<Full_Screen_Dialog>().act();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/FullScreenDialog"));
        MenusController.s.enterFromRight(tempObject, "FullScreenDialog", null);

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        MenusController.s.enterFromLeft(tempObject, "Satan", null);
        StartCoroutine(createNextButton(2f)); 

    }

    IEnumerator createNextButton(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GLOBALS.s.TUTORIAL_PHASE = 1;
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/ArowNext"));
        MenusController.s.enterFromRight(tempObject, "ArowNext", null);

    }

    public void blabla()
    {
        Invoke("LaunchProjectile", 2);
    }

    void LaunchProjectile()
    {
        MenusController.s.destroyMenu("FullScreenDialog", null);
    }
    public void tutorial1Clicked()
    {
        
        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
        MenusController.s.enterFromRight(tempObject, "SmallScroll", null);


    }

}
