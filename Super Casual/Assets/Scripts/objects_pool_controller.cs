using UnityEngine;
using System.Collections;
using DG.Tweening;

public class objects_pool_controller : MonoBehaviour {

	#region ======= Variables Declaration ========
    public static objects_pool_controller s;

    public GameObject note_trail_prefab, note_prefab, floor_prefab, double_spike_prefab, triple_spike_prefab, squares_floor_prefab, scores_floor_prefab;

	[HideInInspector] public GameObject[] note_pool;
	int note_pool_size = 10;
	int note_pool_actual_i = 0;

	[HideInInspector] public GameObject[] note_trail_pool;
	int note_trail_pool_size = 30;
	int note_trail_pool_actual_i = 0;

    [HideInInspector] public GameObject[] floor_pool;
    int floor_pool_size = 15;
    int floor_pool_actual_i = 0;

    [HideInInspector]public GameObject[] double_spikes_pool;
    int double_spikes_pool_size = 20;
    int double_spikes_pool_actual_i = 0;

    [HideInInspector]public GameObject[] triple_spikes_pool;
    int triple_spikes_pool_size = 20;
    int triple_spikes_pool_actual_i = 0;

    [HideInInspector]
    public GameObject[] squares_floor_pool;
    int squares_floor_pool_size = 9;
    int squares_floor_pool_actual_i = 0;

    [HideInInspector]
    public GameObject[] scores_floor_pool;
    int scores_floor_pool_size = 4;
    int scores_floor_pool_actual_i = 0;

	public GameObject[] bgs_pool;

	public GameObject[] bgs1_pool;
	int bgs1_actual_i = 0;

	public GameObject[] bgs2_pool;
	int bgs2_actual_i = 0;

	public GameObject[] bgs3_pool;
	int bgs3_actual_i = 0;

	public GameObject[] bgs4_pool;
	int bgs4_actual_i = 0;

	public GameObject[] bgs5_pool;
	int bgs5_actual_i = 0;

	public GameObject[] bgs_special_pool;
	int bgs_special_actual_i = 0;


	#endregion

	#region ===== INIT =====

    void Awake()
    {
        s = this;
		floor_pool = new GameObject[floor_pool_size];
		note_pool = new GameObject[note_pool_size];
        note_trail_pool = new GameObject[note_trail_pool_size];
        double_spikes_pool = new GameObject[double_spikes_pool_size];
        triple_spikes_pool = new GameObject[triple_spikes_pool_size];
        squares_floor_pool = new GameObject[squares_floor_pool_size];
        scores_floor_pool = new GameObject[scores_floor_pool_size];
        create_initial_tudo();
    }

	void Start () {
        
        //floor_skin_bg_glow();
	}


	void create_initial_tudo()
	{
		bgs1_actual_i = Random.Range (0, bgs1_pool.Length);
		bgs2_actual_i = Random.Range (0, bgs2_pool.Length);
		bgs3_actual_i = Random.Range (0, bgs3_pool.Length);

		int i;
		for(i=0; i<floor_pool_size; i++)
		{
			floor_pool[i] =  (GameObject)Instantiate(floor_prefab, new Vector3(55, 0, 0), transform.rotation);
		}
		for(i=0; i<note_pool_size; i++)
		{
			note_pool[i] =  (GameObject)Instantiate(note_prefab, new Vector3(55, 0, 0), transform.rotation);
		}

		for(i=0; i<note_trail_pool_size; i++)
		{
			note_trail_pool[i] =  (GameObject)Instantiate(note_trail_prefab, new Vector3(55, 0, 0), transform.rotation);
		}

		for (i = 0; i < double_spikes_pool_size; i++)
		{
			double_spikes_pool[i] = (GameObject)Instantiate(double_spike_prefab, new Vector3(55, 0, 0), transform.rotation);
		}
		for (i = 0; i < triple_spikes_pool_size; i++)
		{
			triple_spikes_pool[i] = (GameObject)Instantiate(triple_spike_prefab, new Vector3(55, 0, 0), transform.rotation);
		}

		for (i = 0; i < squares_floor_pool_size; i++)
		{
			squares_floor_pool[i] = (GameObject)Instantiate(squares_floor_prefab, new Vector3(105, 10*i, 0), transform.rotation);
		}


		for (i = 0; i < scores_floor_pool_size; i++)
		{
			scores_floor_pool[i] = (GameObject)Instantiate(scores_floor_prefab, new Vector3(105, 10 * i, 0), transform.rotation);
		}
	}


	void floor_skin_bg_glow() {

		float r = Random.Range(0f, 0.8f);
		float g = Random.Range(0f, 0.8f);
		float b = Random.Range(0f, 0.8f);
		float time = 1f;

		int i;
		for (i = 0; i < floor_pool_size; i++) {
			floor_pool[i].GetComponent<floor>().my_skin_bg.GetComponent<floor_skin_bg>().new_color(r, g, b, time/2);
		}

		Invoke("floor_skin_bg_glow", time);

		for (i = 0; i < double_spikes_pool_size; i++) {
			double_spikes_pool[i].GetComponent<scenario_objects>().new_color(r, g, b, time/2);
		}

	}

