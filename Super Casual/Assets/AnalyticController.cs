using UnityEngine;
using DeltaDNA;

public class AnalyticController : MonoBehaviour {

    void Start() {
        // Enter additional configuration here


        // Launch the SDK
        DDNA.Instance.StartSDK(
            "87199148446217602329834496314561",
            "http://collect7976sprcs.deltadna.net/collect/api",
            "http://engage7976sprcs.deltadna.net",
            DDNA.AUTO_GENERATED_USER_ID
        );
    }

    void ReportTest() {
        Debug.Log("REPORTING EVENT!! ");
        EventBuilder eventParams = new EventBuilder();
        eventParams.AddParam("option", "sword");
        eventParams.AddParam("action", "sell");

        DDNA.Instance.RecordEvent("options", eventParams);

        /////////

        EventBuilder achievementParams = new EventBuilder()
                .AddParam("achievementName", "Sunday Showdown Tournament Win")
                .AddParam("achievementID", "SS-2014-03-02-01")
                .AddParam("reward", new EventBuilder()
                    .AddParam("rewardProducts", new ProductBuilder()
                         .AddRealCurrency("USD", 5000)
                        .AddVirtualCurrency("VIP Points", "GRIND", 20)
                        .AddItem("Sunday Showdown Medal", "Victory Badge", 1))
                        .AddParam("rewardName", "Medal"));

        DDNA.Instance.RecordEvent("achievement", achievementParams);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ReportTest();

        }


    }
}