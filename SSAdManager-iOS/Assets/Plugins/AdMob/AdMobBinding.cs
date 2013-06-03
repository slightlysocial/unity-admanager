using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public enum AdMobBannerType
{
	iPhone_320x50,
	iPad_728x90,
	iPad_468x60,
	iPad_320x250,
	SmartBannerPortrait,
	SmartBannerLandscape
}

public enum AdMobAdPosition
{
	TopLeft,
	TopCenter,
	TopRight,
	Centered,
	BottomLeft,
	BottomCenter,
	BottomRight
}



public class AdMobBinding
{
	[DllImport("__Internal")]
	private static extern void _adMobInit( string publisherId, bool isTesting );

	// Sets the publiser Id and prepares AdMob for action.  Must be called before any other methods!
	public static void init( string publisherId )
	{
		init( publisherId, false );
	}
	
	public static void init( string publisherId, bool isTesting )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_adMobInit( publisherId, isTesting );
	}
	
	
	[DllImport("__Internal")]
	private static extern void _adMobSetTestDevice( string deviceId );
	
	// Adds a test device
	public static void addTestDevice( string deviceId )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_adMobSetTestDevice( deviceId );
	}


	[DllImport("__Internal")]
	private static extern void _adMobCreateBanner( int bannerType, int position );

	// Creates a banner of the given type at the given position
	public static void createBanner( AdMobBannerType bannerType, AdMobAdPosition position )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_adMobCreateBanner( (int)bannerType, (int)position );
	}


	[DllImport("__Internal")]
	private static extern void _adMobDestroyBanner();

	// Destroys the banner and removes it from view
	public static void destroyBanner()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_adMobDestroyBanner();
	}


	[DllImport("__Internal")]
	private static extern void _adMobRequestInterstitalAd( string interstitialUnitId );

	// Starts loading an interstitial ad
	public static void requestInterstitalAd( string interstitialUnitId )
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_adMobRequestInterstitalAd( interstitialUnitId );
	}


	[DllImport("__Internal")]
	private static extern bool _adMobIsInterstitialAdReady();

	// Checks to see if the interstitial ad is loaded and ready to show
	public static bool isInterstitialAdReady()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			return _adMobIsInterstitialAdReady();

		return false;
	}


	[DllImport("__Internal")]
	private static extern void _adMobShowInterstitialAd();

	// If an interstitial ad is loaded this will take over the screen and show the ad
	public static void showInterstitialAd()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_adMobShowInterstitialAd();
	}

}

