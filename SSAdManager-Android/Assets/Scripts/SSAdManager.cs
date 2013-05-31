using UnityEngine;
using System.Collections;

public class SSAdManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public static void showChartBoostFullScreenAd()
	{
		if(SSAdInitializer.ChartBoostActiveStaticFlag)
			ChartBoostAndroid.showInterstitial(null);	
	}
	
	public static void showChartBoostMoreGamesAd()
	{
		if(SSAdInitializer.ChartBoostActiveStaticFlag)
			ChartBoostAndroid.showMoreApps();
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
			AdMobAndroid.createBanner(AdMobAndroidAd.smartBanner,AdMobAdPlacement.BottomCenter);
	}
	
	public static void showPlayHavenFullScreenAd()
	{
		//if(SSAdInitializer.PlayHavenActiveStaticFlag)	
	}
	
	public static void showPlayHavenMoreGamesAd()
	{
		//if(SSAdInitializer.PlayHavenActiveStaticFlag)	
	}
}
