using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CatastropheItem : MonoBehaviour {
    public GameObject cat;
    public GameObject locked;
    public GameObject Intensity, Chance, Activate,title,description;
    GameObject temp;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initCatastrophe(int line)
    {
        if(line == 1)
        { 
            temp = (GameObject)Instantiate(cat, new Vector3(0, 0, 0), Quaternion.identity);
            title.GetComponent<Text>().text = "Cat-astrophe";
            description.GetComponent<Text>().text = "A giant cat have 40% of chance to destroy a city \nReward: 100 souls";
        }
        else
        { 
            temp = (GameObject)Instantiate(locked, new Vector3(0, 0, 0), Quaternion.identity);
            Intensity.SetActive(false);
            Chance.SetActive(false);
            Activate.SetActive(false);
            if(line == 2)
            {
                title.GetComponent<Text>().text = "Pool's Stairs (Locked)";
                description.GetComponent<Text>().text = "Remove pool's stairs and have 40% of chance to drown\nReward: 100 souls";
            }
            if (line == 3)
            {
                title.GetComponent<Text>().text = "Black Friday (Locked)";
                description.GetComponent<Text>().text = "Black Friday have 40% to end with deaths\nReward: 100 souls";
            }
            if (line == 4)
            {
                title.GetComponent<Text>().text = "Nutella (Locked)";
                description.GetComponent<Text>().text = "Destroy Nutella's Factory, 40% of chance to raise the rate of suicides\nReward: 100 souls";
            }
            if (line == 5)
            {
                title.GetComponent<Text>().text = "ACME (Locked)";
                description.GetComponent<Text>().text = "40% chance of occurring rain of anvils\nReward: 100 souls";
            }

        }


        temp.transform.SetParent(gameObject.transform, false);
        temp.GetComponent<RectTransform>().localPosition = new Vector3(-389, 21, 0);
    }
}
