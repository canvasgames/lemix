using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

	public GameObject my_skin;
	public Transform myMaster;
	public Rigidbody2D rb;
	Animator mySkinAnimator;
	[SerializeField] float followSharpness = 0.05f;
	float myXScale;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
		mySkinAnimator = my_skin.GetComponent<Animator> ();
		myXScale = my_skin.transform.localScale.x;
	}
	// Use this for initialization
	void Start () {
	
	}

	public void UpdateMySkin(Skin skin, int bandPosition, Vector2 velocity){
		if (skin.musicStyle == MusicStyle.Pop) {
			if (bandPosition == 2) {
				my_skin.transform.localPosition = new Vector2 (0.05f, 0.04f);
			} else if (bandPosition == 3 || bandPosition == 4) {
				my_skin.transform.localPosition = new Vector2 (0.05f, -0.24f);
				my_skin.transform.localScale = new Vector2 (2.6679f, 2.6679f);
			} else if (bandPosition == 5) {
				my_skin.transform.localPosition = new Vector2 (0.05f, -0.075f);
				my_skin.transform.localScale = new Vector2 (2.272163f, 2.272163f);
			}
		} else {
			my_skin.transform.localPosition = new Vector2 (0.05f, 0.04f);
			my_skin.transform.localScale = new Vector2 (3f, 3f);
		}
		myXScale = my_skin.transform.localScale.x;

		if (velocity.x > 0) {
			my_skin.transform.localScale = new Vector2(-myXScale, my_skin.transform.localScale.y);
		}
		else {
			my_skin.transform.localScale = new Vector2(myXScale, my_skin.transform.localScale.y);
		}

		my_skin.GetComponent<Animator> ().runtimeAnimatorController = 
			Resources.Load ("Sprites/Animations/" + globals.s.ACTUAL_STYLE.ToString () + "Band" + bandPosition + "Animator") as RuntimeAnimatorController;
	}

	public void DeactivateMe(float time){
		StartCoroutine( DeactivateMeCoroutine(time));
	}

	public IEnumerator DeactivateMeCoroutine(float time = 1f){
		Debug.Log (" DDDDDDDDDDDEACTIVATING FOLLOWER!!! time: " + time);

//		yield return new WaitForSeconds (time);
		yield return new WaitForSeconds (0.1f);
		Debug.Log (" DEACTIVATING FOLLOWER!!! " + time);
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void LateUpdate () {
//		transform.position = Vector2.MoveTowards (transform.position, myMaster.position, 0.09f);
//		transform.position += (myMaster.position  - transform.position) * followSharpness;
//		transform.position += (myMaster.position  - transform.position) * followSharpness;
//		transform.position = new Vector2 (myMaster.position.x - 1f, transform.position.y); 
	}

	public void InitMovement(Vector2 velocity){
		rb.velocity = Vector2.zero;
		rb.velocity = velocity;
		if (velocity.x > 0) {
			my_skin.transform.localScale = new Vector2(-myXScale, my_skin.transform.localScale.y);
		}
		else {
			my_skin.transform.localScale = new Vector2(myXScale, my_skin.transform.localScale.y);
		}
//		init_my_skin();
	}

	public void JumpOn(){
			// my_trail.transform.localRotation = new Quaternion(0, 0, 110, 0);
//			sound_controller.s.PlayJump();
//			grounded = false;
			//rb.AddForce (new Vector2 (0, y_jump));
			rb.velocity = new Vector2(rb.velocity.x, globals.s.BALL_SPEED_Y);

			// my_skin.GetComponent<Animator>().Play("Jumping");
			mySkinAnimator.SetBool("Jumping", true);
	}

	public IEnumerator LandOn(float time = 0.2f, float yPos = 0){
		yield return new WaitForSeconds (time);
//		transform.position = new Vector2 (transform.position.x, yPos); 
//		rb.velocity = new Vector2(rb.velocity.x, 0);
		mySkinAnimator.SetBool("Jumping", false);
	}

	public void init_my_skin() {
		if (transform.position.x < 0 ) {
			my_skin.transform.localScale = new Vector2(-my_skin.transform.localScale.x, my_skin.transform.localScale.y);
		}
		else if (transform.position.x > 0) {
			my_skin.transform.localScale = new Vector2(my_skin.transform.localScale.x, my_skin.transform.localScale.y);
		}
	}
		
	public void KillMe(float time){
		StartCoroutine(LetMeSacrificeMyselfForTheGreaterGood(time));
	}

	public IEnumerator LetMeSacrificeMyselfForTheGreaterGood(float time = 1f){
		Debug.Log ("iiiiiiiiiiiiii KILL MY FOLLOWE RMEEEEEES tim: " + time);
		yield return new WaitForSeconds (time);
//		if(sound_controller.s != null) sound_controller.s.PlayExplosion();
		Debug.Log ("KILL MY FOLLOWE RMEEEEEES" + time);
		BallMaster.s.CreateExplosion (transform.position);
		gameObject.SetActive (false);
	}

}
