using UnityEngine;
using System.Collections;
using DG.Tweening;


public class floor : scenario_objects {

   // public int my_floor;
    public GameObject scoreInfo;


    // Use this for initialization
    void Start () {

        if (hud_controller.si.BEST_SCORE == my_floor)
        {

            GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
            obj.GetComponentInChildren<TextMesh>().text = "BEST";
        }
        else if (hud_controller.si.DAY_SCORE == my_floor)
        {

            GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
            obj.GetComponentInChildren<TextMesh>().text = "DAY";
        }
        else if (hud_controller.si.LAST_SCORE == my_floor)
        {

            GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
            obj.GetComponentInChildren<TextMesh>().text = "LAST";
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT*4){
			Destroy(gameObject);
		}
    }

    public void blink()
    {
        if (already_blinked == false)
        {
            if(hud_controller.si.DAY_SCORE +1 == my_floor || hud_controller.si.LAST_SCORE + 1 == my_floor || hud_controller.si.BEST_SCORE + 1 == my_floor)
            {

                int i;
                scenario_objects[] allScenario = GameObject.FindObjectsOfType(typeof(scenario_objects)) as scenario_objects[];

                for (i = 0; i < allScenario.Length; i++)
                {
                    allScenario[i].try_blink(my_floor);
                }
            }
               
        }
    }


}
