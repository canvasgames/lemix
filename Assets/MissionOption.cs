using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MissionOption : ButtonCap
{
    public MissionType missionType;

    public GameObject check;
    public GameObject targetQuantity;
    public GameObject text;


    /*public MissionOption (Mission mission) {
        //targetQuantity = mission.
        missionType = mission.type;
        Init();
    }*/

    public override void ActBT()
    {
        MissionsController.s.ActMission(missionType);
    }

    public void Init(Mission mission) {
        missionType = mission.type;

        if (missionType == MissionType.Spank) {
            text.GetComponent<Text>().text = "Spank Hitler's Ass";
            targetQuantity.GetComponent<Text>().text = "0/1";
        }
        else if (missionType == MissionType.Build) {
            text.GetComponent<Text>().text = "Build 2 Punisher Buildings";
            targetQuantity.GetComponent<Text>().text = "0/2";
        }

        else if (missionType == MissionType.CollectSouls) {
            text.GetComponent<Text>().text = "Collect 300 Souls From Hell's Gate";
            targetQuantity.GetComponent<Text>().text = mission.curQuantity+"/"+mission.targetQuantity;
        }

        if (mission.isComplete) {
            targetQuantity.SetActive(false);
            check.SetActive(true);
        }
    }

}

