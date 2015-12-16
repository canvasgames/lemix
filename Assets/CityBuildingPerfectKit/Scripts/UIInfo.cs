using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///-----------------------------------------------------------------------------------------
///   Namespace:      BE
///   Class:          UIInfo
///   Description:    class for diaplay building info & progress
///   Usage :		  
///   Author:         BraveElephant inc.                    
///   Version: 		  v1.0 (2015-11-15)
///-----------------------------------------------------------------------------------------
namespace BE {
	
	public class UIInfo : MonoBehaviour {
		
		public 	CanvasGroup groupRoot;
		public 	CanvasGroup groupInfo;
		public 	Text 		Name;
		public 	Text 		Level;

		public 	CanvasGroup groupProgress;
		public 	Text 		TimeLeft;
		public 	Image 		Progress;
		public 	Image 		Icon;

		public 	CanvasGroup groupCollect;
		public 	Image 		CollectDialog;
		public 	Image 		CollectIcon;

		public	Building 	building = null;

        GameObject myPart;
        GameObject bigDaddy, finalPos;

        void Update () {
			
		}

		// when user clicked collect dialog
		public void OnButtonCollect() {
            // do collect
            if(GLOBALS.s.TUTORIAL_PHASE == 9)
                TutorialController.s.sadnessCollected();
            if (GLOBALS.s.TUTORIAL_PHASE == 11)
                TutorialController.s.soulsCollected();

            building.Collect();

            bigDaddy = GameObject.Find("Canvas");
            finalPos = GameObject.Find("LabelGold");
            myPart = (GameObject)Instantiate(Resources.Load("Prefabs/sadness_particle"));
            myPart.transform.SetParent(bigDaddy.transform, false);
            myPart.transform.localPosition = transform.localPosition;
            myPart.GetComponent<particlesLogic>().move(bigDaddy.transform, finalPos.transform);

        }
		
	}
	
}