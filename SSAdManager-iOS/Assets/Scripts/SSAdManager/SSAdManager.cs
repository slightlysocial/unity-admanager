using UnityEngine;
using System.Collections;

public class SSAdManager : MonoBehaviour{
	
	//PlayHaven variables
	public static bool PlayHavenRunFullScreenAd;							//Enables/disables Full screen ad in Update method
	public static bool PlayHavenRunMoreGamesAd;
	
	//Ad Delay variables
	public static bool showAdNowFlag;
	public static bool adHasShownFlag;
	public static float adTimer;
	public float TimeUntilNextAd;
	
	//Differential variables
	public static bool activateInterstitial;
	public static bool activateBanner;
	public static bool activateMoreGames;
	
	//Failed variables
	public static bool failedInterstitial;
	public static bool failedBanner;
	public static bool failedMoreGames;
	public static int failCounter;
	
	//Current ad showing flags;
	public static bool showingOnLoad;
	public static bool showingGameOver;
	public static bool showingPause;
	public static bool showingReturn;
	
	//PList website
	public string PListURL = "";
	public static string PListStaticURL;
	public string CurrentVersionNumber = "";
	public static string CurrentStaticVersionNumber = "";

	//Placeholders for ads
	public static AdValue 
		IS_IN_REVIEW,								//0 for Ads, 1 for no Ads
		AD_BOOT_UP_ON,								//If 0, no Boot Up Ads
		AD_BOOT_UP_1,
		AD_BOOT_UP_2,
		AD_BOOT_UP_3,
		AD_BOOT_UP_4,
		AD_BOOT_UP_5,
		AD_PAUSE_ON,								//If 0, no Pause Ads
		AD_PAUSE_1,
		AD_PAUSE_2,
		AD_PAUSE_3,
		AD_PAUSE_4,
		AD_PAUSE_5,
		AD_GAMEOVER_ON,								//If 0, no Gameover Ads
		AD_GAMEOVER_1,
		AD_GAMEOVER_2,
		AD_GAMEOVER_3,
		AD_GAMEOVER_4,
		AD_GAMEOVER_5,
		AD_RETURN_ON,								//If 0, no Return Ads
		AD_RETURN_1,
		AD_RETURN_2,
		AD_RETURN_3,
		AD_RETURN_4,
		AD_RETURN_5,
		AD_BANNER_ON,								//If 0, No Banner Ads
		AD_BANNER_1,
		AD_BANNER_2,
		AD_BANNER_3,
		AD_BANNER_4,
		AD_BANNER_5,
		AD_MOREGAMES_ON,							//If 0, No MoreGames Ads
		AD_MOREGAMES_1,
		AD_MOREGAMES_2,
		AD_MOREGAMES_3,
		AD_MOREGAMES_4,
		AD_MOREGAMES_5;
		
					
	//Ad values
	public enum AdValue
	{
		ADSOFF,
		CHARTBOOST,
		REVMOB,
		PLAYHAVEN,
		MOBCLIX,
		IADS,
		APPLOVIN,
		VUNGLE,
		ADMOB,
	};
	
	// Use this for initialization
	void Awake () {
		PlayHavenRunFullScreenAd = false;
		PlayHavenRunMoreGamesAd = false;
		
		activateBanner = false;
		activateInterstitial = false;
		activateMoreGames = false;
		
		failedBanner = false;
		failedInterstitial = false;
		failedMoreGames = false;
		failCounter = 0;
		
		showingOnLoad = false;
		showingPause = false;
		showingGameOver = false;
		showingReturn = false;
		
		PListStaticURL = PListURL;
		CurrentStaticVersionNumber = CurrentVersionNumber;

		//Ad flags
		adHasShownFlag = false;
		showAdNowFlag = true;
		adTimer = 0.0f;
		
		//Load the Plist
		StartCoroutine(LoadPList());
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(adHasShownFlag){
			showAdNowFlag = false;
			adTimer += Time.deltaTime;
			
			if(adTimer >= TimeUntilNextAd)
			{
				adTimer = 0.0f;
				showAdNowFlag = true;
				adHasShownFlag = false;
			}
		}
 
		if(PlayHavenRunFullScreenAd)
		{
			GameObject.Find("PlayHavenFullScreen").SendMessage("RequestPlayHavenContent");
			PlayHavenRunFullScreenAd = false;
		}
		else if(PlayHavenRunMoreGamesAd)
		{
			GameObject.Find ("PlayHavenMoreGames").SendMessage("RequestPlayHavenContent");
			PlayHavenRunMoreGamesAd = false;
		}
		
		//Failover
		if(failedBanner || failedInterstitial || failedMoreGames)
		{
			AdFailOverLogic();
		}
		
	}
	
// ----------------- LOAD PLIST INTO MEMORY -------------------
	
