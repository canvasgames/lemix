using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hud_controller : MonoBehaviour {

    public static hud_controller si;

    public GameObject game_over_text;
    public GameObject floor;
    public GameObject best;

	// Use this for initialization
    void Awake()
    {
        si = this;
    }

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
        if(globals.s.GAME_OVER == 1 && Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel("Gameplay");
        }
	}

    public void update_floor(int n)
    {
        Debug.Log(" NEW FLOOR!!!!!! ");
        //GetComponentInChildren<TextMesh>().text =  "Floor " + (n+1).ToString();
        floor.GetComponent<Text>().text = "Floor " + (n + 1).ToString();

    }

    public void show_game_over(int score, int best)
    {
        game_over_text.SetActive(true);
        if (game_over_text.GetComponent<Text>().IsActive()) print(" IS GAME OVER ACTIVE ");
        game_over_text.GetComponent<Text>().text = "GAME OVER\nSCORE: " + score + "\n\n BEST: " + best;

    }

    public void display_best(int value)
    {
        best.GetComponent<Text>().text = "BEST " + value;
    }
}
