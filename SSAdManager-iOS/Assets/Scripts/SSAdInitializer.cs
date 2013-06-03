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
	
	//Admob variables
	public bool AdMobActiveFlag;								//Do you want AdMob activated in this project?
	public static bool AdMobActiveStaticFlag;					//Add condition of AdMob Active Flag to this
	public string AdMobID = "";									//AdMob ID
	
	//PlayHaven variables
	public bool PlayHavenActiveFlag;							//Do you want playHaven activated in this project?
	public static bool PlayHavenActiveStaticFlag;				//Add condition of Admob Active Flag to this
	
	
	// Use this for initialization
	void Awake () {
		
		//Set Static flags
		ChartBoostActiveStaticFlag = ChartBoostActiveFlag;
		RevMobActiveStaticFlag = RevMobActiveFlag;
		AdMobActiveStaticFlag = AdMobActiveFlag;
		PlayHavenActiveStaticFlag = PlayHavenActiveFlag;
		
		//Initialize ChartBoost
		if(ChartBoostActiveFlag)
		{
			
		}
		
		//Initialize RevMob
		if(RevMobActiveFlag)
		{
		
		}
		
		//Initialize Admob
		if(AdMobActiveFlag)
		{
				
		}
		
		//Initialize Playhaven
		if(PlayHavenActiveFlag){
			
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}