using UnityEngine;
using System.Collections;

public class ParticleSorintgLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Set the sorting layer of the particle system.
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Default";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 995;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
