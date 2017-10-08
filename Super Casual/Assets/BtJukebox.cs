using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class BtJukebox : MonoBehaviour {

	Button myButton;
	Image myImage;
	[SerializeField] ParticleSystem myParticles;
	[SerializeField] GameObject myReadyEffect;
	// Use this for initialization
	void Awake () {
		myButton = GetComponent<Button> ();
		myImage = GetComponent<Image> ();

//		SetNewStyleState ();
//		SetChangeStyleState ();

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetChangeStyleState(){
		if(myParticles) myParticles.gameObject.SetActive (false);

		Sprite img = Resources.Load<Sprite> ("Sprites/GameOver/change-style");
		Debug.Log ("IMG LOADED?  " + img);
		myImage.sprite = img;

		Sprite sprt = Resources.Load<Sprite> ("Sprites/GameOver/change-style-press");
		SpriteState state = new SpriteState ();
		state.pressedSprite = sprt;
		myButton.spriteState = state;

		if (myReadyEffect)
			myReadyEffect.SetActive (false);

	}

	public void SetNewStyleState(bool glowAnimation = false){
		if (glowAnimation = false) {
			Sprite img = Resources.Load<Sprite> ("Sprites/GameOver/new-style");
			myImage.sprite = img;

			Sprite sprt = Resources.Load<Sprite> ("Sprites/GameOver/new-style-press");
			SpriteState state = new SpriteState ();
			state.pressedSprite = sprt;
			myButton.spriteState = state;

			//activate particles
			myParticles.gameObject.SetActive (true);
		}
		else
			StartCoroutine (ReadyAnimation());
	}

	IEnumerator ReadyAnimation(){

		Debug.Log ("BT JUKEBOX MY READY ANIMATION!!!");
		if (myReadyEffect) {
			myReadyEffect.SetActive (true);
			myReadyEffect.GetComponent<Animator> ().Play ("GlowBlueReadyAnim");
		}

		yield return new WaitForSeconds (0.57f);

		if(myReadyEffect) myReadyEffect.GetComponent<Image> ().DOFade (0, 0.2f);

		//change bt image
		Sprite img = Resources.Load<Sprite> ("Sprites/GameOver/new-style");
		myImage.sprite = img;

		Sprite sprt = Resources.Load<Sprite> ("Sprites/GameOver/new-style-press");
		SpriteState state = new SpriteState ();
		state.pressedSprite = sprt;
		myButton.spriteState = state;

		//activate particles
		myParticles.gameObject.SetActive (true);

	}
}
