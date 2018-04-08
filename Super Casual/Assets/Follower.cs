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

	public IEnumerator DeactivateMe(float time = 1f){
		yield return new WaitForSeconds (time);
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
			my_skin.transform.localScale = new Vector2(-3, my_skin.transform.localScale.y);
		}
		else if (transform.position.x > 0) {
			my_skin.transform.localScale = new Vector2(3, my_skin.transform.localScale.y);
		}
	}


}
