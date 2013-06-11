using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.ComponentModel;

#if UNITY_ANDROID
using TapjoyPlatformPlugin = TapjoyPluginAndroid;
#elif UNITY_IPHONE
using TapjoyPlatformPlugin = TapjoyPluginIOS;
#else
using TapjoyPlatformPlugin = TapjoyPluginDefault;
#endif

public enum TapjoyDisplayAdSize
{
    /// <summary>
    /// Display ad request size 320X50, case sensitive.
    /// </summary>
    [Description("320x50")]
    SIZE_320X50,

    /// <summary>
    /// Display ad request size 640x100, case sensitive.
    /// </summary>
    [Description("640x100")]
    SIZE_640X100,

    /// <summary>
    /// Display ad request size 768x90, case sensitive.
    /// </summary>
    [Description("768x90")]
    SIZE_768X90
}

public enum TapjoyViewType
{
	OFFERWALL,
	FULL_SCREEN_AD,
	DAILY_REWARD_AD,
	VIDEO_AD
}


public class TapjoyPlugin : MonoBehaviour
{
	// iOS settings for collection of MAC address. Use this with the TJC_OPTION_COLLECT_MAC_ADDRESS option.
	// Enables collection of MAC address.
	public const string MacAddressOptionOn = "0";
	
	//Disables collection of MAC address for iOS versions 6.0 and above.
	public const string MacAddressOptionOffWithVersionCheck = "1";
	
	//Completely disables collection of MAC address. THIS WILL EFFECTIVELY DISABLE THE SDK FOR IOS VERSIONS BELOW 6.0!
	public const string MacAddressOptionOff = "2";
	
#region Events
	// CONNECT
	// Fired when RequestTapjoyConnect succeeds
	public static event Action connectCallSucceeded;
	
	// Fired when RequestTapjoyConnect fails
	public static event Action connectCallFailed;
	
	// VIRTUAL CURRENCY
	// Fired when GetTapPoints responds with the users tap points (amount of currency in user account is returned)
	public static event Action<int> getTapPointsSucceeded;
	
	// Fired when GetTapPoints fails to get the users tap points
	public static event Action getTapPointsFailed;
	
	// Fired when SpendTapPoints succeeds and indicates that the user has successfully spent currency (amount of currency left after spending is returned)
	public static event Action<int> spendTapPointsSucceeded;
	
	// Fired when SpendTapPoints fails to spend the currency
	public static event Action spendTapPointsFailed;
	
	// Fired when AwardTapPoints succeeds and indicates that the user has successfully been awarded currency
	public static event Action awardTapPointsSucceeded;
	
	// Fired when AwardTapPoints fails to award the currency
	public static event Action awardTapPointsFailed;
	
	// Fired when the applications starts/resumes, and the user has earned currency in the interim, by completing offers (amount earned is returned)
	public static event Action<int> tapPointsEarned;
	
	// FULLSCREEN AD
	// Fired when GetFullScreenAd finishes succesfully loading
	public static event Action getFullScreenAdSucceeded;
	
	// Fired when GetFullScreenAd fails loading
	public static event Action getFullScreenAdFailed;
	
	// DAILY REWARD
	// Fired when GetDailyRewardAd finishes succesfully loading
	public static event Action getDailyRewardAdSucceeded;
	
	// Fired when GetDailyRewardAd fails loading
	public static event Action getDailyRewardAdFailed;
	
	// DISPLAY AD
	// Fired when GetDisplayAd is received
	public static event Action getDisplayAdSucceeded;
	
	// Fired when GetDisplayAd fails
	public static event Action getDisplayAdFailed;
	
	// VIDEO AD
	// Fired when a video ad begins playing
	public static event Action videoAdStarted;
	
	// Fired when a video ad has completed playing
	public static event Action videoAdFailed;
	
	// Fired when a video ad has completed playing
	public static event Action videoAdCompleted;
	
	// OFFER WALL
	// Fired when Offer Wall fails loading
	public static event Action showOffersFailed;
	
	// GENERAL	
	// Fired when any full screen view is closed
	public static event Action<TapjoyViewType> viewOpened;
	public static event Action<TapjoyViewType> viewClosed;
	

	

