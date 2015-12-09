using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateDemonsScrollView : MonoBehaviour {
    float initialY, xSpace, ySpace, xDif;

    public GameObject placeholder, rankText;
    GameObject myPlaceholder;
    // Use this for initialization
    void Start () {
        xDif = 120;
	    initialY = GetComponent<RectTransform>().rect.height/2 - 265 - 1150; //150 spacing, 650 half of contect UI object
        xSpace = placeholder.GetComponent<RectTransform>().rect.width ;
        xSpace = xSpace + placeholder.GetComponent<RectTransform>().rect.width / 4;

        ySpace = placeholder.GetComponent<RectTransform>().rect.height;
        ySpace = ySpace + placeholder.GetComponent<RectTransform>().rect.height / 4;

        Debug.Log(ySpace);
        createLine(initialY, 1, xSpace,1);
        createLine(initialY - ySpace , 3, xSpace,2);
        createLine(initialY - ySpace*2, 3, xSpace,3);
        createLine(initialY - ySpace*3, 3, xSpace,4);
        createLine(initialY - ySpace * 4, 1, xSpace,5);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void createLine(float yPosition, int numberOfAvatarsInLine, float xSpacing, int line)
    {

        //Create Rank txt
        myPlaceholder = (GameObject)Instantiate(rankText, new Vector3(0, 0, 0), Quaternion.identity);
        myPlaceholder.transform.parent = this.gameObject.transform;
        myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(-xSpacing * 2 + 10 + xDif, yPosition, 0);
        myPlaceholder.GetComponent<Text>().text = "Rank " + line.ToString() ;

        if (numberOfAvatarsInLine == 3)
        {
            //Middle object
            myPlaceholder = (GameObject)Instantiate(placeholder, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.parent = this.gameObject.transform;
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(xDif, yPosition, 0);
            myPlaceholder.GetComponent<demonCreateAvatar>().setMyAvatar(line,2);

            myPlaceholder = (GameObject)Instantiate(placeholder, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.parent = this.gameObject.transform;
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(-xSpacing + xDif, yPosition, 0);
            myPlaceholder.GetComponent<demonCreateAvatar>().setMyAvatar(line, 1);

            myPlaceholder = (GameObject)Instantiate(placeholder, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.parent = this.gameObject.transform;
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(xSpacing + xDif, yPosition, 0);
            myPlaceholder.GetComponent<demonCreateAvatar>().setMyAvatar(line, 3);
        }

        else if (numberOfAvatarsInLine == 2)
        {
            myPlaceholder = (GameObject)Instantiate(placeholder, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.parent = this.gameObject.transform;
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(-xSpacing/2 + xDif, yPosition, 0);
            myPlaceholder.GetComponent<demonCreateAvatar>().setMyAvatar(line, 1);

            myPlaceholder = (GameObject)Instantiate(placeholder, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.parent = this.gameObject.transform;
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(xSpacing/2 + xDif, yPosition, 0);
            myPlaceholder.GetComponent<demonCreateAvatar>().setMyAvatar(line, 2);
        }
        if (numberOfAvatarsInLine == 1)
        {
            //Middle object
            myPlaceholder = (GameObject)Instantiate(placeholder, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.parent = this.gameObject.transform;
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(xDif, yPosition, 0);
            myPlaceholder.GetComponent<demonCreateAvatar>().setMyAvatar(line, 1);
        }
    }
}
