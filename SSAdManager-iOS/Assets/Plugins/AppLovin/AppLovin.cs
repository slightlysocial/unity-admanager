/**
 * AppLovin Unity Plugin C# Wrapper
 *
 * @author David Anderson, Matt Szaro
 * @version 2.5.1
 */

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AppLovin {
    public const float AD_POSITION_CENTER  = -10000;
    public const float AD_POSITION_LEFT  = -20000;
    public const float AD_POSITION_RIGHT  = -30000;
    public const float AD_POSITION_TOP    = -40000;
	public const float AD_POSITION_BOTTOM = -50000;
	
	#if UNITY_IPHONE
		[DllImport ("__Internal")]
		private static extern void _AppLovinShowAd ();
		
		[DllImport ("__Internal")]
		private static extern void _AppLovinHideAd ();
		
		[DllImport ("__Internal")]
		private static extern void _AppLovinShowInterstitial ();
		
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetAdPosition(float x, float y);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetAdWidth(float width);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetSdkKey (string sdkKey);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinInitializeSdk ();
	#endif
	
	#if UNITY_ANDROID
		public AndroidJavaClass applovinFacade = new AndroidJavaClass("com.applovin.sdk.unity.AppLovinFacade");
		public AndroidJavaObject currentActivity = null;
	#endif
	
	public static AppLovin DefaultPlugin = null;
	public AppLovinTargetingData targetingData = null;
	
	/**
	 * Gets the default AppLovin plugin
	 */
	public static AppLovin getDefaultPlugin() {
		if (DefaultPlugin == null) {
			#if UNITY_ANDROID
				AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				DefaultPlugin = new AppLovin( jc.GetStatic<AndroidJavaObject>("currentActivity") );
			#else
				DefaultPlugin = new AppLovin();
			#endif
		}
		
		return DefaultPlugin;
	}
	
	
	/**
	 * Create an instance of AppLovin with a custom activity
	 * 
	 * @param {AndroidJavaObject} The activity that you are using
	 */
	#if UNITY_ANDROID
	public AppLovin(AndroidJavaObject activity) {
		if (activity == null) throw new MissingReferenceException("No parent activity specified");
		
		currentActivity = activity;
		targetingData = new AppLovinTargetingData(currentActivity);
	}
	#endif
	
	public AppLovin() {
		targetingData = new AppLovinTargetingData();	
	}
	
	/**
	 * Get AppLovinTargetingData object to set custom request parameters
	 */
	public AppLovinTargetingData getTargetingData() {
		return targetingData;
	}
	
	
	/**
	 * Manually initialize the SDK
	 */
	public void initializeSdk() {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("InitializeSdk", currentActivity);
		#endif
		#if UNITY_IPHONE
			_AppLovinInitializeSdk();
		#endif
	#endif
	}
	
	/**
	 * Loads and displays the AppLovin banner ad
	 */
	public void showAd() {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
		   applovinFacade.CallStatic("ShowAd", currentActivity);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinShowAd();	
		#endif
	#endif
	}
	
	
	/**
	 * Show an AppLovin Interstitial
	 */
	public void showInterstitial() {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
		   applovinFacade.CallStatic("ShowInterstitial", currentActivity);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinShowInterstitial();	
		#endif
	#endif
	}
	
	
	/**
	 * Hides the AppLovin ad
	 */
	public void hideAd() {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
		   applovinFacade.CallStatic("HideAd", currentActivity);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinHideAd();
		#endif
	#endif
	}
	
	
	/**
	 * Set the position of the banner ad
	 * 
	 * @param {float} x  Horizontal position of the ad (AD_POSITION_LEFT/RIGHT/CENTER)
	 * @param {float} y  Vertical position of the ad (AD_POSITION_TOP/BOTTOM)
	 */
	public void setAdPosition(float x, float y) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetAdPosition", currentActivity, x, y);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetAdPosition(x, y);
		#endif
	#endif
	}
	
	
	/**
	 * Set the width of the banner ad
	 * 
	 * @param {int} width  Desired width of the banner ad in dip
	 */
	public void setAdWidth(int width) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetAdWidth", currentActivity, width);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetAdWidth(width);	
		#endif
	#endif
	}
	
	public void setSdkKey(string sdkKey) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetSdkKey", currentActivity, sdkKey);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetSdkKey(sdkKey);	
		#endif
	#endif
	}

	
	/**
	 * Loads and displays the AppLovin banner ad
	 */
	public static void ShowAd() {
		getDefaultPlugin().showAd();
	}
	
	/**
	 * Loads and displays the AppLovin banner ad at given position
	 * 
	 * @param {float} x  Horizontal position of the ad (AD_POSITION_LEFT, AD_POSITION_CENTER, AD_POSITION_RIGHT) or float
	 * @param {float} y  Vertical position of the ad (AD_POSITION_TOP, AD_POSITION_BOTTOM) or float
	 */
	public static void ShowAd(float x, float y) {
		AppLovin.SetAdPosition (x, y);
		AppLovin.ShowAd ();
	}
	
	
	/**
	 * Show an AppLovin Interstitial
	 */
	public static void ShowInterstitial() {
		getDefaultPlugin().showInterstitial();
	}
	
	
	/**
	 * Hides the AppLovin ad
	 */
	public static void HideAd() {
		getDefaultPlugin().hideAd();
	}
	
	/**
	 * Set the position of the banner ad
	 * 
	 * @param {float} x  Horizontal position of the ad (AD_POSITION_LEFT, AD_POSITION_CENTER, AD_POSITION_RIGHT) or float
	 * @param {float} y  Vertical position of the ad (AD_POSITION_TOP, AD_POSITION_BOTTOM) or float
	 */
	public static void SetAdPosition(float x, float y) {
		getDefaultPlugin().setAdPosition(x, y);
	}
	
	
	
	/**
	 * Set the width of the banner ad
	 * 
	 * @param {int} width  Desired width of the banner ad in dip
	 */
	public static void SetAdWidth(int width) {
		getDefaultPlugin().setAdWidth(width);
	}
	
	public static void SetSdkKey(string sdkKey) {
		getDefaultPlugin().setSdkKey(sdkKey);
	}
	
	/**
	 * Get AppLovinTargetingData object to set custom request parameters
	 */
	public static AppLovinTargetingData GetTargetingData() {
		return getDefaultPlugin().getTargetingData();
	}
	
	// Initialize the AppLovin SDK
	public static void InitializeSdk() {
	    getDefaultPlugin().initializeSdk();
	}
}

