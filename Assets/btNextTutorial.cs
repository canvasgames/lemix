using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class btNextTutorial : ButtonCap
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaa");
                //ActBT();
            }
            else
            {
                Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbb");
               // ActBT();
            }
        }


    }

    public override void ActBT()
    {
        base.ActBT();
        if(GLOBALS.s.TUTORIAL_PHASE == 1)
        {
            Full_Screen_Dialog[] fscreen;
            fscreen = GameObject.FindObjectsOfType(typeof(Full_Screen_Dialog)) as Full_Screen_Dialog[];
            fscreen[0].closeAndDestroy();
            
            Destroy(transform.gameObject);
            TutorialController.s.tutorial1Clicked();
        }

    }


}