    void Awake()
    {
		// Set the GameObject name to the class name for easy access from plugin
		gameObject.name = this.GetType().ToString();
		
		// Tell plugin which gameobject to send messages too
		SetCallbackHandler(gameObject.name);
		print ("UnitySendMessage directs to " + gameObject.name);
		
		DontDestroyOnLoad( this );
    }
	
	
	#region Tapjoy Callback Methods
	
	// CONNECT
	public void TapjoyConnectSuccess(string message)
	{
		if (connectCallSucceeded != null)
			connectCallSucceeded();
	}
	
	public void TapjoyConnectFail(string message)
	{
		if (connectCallFailed != null)
			connectCallFailed();
	}
	
	// VIRTUAL CURRENCY
	public void TapPointsLoaded(string message)
	{
		if (getTapPointsSucceeded != null)
			getTapPointsSucceeded(Int32.Parse(message));
	}
	
	public void TapPointsLoadedError(string message)
	{
		if (getTapPointsFailed != null)
			getTapPointsFailed();
	}
	
	public void TapPointsSpent(string message)
	{
		if (spendTapPointsSucceeded != null)
			spendTapPointsSucceeded(Int32.Parse(message));
	}

	public void TapPointsSpendError(string message)
	{
		if (spendTapPointsFailed != null)
			spendTapPointsFailed();
	}

	public void TapPointsAwarded(string message)
	{
		if (awardTapPointsSucceeded != null)
			awardTapPointsSucceeded();
	}

	public void TapPointsAwardError(string message)
	{
		if (awardTapPointsFailed != null)
			awardTapPointsFailed();
	}
	
	public void CurrencyEarned(string message)
	{
		if (tapPointsEarned != null)
			tapPointsEarned(Int32.Parse(message));
	}
	
	// FULL SCREEN ADS
	public void FullScreenAdLoaded(string message)
	{
		if (getFullScreenAdSucceeded != null)
			getFullScreenAdSucceeded();
	}
	
	public void FullScreenAdError(string message)
	{
		if (getFullScreenAdFailed != null)
			getFullScreenAdFailed();
	}
	
	// DAILY REWARD ADS
	public void DailyRewardAdLoaded(string message)
	{
		if (getDailyRewardAdSucceeded != null)
			getDailyRewardAdSucceeded();
	}
	
	public void DailyRewardAdError(string message)
	{
		if (getDailyRewardAdFailed != null)
			getDailyRewardAdFailed();
	}
	
	// DISPLAY ADS
	public void DisplayAdLoaded(string message)
	{
		if (getDisplayAdSucceeded != null)
			getDisplayAdSucceeded();
	}
	
	public void DisplayAdError(string message)
	{
		if (getDisplayAdFailed != null)
			getDisplayAdFailed();
	}
	
	// VIDEO
	public void VideoAdStart(string message)
	{
		if (videoAdStarted != null)
			videoAdStarted();
	}
	
	public void VideoAdError(string message)
	{
		if (videoAdFailed != null)
			videoAdFailed();
	}
	
	public void VideoAdComplete(string message)
	{
		if (videoAdCompleted != null)
			videoAdCompleted();
	}
	
	// OFFER WALL
	public void ShowOffersError(string message)
	{
		if(showOffersFailed != null)
			showOffersFailed();
	}
	
	// GENERAL	
	public void ViewOpened(string message)
	{
		if (viewOpened != null)
		{
			int vt = Int32.Parse(message);
			viewOpened((TapjoyViewType)vt);
		}
	}
	
