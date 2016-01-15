using UnityEngine;
using System.Collections;

public class CloseBT : MonoBehaviour {

    public GameObject myFather;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clicked()
    {
        MenusController.s.destroyMenu("", myFather);
    }
}