public class AppLovinTargetingData {
	public const string GENDER_MALE 				= "m";
	public const string GENDER_FEMALE 				= "f";
	
    public const string ETHNICITY_MIXED            = "mixed";
    public const string ETHNICITY_ASIAN            = "asian";
    public const string ETHNICITY_BLACK            = "black";
    public const string ETHNICITY_HISPANIC         = "hispanic";
    public const string ETHNICITY_NATIVE_AMERICAN  = "native_american";
    public const string ETHNICITY_WHITE            = "white";
    public const string ETHNICITY_OTHER            = "other";
    
    public const string MARITAL_STATUS_SINGLE      = "single";
    public const string MARITAL_STATUS_MARRIED     = "married";
    public const string MARITAL_STATUS_DIVORCED    = "divorced";
    public const string MARITAL_STATUS_WIDOWED     = "widowed";
	
    public const string EDUCATION_HIGH_SCHOOL      = "high_school";
    public const string EDUCATION_COLLEGE          = "college";
    public const string EDUCATION_BACHELORS        = "bachelors";
    public const string EDUCATION_MASTERS          = "masters";
    public const string EDUCATION_DOCTORAL         = "doctoral";
    public const string EDUCATION_OTHER            = "other";
	
	
	#if UNITY_IPHONE
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetGender (string gender);
		
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetPhoneNumber (string phoneNumber);
		
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetBirthYear (int birthYear);
		
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetLanguage(string language);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetCountry(string country);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetFirstName(string firstName);
		
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetCarrier(string carrier);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetEmail(string email);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetEmails(string[] emails);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetInterests(string[] interests);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetKeywords(string[] keywords);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetEducation(string education);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetEthnicity(string ethnicity);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetIncome(string income);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinSetMaritalStatus(string marital);
	
		[DllImport ("__Internal")]
		private static extern void _AppLovinPutExtra(string key, string val);
	
	#endif
	
	public AppLovinTargetingData() {
		// default constructor
	}
	
