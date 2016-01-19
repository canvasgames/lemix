using UnityEngine;
using System.Collections;

public class QA : MonoBehaviour {

    public static QA s;

    public bool DontSave = false;
    public bool NoTutorial = false;

    void Awake()
    {
        s = this;
    }

}
