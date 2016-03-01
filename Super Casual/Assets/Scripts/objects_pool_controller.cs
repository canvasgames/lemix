using UnityEngine;
using System.Collections;

public class objects_pool_controller : MonoBehaviour {

    public static objects_pool_controller s;

    public GameObject floor_prefab, double_spike_prefab, triple_spike_prefab, squares_floor_prefab;

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

    void Awake()
    {
        floor_pool = new GameObject[floor_pool_size];
        double_spikes_pool = new GameObject[double_spikes_pool_size];
        triple_spikes_pool = new GameObject[triple_spikes_pool_size];
        squares_floor_pool = new GameObject[squares_floor_pool_size];
        create_initial_tudo();
    }
	// Use this for initialization
	void Start () {
        s = this;
	}
	


	// Update is called once per frame
	void Update () {
	
	}

    void create_initial_tudo()
    {
        int i;
        for(i=0; i<floor_pool_size; i++)
        {
            floor_pool[i] =  (GameObject)Instantiate(floor_prefab, new Vector3(55, 0, 0), transform.rotation);
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
        double_spikes_pool[double_spikes_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
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

    #region TRIPE SPIKE THINGS
    public GameObject reposite_triple_spikes(float x_pos, float y_pos)
    {
        clear_flags_triple_spikes();
        triple_spikes_pool[triple_spikes_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
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
    public GameObject reposite_squares_floor_particle(float x_pos, float y_pos)
    {
        squares_floor_pool[squares_floor_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
        GameObject repos_squares = squares_floor_pool[squares_floor_pool_actual_i];

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
        return repos_squares;
    }

    public void clear_squares_floor_particle()
    {
        Invoke("clear_squares_floor_particle_now", 2f);
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
}
