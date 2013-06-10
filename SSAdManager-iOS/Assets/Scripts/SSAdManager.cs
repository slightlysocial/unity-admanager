using UnityEngine;
using System.Collections;

public class SSAdManager : MonoBehaviour {
	
	//PlayHaven variables
	public static bool PlayHavenRunFullScreenAd;							//Enables/disables Full screen ad in Update method
	
	//PList website
	public string PListURL = "";
	public static string PListStaticURL = "";
	public string CurrentVersionNumber = "";
	public static string CurrentStaticVersionNumber = "";
	
	
	//Placeholders for ads
	public static bool adInReview = true;
	public static AdValue adOnLoad1,
						adOnLoad2,
						adOnPause1,
						adOnPause2,
						adOnReturn1,
						adOnReturn2,
						adOnGameOver,
						adBanner;
					
	//Ad values
	public enum AdValue
	{
		OFF,
		CHARTBOOST_FS,
		CHARTBOOST_MOREGAMES,
		REVMOB_FS,
		REVMOB_POPUP,
		REVMOB_BANNER,
		PLAYHAVEN_FS,
		PLAYHAVEN_MOREGAMES,
		APPLIFT,
		ADMOB_BANNER,
		IADS_BANNER,
		MOBCLIX_BANNER,
		VUNGLE,
	};
	
	// Use this for initialization
	void Start () {
		PlayHavenRunFullScreenAd = false;
		PListStaticURL = PListURL;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayHavenRunFullScreenAd)
		{
			gameObject.SendMessage("RequestPlayHavenContent");
			PlayHavenRunFullScreenAd = false;
		}
	}
	
	public static IEnumerator LoadPList()
	{
		//Load plist
		WWW www = new WWW(PListStaticURL);
        yield return www;
		
		//Parse plist
		Hashtable hashTable = new Hashtable();
		PListManager.ParsePListText(www.text, ref hashTable);
		
		foreach(object key in hashTable.Keys)
		{
			if (key.Equals(CurrentStaticVersionNumber))
			{
				Debug.Log("found PLIST version..." + key);
				
				Hashtable adTable = (Hashtable)hashTable[key];
				
				adOnLoad1 = (AdValue)(adTable["AD_ON_LOAD1"]);
				adOnLoad2= (AdValue)(adTable["AD_ON_LOAD2"]);
				adOnPause1 = (AdValue)(adTable["AD_ON_PAUSE1"]);
				adOnPause2 = (AdValue)(adTable["AD_ON_PAUSE2"]);
				adOnReturn1 = (AdValue)(adTable["AD_ON_RETURN1"]);
				adOnReturn2 = (AdValue)(adTable["AD_ON_RETURN2"]);
				adOnGameOver = (AdValue)(adTable["AD_ON_GAMEOVER"]);
				adBanner = (AdValue)(adTable["AD_BANNER"]);
				adInReview  = (((int)adTable["AD_IN_REVIEW"]) != 1);
				
				Debug.Log("loaded PLIST complete");
				
				break;//found version...exit loop
			}
		}
	}
	
	public static void ShowAd(AdValue adValue)
	{
		//don't show ads if in review
		if(adInReview || PlayerPrefs.HasKey("PurchasedSlightlySocialItem"))
		{
			return;	
		}
		
		//don't show ads if player has purchased something
		//TODO
		
		if(adValue == AdValue.CHARTBOOST_FS)
		{
			showChartBoostFullScreenAd();
		}
		else if(adValue == AdValue.CHARTBOOST_MOREGAMES)
		{
			showChartBoostMoreGamesAd();
		}
		else if(adValue == AdValue.REVMOB_FS)
		{
			showRevMobFullScreenAd();
		}
		else if(adValue == AdValue.REVMOB_POPUP)
		{
			showRevMobPopUpAd();
		}
		else if(adValue == AdValue.REVMOB_BANNER)
		{
			showRevMobBannerAd();
		}
		else if(adValue == AdValue.PLAYHAVEN_FS)
		{
			showPlayHavenFullScreenAd();
		}
		else if(adValue == AdValue.PLAYHAVEN_MOREGAMES)
		{
			showPlayHavenMoreGamesAd();
		}
		else if(adValue == AdValue.APPLIFT)
		{
			//TODO
		}
		else if(adValue == AdValue.ADMOB_BANNER)
		{
			showAdMobBanner();
		}
		else if(adValue == AdValue.IADS_BANNER)
		{
			showIAdsBanner();
		}
		else if(adValue == AdValue.MOBCLIX_BANNER)
		{
			//TODO
		}
		else if(adValue == AdValue.VUNGLE)
		{
			//TODO
		}
		
	}
	
	public static void ShowOnLoad()
	{
		ShowAd(adOnLoad1);
		ShowAd(adOnLoad2);
	}
	
	public static void ShowOnPause()
	{
		ShowAd(adOnPause1);
		ShowAd (adOnPause2);
	}
	
	public static void ShowOnReturn()
	{
		
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
	
	public static void showRevMobBannerAd()
	{
		if(SSAdInitializer.RevMobActiveStaticFlag){
		//TODO
		}
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
		if(SSAdInitializer.PlayHavenActiveStaticFlag)	
			PlayHavenRunFullScreenAd = true;
	}
	
	public static void showPlayHavenMoreGamesAd()
	{
		//if(SSAdInitializer.PlayHavenActiveStaticFlag)	
	}
	
	public static void showIAdsBanner()
	{
		if(SSAdInitializer.IAdsActiveStaticFlag)
			AdBinding.createAdBanner(true);
			
	}
}
