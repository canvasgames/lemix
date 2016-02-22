using UnityEngine;
using System.Collections;

public class Score_floor_txt : MonoBehaviour {

    //1 - Best  ------- 2 - Day ------- 3 - Last
    public int my_type;
    public int my_floor;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void try_destroy_me(int floor, int type)
    {
        if(floor == my_floor && type == my_type)
        {
            Destroy(transform.gameObject);
        }
    }
}
