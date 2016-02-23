using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Jump_bar : MonoBehaviour {

    public static Jump_bar s;

    public GameObject bar;
    public Text txt;
    // Use this for initialization
    void Start () {
        s = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void update_value()
    {
     

            txt.text = globals.s.JUMP_COUNT_PW + "/" + GD.s.GD_JUMPS_PW_BAR_FULL + " JUMPS";
            float scale_x = (globals.s.JUMP_COUNT_PW / (float)GD.s.GD_JUMPS_PW_BAR_FULL);
            bar.transform.localScale = new Vector3(scale_x, transform.localScale.y, transform.localScale.z);

            if (scale_x == 1)
            {
                bar.GetComponent<Image>().color = Color.magenta;


            }

        

    }
}
