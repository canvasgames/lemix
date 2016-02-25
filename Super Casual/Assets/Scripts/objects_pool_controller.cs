using UnityEngine;
using System.Collections;

public class objects_pool_controller : MonoBehaviour {

    public static objects_pool_controller s;

    public GameObject floor_prefab, spike_prefab;

    [HideInInspector] public GameObject[] floor_pool;
    int floor_pool_size = 15;
    int floor_pool_actual_i = 0;

    [HideInInspector]
    public GameObject[] spikes_pool;
    int spikes_pool_size = 20;
    int spikes_pool_actual_i = 0;

    void Awake()
    {
        floor_pool = new GameObject[floor_pool_size];
        spikes_pool = new GameObject[spikes_pool_size];
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
        for (i = 0; i < spikes_pool_size; i++)
        {
            spikes_pool[i] = (GameObject)Instantiate(spike_prefab, new Vector3(55, 0, 0), transform.rotation);
        }

    }

    #region FLOOR THINGS
    public GameObject reposite_floor(float x_pos, float y_pos)
    {
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

    }
    #endregion

    #region SPIKE THINGS
    public GameObject reposite_spikes(float x_pos, float y_pos)
    {
        spikes_pool[spikes_pool_actual_i].transform.position = new Vector3(x_pos, y_pos, 0);
        GameObject repositing_spike = floor_pool[floor_pool_actual_i];

        spikes_pool_actual_i++;
        if (spikes_pool_actual_i == spikes_pool_size)
        {
            spikes_pool_actual_i = 0;
        }
        return repositing_spike;
    }

    public void clear_flags_spikes()
    {

    }
    #endregion
}
