using UnityEngine;
using System.Collections;

public class SSAdManager : MonoBehaviour {
	
	//PlayHaven variables
	public static bool PlayHavenRunFullScreenAd;							//Enables/disables Full screen ad in Update method
	public static bool PlayHavenRunMoreGamesAd;
	
	//PList website
	public string PListURL = "";
	public static string PListStaticURL;
	public string CurrentVersionNumber = "";
	public static string CurrentStaticVersionNumber = "";

	
	
	//Placeholders for ads
	public static AdValue adOnLoad1,
						adOnLoad2,
						adOnLoadFail,
						adOnPause1,
						adOnPause2,
						adOnPauseFail,
						adOnReturn1,
						adOnReturn2,
						adOnReturnFail,
						adOnGameOver1,
						adOnGameOver2,
						adOnGameOverFail,
						adBanner1,
						adBanner2,
						adBannerFail,
						adMoreGames1,
						adMoreGames2,
						adMoreGamesFail,
						adInReview;
					
	//Ad values
	public enum AdValue
	{
		ADSON,
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
	void Awake () {
		PlayHavenRunFullScreenAd = false;
		PlayHavenRunMoreGamesAd = false;
		PListStaticURL = PListURL;
		CurrentStaticVersionNumber = CurrentVersionNumber;
		//Debug.Log(PListStaticURL);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayHavenRunFullScreenAd)
		{
			gameObject.SendMessage("RequestPlayHavenContent");
			PlayHavenRunFullScreenAd = false;
		}
		else if(PlayHavenRunMoreGamesAd)
		{
			GameObject.Find ("PlayHavenMoreGames").SendMessage("RequestPlayHavenContent");
			PlayHavenRunMoreGamesAd = false;
		}
	}
	
	public static IEnumerator LoadPList()
	{
		
		
		//Load plist
		WWW www = new WWW(PListStaticURL);
        yield return www;
		
		if (www.error!= null)
		{
			Debug.Log(www.error);
			return false;
		}
		
		
		//Parse plist
		Hashtable hashTable = new Hashtable();
		PListManager.ParsePListText(www.text, ref hashTable);
		
		Debug.Log("start parse");
		
		foreach(object key in hashTable.Keys)
		{
			if (key.Equals(CurrentStaticVersionNumber))
			{
				Debug.Log("found PLIST version..." + key);
				
				Hashtable adTable = (Hashtable)hashTable[key];
				
				adOnLoad1 = (AdValue)(adTable["AD_ON_LOAD1"]);
				adOnLoad2= (AdValue)(adTable["AD_ON_LOAD2"]);
				adOnLoadFail= (AdValue)(adTable["AD_ON_LOAD_FAIL"]);
				adOnPause1 = (AdValue)(adTable["AD_ON_PAUSE1"]);
				adOnPause2 = (AdValue)(adTable["AD_ON_PAUSE2"]);
				adOnPauseFail = (AdValue)(adTable["AD_ON_PAUSE_FAIL"]);
				adOnReturn1 = (AdValue)(adTable["AD_ON_RETURN1"]);
				adOnReturn2 = (AdValue)(adTable["AD_ON_RETURN2"]);
				adOnReturnFail = (AdValue)(adTable["AD_ON_RETURN_FAIL"]);
				adOnGameOver1 = (AdValue)(adTable["AD_ON_GAMEOVER1"]);
				adOnGameOver2 = (AdValue)(adTable["AD_ON_GAMEOVER2"]);
				adOnGameOverFail = (AdValue)(adTable["AD_ON_GAMEOVER_FAIL"]);
				adBanner1 = (AdValue)(adTable["AD_BANNER1"]);
				adBanner2 = (AdValue)(adTable["AD_BANNER2"]);
				adBannerFail = (AdValue)(adTable["AD_BANNER_FAIL"]);
				adMoreGames1 = (AdValue)(adTable["AD_MORE_GAMES1"]);
				adMoreGames2 = (AdValue)(adTable["AD_MORE_GAMES2"]);
				adMoreGamesFail = (AdValue)(adTable["AD_MORE_GAMES_FAIL"]);
				adInReview  = (AdValue)(adTable["AD_IN_REVIEW"]);
				
				Debug.Log("loaded PLIST complete");
				
				break;//found version...exit loop
			}
		}
		
		
		Debug.Log(adInReview);
		Debug.Log("End of plist loading...");
	}
	
	public static void ShowAd(AdValue adValue)
	{
		//don't show ads if in review
		if(adInReview != AdValue.ADSON || PlayerPrefs.HasKey("TurnOffAds"))
		{
			return;	
		}

		
		if(adValue == AdValue.CHARTBOOST_FS)
		{
			showChartBoostFullScreenAd();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.CHARTBOOST_MOREGAMES)
		{
			showChartBoostMoreGamesAd();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.REVMOB_FS)
		{
			showRevMobFullScreenAd();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.REVMOB_POPUP)
		{
			showRevMobPopUpAd();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.REVMOB_BANNER)
		{
			showRevMobBannerAd();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.PLAYHAVEN_FS)
		{
			showPlayHavenFullScreenAd();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.PLAYHAVEN_MOREGAMES)
		{
			showPlayHavenMoreGamesAd();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.APPLIFT)
		{
			//TODO
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.ADMOB_BANNER)
		{
			showAdMobBanner();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.IADS_BANNER)
		{
			showIAdsBanner();
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.MOBCLIX_BANNER)
		{
			//TODO
			Debug.Log("AD SHOWING");
		}
		else if(adValue == AdValue.VUNGLE)
		{
			//TODO
			Debug.Log("AD SHOWING");
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
		//ShowAd (adOnPause2);
	}
	
	public static void ShowOnReturn()
	{
		ShowAd (adOnReturn1);
		//ShowAd (adOnReturn2);
	}
	
	public static void ShowOnGameOver()
	{
		ShowAd (adOnGameOver1);	
	}
	
	public static void ShowBanner()
	{
		ShowAd (adBanner1);
	}
	
	public static void ShowMoreGames()
	{
		ShowAd (adMoreGames1);	
	}
	
	public static void showChartBoostFullScreenAd()
	{
		if(SSAdInitializer.ChartBoostActiveStaticFlag)
		{
			ChartBoostBinding.showInterstitial(null);	
		}
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
			SSAdInitializer.revMobSession.CreateBanner(0f, Screen.height - 100f, Screen.width, 100f, null, null);
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
		if(SSAdInitializer.PlayHavenActiveStaticFlag)
			PlayHavenRunMoreGamesAd = true;
	}
	
	public static void showIAdsBanner()
	{
		if(SSAdInitializer.IAdsActiveStaticFlag)
			AdBinding.createAdBanner(true);
			
	}
}