	public void ViewClosed(string message)
	{
		if (viewClosed != null)
		{
			int vt = Int32.Parse(message);
			viewClosed((TapjoyViewType)vt);
		}
	}
	#endregion
	
#endregion
	
	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>
	public static void SetCallbackHandler(string handlerName)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.SetCallbackHandler(handlerName);
	}
	
	/// <summary>
	/// This method is called to initialize the TapjoyConnect system. 
	/// This method should be called upon app initialization and must not
	/// have logic to prevent it from being called in any case.
	/// </summary>
	/// <param name="appID">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="secretKey">
	/// A <see cref="System.String"/>
	/// </param>
	public static void RequestTapjoyConnect(string appID, string secretKey)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.RequestTapjoyConnect(appID, secretKey);
	}

	/// <summary>
	/// This method is called to initialize the TapjoyConnect system.
	/// This method should be called upon app initialization and must
	/// not have logic to prevent it from being called in any case.
	/// </summary>
	/// <param name="appID">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="secretKey">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="flags">
	/// A <see cref="System.Collections.Hashtable" />
	/// </param>
	public static void RequestTapjoyConnect(string appID, string secretKey, Dictionary<string, string> flags)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.RequestTapjoyConnect(appID, secretKey, flags);
	}
	
	/// <summary>
	/// Enables Tapjoy library logging to the console.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void EnableLogging(bool enable)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.EnableLogging(enable);	
	}
	
	/// <summary>
	/// This is called when an action is completed.
	/// Actions are much like connects, except that this method is
	/// only called when a user completes an in-game action.
	/// </summary>
	/// <param name="actionID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void ActionComplete(string actionID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ActionComplete(actionID);
	}
	
	/// <summary>
	/// Sets the user ID. The user ID defaults to the UDID.
	/// This is only changed when NOT using Tapjoy Managed Currency.
	/// </summary>
	/// <param name="userID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SetUserID(string userID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.SetUserID(userID);
	}
	
	/// <summary>
	/// Displays the offer wall.
	/// </summary>
	public static void ShowOffers()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowOffers();
	}
	
	/// <summary>
	/// Initiates a request to get a user's Tap Points (Tapjoy Managed currency).
	/// </summary>
	public static void GetTapPoints()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetTapPoints();
	}
	
	/// <summary>
	/// Updates the Tap Points for the user with the given spent amount of currency.
	/// If the spent amount exceeds the current amount of currency the user has, nothing will happen.
	/// </summary>
	/// <param name="points">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void SpendTapPoints(int points)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.SpendTapPoints(points);
	}
	
	/// <summary>
	/// Updates the Tap Points for the user with the given awarded amount of currency.
	/// </summary>
	/// <param name="points">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void AwardTapPoints(int points)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.AwardTapPoints(points);
	}
	
	/// <summary>
	/// Returns the current amount of Tap Points that a user has.
	/// When using Tapjoy Managed currency, the user is determined by the UDID of the device.
	/// This can safely be called after Tap Points data is retrieved from the server.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Int32"/>
	/// </returns>
	public static int QueryTapPoints()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return 0;
		
		return TapjoyPlatformPlugin.QueryTapPoints();
	}
	
	/// <summary>
	/// Shows the default earned currency alert in iOS.
	/// </summary>
	public static void ShowDefaultEarnedCurrencyAlert()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowDefaultEarnedCurrencyAlert();	
	}

	//========================================================================================================================
	// DISPLAY ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get a Tapjoy Display Ad.
	/// </summary>
	public static void GetDisplayAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetDisplayAd();
	}
	
	/// <summary>
	/// Shows the Display ad.  Call after receiving the DisplayAdLoaded(...) notifier.
	/// </summary>
	public static void ShowDisplayAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowDisplayAd();
	}
	
	/// <summary>
	/// Hides the Display ad.
	/// </summary>
	public static void HideDisplayAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.HideDisplayAd();
	}
	
	/// <summary>
	/// Sets the Display ad size.  Use TapjoyDisplayAdSize enum for supported sizes.
	/// </summary>
	/// <param name="size">
	/// A <see cref="System.Int32"/>
	/// </param>
	[Obsolete("SetDisplayAdContentSize is deprecated. Please use SetDisplayAdSize.")]
	public static void SetDisplayAdContentSize(int size)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlugin.SetDisplayAdSize((TapjoyDisplayAdSize)size);
	}
	
	/// <summary>
	/// Sets the Display ad size.  Use TapjoyDisplayAdSize enum for supported sizes.
	/// </summary>
	/// <param name="size">
	/// A <see cref="TapjoyDisplayAdSize"/>
	/// </param>
	public static void SetDisplayAdSize(TapjoyDisplayAdSize size)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		string sizeString = "320x50";
		if (size == TapjoyDisplayAdSize.SIZE_640X100)
			sizeString = "640x100";
		if (size == TapjoyDisplayAdSize.SIZE_768X90)
			sizeString = "768x90";
		
		TapjoyPlatformPlugin.SetDisplayAdSize(sizeString);
	}
	
	/// <summary>
	/// This method was renamed to EnableDisplayAdAutoRefresh to better
	/// explain it's purpose.
	/// 
	/// Updates the auto-refresh with a new boolean value.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// True if the Display ad be auto-refreshed.
	/// </param>
	[Obsolete("RefreshDisplayAd(bool enable) is deprecated. Please use EnableDisplayAdAutoRefresh.")]
	public static void RefreshDisplayAd(bool enable)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.EnableDisplayAdAutoRefresh(enable);
	}
	
	/// <summary>
	/// Updates the Display ad with a new one or disables auto-refresh.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void EnableDisplayAdAutoRefresh(bool enable)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.EnableDisplayAdAutoRefresh(enable);
	}
	
	/// <summary>
	/// Refreshs the display ad.
	/// </summary>
	[Obsolete("RefreshDisplayAd() is deprecated. Please use GetDisplayAd.")]
	public static void RefreshDisplayAd()
	{
		TapjoyPlatformPlugin.GetDisplayAd();
	}
	
	/// <summary>
	/// Moves the location of the Display Ad.
	/// </summary>
	/// <param name="x">
	/// A <see cref="System.Int32"/>
	/// </param>
	/// <param name="y">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void MoveDisplayAd(int x, int y)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.MoveDisplayAd(x, y);
	}
	
	//========================================================================================================================
	// PLATFORM SETTINGS
	//========================================================================================================================
	/// <summary>
	/// Sets the user defined color with int value.
	/// </summary>
	/// <param name='color'>
	/// Color.
	/// </param>
	[Obsolete("SetUserDefinedColorWithIntValue is deprecated. There is no navigation bar for iOS in v9.x.x+.")]
	public static void SetUserDefinedColorWithIntValue(int color)
	{ }
	
	/// <summary>
	/// IOS ONLY
	/// Sets the transition effect.
	/// </summary>
	/// <param name='transition'>
	/// Transition.
	/// </param>
	public static void SetTransitionEffect(int transition)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.SetTransitionEffect(transition);
	}
	
	//========================================================================================================================
	// FULL SCREEN ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get the full screen ad.
	/// </summary>
	[Obsolete("GetFeaturedApp is deprecated, please use GetFullScreenAd instead.")]
	public static void GetFeaturedApp()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetFullScreenAd();
	}
	
	/// <summary>
	/// Sets the display count for full screen ad. The display count is separate for each unique full screen ad.
	/// </summary>
	/// <param name="displayCount">
	/// A <see cref="System.Int32"/>
	/// </param>
	[Obsolete("SetFeaturedAppDisplayCount is deprecated.")]
	public static void SetFeaturedAppDisplayCount(int displayCount)
	{
	}
	
	/// <summary>
	/// Shows a full screen  ad. This should be called after full screen ad data is retrieved from the server.
	/// </summary>
	[Obsolete("ShowFeaturedAppFullScreenAd is deprecated, please use ShowFullScreenAd instead.")]
	public static void ShowFeaturedAppFullScreenAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowFullScreenAd();
	}

	/// <summary>
	/// Initiates a request to get the full screen ad.
	/// </summary>
	public static void GetFullScreenAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetFullScreenAd();
	}
	
	/// <summary>
	/// Shows a full screen  ad. This should be called after full screen ad data is retrieved from the server.
	/// </summary>
	public static void ShowFullScreenAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowFullScreenAd();
	}
	
	//========================================================================================================================
	// DAILY REWARD ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get the daily reward ad.
	/// </summary>
	[Obsolete("GetReEngagementAd is deprecated, please use GetDailyRewardAd instead.")]
	public static void GetReEngagementAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetDailyRewardAd();
	}
	
	/// <summary>
	/// Shows a daily reward ad. This should be called after daily reward ad data is retrieved from the server.
	/// </summary>
	[Obsolete("ShowReEngagementAd is deprecated, please use ShowDailyRewardAd instead.")]
	public static void ShowReEngagementAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowDailyRewardAd();
	}

	/// <summary>
	/// Initiates a request to get the daily reward ad.
	/// </summary>
	public static void GetDailyRewardAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetDailyRewardAd();
	}
	
	/// <summary>
	/// Shows a daily reward ad. This should be called after daily reward ad data is retrieved from the server.
	/// </summary>
	public static void ShowDailyRewardAd()
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowDailyRewardAd();
	}
		
	//========================================================================================================================
	// VIDEOS
	//========================================================================================================================
	/// <summary>
	/// Initializes video ads. It is recommended that this be called upon app start. No additional
	/// steps are needed to integrate video ads. 
	/// Caching automatically begins after this method is called and will automatically be 
	/// displayed in the offer wall when it has finished caching onto the device.
	/// </summary>
	[Obsolete("InitVideoAd is deprecated, video is now controlled via your Tapjoy Dashboard.")]
	public static void InitVideoAd()
	{ }
	
	/// <summary>
	/// Sets the number of videos to cache on the device. This value is 5 by default.
	/// Lower this value if there is heavy network dependent code running during the initialization of the application.
	/// </summary>
	/// <param name="cacheCount">
	/// A <see cref="System.Int32"/>
	/// </param>
	[Obsolete("SetVideoCacheCount is deprecated, video is now controlled via your Tapjoy Dashboard.")]
	public static void SetVideoCacheCount(int cacheCount)
	{ }
	
	/// <summary>
	/// Sets whether to enable caching for videos.  By default this is enabled.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// </param>
	[Obsolete("EnableVideoCache is deprecated, video is now controlled via your Tapjoy Dashboard.")]
	public static void EnableVideoCache(bool enable)
	{ }
	
	//========================================================================================================================
	// EVENT TRACKING
	//========================================================================================================================
	/// <summary>
	/// Sends a shutdown event.
	/// </summary>
	public static void SendShutDownEvent()
	{
		TapjoyPlatformPlugin.SendShutDownEvent();
	}
	
	/// <summary>
	/// Event when an In-App-Purchased occurs.
	/// </summary>
	/// <param name="name">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="price">
	/// A <see cref="System.Single"/>
	/// </param>
	/// <param name="quantity">
	/// A <see cref="System.Int32"/>
	/// </param>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SendIAPEvent(string name, float price, int quantity, string currencyCode)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.SendIAPEvent(name, price, quantity, currencyCode);
	}
	
	//========================================================================================================================
	// MULTIPLE CURRENCY (currency cannot be managed by Tapjoy)
	//========================================================================================================================
	/// <summary>
	/// Shows the offer wall with a specified currencyID.  The selector is whether 
	/// the offer wall should allow toggling between the app's currencies.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="selector">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void ShowOffersWithCurrencyID(string currencyID, bool selector)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.ShowOffersWithCurrencyID(currencyID, selector);
	}
	
	/// <summary>
	/// Initiates a request to get a Tapjoy Display ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetDisplayAdWithCurrencyID(string currencyID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetDisplayAdWithCurrencyID(currencyID);
	}
	
	/// <summary>
	/// Refreshs the display ad with currency ID.
	/// </summary>
	/// <param name='currencyID'>
	/// Currency ID.
	/// </param>
	[Obsolete("RefreshDisplayAdWithCurrencyID is deprecated, please use GetDisplayAdWithCurrencyID instead.")]
	public static void RefreshDisplayAdWithCurrencyID(string currencyID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetDisplayAdWithCurrencyID(currencyID);
	}
	
	/// <summary>
	/// Initiates a request to get the full screen ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	[Obsolete("GetFeaturedAppWithCurrencyID is deprecated, please use GetFullScreenAdWithCurrencyID instead.")]
	public static void GetFeaturedAppWithCurrencyID(string currencyID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetFullScreenAdWithCurrencyID(currencyID);
	}

	/// <summary>
	/// Initiates a request to get the full screen ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetFullScreenAdWithCurrencyID(string currencyID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetFullScreenAdWithCurrencyID(currencyID);
	}
	
	/// <summary>
	/// Set a multiplier for virtual currency appearance on the offer wall, Display ads, etc.
	/// </summary>
	/// <param name="multiplier">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void SetCurrencyMultiplier(float multiplier)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.SetCurrencyMultiplier(multiplier);
	}
	
	/// <summary>
	/// Initiates a request to get the daily reward ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	[Obsolete("GetReEngagementAdWithCurrencyID is deprecated, please use GetDailyRewardAdWithCurrencyID instead.")]
	public static void GetReEngagementAdWithCurrencyID(string currencyID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetDailyRewardAdWithCurrencyID(currencyID);
	}
	
	/// <summary>
	/// Initiates a request to get the daily reward ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetDailyRewardAdWithCurrencyID(string currencyID)
	{
		if (Application.platform == RuntimePlatform.OSXEditor)
			return;
		
		TapjoyPlatformPlugin.GetDailyRewardAdWithCurrencyID(currencyID);
	}
}

