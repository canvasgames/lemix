using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;


public enum CollectSoulsType {
    MissionSpank = 0,
    MissionBuild = 1,
    Catastrophe = 2
}
public class CollectSoulsBt : ButtonCap {
    public GameObject allObjects, souls;
    [HideInInspector]public int quantityToCollect;
    public CollectSoulsType myType;
    // Use this for initialization
    void Start() {

    }

    public override void ActBT() {

        BE.SceneTown.instance.CapacityCheck();

        souls = (GameObject)Instantiate(Resources.Load("Prefabs/SoulsForCollect"));
        souls.transform.SetParent(MenusController.s.bigDaddy, false);

        souls.SetActive(true);
        particlesLogic[] particles;
        particles = souls.GetComponentsInChildren<particlesLogic>() as particlesLogic[];
        souls.transform.GetComponent<CanvasGroup>().DOFade(1f, 0.1f);
        foreach (particlesLogic part in particles) {
            part.moveCatrastofe();
        }

        if (myType == CollectSoulsType.MissionSpank) {
            Debug.Log("Invoking");
            BE.SceneTown.Elixir.ChangeDelta((double)quantityToCollect);
            //allObjects.transform.DOMoveY(-450, 1f).OnComplete(() => M);
            Invoke("onAnimationEnd", 1.5f);
        }
    }

    void onAnimationEnd() {
        Debug.Log("Invoked");

        MissionsController.s.OnSoulsCollected();
    }

}
