using UnityEngine;
using System.Collections;

public class rotate_bt_roulette : MonoBehaviour {
    public GameObject pizza_chart;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void click()
    {
        pizza_chart.GetComponent<pizza_char>().rotate();
    }
}
