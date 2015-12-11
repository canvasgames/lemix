using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

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

        createLine(initialY, 1, xSpace,1);
        createLine(initialY - ySpace , 3, xSpace,2);
        createLine(initialY - ySpace*2, 3, xSpace,3);
        createLine(initialY - ySpace*3, 3, xSpace,4);
        createLine(initialY - ySpace * 4, 1, xSpace,5);

        //transform.DOLocalMoveY(GetComponent<RectTransform>().rect.height, 3f);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void moveList()
    {
        transform.DOLocalMoveY(GetComponent<RectTransform>().rect.height - 530, 3f);
    }


    void createLine(float yPosition, int numberOfAvatarsInLine, float xSpacing, int line)
    {

        //Create Rank txt
        createObject(line, 0, -xSpacing * 2 + 10 + xDif, yPosition);

        if (numberOfAvatarsInLine == 3)
        {
            //Middle object
            createObject(line, 2, xDif, yPosition);

            createObject(line, 1, -xSpacing + xDif, yPosition);
            createObject(line, 3, xSpacing + xDif, yPosition);
        }

        else if (numberOfAvatarsInLine == 2)
        {
            createObject(line, 1, -xSpacing / 2 + xDif, yPosition);

            createObject(line, 2, xSpacing / 2 + xDif, yPosition);
        }
        if (numberOfAvatarsInLine == 1)
        {
            //Middle object
            createObject(line, 1, xDif + xDif, yPosition);
        }
    }

    void createObject(int line, int collumn, float xPosition, float yPosition)
    {
        
        //If have to create a avatar (else is Rank txt)
        if (collumn != 0)
        {
            myPlaceholder = (GameObject)Instantiate(placeholder, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.SetParent(gameObject.transform, false);
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);
            myPlaceholder.GetComponent<demonCreateAvatar>().setMyAvatar(line, collumn);
        }
        else
        {
            myPlaceholder = (GameObject)Instantiate(rankText, new Vector3(0, 0, 0), Quaternion.identity);
            myPlaceholder.transform.SetParent(gameObject.transform, false);
            myPlaceholder.GetComponent<RectTransform>().localPosition = new Vector3(xPosition, yPosition, 0);
            myPlaceholder.GetComponent<Text>().text = "Rank " + line.ToString();
        }
    }
}
