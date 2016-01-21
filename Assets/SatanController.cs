using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SatanController : MonoBehaviour {
    public static SatanController s;
    public Transform Canvas;
    private GameObject Satan;

    GameObject FireBg;

    void Awake() { s = this; }
	// Use this for initialization
	void Start () {
        Invoke("satan_entering_animation", 1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void satan_entering_animation()
    {
        FireBg = (GameObject)Instantiate(Resources.Load("Prefabs/FireBg"));
        FireBg.SetActive(false);
        FireBg.transform.SetParent(Canvas, false);

        Satan = (GameObject)Instantiate(Resources.Load("Prefabs/SatanEntering"));
        Satan.transform.SetParent(Canvas, false);

        //Invoke("satan_finished_appearing", 1f);
    }

    public void satan_finished_appearing()
    {
        FireBg.SetActive(true);
        FireBg.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => fire_finished_fading());
        Satan.GetComponent<Animation>().Stop();
     }

    public void fire_finished_fading()
    {
        Satan.transform.DOLocalMove(new Vector3(-236f, -129f, 0), 0.5f);
        Satan.transform.DOScale(new Vector3(0.65f, 0.65f, 0), 0.5f);
    }
}
