﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class PwWheelMaster : MonoBehaviour {
	public GameObject title, top, bottom, tampa, youNowHaveMenu;
    public Text youNowHaveText;
    public pizza_char pitissa;
	public GameObject haste;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {

        if (hud_controller.si.CAN_ROTATE_ROULETTE == true)
        {
            openTampa();
            pitissa.hand.SetActive(true);
        }
        else
        {
            pitissa.hand.SetActive(false);
        }
    }
    void OnDisable()
    {
        tampa.transform.DOKill();
        tampa.transform.localPosition = new Vector3(tampa.transform.localPosition.x,6,tampa.transform.localPosition.z);
    }
    public void openTampa()
    {
        pitissa.openingTampaDoTeuCu = true;
        tampa.transform.DOLocalMoveY(tampa.transform.localPosition.y, 1).OnComplete(realOpen);
    }

    void realOpen()
    {
        tampa.transform.DOLocalMoveY(-436, 2).OnComplete(canRotate);


    }
    public void openRewardMenu(float value)
    {
        youNowHaveMenu.SetActive(true);
        youNowHaveText.text = value.ToString();
    }
    public void closeRewardMenu()
    {
        youNowHaveMenu.SetActive(false);
        Invoke("closeTampa",0.3f);
    }

    void closeTampa()
    {

        tampa.transform.DOLocalMoveY(6, 2);
    }
    
    void canRotate()
    {
        pitissa.openingTampaDoTeuCu = false;
        pitissa.hand.SetActive(true);

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
