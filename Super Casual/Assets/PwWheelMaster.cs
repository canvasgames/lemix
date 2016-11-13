using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PwWheelMaster : MonoBehaviour {
	public GameObject title, top, bottom, tampa;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator Entrance(){
		Debug.Log ("EEEEEENTRANCE ");
		float y_pos = title.transform.localPosition.y;
		title.transform.localPosition = new Vector2 (title.transform.localPosition.x, title.transform.localPosition.y + title.GetComponent <RectTransform> ().rect.height);
		title.transform.DOLocalMoveY(y_pos
			, 0.5f).SetEase (Ease.OutQuart);
		
//		title.transform.DOLocalMoveY(title.GetComponent <RectTransform> ().rect.height
//			, 0.5f).SetEase (Ease.OutQuart);
		yield return new WaitForSeconds (0.14f);

//		header.transform.DOLocalMoveY (game_title.transform.localPosition.y + 500
//			, 0.5f).SetEase (Ease.OutQuart);
//		yield return new WaitForSeconds (0.2f);
	}

}
