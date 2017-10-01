using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameOverController : MonoBehaviour {

	#region === Variables ===
	public static GameOverController s;

	public GameObject jukeboxGroup;
	public GameObject jukeboxBt, jukeboxNote, jukeboxIcon, jukeboxGetNow, jukeboxPauta;
	public GameObject jukeboxBtGlow, jukeboxBtText;
	public Text jukeboxBarNotesText;
	public GameObject jukeboxGreenBar;
	[Space(10)] 

//	public Text ;
	public GameObject diskGroup;
	public GameObject diskGroupBg, storeGroupBg;
	public GameObject diskFreeSpinText, diskTimeLeft, diskFreeSpinNowText;
	public GameObject diskBt, diskBarGreen;
	[Space(10)] 
	public GameObject replayBt;

	bool DiskSpinNowAnimationStarted = false;

//	float noteXStart= -270 , noteXEnd = 272;
	float noteXStart= -678f , noteXEnd = -243f;
	float noteXLocalStart= -678f , noteXLocalEnd = -243f;

	float jukeboxBarInitialFill = 0.05f;

	float xTarget = 0, fillTarget = 0;
	// Use this for initialization
	float noteBarXPos;

	#endregion

	#region === Init ===
	void Awake () {
		s = this;
//		noteBarXPos = jukeboxNote.transform.localPosition.x;
//		noteBarXPos = jukeboxNote.GetComponent<Rect>().rectTransform.anchoredPosition.x;
		noteBarXPos = jukeboxNote.GetComponent<RectTransform> ().anchoredPosition.x;
//
//		globals.s.curGameScreen = GameScreen.LevelEnd;
//		USER.s.NOTES = 58;
//		globals.s.NOTES_COLLECTED = 15;
//
//		Invoke ("Init", 0.3f);
//		EnteringAnimations();
//		Invoke ("EnteringAnimations", 0.5f);
	}

	public void Init(){
		Debug.Log (" USER NOTES:" + USER.s.NOTES + "  jukebox price : " + globals.s.JUKEBOX_CURRENT_PRICE);

		if (USER.s.NEWBIE_PLAYER == 0) {
			replayBt.transform.localPosition = new Vector2 (0, -157); 
		}

		// Disk button logic
		if (FTUController.s.diskIntroduced == 0) {
			diskBt.SetActive (false);
			jukeboxBt.SetActive (false);
//			diskGroupBg.SetActive (false);
//			storeGroupBg.SetActive (false);

		}

		//Jukebox Logic
		if (USER.s.NOTES - globals.s.NOTES_COLLECTED < globals.s.JUKEBOX_CURRENT_PRICE) {
			//			jukeboxIcon.GetComponent<Image> ().color = Color.gray;
			SetJukeboxProgressState ();

			//			noteXStart = jukeboxNote.transform.localPosition.x;
			//			float xDif = noteXEnd - noteXStart;
			//			Debug.Log ("NOTE X START: " + noteXStart + "  NOTE X END: " + noteXEnd + " NOTE DIF: " + xDif);
			//			float xCurrent = (USER.s.NOTES - globals.s.NOTES_COLLECTED) * xDif / globals.s.JUKEBOX_CURRENT_PRICE;
			//			xTarget = (USER.s.NOTES) * xDif / globals.s.JUKEBOX_CURRENT_PRICE;
			//			jukeboxNote.transform.localPosition = new Vector2 (noteXStart + xCurrent, jukeboxNote.transform.localPosition.y);

			DisplayJukeboxNotes (USER.s.NOTES - globals.s.NOTES_COLLECTED);

			float fillDif = 1 - jukeboxBarInitialFill;
			float fillCurrent = (USER.s.NOTES - globals.s.NOTES_COLLECTED) * fillDif / globals.s.JUKEBOX_CURRENT_PRICE;

			jukeboxGreenBar.GetComponent<Image> ().fillAmount = fillCurrent + jukeboxBarInitialFill;
			fillTarget = (USER.s.NOTES) * fillDif / globals.s.JUKEBOX_CURRENT_PRICE;
		} else {
			StartCoroutine (JukeboxGetNow (0));
			//			jukeboxNote.transform.localPosition = new Vector2 (xCurrent, 0);

			//			jukeboxGetNow.SetActive (true);
			//			jukeboxPauta.SetActive(false);
			//
			//			BlinkGetNow ();
			//
			//			jukeboxNote.transform.localPosition = new Vector2 (noteXEnd, jukeboxNote.transform.localPosition.y);
			//		
			//			jukeboxGreenBar.GetComponent<Image> ().fillAmount = 1;
			//
			//			jukeboxIcon.GetComponent<Animator>().Play("jukebox icon animation");
		}

		StartCoroutine (EnteringAnimations ());

	}

	void InitJukeboxGroupInfo(){
		if (USER.s.NOTES - globals.s.NOTES_COLLECTED < globals.s.JUKEBOX_CURRENT_PRICE) 
			StartCoroutine (NoteAnimation());
	}

	public void Enterer(){
		StartCoroutine (EnteringAnimations ());
	}
	public IEnumerator EnteringAnimations(){
//		Debug.Log ("x pos : "+  jukeboxGroup.transform.position +  " width "+ jukeboxGroup.GetComponent<RectTransform> ().rect.width);
		jukeboxGroup.GetComponent<RectTransform> ().position = new Vector2 (0 - jukeboxGroup.GetComponent<RectTransform> ().rect.width/100 , jukeboxGroup.GetComponent<RectTransform> ().position.y);
		float localY = replayBt.transform.localPosition.y;
//		replayBt.transform.position = new Vector2 (replayBt.transform.position.x,  globals.s.CANVAS_Y_BOTTOM/100 - replayBt.GetComponent<RectTransform> ().rect.height/100); 
		replayBt.transform.position = new Vector2 (replayBt.transform.position.x,  replayBt.transform.position.y + -3f); 
		diskGroup.GetComponent<RectTransform> ().position = new Vector2 (0 - diskGroup.GetComponent<RectTransform> ().rect.width / 100, diskGroup.GetComponent<RectTransform> ().position.y);

		if (USER.s.NEWBIE_PLAYER == 0) {
			jukeboxGroup.GetComponent<RectTransform> ().DOLocalMoveX (0, 0.5f).SetEase (Ease.OutCubic).OnComplete(InitJukeboxGroupInfo);
			yield return new WaitForSeconds (0.45f);
//		Debug.Log ("22 x pos : "+  jukeboxGroup.transform.position +  " width "+ jukeboxGroup.GetComponent<RectTransform> ().rect.width);
		}

		if (FTUController.s.diskIntroduced == 1) {
			diskGroup.GetComponent<RectTransform> ().DOLocalMoveX (0, 0.5f).SetEase (Ease.OutCubic);
			yield return new WaitForSeconds (0.25f);
		}
//		Debug.Log ("y bottom " + (globals.s.CANVAS_Y_BOTTOM) + " Y REPLAY POS: " + replayBt.transform.position.y);
//		Debug.Log ("2222y bottom " + (globals.s.CANVAS_Y_BOTTOM) + " Y REPLAY POS: " + replayBt.transform.position.y + " FINAL: " + ((globals.s.CANVAS_Y_BOTTOM/100) - replayBt.GetComponent<RectTransform> ().rect.height/100) );
		replayBt.transform.DOLocalMoveY(localY, 0.5f);
	}

	#endregion

	// Update is called once per frame
	void Update () {
		if(globals.s.curGameScreen == GameScreen.LevelEnd){
			if (hud_controller.si.CAN_ROTATE_ROULETTE == false) {
				DiskSpinNowAnimationStarted = true;

				hud_controller.si.show_roullete_time_level_end ();
			}
			else {
				DeactivateSpinTimerTexts ();
			}
			//			GetComponent<Text>().text =  difference.Minutes + ":" + difference.Seconds + "";
		}
	}

	#region === Jukebox ===

	public void UpdateJukeboxInformation(){

		if (USER.s.NOTES > globals.s.JUKEBOX_CURRENT_PRICE) {
			SetJukeboxNewMusicState ();
		}
		else
			SetJukeboxProgressState ();

		DisplayJukeboxNotes (USER.s.NOTES);

		float fillDif = 1 - jukeboxBarInitialFill;
		float fillCurrent = (USER.s.NOTES) * fillDif / globals.s.JUKEBOX_CURRENT_PRICE;

		jukeboxGreenBar.GetComponent<Image> ().fillAmount = fillCurrent + jukeboxBarInitialFill;

	}


	void SetJukeboxNewMusicState(){
		jukeboxGetNow.SetActive (true);
		jukeboxPauta.SetActive(false);
		jukeboxNote.SetActive(false);
		jukeboxBarNotesText.gameObject.SetActive(false);
		jukeboxBt.GetComponent<Animator> ().Play ("bt_store_new_style");
	}

	void SetJukeboxProgressState(){
		jukeboxGetNow.SetActive (false);
		jukeboxPauta.SetActive(true);
		jukeboxNote.SetActive(true);
		jukeboxBarNotesText.gameObject.SetActive(true);
		jukeboxBt.GetComponent<Animator> ().Play ("bt_store_anim");
	}


	public void DisplayJukeboxNotes(int notesToShow){
		if (GD.s.JUKEBOX_FTU_PRICE == globals.s.JUKEBOX_CURRENT_PRICE) {
//			jukeboxNote.transform.localPosition = new Vector2 (noteBarXPos, jukeboxNote.transform.localPosition.y);
			jukeboxNote.GetComponent<RectTransform>().anchoredPosition = new Vector2 (noteBarXPos, jukeboxNote.transform.localPosition.y);
			jukeboxBarNotesText.text = notesToShow.ToString() + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}
		else {
			jukeboxNote.GetComponent<RectTransform>().anchoredPosition = new Vector2 (noteBarXPos + 40f, jukeboxNote.transform.localPosition.y);

			if (notesToShow < 10) {
				jukeboxBarNotesText.text = "0" + notesToShow.ToString() + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
			}
			else
				jukeboxBarNotesText.text = notesToShow.ToString() + "/" + globals.s.JUKEBOX_CURRENT_PRICE.ToString ();
		}
	}

	IEnumerator JukeboxGetNow(float wait){
		yield return new WaitForSeconds (wait);
		SetJukeboxNewMusicState ();

//		jukeboxNote.transform.localPosition = new Vector2 (noteXEnd, jukeboxNote.transform.localPosition.y);

		jukeboxGreenBar.GetComponent<Image> ().fillAmount = 1;

		BlinkJukeboxGroup();
//		jukeboxIcon.GetComponent<Animator>().Play("jukebox icon animation");
	}


	IEnumerator NoteAnimation(){
		yield return new WaitForSeconds (0.3f);

		StartCoroutine(IncreaseNoteNumbers(0.5f));

		//		jukeboxNote.transform.DOLocalMoveX (noteXStart + xTarget, 0.5f).SetEase (Ease.InOutCubic);

		Debug.Log (" FILL AMOUNT TARGET IS : " + fillTarget);

		if (USER.s.NOTES > globals.s.JUKEBOX_CURRENT_PRICE) {
			jukeboxGreenBar.GetComponent<Image> ().DOFillAmount (jukeboxBarInitialFill + fillTarget, 0.5f).SetEase (Ease.InOutCubic).OnComplete(() 
				=> StartCoroutine(JukeboxGetNow(0.15f)));

		} else {
			jukeboxGreenBar.GetComponent<Image> ().DOFillAmount (jukeboxBarInitialFill + fillTarget, 0.5f).SetEase (Ease.InOutCubic);
		}

		globals.s.NOTES_COLLECTED = 0;
	}


	IEnumerator IncreaseNoteNumbers(float time){
		float dif = USER.s.NOTES - globals.s.NOTES_COLLECTED ;
		for (int i = 0; i < dif ; i++) {
			DisplayJukeboxNotes ((int)dif + i);
			yield return new WaitForSeconds (time / dif);
		}
	}

	void BlinkJukeboxGroup(){
		jukeboxGetNow.GetComponent<Text> ().color = Color.green;
		jukeboxBt.GetComponent<Image> ().color = new Color(0.4f, 1, 1);
		//		jukeboxBtGlow.GetComponent<Image> ().color = new Color(0.4f, 1, 1);
		Invoke ("BlinkJukeboxGroup2", 0.2f);
	}
	void BlinkJukeboxGroup2(){
		jukeboxGetNow.GetComponent<Text> ().color = Color.white;
		jukeboxBt.GetComponent<Image> ().color = Color.white;
		//		jukeboxBtGlow.GetComponent<Image> ().color = Color.white;

		Invoke ("BlinkJukeboxGroup", 0.2f);
	}

	#endregion

	#region === SpinDisk === 

	void SetDiskCountDownState(){
		diskBarGreen.SetActive (false);
		diskFreeSpinNowText.SetActive (false);
		diskTimeLeft.SetActive (true);
		diskFreeSpinText.SetActive (true);
		diskBt.GetComponent<Button> ().interactable = false;
	}

	void SetDiskSpinNowState(){
		diskBarGreen.SetActive (true);
		diskFreeSpinNowText.SetActive (true);
		diskTimeLeft.SetActive (false);
		diskFreeSpinText.SetActive (false);

		diskBt.GetComponent<Button> ().interactable = true;
	}

	void DeactivateSpinTimerTexts(){
		diskFreeSpinText.SetActive (false);
		diskTimeLeft.SetActive (false);
		diskFreeSpinNowText.SetActive (true);
		diskBarGreen.SetActive (true);

		if (DiskSpinNowAnimationStarted == false) {
			DiskSpinNowAnimationStarted = true;
			BlinkSpinNow ();
		}
	}

	void BlinkSpinNow(){
		if (DiskSpinNowAnimationStarted == true) {
			diskFreeSpinNowText.GetComponent<Text> ().color = Color.green;
			diskBt.GetComponent<Image> ().color = new Color (0.4f, 1, 1);
			//		jukeboxBtGlow.GetComponent<Image> ().color = new Color(0.4f, 1, 1);
			Invoke ("BlinkSpinNow2", 0.2f);
		}
	}
	void BlinkSpinNow2(){
		if (DiskSpinNowAnimationStarted == true) {

			diskFreeSpinNowText.GetComponent<Text> ().color = Color.white;
			diskBt.GetComponent<Image> ().color = Color.white;
			//		jukeboxBtGlow.GetComponent<Image> ().color = Color.white;

			Invoke ("BlinkSpinNow", 0.2f);
		}
	}
	#endregion



}
