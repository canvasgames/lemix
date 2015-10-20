using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;


public class eraser_sprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown()
	{

	}
	public void step1()
	{
		gameObject.SetActive(true);
		gameObject.transform.DOScale(new Vector3(1,1,1),0.2f);
		gameObject.transform.DOMove(new Vector3(-200f,324f,5f),0.2f).OnComplete(step2);
	}
	void step2()
	{
		gameObject.transform.DOMove(new Vector3(1f,-22f,5f),0.2f).OnComplete(step3);
	}
	void step3()
	{
		gameObject.transform.DOMove(new Vector3(201f,324f,5f),0.2f).OnComplete(step4);		
	}
	void step4()
	{
		gameObject.transform.DOMove(new Vector3(408f,-22f,5f),0.2f).OnComplete(step5);		
	}
	void step5()
	{
		gameObject.transform.DOMove(new Vector3(602f,324f,5f),0.2f).OnComplete(step6);		
	}

	void step6()
	{
		gameObject.transform.DOScale(new Vector3(0,0,0),0.2f).OnComplete(step7);
	}
	void step7()
	{
		gameObject.SetActive(false);
		gameObject.transform.DOMove(new Vector3(-585f,-11f,5f),0.2f);
		gameObject.transform.DOScale(new Vector3(1,1,1),0.2f);

	}
}
