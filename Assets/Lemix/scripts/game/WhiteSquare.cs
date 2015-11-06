using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class WhiteSquare : MonoBehaviour {
	public int myID;
	public int myIDindex;
	public string myLetter;
	public int showed;
	private int foundedByPlayer;
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

			foundedByPlayer = player;

			transform.DOLocalMoveX(transform.position.x,(0.1f * myIDindex)).OnComplete(activeXTIME);
 
		}
	}

	void activeXTIME()
	{
		GetComponent<TextMesh> ().text = myLetter.ToString ();
		GetComponent<TextMesh> ().color = Color.white;

		if(foundedByPlayer == GLOBALS.Singleton.MP_PLAYER)
			GetComponentInChildren<SpriteRenderer> ().color = new Color32(52, 152, 219,255);
		else
			GetComponentInChildren<SpriteRenderer> ().color = new Color32(226, 60, 43,255); 

		Vector3 temp = new Vector3 (0,-15,0);
		transform.DOPunchPosition(temp,0.6f,0,0);
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
