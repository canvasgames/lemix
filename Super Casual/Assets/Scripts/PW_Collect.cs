using UnityEngine;
using System.Collections;

public class PW_Collect : MonoBehaviour {

    public int pw_type;
    int rand;
    // Use this for initialization
    void Start () {
        
        rand = Random.Range(1, 3);
        rand = 1;
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
