using UnityEngine;
using System.Collections;

public class SSAdEventListener : MonoBehaviour, IRevMobListener {

	// Use this for initialization
	void Start () {
		
		//Chartboost enable callbacks
		ChartBoostManager.didCacheInterstitialEvent += didLoadChartboostInterstitial;
		ChartBoostManager.didCacheMoreAppsEvent += didLoadChartboostMoreGames;
		ChartBoostManager.didFailToLoadInterstitialEvent += didFailToLoadChartboostInterstitial;
		ChartBoostManager.didFailToLoadMoreAppsEvent += didFailToLoadChartboostMoreApps;
		
		//Playhaven enable callbacks
		PlayHaven.PlayHavenManager.instance.OnErrorContentRequest += didFailToLoadPlayHavenContent;
		PlayHaven.PlayHavenManager.instance.OnDidDisplayContent += didLoadPlayHavenContent;
		
		//iAds enable callbacks
		AdManager.interstitalAdFailed += didLoadiAdsBanner;
		AdManager.interstitalAdFailed += didFailToLoadiAdBanner;
		
		//Applovin enable callbacks
		// missing manager or event listener class
		
		//Vungle enable callbacks
		//Use AdIsAvailable function from Binding class in SSAdManager
		
		//Admob enable callbacks
		AdMobManager.adViewDidReceiveAdEvent += didLoadAdMobBanner;
		AdMobManager.adViewFailedToReceiveAdEvent += didFailToLoadAdMobBanner;
		
	}
	
//------------- CHARTBOOST ----------------
	
	public void didLoadChartboostInterstitial( string message )
	{
		SSAdManager.adHasShownFlag = true;
		SSAdManager.activateInterstitial = false;
		SSAdManager.failCounter = 0;
	}
	
	public void didLoadChartboostMoreGames ()
	{
		SSAdManager.failedMoreGames = true;
		SSAdManager.failCounter = 0;
	}
	
	public void didFailToLoadChartboostInterstitial( string location )
	{
		Debug.Log("Failed to load Chartboost Interstitial");
		SSAdManager.failedInterstitial = true;
		SSAdManager.failCounter++;
	}
	
	public void didFailToLoadChartboostMoreApps()
	{
		Debug.Log("Failed to load Chartboost More Games");
		SSAdManager.failedMoreGames = true;
		SSAdManager.failCounter++;
	}
	
//------------- REVMOB --------------------

	public void RevMobAdDidReceive (string revMobAdType) {
        Debug.Log("Ad did receive.");
		SSAdManager.adHasShownFlag = true;
		SSAdManager.activateInterstitial = false;
		SSAdManager.failCounter = 0;		
    }
	
	public void RevMobAdDidFail(string revMobAdType)
	{
		Debug.Log("Failed to load Revmob Ad");
		SSAdManager.failedInterstitial = true;
		SSAdManager.failCounter++;
	}

    public void RevMobAdDisplayed (string revMobAdType) {
        Debug.Log("Ad displayed.");
    }

    public void RevMobUserClickedInTheAd (string revMobAdType) {
        Debug.Log("Ad clicked.");
    }

    public void RevMobUserClosedTheAd (string revMobAdType) {
        Debug.Log("Ad closed.");
    }
	
	public void RevMobInstallDidReceive (string message) {
        Debug.Log("Install received.");
    }
	
	public void RevMobInstallDidFail (string message) {
        Debug.Log("Install failed.");
    }
	
//-------------- Playhaven ---------------------
	
	public void didLoadPlayHavenContent(int num)
	{
		Debug.Log("Loaded Playhaven content");
		if(SSAdManager.activateInterstitial)
		{
			SSAdManager.adHasShownFlag = true;
			SSAdManager.activateInterstitial = false;
			SSAdManager.failCounter = 0;
		}
		else if(SSAdManager.activateMoreGames)
		{
			SSAdManager.activateInterstitial = false;
			SSAdManager.failCounter = 0;
		}
		
	}
	
	public void didFailToLoadPlayHavenContent (int num, PlayHaven.Error error)
	{
		Debug.Log("Failed to load PlayHaven content");
		if(SSAdManager.activateInterstitial){
			SSAdManager.failedInterstitial = true;
			SSAdManager.failCounter++;
		}
		else if(SSAdManager.activateMoreGames)
		{
			SSAdManager.failedMoreGames = true;
			SSAdManager.failCounter++;
		}
	}
	
//---------------- MobClix ----------------------- // DO NOT USE
	
	public void didFailToLoadMobclixBanner (string error)
	{
		Debug.Log("Failed to load mobclix banner");	
		SSAdManager.failedBanner = true;
		SSAdManager.failCounter++;
	}
	
//---------------- iAds ------------------------
	
	public void didLoadiAdsBanner (string error)
	{
		SSAdManager.activateBanner = false;
		SSAdManager.failCounter = 0;
	}
	
	public void didFailToLoadiAdBanner( string error)
	{
		Debug.Log("Failed to load iAd Banner");	
		SSAdManager.failedBanner = true;
		SSAdManager.failCounter++;
	}
	
//---------------- AppLovin -------------------------  // do not use
	
	public void didFailToLoadAppLovinAd( string error)
	{
		Debug.Log("Failed to load applovin ad");
		SSAdManager.failedInterstitial = true;
		SSAdManager.failCounter++;
	}
	
//--------------- VUNGLE ---------------------------   //DO NOT USE
	
	public void didFailToLoadVungleAd( string error)
	{
		Debug.Log("Failed to load vungle ad");
		SSAdManager.failedInterstitial = true;
		SSAdManager.failCounter++;
	}
	
// ------------------ ADMOB ---------------------------
	
	public void didLoadAdMobBanner()
	{
		SSAdManager.activateBanner = false;
		SSAdManager.failCounter = 0;
	}
	
	public void didFailToLoadAdMobBanner( string error)
	{
		Debug.Log("Failed to load admob banner");
		SSAdManager.failedBanner = true;
		SSAdManager.failCounter++;
	}
}
