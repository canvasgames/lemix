using UnityEngine;
using System.Collections;
using DG.Tweening;


public class floor : scenario_objects {

   // public int my_floor;
    public GameObject scoreInfo;
    public GameObject colliderPW;
    GameObject my_text;
    public bool pw_super_collided = false;

    // Use this for initialization
    void Start () {
        if (globals.s.GAME_STARTED == true)
        {

            if(my_floor > 5)
            {
                if (USER.s.BEST_SCORE == my_floor)
                {

                    GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                    obj.GetComponentInChildren<TextMesh>().text = "BEST";
                    obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
                    obj.GetComponentInChildren<Score_floor_txt>().my_type = 1;
                }
                else if (USER.s.DAY_SCORE == my_floor)
                {

                    GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                    obj.GetComponentInChildren<TextMesh>().text = "DAY";
                    obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
                    obj.GetComponentInChildren<Score_floor_txt>().my_type = 2;
                }
                else if (USER.s.LAST_SCORE == my_floor)
                {

                    GameObject obj = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);
                    obj.GetComponentInChildren<TextMesh>().text = "LAST";
                    obj.GetComponentInChildren<Score_floor_txt>().my_floor = my_floor;
                    obj.GetComponentInChildren<Score_floor_txt>().my_type = 3;
                }

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
        Debug.Log("user day score: " + USER.s.DAY_SCORE);
        if (already_blinked == false && USER.s.DAY_SCORE >= 5 )

        {
            //USER.s.LAST_SCORE + 1 == my_floor
            if (USER.s.DAY_SCORE + 1 == my_floor  || USER.s.BEST_SCORE + 1 == my_floor)
            {

                    int i;
                    scenario_objects[] allScenario = GameObject.FindObjectsOfType(typeof(scenario_objects)) as scenario_objects[];

                    for (i = 0; i < allScenario.Length; i++)
                    {
                        allScenario[i].try_blink(my_floor);
                    }


                if (USER.s.BEST_SCORE + 1 == my_floor)
                {
                    destroy_previous_score_create_new(1);
                }
                else if (USER.s.DAY_SCORE + 1 == my_floor)
                {
                    destroy_previous_score_create_new(2); 
                }
                else if (USER.s.LAST_SCORE + 1 == my_floor)
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
        my_text = (GameObject)Instantiate(scoreInfo, new Vector3(0, transform.position.y - 0.6f, transform.position.z), transform.rotation);

        if (score_type == 1)
        {
            my_text.GetComponentInChildren<TextMesh>().text = "BEST";
        }
        else if (score_type == 2)
        {
            my_text.GetComponentInChildren<TextMesh>().text = "DAY";
        }
        else if (score_type == 3)
        {
            my_text.GetComponentInChildren<TextMesh>().text = "LAST";
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

    public void activate_colider_super_pw(int floor_actual)
    {
        colliderPW.SetActive(true);
        colliderPW.GetComponent<Rigidbody2D>().isKinematic = false;

        if(my_floor > floor_actual && my_floor < floor_actual+6 && transform.position.x>=0)
        {
           objects_pool_controller.s.reposite_squares_floor_particle(0, transform.position.y);
        }
    }

    public void unactivate_colider_super_pw()
    {
        colliderPW.SetActive(false);
    }

    public void colidded_super_pw()
    {
        pw_super_collided = true;
        transform.GetComponent<SpriteRenderer>().enabled = false;
    }



    public void clear_flags_reposite()
    {

        if(my_text != null)
        {
            Destroy(my_text);   
        }


            pw_super_collided = false;
            transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void reaper_post_PW_super(int floor)
    {
        if(my_floor > floor)
        {
            pw_super_collided = false;
            transform.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
   /* void destroy_me_baby()
    {
        Destroy(gameObject);
    }*/
}
