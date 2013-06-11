#if UNITY_ANDROID
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;


public class TapjoyPluginAndroid : MonoBehaviour
{
	private static AndroidJavaObject currentActivity;
	
	private static AndroidJavaClass tapjoyConnect;
	private static AndroidJavaClass TapjoyConnect
	{
		get
		{
			if (tapjoyConnect == null)
			{
				print("Loading TapjoyPlugin");
			
				// Get the activity context for Android.
				AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
				currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
				
				// Create java class object.
				tapjoyConnect = new AndroidJavaClass("com.tapjoy.TapjoyConnectUnity");
			}
			
			return tapjoyConnect;
		}
	}
	
	private static AndroidJavaObject tapjoyConnectInstance;
	private static AndroidJavaObject TapjoyConnectInstance
	{
		get
		{
			if (tapjoyConnectInstance == null)
			{
				tapjoyConnectInstance = TapjoyConnect.CallStatic<AndroidJavaObject>("getTapjoyConnectInstance");
			}
			
			return tapjoyConnectInstance;
		}
	}
	
	
	/// <summary>
	/// Sets the GameObject handler class name.  MUST BE CALLED BEFORE ANY TAPJOY LIBRARY METHODS.
	/// </summary>
	/// <param name="handlerName">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SetCallbackHandler(string handlerName)
	{
		TapjoyConnect.CallStatic("setHandlerClass", handlerName);
	}
	
	/// <summary>
	/// This method is called to initialize the TapjoyConnect system. This method should be called upon app initialization and must not have logic to prevent it from being called in any case.
	/// </summary>
	/// <param name="appID">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="secretKey">
	/// A <see cref="System.String"/>
	/// </param>
	public static void RequestTapjoyConnect(string appID, string secretKey)
	{
		RequestTapjoyConnect(appID, secretKey, null);
	}

	/// <summary>
	/// This method is called to initialize the TapjoyConnect system. This method should be called upon app initialization and must not have logic to prevent it from being called in any case.
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
		if (flags != null)
		{
			foreach (KeyValuePair<string, string> kvp in flags)
			{
				TapjoyConnect.CallStatic("setFlagKeyValue", kvp.Key, kvp.Value);
			}
		}

