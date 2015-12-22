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
                ActBT();
            }
            else
            {
                ActBT();
            }
        }


    }

    public override void ActBT()
    {
        base.ActBT();
        if(GLOBALS.s.TUTORIAL_PHASE == 1)
        {
            
            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.tutorial1Clicked();
        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 2)
        {

            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.tutorial1Phase2Clicked();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 3)
        {

            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.tutorial1Phase3Clicked();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 4)
        {

            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.tutorial1Phase4Clicked();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 6)
        {
            
            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.indicateBuildBT();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 7)
        {
            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.destroySelectPunisher();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 8)
        {

            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.destroySelectPunisher();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 10)
        {

            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.collectSoulPhase();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 12)
        {

            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.pressBuildBtConstructImp();

        }
        else if (GLOBALS.s.TUTORIAL_PHASE == 17)
        {
            MenusController.s.destroyMenu("ArowNext", null);
            TutorialController.s.realEndTutorial();

        }
    }


}