	#endregion

	public GameObject create_and_reposite_bg(int stage, float x_pos, float y_pos, bool special_wave = false){
		GameObject to_return = null;
		if (stage == 5) {
			bgs1_pool [bgs1_actual_i].transform.position = new Vector2 (x_pos, y_pos);
			to_return = bgs1_pool [bgs1_actual_i];

			bgs1_actual_i++;
			if (bgs1_actual_i >= bgs1_pool.Length)
				bgs1_actual_i = 0;

		} else if (stage == 2) {
			bgs2_pool [bgs2_actual_i].transform.position = new Vector2 (x_pos, y_pos);
			to_return = bgs2_pool [bgs2_actual_i];
			bgs2_actual_i++;
			if (bgs2_actual_i >= bgs2_pool.Length)
				bgs2_actual_i = 0;

		} else if (stage == 3) {
			bgs3_pool [bgs3_actual_i].transform.position = new Vector2 (x_pos , y_pos );
			to_return = bgs3_pool [bgs3_actual_i];
			bgs3_actual_i++;
			if (bgs3_actual_i >= bgs3_pool.Length)
				bgs3_actual_i = 0;

		} else if (stage == 4) {
			bgs4_pool [bgs4_actual_i].transform.position = new Vector2 (x_pos, y_pos);
			to_return = bgs4_pool [bgs4_actual_i];
			bgs4_actual_i++;
			if (bgs4_actual_i >= bgs4_pool.Length)
				bgs4_actual_i = 0;
		
		} else if (stage == 1) {
			bgs5_pool [bgs5_actual_i].transform.position = new Vector2 (x_pos, y_pos);
			to_return = bgs5_pool [bgs5_actual_i];
			bgs5_actual_i++;
			if (bgs5_actual_i >= bgs5_pool.Length)
				bgs5_actual_i = 0;
		}

		if (special_wave) {
			bgs_special_pool [bgs_special_actual_i].transform.position = new Vector2 (x_pos, y_pos - 0.5f);
			to_return = bgs_special_pool [bgs_special_actual_i];
			bgs_special_actual_i++;
			if (bgs_special_actual_i >= bgs_special_pool.Length)
				bgs_special_actual_i= 0;

		} 

		return to_return;


	}

