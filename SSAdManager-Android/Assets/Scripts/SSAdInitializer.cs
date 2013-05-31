using UnityEngine;
using System.Collections;

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
	
	
	// Use this for initialization
	void Awake () {
		
		//Set Static flags
		ChartBoostActiveStaticFlag = ChartBoostActiveFlag;
		RevMobActiveStaticFlag = RevMobActiveFlag;
		
		//Initialize ChartBoost
		if(ChartBoostActiveFlag)
		{
			ChartBoostAndroid.init(ChartBoostID, ChartBoostSIG, false);
			ChartBoostAndroid.onStart();
			ChartBoostAndroid.cacheInterstitial(null);
			ChartBoostAndroid.cacheMoreApps();
		}
		
		if(RevMobActiveFlag)
		{
				
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
