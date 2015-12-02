using UnityEngine;
using System.Collections;

public class BtsMenuClassCollider : MonoBehaviour {

    public bool deactivate = false;
    bool clickedState = false;

    public void OnMouseDown()
    {
        if (GLOBALS.Singleton.DISCONNECTED_MENU == false && GLOBALS.Singleton.WAITING_MENU == false &&
            deactivate == false)
        {
            clicked();
        }
    }
    public void OnMouseUp()
    {
        if(clickedState == true)
            ActBT();
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

    public virtual void clicked()
    {
        clickedState = true;         
    }
    public virtual void ActBT()
    {
        clickedState = false;
    }
}
