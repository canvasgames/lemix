using UnityEngine;
using System.Collections;

public class trail_manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("note_creator", 0.3f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void note_creator() {
        Invoke("note_creator", 0.15f);
        create_random_note();
    }

    void create_random_note() {
        GameObject instance = Instantiate(Resources.Load("Prefabs/Note_Trail",
            typeof(GameObject)), new Vector3(transform.position.x, transform.position.y + Random.Range(-0.4f, 0.4f), 0), transform.rotation) as GameObject;
    }
}
