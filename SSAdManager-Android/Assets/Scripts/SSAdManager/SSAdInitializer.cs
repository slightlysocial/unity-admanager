using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SSAdInitializer : MonoBehaviour {
	
	//ChartBoost Variables
	public bool ChartBoostActiveFlag;							//Do you want Chartboost activated in this project?
	public static bool ChartBoostActiveStaticFlag;				//Add condition of Chartboost Active Flag to this
	public string ChartBoostID = "";							//ChartBoost ID
	public string ChartBoostSIG = "";							//ChartBoost Signature
	
	//RevMob Variables
	public bool RevMobActiveFlag;								//Do you want RevMob activated in this project?
	public static bool RevMobActiveStaticFlag;					//Add condition of RevMob Active Flag to this
	public string RevMobID = "";								//RevMob ID
	private static readonly Dictionary<String, String> REVMOB_APP_IDS = new Dictionary<String, String>();	
																//Must use this to store RevMob ID
	//public static RevMob revMobSession;								//Used to create a session
	public static RevMob revMobSession;							//RevMob session variable
	
	//Admob variables
	public bool AdMobActiveFlag;								//Do you want AdMob activated in this project?
	public static bool AdMobActiveStaticFlag;					//Add condition of AdMob Active Flag to this
	public string AdMobID = "";									//AdMob ID
	
	//PlayHaven variables
	public bool PlayHavenActiveFlag;							//Do you want playHaven activated in this project?
	public static bool PlayHavenActiveStaticFlag;				//Add condition of PlayHaven Active Flag to this
	
	//iAds variables
	public bool IAdsActiveFlag;							//Do you want iAds activated in this project?
	public static bool IAdsActiveStaticFlag;			//Add condition of iAds Active flag to this
	
	//Tapyjoy variables
	public bool TapJoyActiveFlag;						//Do you want TapJoy activated in this project
	public static bool TapJoyActiveStaticFlag;			// Add conidition of TapJoy Active flag to this
	public string TapJoyID = "";
	public string TapJoySecretKey = "";
	
	//Vungle variables
	public bool VungleActiveFlag;						//Do you want Vungle activated in this project?
	public static bool VungleActiveStaticFlag;			//Add condition of Vungle Active Flag to this
	public string VungleID = "";						//Vungle ID
	
	//Flurry variables
	public bool FlurryActiveFlag;						//Do you want Flurry activated in this project?
	public static bool FlurryActiveStaticFlag;			//Add condition of Flurry Active Flag to this
	public string FlurryID = "";						//Flurry ID
	
	
	// Use this for initialization
	void Awake () {
		
		//Set Static flags
		ChartBoostActiveStaticFlag = ChartBoostActiveFlag;
		RevMobActiveStaticFlag = RevMobActiveFlag;
		AdMobActiveStaticFlag = AdMobActiveFlag;
		PlayHavenActiveStaticFlag = PlayHavenActiveFlag;
		IAdsActiveStaticFlag = IAdsActiveFlag;
		TapJoyActiveStaticFlag = TapJoyActiveFlag;
		VungleActiveStaticFlag = VungleActiveFlag;
		FlurryActiveStaticFlag = FlurryActiveFlag;
		
		//Initialize ChartBoost
		if(ChartBoostActiveFlag)
		{
			ChartBoostAndroid.init(ChartBoostID, ChartBoostSIG, false);
			ChartBoostAndroid.onStart();
			ChartBoostAndroid.cacheInterstitial(null);
			ChartBoostAndroid.cacheMoreApps();
		}
		
		//Initialize RevMob
		if(RevMobActiveFlag)
		{
			REVMOB_APP_IDS.Add("Android", RevMobID);
			revMobSession = RevMob.Start(REVMOB_APP_IDS);
		}
		
		//Initialize Admob
		if(AdMobActiveFlag)
		{
			AdMobAndroid.init(AdMobID);
		}
		
		//Initialize Playhaven
		if(PlayHavenActiveFlag){
			gameObject.SendMessage("OpenNotification");
		}
		
		//Initialize iAds
		if(IAdsActiveFlag)
		{
			
		}
		
		//Initialize TapJoy
		if(TapJoyActiveFlag)
		{
			//TapjoyPluginIOS.RequestTapjoyConnect(TapJoyID, TapJoySecretKey);
			TapjoyPluginAndroid.RequestTapjoyConnect(TapJoyID, TapJoySecretKey);
		}
		
		//Initialize Vungle
		if(VungleActiveFlag)
		{
			//VungleBinding.startWithAppId(VungleID);	
		}
		
		//Initialize Flurry
		if(FlurryActiveFlag)
		{
			//FlurryBinding.startSession(FlurryID);
			//FlurryBinding.logEvent("Testing session started", false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
