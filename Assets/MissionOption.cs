using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class MissionOption : ButtonCap
{
    public MissionType missionType;
    

    public override void ActBT()
    {
        MissionsController.s.ActMission(missionType);
    }

}