	#if UNITY_ANDROID
		public AndroidJavaObject currentActivity = null;
		public AndroidJavaClass applovinFacade = new AndroidJavaClass("com.applovin.sdk.unity.AppLovinFacade");
	
		public AppLovinTargetingData(AndroidJavaObject activity) {
			if (activity == null) throw new MissingReferenceException("No parent activity specified");

			currentActivity = activity;
		}
	#endif

	/**
	 * Set the current user's gender
	 * 
	 * @param {string} gender  The current user's gender
	 */
	public void setGender(string gender) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetGender", currentActivity, gender);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetGender(gender);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's phone number
	 * 
	 * @param {string} phoneNumber  The current user's phone number
	 */
	public void setPhoneNumber(string phoneNumber) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetPhoneNumber", currentActivity, phoneNumber);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetPhoneNumber(phoneNumber);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's year of birth
	 * 
	 * @param {int} birthYear  The current user's year of birth
	 */
	public void setBirthYear(int birthYear) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetBirthYear", currentActivity, birthYear);
		#endif
				
		#if UNITY_IPHONE
			_AppLovinSetBirthYear(birthYear);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's language
	 * 
	 * @param {string} language  The current user's language
	 */
	public void setLanguage(string language) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetLanguage", currentActivity, language);
		#endif
				
		#if UNITY_IPHONE
			_AppLovinSetLanguage(language);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's country
	 * 
	 * @param {string} country  The current user's country
	 */
	public void setCountry(string country) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetCountry", currentActivity, country);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetCountry(country);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's first name
	 * 
	 * @param {string} firstName  The current user's first name
	 */
	public void setFirstName(string firstName) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetFirstName", currentActivity, firstName);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetFirstName(firstName);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's carrier
	 * 
	 * @param {string} carrier  The current user's carrier
	 */
	public void setCarrier(string carrier) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetCarrier", currentActivity, carrier);
		#endif

		#if UNITY_IPHONE
			_AppLovinSetCarrier(carrier);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's email
	 * 
	 * @param {string} email  The current user's email
	 */
	public void setEmail(string email) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetEmail", currentActivity, email);
		#endif

		#if UNITY_IPHONE
			_AppLovinSetEmail(email);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's emails
	 * 
	 * @param {params string[]} emails  The current user's emails
	 */
	public void setEmails(params string[] emails) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetEmails", currentActivity, emails);
		#endif

		#if UNITY_IPHONE
			_AppLovinSetEmails(emails);
		#endif
	#endif
	}
	
	/*
	 * Set the current user's interests
	 * 
	 * @param {params string[]} interests  The current user's interests
	 */
	public void setInterests(params string[] interests) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetInterests", currentActivity, interests);
		#endif

		#if UNITY_IPHONE
			_AppLovinSetInterests(interests);
		#endif
	#endif
	}
	
	/**
	 * Set the keywords for the current app
	 * 
	 * @param {params string[]} keywords  Some keywords for the current app
	 */
	public void setKeywords(params string[] keywords) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetKeywords", currentActivity, keywords);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetKeywords(keywords);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's education
	 * 
	 * @param {string} education  The current user's education
	 */
	public void setEducation(string education) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetEducation", currentActivity, education);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetEducation(education);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's ethnicity
	 * 
	 * @param {string} education  The current user's ethnicity
	 */
	public void setEthnicity(string ethnicity) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetEthnicity", currentActivity, ethnicity);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetEthnicity(ethnicity);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's marital status
	 * 
	 * @param {string} marital  The current user's marital status
	 */
	public void setMaritalStatus(string marital) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetMaritalStatus", currentActivity, marital);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetMaritalStatus(marital);
		#endif
	#endif
	}
	
	/**
	 * Set the current user's income
	 * 
	 * @param {string} income  The current user's income
	 */
	public void setIncome(string income) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("SetIncome", currentActivity, income);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinSetIncome(income);
		#endif
	#endif
	}
	
	/**
	 * Set extra parameter for ad requests
	 * 
	 * @param {string} key  Key of the parameter
	 * @param {string} val  Value of the parameter
	 */
	public void putExtra(string key, string val) {
	#if !UNITY_EDITOR
		#if UNITY_ANDROID
			applovinFacade.CallStatic("PutExtra", currentActivity, key, val);
		#endif
		
		#if UNITY_IPHONE
			_AppLovinPutExtra(key, val);
		#endif
	#endif
	}
}
