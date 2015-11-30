using UnityEngine;
using System.Collections;

public class BtsMenuClassCollider : MonoBehaviour {

    public bool deactivate = false;

    public void OnMouseUp()
    {
        if(GLOBALS.Singleton.DISCONNECTED_MENU == false && deactivate == false)
            ActBT();
        //if (deactivate == false)
    }

    public void OnMouseEnter()
    {
        if(deactivate == false)
            this.transform.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void OnMouseExit()
    {
        if (deactivate == false)
            this.transform.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void DeactivateBt()
    {
        deactivate = true;
        this.transform.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public virtual void ActBT()
    {

    }
}
