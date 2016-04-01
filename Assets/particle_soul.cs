using UnityEngine;
using System.Collections;
using DG.Tweening;

public class particle_soul : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void vaiviado(float pos_x, float pos_y, float pos_z)
    {
        transform.DOJump(new Vector3(pos_x, pos_y, pos_z), Random.Range(-7, 7), 1, Random.Range(0.5f, 1.3f)).OnComplete(suicide_solution);
    }

    void suicide_solution()
    {
        Destroy(gameObject);
    }
}
