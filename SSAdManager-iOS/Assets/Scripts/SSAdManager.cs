using UnityEngine;
using System.Collections;

public class SSAdManager : MonoBehaviour {
	
	//PlayHaven variables
	public static bool PlayHavenRunFullScreenAd;							//Enables/disables Full screen ad in Update method
	
	// Use this for initialization
	void Start () {
		PlayHavenRunFullScreenAd = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayHavenRunFullScreenAd)
		{
			gameObject.SendMessage("RequestPlayHavenContent");
			PlayHavenRunFullScreenAd = false;
		}
	}
	
	
	public static void showChartBoostFullScreenAd()
	{
		if(SSAdInitializer.ChartBoostActiveStaticFlag)
			ChartBoostBinding.showInterstitial(null);	
	}
	
	public static void showChartBoostMoreGamesAd()
	{
		if(SSAdInitializer.ChartBoostActiveStaticFlag)
			ChartBoostBinding.showMoreApps();
	}
	
	public static void showRevMobPopUpAd()
	{
		if(SSAdInitializer.RevMobActiveStaticFlag)
			SSAdInitializer.revMobSession.ShowPopup();
	}
	
	public static void showRevMobFullScreenAd()
	{
		if(SSAdInitializer.RevMobActiveStaticFlag)
			SSAdInitializer.revMobSession.ShowFullscreen();
	}
	
	public static void showAdMobBanner()
	{
		if(SSAdInitializer.AdMobActiveStaticFlag)
			AdMobBinding.createBanner(AdMobBannerType.SmartBannerPortrait, AdMobAdPosition.BottomCenter);
	}
	
	public static void showPlayHavenFullScreenAd()
	{
		//if(SSAdInitializer.PlayHavenActiveStaticFlag)	
			//PlayHavenRunFullScreenAd = true;
	}
	
	public static void showPlayHavenMoreGamesAd()
	{
		//if(SSAdInitializer.PlayHavenActiveStaticFlag)	
	}
	
	public static void showIAdsBanner()
	{
		//if(SSAdInitializer.IAdsActiveStaticFlag)
			
	}
}
