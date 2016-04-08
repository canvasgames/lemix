using UnityEngine;
using System.Collections;

public class collider_tutorial_left : MonoBehaviour {
    public GameObject my_objs;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            Debug.Log("Ativaaaa");
            my_objs.SetActive(true);
        }
    }
    }
