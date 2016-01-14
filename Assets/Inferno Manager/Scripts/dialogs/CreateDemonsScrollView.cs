using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class CreateDemonsScrollView : MonoBehaviour {
    float initialY, xSpace, ySpace, xMiddle;

    public GameObject placeholder, rankText;
    GameObject myPlaceholder;
    // Use this for initialization
    void Start () {
        xMiddle = 140;
	    initialY = GetComponent<RectTransform>().rect.height/2 - 960; //150 spacing, 650 half of contect UI object
        xSpace = placeholder.GetComponent<RectTransform>().rect.width - 20 ;

        ySpace = placeholder.GetComponent<RectTransform>().rect.height - 40;


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
        transform.DOLocalMoveY(GetComponent<RectTransform>().rect.height - 410, 2f);
    }


    void createLine(float yPosition, int numberOfAvatarsInLine, float xSpacing, int line)
    {

        //Create Rank txt
        createObject(line, 0, -xSpacing * 2 - 30 + xMiddle, yPosition);

        if (numberOfAvatarsInLine == 3)
        {
            //Middle object
            createObject(line, 2, xMiddle, yPosition);

            createObject(line, 1, -xSpacing + xMiddle, yPosition);
            createObject(line, 3, xSpacing + xMiddle, yPosition);
        }

        else if (numberOfAvatarsInLine == 2)
        {
            createObject(line, 1, -xSpacing / 2 + xMiddle, yPosition);

            createObject(line, 2, xSpacing / 2 + xMiddle, yPosition);
        }
        if (numberOfAvatarsInLine == 1)
        {
            //Middle object
            createObject(line, 1, xMiddle, yPosition);
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
           // myPlaceholder.GetComponent<Text>().text = "Rank " + line.ToString();
            if(line == 1)
                myPlaceholder.GetComponent<Text>().text = "Satan";
            else if(line == 2)
                myPlaceholder.GetComponent<Text>().text = "Archduke";
            else if (line == 3)
                myPlaceholder.GetComponent<Text>().text = "Count";
            else if (line == 4)
                myPlaceholder.GetComponent<Text>().text = "Baron";
            else if (line == 5)
                myPlaceholder.GetComponent<Text>().text = "Lord";


            
                
                
        }
    }
}
