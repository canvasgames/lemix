using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class scrollOption : ButtonCap
{

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
        TutorialController.s.questionAnswered();
    }
}
