using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QA_PW_type : MonoBehaviour {
    public Text my_txt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        hud_controller.si.HUD_BUTTON_CLICKED = true;
        if(QA.s.COLLECTABLE_PW_TRUE_OR_JUMP_FALSE == true)
        {
            my_txt.text = "PW JUMPING";
            QA.s.COLLECTABLE_PW_TRUE_OR_JUMP_FALSE = false;
        }
        else
        {
            my_txt.text = "COLLECT PW";
            QA.s.COLLECTABLE_PW_TRUE_OR_JUMP_FALSE = true;
        }
            
    }
}
