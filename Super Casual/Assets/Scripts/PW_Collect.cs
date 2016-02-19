using UnityEngine;
using System.Collections;

public enum PW_Types
{

    Heart = 1, Super = 2, Sight = 3
}

public class PW_Collect : MonoBehaviour {

    public int pw_type;
    int rand;
    // Use this for initialization
    void Start () {
        
        rand = Random.Range((int)PW_Types.Heart, (int)PW_Types.Sight);
        rand = (int)PW_Types.Super;
        pw_type = rand;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void collect()
    {
        Destroy(transform.gameObject);
    }

}
