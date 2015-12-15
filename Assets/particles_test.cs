using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class particles_test : MonoBehaviour
{
    GameObject myPart;
    public Transform bigDaddy, finalPos;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseUp()
    {
        myPart = (GameObject)Instantiate(Resources.Load("Prefabs/sadness_particle"));
        myPart.transform.SetParent(bigDaddy, false);
        myPart.transform.localPosition = transform.localPosition;
        myPart.GetComponent<particlesLogic>().move(bigDaddy, finalPos) ;

    }
}