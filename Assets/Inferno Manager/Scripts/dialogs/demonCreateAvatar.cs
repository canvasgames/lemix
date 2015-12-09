using UnityEngine;
using System.Collections;
using DG.Tweening;

public class demonCreateAvatar : MonoBehaviour {
    
    public GameObject a, b, c, d, e, f, g, h, i, j, k;
    GameObject temp;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setMyAvatar(int line, int collum)
    {
        if (line == 1)
        {
            temp = (GameObject)Instantiate(a, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if(line == 2)
        {
            if (collum == 1)
            {
                temp = (GameObject)Instantiate(b, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else if (collum == 2)
            {
                temp = (GameObject)Instantiate(c, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                temp = (GameObject)Instantiate(d, new Vector3(0, 0, 0), Quaternion.identity);
            }
            
        }
        else if (line == 3)
        {
            if (collum == 1)
            {
                temp = (GameObject)Instantiate(e, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else if (collum == 2)
            {
                temp = (GameObject)Instantiate(f, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                temp = (GameObject)Instantiate(g, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        else if (line == 4)
        {
            if (collum == 1)
            {
                temp = (GameObject)Instantiate(h, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else if (collum == 2)
            {
                temp = (GameObject)Instantiate(i, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                temp = (GameObject)Instantiate(j, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        else
        {
            temp = (GameObject)Instantiate(k, new Vector3(0, 0, 0), Quaternion.identity);
        }

        temp.transform.parent = this.gameObject.transform;
        temp.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }
}
