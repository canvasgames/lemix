using UnityEngine;
using System.Collections;

public class QA : MonoBehaviour {

    public static QA s;

    public bool DontSave = false;
    public bool NoTutorial = false;
    public bool CameraNavigationOnRelease = false;
    public bool CrazyProduction = false;

    public int ProductionMultiplier = 1;


    void Awake() {
        s = this;
        if (CrazyProduction) ProductionMultiplier = 20;
        else                 ProductionMultiplier = 1;
    }
}

