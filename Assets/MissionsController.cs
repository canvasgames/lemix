using UnityEngine;
using System.Collections;

public enum MissionType {
    None = 0,
    Spank = 1,
    Build = 2,
    LevelUp = 3,
    CollectSouls = 4
}

public class MissionsController : MonoBehaviour {
    public static MissionsController s;
	// Use this for initialization

    void Awake() { s = this; }
	
	// Update is called once per frame
	void Update () {
        // GLOBALS.s.TUTORIAL_OCCURING = true ;
    }

    public void GetMissionInfoForList(MissionType type) { 
        if(type == MissionType.Spank)
        {

        }

        if (type == MissionType.LevelUp) {

        }
    }

    #region Open and Close
    public void OpenMissionDialog()
    {
        if(GLOBALS.s.TUTORIAL_OCCURING == false && GLOBALS.s.DIALOG_ALREADY_OPENED == false)
        {
            Debug.Log(" OPENING MISSION DIALOG");
            GameObject tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/DialogMissions"));
            MenusController.s.moveMenu(MovementTypes.Down, tempObject, "DialogMission", 0, 0);
        }

    }

    public void CloseMissionDialog() {
        Debug.Log(" closing MISSION DIALOG");

        MenusController.s.destroyMenu("DialogMission", null);
    }

    #endregion

    public void ActMission(MissionType type)
    {
        CloseMissionDialog();

        if (type == MissionType.Spank) {
            SpankController.s.StartAnimation();
        }
        else if (type == MissionType.Build) {
            BE.SceneTown.instance.OnButtonShop();
        }
        else if (type == MissionType.CollectSouls) {
            Vector3 pos = new Vector3(19f, 0f, 4f);
            BE.SceneTown.instance.move_camera_to_building(pos);
        }

    }

    public void RewardMisison(MissionType type) {
        if(type == MissionType.Spank) {
            GameObject tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "SmallScroll", 0, 0);

            //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
            tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SatanProud"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "Satan", 0, 0);

            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Buttons/CollectSoulsBt"));
            MenusController.s.moveMenu(MovementTypes.Left, temp, "Bt", 0, 0);

            /*
            CollectSoulsBt temp = (CollectSoulsBt)Instantiate(Resources.Load("Prefabs/Buttons/CollectSoulsBt"));
            temp.myType = CollectSoulsType.MissionSpank;
            temp.quantityToCollect = 250;
            */
        }
    }

    public void OnSoulsCollected() {
        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("SmallScroll", null);
        MenusController.s.destroyMenu("Bt", null);
    }
}