		TapjoyConnect.CallStatic("requestTapjoyConnect", currentActivity, appID, secretKey);
	}
	
	/// <summary>
	/// Enables Tapjoy library logging to the console.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void EnableLogging(bool enable)
	{
		// Enable logging for debugging.
		AndroidJavaClass tapjoyLog = new AndroidJavaClass("com.tapjoy.TapjoyLog"); 
		tapjoyLog.CallStatic("enableLogging", enable);	
	}
	
	/// <summary>
	/// This is called when an action is completed. Actions are much like connects, except that this method is only called when a user completes an in-game action.
	/// </summary>
	/// <param name="actionID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void ActionComplete(string actionID)
	{
		TapjoyConnectInstance.Call("actionComplete", actionID);
	}
	
	/// <summary>
	/// Sets the user ID. The user ID defaults to the UDID. This is only changed when NOT using Tapjoy Managed Currency.
	/// </summary>
	/// <param name="userID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SetUserID(string userID)
	{
		TapjoyConnectInstance.Call("setUserID", userID);
	}
	
	/// <summary>
	/// Displays the offer wall.
	/// </summary>
	public static void ShowOffers()
	{
		TapjoyConnectInstance.Call("showOffers");
	}
	
	/// <summary>
	/// Initiates a request to get a user's Tap Points (Tapjoy Managed currency).
	/// </summary>
	public static void GetTapPoints()
	{
		TapjoyConnectInstance.Call("getTapPoints");
	}
	
	/// <summary>
	/// Updates the Tap Points for the user with the given spent amount of currency. If the spent amount exceeds the current amount of currency the user has, nothing will happen.
	/// </summary>
	/// <param name="points">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void SpendTapPoints(int points)
	{
		TapjoyConnectInstance.Call("spendTapPoints", points);
	}
	
	/// <summary>
	/// Updates the Tap Points for the user with the given awarded amount of currency.
	/// </summary>
	/// <param name="points">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void AwardTapPoints(int points)
	{
		TapjoyConnectInstance.Call("awardTapPoints", points);
	}
	
	/// <summary>
	/// Returns the current amount of Tap Points that a user has. When using Tapjoy Managed currency, the user is determined by the UDID of the device. This can safely be called after Tap Points data is retrieved from the server.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Int32"/>
	/// </returns>
	public static int QueryTapPoints()
	{
		return TapjoyConnectInstance.Call<int>("getTapPointsTotal");
	}
	
	/// <summary>
	/// Enables a notifier which triggers whenever the user earns currency through Tapjoy.  It will be called in CurrencyEarned(...).
	/// </summary>
	public static void SetEarnedPointsNotifier()
	{
		TapjoyConnectInstance.Call("setEarnedPointsNotifier");
	}
	
	/// <summary>
	/// Does nothing.
	/// Shows the default earned currency alert on iOS only.
	/// </summary>
	public static void ShowDefaultEarnedCurrencyAlert()
	{
		TapjoyConnectInstance.Call("showDefaultEarnedCurrencyAlert");	
	}

	//========================================================================================================================
	// DISPLAY ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get a Tapjoy display Ad.
	/// </summary>
	public static void GetDisplayAd()
	{
		TapjoyConnectInstance.Call("getDisplayAd");
	}
	
	/// <summary>
	/// Shows the display ad.  Call after receiving the DisplayAdLoaded(...) notifier.
	/// </summary>
	public static void ShowDisplayAd()
	{
		TapjoyConnectInstance.Call("showDisplayAd");
	}
	
	/// <summary>
	/// Hides the display ad.
	/// </summary>
	public static void HideDisplayAd()
	{
		TapjoyConnectInstance.Call("hideDisplayAd");
	}
	
	/// <summary>
	/// Sets the display ad size.  Use TapjoyDisplayAdSize enum for supported sizes.
	/// </summary>
	/// <param name="size">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void SetDisplayAdSize(string size)
	{
		TapjoyConnectInstance.Call("setDisplayAdSize", size);
	}
	
	/// <summary>
	/// Updates the display ad with a new one or disables auto-refresh.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void EnableDisplayAdAutoRefresh(bool enable)
	{
		TapjoyConnectInstance.Call("enableDisplayAdAutoRefresh", enable);
	}
	
	/// <summary>
	/// Refreshs the display ad.
	/// </summary>
	public static void RefreshDisplayAd()
	{
		TapjoyConnectInstance.Call("getDisplayAd");
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
		TapjoyConnectInstance.Call("setDisplayAdPosition", x, y);
	}
	
	/// <summary>
	/// IOS ONLY
	/// Sets the transition effect.
	/// </summary>
	/// <param name='transition'>
	/// Transition.
	/// </param>
	public static void SetTransitionEffect(int transition)
	{
		// Not implemented in Android.
	}
	
	/// <summary>
	/// Initiates a request to get the full screen ad.
	/// </summary>
	public static void GetFullScreenAd()
	{
		TapjoyConnectInstance.Call("getFullScreenAd");
	}
	
	/// <summary>
	/// Shows a full screen  ad. This should be called after full screen ad data is retrieved from the server.
	/// </summary>
	public static void ShowFullScreenAd()
	{
		TapjoyConnectInstance.Call("showFullScreenAd");
	}
	
	//========================================================================================================================
	// DAILY REWARD ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get the daily reward ad.
	/// </summary>
	public static void GetDailyRewardAd()
	{
		TapjoyConnectInstance.Call("getDailyRewardAd");
	}
	
	/// <summary>
	/// Shows a Daily Reward ad. This should be called after Daily Reward ad data is retrieved from the server.
	/// </summary>
	public static void ShowDailyRewardAd()
	{
		TapjoyConnectInstance.Call("showDailyRewardAd");
	}
	
	//========================================================================================================================
	// EVENT TRACKING
	//========================================================================================================================
	/// <summary>
	/// Sends a shutdown event.
	/// </summary>
	public static void SendShutDownEvent()
	{
		TapjoyConnectInstance.Call("sendShutDownEvent");
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
		TapjoyConnectInstance.Call("sendIAPEvent", name, price, quantity, currencyCode);
	}
	
	//========================================================================================================================
	// MULTIPLE CURRENCY (currency cannot be managed by Tapjoy)
	//========================================================================================================================
	/// <summary>
	/// Shows the offer wall with a specified currencyID.  The selector is whether the offer wall should allow toggling between the app's currencies.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="selector">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void ShowOffersWithCurrencyID(string currencyID, bool selector)
	{
		TapjoyConnectInstance.Call("showOffersWithCurrencyID", currencyID, selector);
	}
	
	/// <summary>
	/// Initiates a request to get a Tapjoy display ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetDisplayAdWithCurrencyID(string currencyID)
	{
		TapjoyConnectInstance.Call("getDisplayAdWithCurrencyID", currencyID);
	}
	
	/// <summary>
	/// Initiates a request to get the full screen ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetFullScreenAdWithCurrencyID(string currencyID)
	{
		TapjoyConnectInstance.Call("getFullScreenAdWithCurrencyID", currencyID);
	}
	
	/// <summary>
	/// Set a multiplier for virtual currency appearance on the offer wall, display ads, etc.
	/// </summary>
	/// <param name="multiplier">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void SetCurrencyMultiplier(float multiplier)
	{
		TapjoyConnectInstance.Call("setCurrencyMultiplier", multiplier);
	}
	
	/// <summary>
	/// Initiates a request to get the daily reward ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetDailyRewardAdWithCurrencyID(string currencyID)
	{
		TapjoyConnectInstance.Call("getDailyRewardAdWithCurrencyID", currencyID);
	}
}
#endif