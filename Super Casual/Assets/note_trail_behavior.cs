using UnityEngine;
using System.Collections;

public class note_trail_behavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play("note_" + Random.Range(1, 11));
       // GetComponent<Animator>().Stop();

        Invoke("destroy_me", 0.7f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void destroy_me() {
        Destroy(gameObject);
    }
}
