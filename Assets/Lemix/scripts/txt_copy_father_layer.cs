using UnityEngine;
using System.Collections;

public class txt_copy_father_layer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().sortingLayerName = transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
		GetComponent<Renderer>().sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder +1;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
