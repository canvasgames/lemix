using UnityEngine;
using System.Collections;

public class power_bar : MonoBehaviour {
	GameObject blueClash,redClash;
	// Use this for initialization
	void Start () {
		blueClash = GameObject.Find ("hud_blue_bar_clash");
		redClash = GameObject.Find ("hud_red_bar_clash");
	}
	
	// Update is called once per frame
	void Update () {
		var renderer = gameObject.GetComponent<Renderer>();
		blueClash.transform.position = new Vector3((transform.position.x + renderer.bounds.size.x), transform.position.y, transform.position.z) ;
		redClash.transform.position = new Vector3((transform.position.x + renderer.bounds.size.x), transform.position.y, transform.position.z) ;
	}
}