	public IEnumerator LoadPList()
	{
		
		
		//Load plist
		WWW www = new WWW(PListStaticURL);
        yield return www;
		
		if (www.error!= null)
		{
			Debug.Log(www.error);
			yield break;
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
				
				//NEW
				AD_BOOT_UP_ON = (AdValue)(adTable["AD_BOOT_UP_ON"]);
				AD_BOOT_UP_1 = (AdValue)(adTable["AD_BOOT_UP_1"]);
				AD_BOOT_UP_2 = (AdValue)(adTable["AD_BOOT_UP_2"]);
				AD_BOOT_UP_3 = (AdValue)(adTable["AD_BOOT_UP_3"]);
				AD_BOOT_UP_4 = (AdValue)(adTable["AD_BOOT_UP_4"]);
				AD_BOOT_UP_5 = (AdValue)(adTable["AD_BOOT_UP_5"]);
				
				AD_PAUSE_ON = (AdValue)(adTable["AD_PAUSE_ON"]);
				AD_PAUSE_1 = (AdValue)(adTable["AD_PAUSE_1"]);
				AD_PAUSE_2 = (AdValue)(adTable["AD_PAUSE_2"]);
				AD_PAUSE_3 = (AdValue)(adTable["AD_PAUSE_3"]);
				AD_PAUSE_4 = (AdValue)(adTable["AD_PAUSE_4"]);
				AD_PAUSE_5 = (AdValue)(adTable["AD_PAUSE_5"]);
				
				AD_GAMEOVER_ON = (AdValue)(adTable["AD_GAMEOVER_ON"]);
				AD_GAMEOVER_1 = (AdValue)(adTable["AD_GAMEOVER_1"]);
				AD_GAMEOVER_2 = (AdValue)(adTable["AD_GAMEOVER_2"]);
				AD_GAMEOVER_3 = (AdValue)(adTable["AD_GAMEOVER_3"]);
				AD_GAMEOVER_4 = (AdValue)(adTable["AD_GAMEOVER_4"]);
				AD_GAMEOVER_5 = (AdValue)(adTable["AD_GAMEOVER_5"]);
				
				AD_RETURN_ON = (AdValue)(adTable["AD_RETURN_ON"]);
				AD_RETURN_1 = (AdValue)(adTable["AD_RETURN_1"]);
				AD_RETURN_2 = (AdValue)(adTable["AD_RETURN_2"]);
				AD_RETURN_3 = (AdValue)(adTable["AD_RETURN_3"]);
				AD_RETURN_4 = (AdValue)(adTable["AD_RETURN_4"]);
				AD_RETURN_5 = (AdValue)(adTable["AD_RETURN_5"]);
				
				AD_BANNER_ON = (AdValue)(adTable["AD_BANNER_ON"]);
				AD_BANNER_1 = (AdValue)(adTable["AD_BANNER_1"]);
				AD_BANNER_2 = (AdValue)(adTable["AD_BANNER_2"]);
				AD_BANNER_3 = (AdValue)(adTable["AD_BANNER_3"]);
				AD_BANNER_4 = (AdValue)(adTable["AD_BANNER_4"]);
				AD_BANNER_5 = (AdValue)(adTable["AD_BANNER_5"]);
				
				AD_MOREGAMES_ON = (AdValue)(adTable["AD_MOREGAMES_ON"]);
				AD_MOREGAMES_1 = (AdValue)(adTable["AD_MOREGAMES_1"]);
				AD_MOREGAMES_2 = (AdValue)(adTable["AD_MOREGAMES_2"]);
				AD_MOREGAMES_3 = (AdValue)(adTable["AD_MOREGAMES_3"]);
				AD_MOREGAMES_4 = (AdValue)(adTable["AD_MOREGAMES_4"]);
				AD_MOREGAMES_5 = (AdValue)(adTable["AD_MOREGAMES_5"]);
				

				Debug.Log("loaded PLIST complete");
				
				break;//found version...exit loop
			}
		}