    #region FLOOR THINGS
    public GameObject reposite_floor(float x_pos, float y_pos)
    {
        clear_flags_floor();
        floor_pool[floor_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
        GameObject repositing_floor = floor_pool[floor_pool_actual_i];

        floor_pool_actual_i++;
        if(floor_pool_actual_i == floor_pool_size)
        {
            floor_pool_actual_i = 0;
        }

        
        return repositing_floor;
    }

    public void clear_flags_floor()
    {
        if(floor_pool[floor_pool_actual_i] != null)
        {
            if (floor_pool[floor_pool_actual_i].GetComponent<floor>() !=null)
            {
                floor_pool[floor_pool_actual_i].GetComponent<floor>().clear_flags_reposite();
            }
            else
            {
                Debug.Log("naum tem scripttttttttttttttttttttttttttttttttttttttttttttttttttttt");
            }
       }
        else
        {
            Debug.Log("naum tem objetioooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
        }

    }
    #endregion

    #region DOUBLE SPIKE THINGS
    public GameObject reposite_double_spikes(float x_pos, float y_pos)
    {
        clear_flags_double_spikes();
        double_spikes_pool[double_spikes_pool_actual_i].transform.position = new Vector3(x_pos, y_pos-0.05f, 0);
        GameObject repos_spike = double_spikes_pool[double_spikes_pool_actual_i];

        double_spikes_pool_actual_i++;
        if (double_spikes_pool_actual_i == double_spikes_pool_size)
        {
            double_spikes_pool_actual_i = 0;
        }


        return repos_spike;
    }

    public void clear_flags_double_spikes()
    {
        if (double_spikes_pool[double_spikes_pool_actual_i] != null)
        {
            if (double_spikes_pool[double_spikes_pool_actual_i].GetComponent<spike>() != null)
            {
                double_spikes_pool[double_spikes_pool_actual_i].GetComponent<spike>().clear_flags_reposite();
                
            }
            else
            {
                Debug.Log("naum tem scripttttttttttttttttttttttttttttttttttttttttttttttttttttt");
            }
        }
        else
        {
            Debug.Log("naum tem objetioooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
        }
    }
    #endregion

	#region ====== NOTES =======
	public GameObject reposite_note(float x_pos, float y_pos)
	{
		//clear_flags_notes();

		note_pool[note_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
		GameObject repos_note = note_pool[note_pool_actual_i];
		repos_note.GetComponent <note_behaviour>().Init ();

		note_pool_actual_i++;
		if (note_pool_actual_i == note_pool_size)
			note_pool_actual_i = 0;

		return repos_note;
	}

	public void clear_flags_notes()
	{
		if (note_pool[note_pool_actual_i] != null)
		{
			if (note_pool[note_pool_actual_i].GetComponent<spike>() != null)
			{
				double_spikes_pool[double_spikes_pool_actual_i].GetComponent<spike>().clear_flags_reposite();

			}
			else
			{
				Debug.Log("naum tem scripttttttttttttttttttttttttttttttttttttttttttttttttttttt");
			}
		}
		else
		{
			Debug.Log("naum tem objetioooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
		}
	}
	#endregion

	#region ====== NOTES TRAIL =======
	public GameObject reposite_note_trail(float x_pos, float y_pos)
	{
		note_trail_pool[note_trail_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
		GameObject repos_note = note_trail_pool[note_trail_pool_actual_i];
		//nt = repos_note.GetComponent <note_trail_behavior> ().Init ();
		repos_note.GetComponent <note_trail_behavior>().Init ();

		note_trail_pool_actual_i++;
		if (note_trail_pool_actual_i == note_trail_pool_size)
			note_trail_pool_actual_i = 0;

		return repos_note;
	}

	#endregion


    #region TRIPE SPIKE THINGS
    public GameObject reposite_triple_spikes(float x_pos, float y_pos)
    {
        clear_flags_triple_spikes();
        triple_spikes_pool[triple_spikes_pool_actual_i].transform.position = new Vector3(x_pos, y_pos-0.05f, 0);
        GameObject repos_t_spike = triple_spikes_pool[triple_spikes_pool_actual_i];

        triple_spikes_pool_actual_i++;
        if (triple_spikes_pool_actual_i == triple_spikes_pool_size)
        {
            triple_spikes_pool_actual_i = 0;
        }
        return repos_t_spike;
    }

    public void clear_flags_triple_spikes()
    {
        if (triple_spikes_pool[triple_spikes_pool_actual_i] != null)
        {
            if (triple_spikes_pool[triple_spikes_pool_actual_i].GetComponent<spike>() != null)
            {
                triple_spikes_pool[triple_spikes_pool_actual_i].GetComponent<spike>().clear_flags_reposite();

            }
            else
            {
                Debug.Log("naum tem scripttttttttttttttttttttttttttttttttttttttttttttttttttttt");
            }
        }
        else
        {
            Debug.Log("naum tem objetioooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
        }
    }
    #endregion

    #region SQUARES PARTICLE PW
    public void reposite_squares_floor_particle(float x_pos, float y_pos)
    {
        //GameObject repos_squares = squares_floor_pool[squares_floor_pool_actual_i];

            squares_floor_pool[squares_floor_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
            

            Rigidbody2D[] rigids = squares_floor_pool[squares_floor_pool_actual_i].GetComponentsInChildren<Rigidbody2D>();
            if (rigids != null)
            {

                foreach (Rigidbody2D rigid in rigids)
                {
                    rigid.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }

            squares_floor_pool_actual_i++;
            if (squares_floor_pool_actual_i == squares_floor_pool_size)
            {
                squares_floor_pool_actual_i = 0;
            }


           
        

       // return repos_squares;

    }

    public void clear_squares_floor_particle()
    {
        Invoke("clear_squares_floor_particle_now", 1f);
    }

    void clear_squares_floor_particle_now()
    {
        int i;
        for (i = 0; i < squares_floor_pool_size; i++)
        {

            Rigidbody2D[] rigids = squares_floor_pool[i].GetComponentsInChildren<Rigidbody2D>();
            if (rigids != null)
            {
                foreach (Rigidbody2D rigid in rigids)
                {
                    rigid.GetComponent<Rigidbody2D>().isKinematic = true;
                    rigid.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }

            floor_square_pw_destruct[] squares = squares_floor_pool[i].GetComponentsInChildren<floor_square_pw_destruct>();
         
            if (squares != null)
            {
                foreach (floor_square_pw_destruct square in squares)
                {
                    square.reposite_squares_tururu();
                }
                squares_floor_pool[i].transform.position = new Vector3(105, 10 * i, 0);
            }
            else
            {
                Debug.Log("naum tem scripttttttttttttttttttttttttttttttttttttttttttttttttttttt");
            }
        }
    }
    #endregion

    public GameObject reposite_score(float x_pos, float y_pos)
    {
        
        scores_floor_pool[scores_floor_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
        GameObject repositing_score = scores_floor_pool[scores_floor_pool_actual_i];

        scores_floor_pool_actual_i++;
        if (scores_floor_pool_actual_i == scores_floor_pool_size)
        {
            scores_floor_pool_actual_i = 0;
        }

        return repositing_score;
    }

}
