using UnityEngine;
using System.Collections;

public class WhiteSquare : MonoBehaviour {
	public int myID;
	public int myIDindex;
	public string myLetter;
	public int showed;

	//Marca se eh a ultima letra
	public bool lastletter;

	public Color originalColor;

	// Use this for initialization
	void Start () {
		showed =0;
		originalColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.renderer.material.color = Color.blue;
	}

	public void appear(int id, int player)
	{
		if (id == myID) 
		{
			showed = 1;
			GetComponent<TextMesh> ().text = myLetter.ToString ();
			GetComponent<TextMesh> ().color = Color.white;
			if(player == 1)
				GetComponentInChildren<SpriteRenderer> ().color = new Color32(52, 152, 219,255);
			else if(player == 2)
				GetComponentInChildren<SpriteRenderer> ().color = new Color32(226, 60, 43,255);  
		}
	}

	public void appearPowerUp()
	{
		GetComponent<TextMesh> ().text = myLetter.ToString ();
		GetComponent<TextMesh> ().color = Color.black;
		showed = 1;

	}

	public void erasePowerUp()
	{
		GetComponent<TextMesh> ().text = " ".ToString ();
		GetComponentInChildren<SpriteRenderer> ().color = originalColor;
		showed = 0;
		
	}
}
