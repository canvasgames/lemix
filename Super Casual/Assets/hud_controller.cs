using UnityEngine;
using System.Collections;

public class hud_controller : MonoBehaviour {

    public static hud_controller si;

	// Use this for initialization
	void Start () {
        si = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void update_floor(int n)
    {
        Debug.Log(" NEW FLOOR!!!!!! ");
       GetComponentInChildren<TextMesh>().text =  "Floor " + (n+1).ToString();
    }
}
