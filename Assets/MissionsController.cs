using UnityEngine;
using System.Collections;

public enum MissionType {
    None = 0,
    Spank = 1,
    Build = 2,
    LevelUp = 3,
}

public class MissionsController : MonoBehaviour {
    public static MissionsController s;
	// Use this for initialization

    void Awake() { s = this; }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetMissionInfoForList(MissionType type) { 
        if(type == MissionType.Spank)
        {

        }

        if (type == MissionType.LevelUp) {

        }
    }


    public void MissionBtPressed()
    {
        GameObject tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DialogMissions"));
        MenusController.s.enterFromDown(tempObject, "DialogMission", 0, 0);
    }

    public void ActMission(MissionType type)
    {

    }
}
