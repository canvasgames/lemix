using UnityEngine;
using System.Collections;
using DG.Tweening;


public class floor : scenario_objects {

   // public int my_floor;
    public GameObject scoreInfo;
    public GameObject squaresUp;
    public GameObject squaresDown;
    public GameObject colliderPW;

    // Use this for initialization
    void Start () {
        if (globals.s.GAME_STARTED == true)
        {
            if (hud_controller.si.BEST_SCORE == my_floor)
            {

                GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                obj.GetComponentInChildren<TextMesh>().text = "BEST";
                obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
                obj.GetComponentInChildren<Score_floor_txt>().my_type = 1;
            }
            else if (hud_controller.si.DAY_SCORE == my_floor)
            {

                GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                obj.GetComponentInChildren<TextMesh>().text = "DAY";
                obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
                obj.GetComponentInChildren<Score_floor_txt>().my_type = 2;
            }
            else if (hud_controller.si.LAST_SCORE == my_floor)
            {

                GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                obj.GetComponentInChildren<TextMesh>().text = "LAST";
                obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
                obj.GetComponentInChildren<Score_floor_txt>().my_type = 3;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < globals.s.BALL_Y - globals.s.FLOOR_HEIGHT*4){
			//Destroy(gameObject);
		}
    }

    public void try_to_display_best_score()
    {
        if (already_blinked == false && hud_controller.si.DAY_SCORE >=5 )

        {
            //hud_controller.si.LAST_SCORE + 1 == my_floor
            if (hud_controller.si.DAY_SCORE + 1 == my_floor  || hud_controller.si.BEST_SCORE + 1 == my_floor)
            {

                    int i;
                    scenario_objects[] allScenario = GameObject.FindObjectsOfType(typeof(scenario_objects)) as scenario_objects[];

                    for (i = 0; i < allScenario.Length; i++)
                    {
                        allScenario[i].try_blink(my_floor);
                    }


                if (hud_controller.si.BEST_SCORE + 1 == my_floor)
                {
                    destroy_previous_score_create_new(1);
                }
                else if (hud_controller.si.DAY_SCORE + 1 == my_floor)
                {
                    destroy_previous_score_create_new(2); 
                }
                else if (hud_controller.si.LAST_SCORE + 1 == my_floor)
                {
                    destroy_previous_score_create_new(3);  
                }
            }
               
        }
    }

    //Under floor score destroy
    void destroy_previous_score_create_new(int score_type)
    {
        Score_floor_txt[] Score_txt = GameObject.FindObjectsOfType(typeof(Score_floor_txt)) as Score_floor_txt[];
        int i;

        for (i = 0; i < Score_txt.Length; i++)
        {
            Score_txt[i].try_destroy_me(my_floor - 1, score_type);
            Score_txt[i].try_destroy_me(my_floor, score_type);
        }

        create_score_text(score_type);

    }

    public void create_score_text(int score_type)
    {
        GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);

        if (score_type == 1)
        {
            obj.GetComponentInChildren<TextMesh>().text = "BEST";
        }
        else if (score_type == 2)
        {
            obj.GetComponentInChildren<TextMesh>().text = "DAY";
        }
        else if (score_type == 3)
        {
            obj.GetComponentInChildren<TextMesh>().text = "LAST";
        }
    }

    public void create_score_game_over (int floor, int type)
    {
        if(floor == my_floor)
        {
            create_score_text(type);
            try_blink(floor);
        }
    }
    public void activate_squares()
    {
        squaresDown.SetActive(true);
        squaresUp.SetActive(true);
        if (colliderPW != null)
        {
            colliderPW.SetActive(true);
        }
    }

    public void unactivate_squares()
    {
        squaresDown.SetActive(false);
        squaresUp.SetActive(false);
        if (colliderPW != null)
        {
            colliderPW.SetActive(false);
        }

    }

    public void destroy_pw_super_under_floors(float y_pos_ball)
    {
        if(colliderPW != null)
            colliderPW.SetActive(false);

        if (y_pos_ball > transform.position.y)
        {
            transform.DOScaleZ(1, 0.3f)
                //.OnComplete(destroy_me_baby);
        }
    }

   /* void destroy_me_baby()
    {
        Destroy(gameObject);
    }*/
}
