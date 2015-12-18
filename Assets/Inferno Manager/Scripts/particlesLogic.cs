using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class particlesLogic : MonoBehaviour {
    Transform finalPos, BigD, initialPos;
    GameObject puft;
    float xPosLoc, yPosLoc;
    // Use this for initialization
    void Start () {
	
	}

    public void move(Transform daddy, Transform fPos, Transform iPos)
    {
        finalPos = fPos;
        BigD = daddy;
        initialPos = iPos;
        transform.localPosition = new Vector3(initialPos.localPosition.x, initialPos.localPosition.y, daddy.transform.position.z);
        transform.DOLocalMove(finalPos.transform.localPosition,1f).OnComplete(createsmoke);
    }

    void createsmoke()
    {

        puft = (GameObject)Instantiate(Resources.Load("Prefabs/smoke"));

        puft.transform.SetParent(BigD, false);
        puft.transform.localPosition = transform.localPosition;
        puft.transform.localScale = new Vector3(0f, 0f, 0f);

        Invoke("smokeGrow", 0.1f);
    }

    void smokeGrow()
    {
        puft.transform.DOScale(1f, 0.2f).OnComplete(destroyObj);
    }

    void destroyObj()
    {
        Destroy(transform.gameObject);
        //puft.transform.DOScale(0.5f, 0.1f).OnComplete(destroySmoke);
        puft.GetComponent<Image>().DOFade(0f,0.4f).OnComplete(destroySmoke); ;
    }

    void destroySmoke()
    {
        
        Destroy(puft);
    } 

// Update is called once per frame
    void Update ()
    {

    }
}
