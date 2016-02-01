using UnityEngine;
using System.Collections;

public class QA : MonoBehaviour {

    public static QA s;

    public bool DontSave = false;
    public bool NoTutorial = false;
    public bool CameraNavigationOnRelease = false;


    void Awake() {
        s = this;
    }
}

