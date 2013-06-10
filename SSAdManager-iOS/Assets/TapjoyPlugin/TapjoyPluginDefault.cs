#if !UNITY_IPHONE && !UNITY_ANDROID
using System;
using System.Collections.Generic;

public class TapjoyPluginDefault
{
	/// <summary>
	/// Sets the callback handler.
	/// </summary>
	/// <param name='handlerName'>
	/// Handler name. Must match a Unity GameObject name, for the native code
	/// to utilize UnitySendMessage() properly.
	/// </param>
	public static void SetCallbackHandler(string handlerName) { }
	
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
	public static void RequestTapjoyConnect(string appID, string secretKey) { }

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
	public static void RequestTapjoyConnect(string appID, string secretKey, Dictionary<string, string> flags) { }
	
	/// <summary>
	/// Enables Tapjoy library logging to the console.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void EnableLogging(bool enable) { }
	
	/// <summary>
	/// This is called when an action is completed.
	/// Actions are much like connects, except that this method is
	/// only called when a user completes an in-game action.
	/// </summary>
	/// <param name="actionID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void ActionComplete(string actionID) { }
	
	/// <summary>
	/// Sets the user ID. The user ID defaults to the UDID.
	/// This is only changed when NOT using Tapjoy Managed Currency.
	/// </summary>
	/// <param name="userID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void SetUserID(string userID) { }
	
	/// <summary>
	/// Displays the offer wall.
	/// </summary>
	public static void ShowOffers() { }
	
	/// <summary>
	/// Initiates a request to get a user's Tap Points (Tapjoy Managed currency).
	/// </summary>
	public static void GetTapPoints() { }
	
	/// <summary>
	/// Updates the Tap Points for the user with the given spent amount of currency.
	/// If the spent amount exceeds the current amount of currency the user has, nothing will happen.
	/// </summary>
	/// <param name="points">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void SpendTapPoints(int points) { }
	
	/// <summary>
	/// Updates the Tap Points for the user with the given awarded amount of currency.
	/// </summary>
	/// <param name="points">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void AwardTapPoints(int points) { }
	
	/// <summary>
	/// Returns the current amount of Tap Points that a user has.
	/// When using Tapjoy Managed currency, the user is determined by the UDID of the device.
	/// This can safely be called after Tap Points data is retrieved from the server.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Int32"/>
	/// </returns>
	public static int QueryTapPoints() { return 0; }
	
	/// <summary>
	/// Shows the default earned currency alert in iOS.
	/// </summary>
	public static void ShowDefaultEarnedCurrencyAlert() { }

	//========================================================================================================================
	// DISPLAY ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get a Tapjoy Display Ad.
	/// </summary>
	public static void GetDisplayAd() { }
	
	/// <summary>
	/// Shows the Display ad.  Call after receiving the DisplayAdLoaded(...) notifier.
	/// </summary>
	public static void ShowDisplayAd() { }
	
	/// <summary>
	/// Hides the Display ad.
	/// </summary>
	public static void HideDisplayAd() { }
	
	/// <summary>
	/// Sets the Display ad size.  Use TapjoyDisplayAdSize enum for supported sizes.
	/// </summary>
	/// <param name="size">
	/// A <see cref="TapjoyDisplayAdSize"/>
	/// </param>
	public static void SetDisplayAdSize(string size) { }
	
	/// <summary>
	/// Updates the Display ad with a new one or disables auto-refresh.
	/// </summary>
	/// <param name="enable">
	/// A <see cref="System.Boolean"/>
	/// </param>
	public static void EnableDisplayAdAutoRefresh(bool enable) { }
	
	/// <summary>
	/// Moves the location of the Display Ad.
	/// </summary>
	/// <param name="x">
	/// A <see cref="System.Int32"/>
	/// </param>
	/// <param name="y">
	/// A <see cref="System.Int32"/>
	/// </param>
	public static void MoveDisplayAd(int x, int y) { }
	
	//========================================================================================================================
	// PLATFORM SETTINGS
	//========================================================================================================================
	/// <summary>
	/// IOS ONLY
	/// Sets the transition effect.
	/// </summary>
	/// <param name='transition'>
	/// Transition.
	/// </param>
	public static void SetTransitionEffect(int transition) { }
	
	//========================================================================================================================
	// FULL SCREEN ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get the full screen ad.
	/// </summary>
	public static void GetFullScreenAd() { }
	
	/// <summary>
	/// Shows a full screen  ad. This should be called after full screen ad data is retrieved from the server.
	/// </summary>
	public static void ShowFullScreenAd() { }
	
	//========================================================================================================================
	// DAILY REWARD ADS
	//========================================================================================================================
	/// <summary>
	/// Initiates a request to get the daily reward ad.
	/// </summary>
	public static void GetDailyRewardAd() { }
	
	/// <summary>
	/// Shows a daily reward ad. This should be called after daily reward ad data is retrieved from the server.
	/// </summary>
	public static void ShowDailyRewardAd() { }
	
	//========================================================================================================================
	// EVENT TRACKING
	//========================================================================================================================
	/// <summary>
	/// Sends a shutdown event.
	/// </summary>
	public static void SendShutDownEvent() { }
	
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
	public static void SendIAPEvent(string name, float price, int quantity, string currencyCode) { }
	
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
	public static void ShowOffersWithCurrencyID(string currencyID, bool selector) { }
	
	/// <summary>
	/// Initiates a request to get a Tapjoy Display ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetDisplayAdWithCurrencyID(string currencyID) { }
	
	/// <summary>
	/// Initiates a request to get the full screen ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetFullScreenAdWithCurrencyID(string currencyID) { }
	
	/// <summary>
	/// Set a multiplier for virtual currency appearance on the offer wall, Display ads, etc.
	/// </summary>
	/// <param name="multiplier">
	/// A <see cref="System.Single"/>
	/// </param>
	public static void SetCurrencyMultiplier(float multiplier) { }
	
	/// <summary>
	/// Initiates a request to get the daily reward ad with a specified currencyID.
	/// </summary>
	/// <param name="currencyID">
	/// A <see cref="System.String"/>
	/// </param>
	public static void GetDailyRewardAdWithCurrencyID(string currencyID) { }
}
#endif