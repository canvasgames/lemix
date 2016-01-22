using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;


public class ChickenController : MonoBehaviour {

    public static ChickenController s;
    public Transform Canvas;
    GameObject chicken;
    GameObject handTut;
    GameObject arrow;
    GameObject boot;

    bool touchActive = false;

    void Awake() { s = this; }

    void Start()
    {
        start_animation();
    }

    void Update()
    {
        //Input.GetMouseButtonDown();
    }

    public void start_animation()
    {
        Debug.Log(" COCOOOOOOOOOOO");
        chicken = (GameObject)Instantiate(Resources.Load("Prefabs/Chicken/Chicken"));
        chicken.transform.SetParent(Canvas, false);

        arrow = (GameObject)Instantiate(Resources.Load("Prefabs/Chicken/Arrow"));
        arrow.transform.SetParent(Canvas, false);
        arrow.GetComponent<CanvasGroup>().alpha = 0;
        arrow.GetComponent<CanvasGroup>().DOFade(1, 0.6f);
        //chicken.SetActive(true);

        handTut = (GameObject)Instantiate(Resources.Load("Prefabs/Chicken/handTut"));
        handTut.transform.SetParent(Canvas, false);
        handTut.GetComponent<CanvasGroup>().alpha = 0;
        handTut.GetComponent<CanvasGroup>().DOFade(1, 0.6f).OnComplete(() => hand_visible());
        //arrow.get
        //chicken.SetActive(true);

    }

    public void hand_visible()
    {
        handTut.transform.localPosition = new Vector3(-435, handTut.transform.localPosition.y);
        handTut.transform.DOLocalMoveX(109, 0.65f).OnComplete(() => restart_hand()) ; 
    }

    public void restart_hand()
    {
        Invoke("hand_visible", 0.3f);
    }


}