		Debug.Log("End of plist loading...");
	}
	
// ---------------- SHOW AD ---------------------
	
	public static void ShowAd(AdValue adValue)
	{
		//don't show ads if in review
		if(IS_IN_REVIEW != AdValue.ADSOFF)
		{
			return;	
		}

		if(showAdNowFlag)
		{
			//Show FS, Pop up, Video Ads
			if(!activateBanner && !activateMoreGames && activateInterstitial){
				if(adValue == AdValue.CHARTBOOST)
				{
					showChartBoostFullScreenAd();
				}
				else if(adValue == AdValue.REVMOB)
				{
					showRevMobFullScreenAd();
				}
				else if(adValue == AdValue.PLAYHAVEN)
				{
					showPlayHavenFullScreenAd();
				}
				else if(adValue == AdValue.APPLOVIN)
				{
					showAppLovinFullScreenAd();
				}
				else if(adValue == AdValue.VUNGLE)
				{
					showVungleAd();
				}
			}
		}
		
		//Show Banner and More Games ads
		if(!activateInterstitial && (activateBanner || activateMoreGames)){
			if(adValue == AdValue.CHARTBOOST)
			{
				showChartBoostMoreGamesAd();
			}
			else if(adValue == AdValue.PLAYHAVEN)
			{
				showPlayHavenMoreGamesAd();
			}
			else if(adValue == AdValue.IADS)
			{
				showIAdsBanner();
			}
			else if(adValue == AdValue.ADMOB)
			{
				showAdMobBanner();
			}
			else if(adValue == AdValue.APPLOVIN)
			{
				showAppLovinBanner();
			}
			else if(adValue == AdValue.REVMOB)
			{
				showRevMobBannerAd();
			}
		}

	}
	
//---------------- AD CHECKS -------------
	
	public static void ShowOnLoad()
	{
		if(AD_BOOT_UP_ON != AdValue.ADSOFF){
			showingOnLoad = true;
			activateInterstitial = true;        
			ShowAd(AD_BOOT_UP_1);
			activateInterstitial = true;
			ShowAd(AD_BOOT_UP_2);
			//adHasShownFlag = true;
		}
	}
	
	public static void ShowOnPause()
	{
		if(AD_PAUSE_ON != AdValue.ADSOFF){
			showingPause = true;
			activateInterstitial = true;
			ShowAd(AD_PAUSE_1);
			//ShowAd (adOnPause2);
			//adHasShownFlag = true;
		}
	}
	
	public static void ShowOnReturn()
	{
		if(AD_RETURN_ON != AdValue.ADSOFF){
			showingReturn = true;
			activateInterstitial = true;
			ShowAd (AD_RETURN_1);
			//ShowAd (adOnReturn2);
			//adHasShownFlag = true;
		}
	}
	
	public static void ShowOnGameOver()
	{
		if(AD_GAMEOVER_ON != AdValue.ADSOFF){
			showingGameOver = true;
			activateInterstitial = true;
			ShowAd (AD_GAMEOVER_1);	
			//adHasShownFlag = true;
		}
	}
	
	public static void ShowBanner()
	{
		if(AD_BANNER_ON != AdValue.ADSOFF){
			activateBanner = true;
			ShowAd (AD_BANNER_1);
		}
	}
	
	public static void ShowMoreGames()
	{
		if(AD_MOREGAMES_ON != AdValue.ADSOFF){
			activateMoreGames = true;
			ShowAd (AD_MOREGAMES_1);
		}
	}
	
