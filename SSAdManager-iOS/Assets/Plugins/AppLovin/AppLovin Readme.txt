
AppLovin Unity Plugin 2.1

https://www.applovin.com/

================


- Getting Started -


Android -

The first thing you need to do is edit the AndroidManifest file and put your AppLovin SDK key where it says YOUR_SDK_KEY.

The second thing you need to do is replace all instances of "YOUR_PACKAGE_NAME" with your application's package name.


iOS -

You need to call AppLovin.setSdkKey("YOUR_SDK_KEY") or set AppLovinSdkKey to your SDK Key in your applications info.plist every time after compiling from Unity.


Both -

We recommend you call AppLovin.InitializeSdk() before calling any of the showAd/showInterstitial methods.
This will allow the SDK to perform initial start-up tasks like pre-caching the first ad, resulting in
a more responsive initial ad display.

---------------------------


- Plugin Usage -


Using the AppLovin C# wrapper class and the AppLovinFacade java class coupled with the iOS native plugin, 
you can easily manipulate Ads programmatically across both platforms.


---------------------------


Show Banner Ad:

	AppLovin.ShowAd();


---------------------------


Show Banner Ad at Position:

With constants -

	AppLovin.ShowAd(AppLovin.AD_POSITION_CENTER, AppLovin.AD_POSITION_BOTTOM);

Available horizontal constants are: AD_POSITION_CENTER, AD_POSITION_LEFT, AD_POSITION_RIGHT
Available vertical constants are: AD_POSITION_TOP, AD_POSITION_BOTTOM

With dp -

	AppLovin.ShowAd("50", "50");

---------------------------


Hide the Ad:

	AppLovin.HideAd();


---------------------------


Interstitial Ad:

	AppLovin.ShowInterstitial();


---------------------------


Update Ad Position:

With constants -

	AppLovin.SetAdPosition(AppLovin.AD_POSITION_CENTER, AppLovin.AD_POSITION_BOTTOM);

Available horizontal constants are: AD_POSITION_CENTER, AD_POSITION_LEFT, AD_POSITION_RIGHT
Available vertical constants are: AD_POSITION_TOP, AD_POSITION_BOTTOM

With dp -

	AppLovin.SetAdPosition("50", "50");

---------------------------


Set Ad Width:

	AppLovin.SetAdWidth(400);

The width is in dip.


---------------------------



- Targeting -

The SDK now supports passing custom targeting parameters to improve the performance of AppLovin advertisements.

1. Instantiate the targeting object -

	AppLovinTargetingData targeting = AppLovin.GetTargetingData();

2. Call any of the following targeting functions -

Set Country Code, two-character ISO 3166-1 country code:
	targeting.setCountry("US");

Phone Number:
	targeting.setPhoneNumber("+1-555-555-1234");

Email:
	targeting.setEmail("test@gmail.com");

Emails (Can set multiple emails):
	targeting.setEmails("test@gmail.com", "test@yahoo.com", "test@live.com");

First Name:
	targeting.setFirstName("Bob");

Birth Year:
	targeting.setBirthYear(1978);

Gender:
	targeting.setGender(AppLovinTargetingData.GENDER_MALE);
	
	Note: use GENDER_MALE or GENDER_FEMALE as constants of this class or pass ‘m’ or ‘f’ directly

Language (two character ISO 639-1 language code):
	targeting.setLanguage("US");

KeyWords:
	targeting.setKeywords("applovin", "test", "demo", "example");

Interests:
	targeting.setInterests("books", "games", "pizza");

Education Level:
	targeting.setEducation(AppLovinTargetingData.EDUCATION_BACHELORS);

	Note: Use the following constants EDUCATION_NONE, 	EDUCATION_HIGH_SCHOOL,	EDUCATION_COLLEGE,	EDUCATION_BACHELORS,	EDUCATION_MASTERS, EDUCATION_DOCTORAL and	EDUCATION_OTHER

Marital Status:
	targeting.setMaritalStatus(AppLovinTargetingData.MARITAL_STATUS_SINGLE);

	Note: Use the following constants MARITAL_STATUS_NONE, 
		MARITAL_STATUS_SINGLE, MARITAL_STATUS_MARRIED, 
		MARITAL_STATUS_DIVORCED and MARITAL_STATUS_WIDOWED


Ethnicity:
	targeting.setEthnicity( AppLovinTargetingData.ETHNICITY_MIXED );

	Note: Use the following constants ETHNICITY_NONE, 	ETHNICITY_MIXED, ETHNICITY_ASIAN, ETHNICITY_BLACK, 	ETHNICITY_HISPANIC, ETHNICITY_NATIVE_AMERICAN, 	ETHNICITY_WHITE and ETHNICITY_OTHER
         
Income Level:
	targeting.setIncome("80000-120K");

	Note: Following methods are supported
		An exact number like 100000 or 100K
		A range like 10K-30K
		A range like ">100K" or "<100K"

Extra (free form data not included above):
	targeting.putExtra("some_key", "some_value");
	

---------------------------



If you have any questions regarding the Unity Plugin, contact AppLovin support at support@applovin.com

https://www.applovin.com/