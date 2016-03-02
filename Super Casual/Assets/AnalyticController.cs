using UnityEngine;
using DeltaDNA;

public class AnalyticController : MonoBehaviour {

    public static AnalyticController s;

    void Awake() { s = this; }

    void Start() {
        // Enter additional configuration here
        
        // Launch the SDK
        DDNA.Instance.Settings.DebugMode = true;
        DDNA.Instance.ClientVersion = "1.0.0";
        DDNA.Instance.StartSDK(
            "87199148446217602329834496314561",
            "http://collect7976sprcs.deltadna.net/collect/api",
            "http://engage7976sprcs.deltadna.net",
            DDNA.AUTO_GENERATED_USER_ID
        );
    }


    public void ReportGameStarted() {
        EventBuilder eventParams = new EventBuilder();
        //eventParams.AddParam("clientVersion", "teste");
        eventParams.AddParam("isTutorial", false);
        eventParams.AddParam("missionName", "game started");
        //eventParams.AddParam("platform", DDNA.Instance.Platform);
        eventParams.AddParam("userHighScore",USER.s.BEST_SCORE);
        eventParams.AddParam("userTotalGames", USER.s.TOTAL_GAMES);
        eventParams.AddParam("userTotalVideosWatched", USER.s.TOTAL_VIDEOS_WATCHED);

        DDNA.Instance.RecordEvent("missionStarted", eventParams);
    }

    public void ReportGameEnded(string killer_wave_name, int duration) {
        EventBuilder eventParams = new EventBuilder();
        //eventParams.AddParam("clientVersion", "teste");
        eventParams.AddParam("isTutorial", false);
        eventParams.AddParam("missionName", "game ended");
       // eventParams.AddParam("platform", DDNA.Instance.Platform);
        eventParams.AddParam("userHighScore", USER.s.BEST_SCORE);
        eventParams.AddParam("userTotalGames", USER.s.TOTAL_GAMES);
        eventParams.AddParam("userTotalVideosWatched", USER.s.TOTAL_VIDEOS_WATCHED);
        eventParams.AddParam("userScore", globals.s.BALL_FLOOR);
        eventParams.AddParam("killerWaveName", killer_wave_name);
        eventParams.AddParam("gameDuration", 5);

        DDNA.Instance.RecordEvent("missionCompleted", eventParams);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //ReportTest();

        }
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


        eventParams = new EventBuilder();
        eventParams.AddParam("aaa", "lime");
        eventParams.AddParam("aaction", "be a dark Lord");
        DDNA.Instance.RecordEvent("zeptile", eventParams);
    }


}