using UnityEngine;
using System.Collections;

public class TestScriptGUI : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, 300, 100), "Show Chartboost Full screen Ad"))
		{
			SSAdManager.showChartBoostFullScreenAd();
		}
		
		if(GUI.Button(new Rect(0, 150, 300, 100), "Show Chartboost More Games Ad"))
		{
			SSAdManager.showChartBoostMoreGamesAd();
		}
		
		if(GUI.Button(new Rect(0, 300, 300, 100), "Show RevMob PopUp Ad"))
		{
			SSAdManager.showRevMobPopUpAd();
		}
		
		if(GUI.Button(new Rect(0, 450, 300, 100), "Show RevMob Full Screen Ad"))
		{
			SSAdManager.showRevMobFullScreenAd();
		}
		
		if(GUI.Button(new Rect(0, 600, 300, 100), "Show AdMob Bottom Banner"))
		{
			SSAdManager.showAdMobBanner();
		}
		
		if(GUI.Button(new Rect(0, 750, 300, 100), "Show Play Haven Full Screen Ad"))
		{
			SSAdManager.showPlayHavenFullScreenAd();
		}
		
		if(GUI.Button(new Rect(0, 900, 300, 100), "Show iAds Banner"))
		{
			SSAdManager.showIAdsBanner();
		}
		
	}
}