//-------------------- NATIVE AD FUNCTIONS -------------------
	
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
			RevMobBanner banner = SSAdInitializer.revMobSession.CreateBanner();
			banner.Show();
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

	public static void showVungleAd()
	{
		if(SSAdInitializer.VungleActiveStaticFlag){
			if(VungleBinding.isAdAvailable()){
				VungleBinding.playModalAd(true);
				adHasShownFlag = true;
				activateInterstitial = false;
				failCounter = 0;
			}
			else{
				failedInterstitial = true;
				failCounter++;
			}
		}
	}
	
	public static void showAppLovinBanner()
	{
		if(SSAdInitializer.AppLovinStaticFlag)
		{
			AppLovin.ShowAd(AppLovin.AD_POSITION_CENTER, AppLovin.AD_POSITION_BOTTOM);	
		}
	}
	
	public static void showAppLovinFullScreenAd()
	{
		if(SSAdInitializer.AppLovinStaticFlag)
		{
			AppLovin.ShowInterstitial();	
		}
	}
	
//------------ FAIL OVER --------------------
	
	public void AdFailOverLogic()
	{
		if(failedBanner)
		{
			activateBanner = true;
			
			if(failCounter == 1)
			{
				ShowAd (AD_BANNER_2);
			}
			else if(failCounter == 2)
			{
				ShowAd (AD_BANNER_3);
			}
			else if(failCounter == 3)
			{
				ShowAd (AD_BANNER_4);
			}
			else if(failCounter == 4)
			{
				ShowAd (AD_BANNER_5);
			}
			else
			{
				failCounter = 0;
				activateBanner = false;
			}
		}
		else if(failedInterstitial)
		{
			activateInterstitial = true;
			
			if(showingOnLoad)
			{
				if(failCounter == 1)
				{
					ShowAd(AD_BOOT_UP_2);
				}
				else if(failCounter == 2)
				{
					ShowAd(AD_BOOT_UP_3);
				}
				else if(failCounter == 3)
				{
					ShowAd(AD_BOOT_UP_4);
				}
				else if(failCounter == 4)
				{
					ShowAd(AD_BOOT_UP_5);
				}
				else
				{
					failCounter = 0;
					activateInterstitial = false;
				}
			}
			else if(showingPause)
			{
				if(failCounter == 1)
				{
					ShowAd (AD_PAUSE_2);
				}
				else if(failCounter == 2)
				{
					ShowAd (AD_PAUSE_3);
				}
				else if(failCounter == 3)
				{
					ShowAd (AD_PAUSE_4);
				}
				else if(failCounter == 4)
				{
					ShowAd (AD_PAUSE_5);
				}
				else
				{
					failCounter = 0;	
					activateInterstitial = false;
				}
			}
			else if(showingReturn)
			{
				if(failCounter == 1)
				{
					ShowAd (AD_RETURN_2);
				}
				else if(failCounter == 2)
				{
					ShowAd (AD_RETURN_3);
				}
				else if(failCounter == 3)
				{
					ShowAd (AD_RETURN_4);
				}
				else if(failCounter == 4)
				{
					ShowAd (AD_RETURN_5);
				}
				else
				{
					failCounter = 0;
					activateInterstitial = false;
				}		
			}
			else if(showingGameOver)
			{
				if(failCounter == 1)
				{
					ShowAd(AD_GAMEOVER_2);
				}
				else if(failCounter == 2)
				{
					ShowAd(AD_GAMEOVER_2);
				}
				else if(failCounter == 3)
				{
					ShowAd(AD_GAMEOVER_2);
				}
				else if(failCounter == 4)
				{
					ShowAd(AD_GAMEOVER_2);
				}
				else
				{
					failCounter = 0;
					activateInterstitial = false;
				}
			}
		}
		else if(failedMoreGames)
		{
			activateMoreGames = true;
			
			if(failCounter == 1)
			{
				ShowAd(AD_MOREGAMES_2);
			}
			else if(failCounter == 2)
			{
				ShowAd(AD_MOREGAMES_3);
			}
			else if(failCounter == 3)
			{
				ShowAd(AD_MOREGAMES_4);
			}
			else if(failCounter == 4)
			{
				ShowAd(AD_MOREGAMES_5);
			}
			else
			{
				failCounter = 0;
				activateMoreGames = false;
			}
		}	

		failedBanner = failedInterstitial = failedMoreGames = false;
	}
}
