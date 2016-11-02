using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GlowBehaviour : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		glow_animation_wait ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void glow_animation_start() {
		GetComponent<SpriteRenderer>().DOFade(1f, GD.s.GlowInTime).OnComplete(glow_animation_wait);
	}
	public void glow_animation_wait(){
		Invoke("glow_animation_end", GD.s.GlowStaticTime);
	}
	public void glow_animation_end() {
		GetComponent<SpriteRenderer>().DOFade(0, GD.s.GlowOutTime).OnComplete(glow_animation_start);
	}

}
