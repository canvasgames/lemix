using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class MissionOption : ButtonCap
{
    public MissionType missionType = 0;
    

    public override void ActBT()
    {
        MissionsController.s.ActMission(missionType);
    }

}

