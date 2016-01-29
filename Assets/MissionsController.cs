﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MissionType {
    None = 0,
    Spank = 1,
    Build = 2,
    LevelUp = 3,
    CollectSouls = 4
}

public class Mission {
    public int targetQuantity = 0;
    public int curQuantity = 0;
    public MissionType type;
    public bool isComplete = false;

    

    public Mission( MissionType _type, int _targetQuantity) {
        targetQuantity = _targetQuantity;
        type = _type;
        curQuantity = 0;
    }
}

public class MissionsController : MonoBehaviour {
    public static MissionsController s;
    int PunisherBuildingsCont = 0;
    MissionOption[] MissionOptions;
    public List<Mission> allMissions = new List<Mission>();
    public float soulsCollected = 0;

    // Use this for initialization

    void Awake() {
        s = this;  //}

        // ADDING MISSIONS TEMPORALY
        allMissions.Add(new Mission(MissionType.Spank, 1));
        allMissions.Add(new Mission(MissionType.Build, 2));
        allMissions.Add(new Mission(MissionType.CollectSouls, 250));
    }
    
	
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

            tempObject.GetComponent<DialogMission>().CreateMissionsList(allMissions);

            //DialogMission a = (DialogMission)tempObject;
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

    //Reward Mission Dialog
    public void RewardMisison(MissionType type) {
        if(type == MissionType.Spank) {
            GameObject tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "SmallScroll", 0, 0, "MissionSpank");

            //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
            tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan/SatanProud"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "Satan", 0, 0);

            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Buttons/CollectSoulsBt"));
            MenusController.s.moveMenu(MovementTypes.Left, temp, "Bt", 0, 0);
            temp.GetComponent<CollectSoulsBt>().myBtType = CollectSoulsType.MissionSpank;

            /*
            CollectSoulsBt temp = (CollectSoulsBt)Instantiate(Resources.Load("Prefabs/Buttons/CollectSoulsBt"));
            temp.myType = CollectSoulsType.MissionSpank;
            temp.quantityToCollect = 250;
            */
        }

        if (type == MissionType.Build) {
            GameObject tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "SmallScroll", 0, 0, "MissionBuild");

            //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
            tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan/SatanProud"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "Satan", 0, 0);

            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Buttons/CollectSoulsBt"));
            MenusController.s.moveMenu(MovementTypes.Left, temp, "Bt", 0, 0);
            temp.GetComponent<CollectSoulsBt>().myBtType = CollectSoulsType.MissionBuild;
        }

        if (type == MissionType.CollectSouls) {
            GameObject tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/SmallScroll"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "SmallScroll", 0, 0, "MissionCollect");

            //tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan"));
            tempObject = (GameObject)Instantiate(Resources.Load("Prefabs/Satan/SatanProud"));
            MenusController.s.moveMenu(MovementTypes.Left, tempObject, "Satan", 0, 0);

            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Buttons/CollectSoulsBt"));
            MenusController.s.moveMenu(MovementTypes.Left, temp, "Bt", 0, 0);
            temp.GetComponent<CollectSoulsBt>().myBtType = CollectSoulsType.MissionBuild;
        }
    }

    public void OnSoulsCollected(CollectSoulsType btType) {
        MenusController.s.destroyMenu("Satan", null);
        MenusController.s.destroyMenu("SmallScroll", null);
        MenusController.s.destroyMenu("Bt", null);

        if (btType == CollectSoulsType.MissionSpank)
            allMissions[0].isComplete = true;
        else if (btType == CollectSoulsType.MissionBuild)
            allMissions[1].isComplete = true;
        else if (btType == CollectSoulsType.MissionCollect)
            allMissions[2].isComplete = true;
    }

    public void OnBuildingComplete(int buildingType) {
        if (buildingType >= 11 && buildingType <= 15) { // if the building is a punisher type
            PunisherBuildingsCont++;
            if(PunisherBuildingsCont == 2) {
                RewardMisison(MissionType.Build);
            }
        }
    }

    public void OnSoulsCollected(float quant) {
        soulsCollected += quant;
        allMissions[2].curQuantity += (int)quant;
        if (soulsCollected >= allMissions[2].targetQuantity && allMissions[2].isComplete == false)
            RewardMisison(MissionType.CollectSouls);
    }

}
