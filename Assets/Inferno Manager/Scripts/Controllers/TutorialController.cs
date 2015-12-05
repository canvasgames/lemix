using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
    public static TutorialController s;
    GameObject tempObject;
    public Transform bigDaddy;

    float tutorial1Timer;

    void Awake()
    {
        s = this;
    }

    // Use this for initialization
    void Start () {
        int firstGame = PlayerPrefs.GetInt("firstGame");
        Debug.Log(firstGame);

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

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/FullScreenDialog"));
        addToGUIAndRepositeObject(tempObject);
        tempObject.GetComponent<Full_Screen_Dialog>().act();

        tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
        addToGUIAndRepositeObject(tempObject);
        tempObject.GetComponent<Satan_HUD>().act();

    }

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

        Image[] images = appear. GetComponentsInChildren<Image>();
        
        foreach (Image myImg in images)
        {
            myImg.DOFade(1f, 2f);

        }
        
    }

    void addToGUIAndRepositeObject(GameObject menu)
    {
        //Copy thereated local position
        float xPos, yPos;
        xPos = menu.transform.localPosition.x;
        yPos = menu.transform.localPosition.y;

        //Set the Canvas of GUI was parent
        menu.transform.SetParent(bigDaddy);

        //Set again the local position, now in the GUI
        menu.transform.localPosition = new Vector3(xPos, yPos, 0f);
    }
}
