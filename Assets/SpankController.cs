using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;


public class SpankController : MonoBehaviour {

    #region Variables Declaration

    public static SpankController s;
    public Transform Canvas;
    GameObject body;

    GameObject handTutLeft;
    GameObject handTutRight;
    float handLeftX;
    float handRightX;
    GameObject arrowLeft;
    GameObject arrowRight;
    GameObject handSpank;

    GameObject slap;
    GameObject handSlapper;
    GameObject painFace;

    bool touchActive = false;
    bool swiping = false;
    bool swiped = false;
    float swipeXpos = 0;

    int nSlaps = 0;

    void Awake() { s = this; }

    #endregion

    void Start() {
        GLOBALS.s.SPANKING_OCURRING = true;
        StartAnimation();
    }

    void Update() {
        if (GLOBALS.s.SPANKING_OCURRING && handSlapper != null) {
            if (Input.GetMouseButton(0)) { 
                handSlapper.SetActive(true);
                handSlapper.transform.position = Input.mousePosition;
                Debug.Log( " MOUSE X: " +Input.mousePosition.x);
                if (Input.mousePosition.x > 0)
                    handSlapper.transform.localScale.Set(1,1,1);
                else
                    handSlapper.transform.localScale.Set(-1, 1, 1);
            }
            else if (Input.GetMouseButtonUp(0))
                handSlapper.SetActive(false);


            if (touchActive && Input.GetMouseButtonDown(0) && (Input.mousePosition.x <= -50 || Input.mousePosition.x >= 50)) {
                swiping = true;
                swipeXpos = Input.mousePosition.x;
            }
            else if (swiping && Input.GetMouseButtonUp(0)) {
                swiping = false;
                if ((swipeXpos < 0 && Input.mousePosition.x >= swipeXpos + 100) || (swipeXpos > 0 && Input.mousePosition.x <= swipeXpos - 100)) {
                    SlapThatAss();
                    swipeXpos = 0;
                    touchActive = false;
                    //swiped = true;
                }
            }
        }
    }

    #region Animation Start
    public void StartAnimation() {

        nSlaps = 0;
        //display slaps here

        body = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/Body"));
        body.transform.SetParent(Canvas, false);

        painFace = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/PainFace"));
        painFace.SetActive(false);
        painFace.transform.SetParent(Canvas, false);
            


        handSlapper = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/Hand"));
        handSlapper.transform.SetParent(Canvas, false);
        handSlapper.SetActive(false);

        arrowLeft = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/ArrowSlapLeft"));
        arrowLeft.transform.SetParent(Canvas, false);
        arrowLeft.SetActive(true);
        arrowLeft.GetComponent<CanvasGroup>().alpha = 0;
        arrowLeft.GetComponent<CanvasGroup>().DOFade(1, 0.9f);
        //chicken.SetActive(true);

        handTutLeft = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/handTutLeft"));
        handTutLeft.transform.SetParent(Canvas, false);
        handTutLeft.SetActive(true);
        handTutLeft.GetComponent<CanvasGroup>().alpha = 0;
        handTutLeft.GetComponent<CanvasGroup>().DOFade(1, 0.9f).OnComplete(() => hand_visible());
        handLeftX = handTutLeft.transform.localPosition.x;

        arrowRight = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/ArrowSlapRight"));
        arrowRight.transform.SetParent(Canvas, false);
        arrowRight.SetActive(true);
        arrowRight.GetComponent<CanvasGroup>().alpha = 0;
        arrowRight.GetComponent<CanvasGroup>().DOFade(1, 0.9f);
        //chicken.SetActive(true);

        handTutRight = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/handTutRight"));
        handTutRight.transform.SetParent(Canvas, false);
        handTutRight.SetActive(true);
        handTutRight.GetComponent<CanvasGroup>().alpha = 0;
        handTutRight.GetComponent<CanvasGroup>().DOFade(1, 0.9f);
        handRightX = handTutRight.transform.localPosition.x;

        //arrow.get
        //chicken.SetActive(true);
    }

    public void hand_visible() {
        handTutLeft.transform.localPosition = new Vector3(handLeftX, handTutLeft.transform.localPosition.y);
        handTutRight.transform.localPosition = new Vector3(handRightX, handTutRight.transform.localPosition.y);
        Invoke("hand_start_moving", 0.3f);
    }

    void hand_start_moving() {
        handTutRight.transform.DOLocalMoveX(handTutRight.transform.localPosition.x - 266, 0.5f).OnComplete(() => restart_hand());
        handTutLeft.transform.DOLocalMoveX(handTutLeft.transform.localPosition.x + 266, 0.5f);
    }
    public void restart_hand() {
        Invoke("hand_visible", 0.25f);
        touchActive = true;
    }

    #endregion

    #region Slap Animation Trigger
    public void SlapThatAss() {
        //MenusController.s.destroyMenu("SmallScroll", null);

        handTutLeft.SetActive(false);
        handTutRight.SetActive(false);
        arrowLeft.SetActive(false);
        arrowRight.SetActive(false);

        Debug.Log("SLAP THAT ASS");

        slap = (GameObject)Instantiate(Resources.Load("Prefabs/Spank/Slap"));
        slap.transform.localPosition = new Vector2(slap.transform.localPosition.x + Random.Range(-150,150), slap.transform.localPosition.y + Random.Range(-150, 150));
        slap.SetActive(true);
        slap.transform.SetParent(Canvas, false);
        slap.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.5f);
        slap.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => slap.SetActive(false));

        painFace.SetActive(true);
        CancelInvoke("SlapEnd");
        Invoke("SlapEnd", 0.35f);
    }

    void SlapEnd() {
        painFace.SetActive(false);
    }


    void SlapSuccess() {
        //boot.transform.DORotate(new Vector3(0, 0, 100), 0.8f);

        
        
    }

    #endregion

    void SlapAnimationEnd() {
        body.SetActive(false);

        //TutorialController.s.after_chicken_kicked();

        // call tutorial back
    }



}
