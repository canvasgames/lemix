using UnityEngine;
using System.Collections;

public class text_z_order : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Renderer>().sortingLayerID = this.transform.parent.GetComponent<Renderer>().sortingLayerID;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
