//
//  AppLovinUnity.mm
//  sdk
//
//  Created by David Anderson on 10/9/12.
//  Updated by Matt Szaro on 3/25/13.
//

#import "AppController.h"
#import "ALSdk.h"
#import "ALAdView.h"
#import "ALInterstitialAd.h"

UIView* UnityGetGLView();


// When native code plugin is implemented in .mm / .cpp file, then functions
// should be surrounded with extern "C" block to conform C function naming rules
extern "C" {
    static const NSString * UNITY_PLUGIN_VERSION = @"2.1";

    static bool adLoaded = NO;
    
    static const CGFloat POSITION_CENTER = -10000;
    static const CGFloat POSITION_LEFT = -20000;
    static const CGFloat POSITION_RIGHT = -30000;
    static const CGFloat POSITION_TOP = -40000;
    static const CGFloat POSITION_BOTTOM = -50000;
    
    static CGFloat adX;
    static CGFloat adY;
    
    static ALAdView *adView;
    
    /**
     *  For internal use only
     */
    ALAdView *SharedAdview()
    {
        if (!adView)
        {
            [[ALSdk shared] setPluginVersion:[NSString stringWithFormat:@"unity-%@", UNITY_PLUGIN_VERSION]];
            adView = [[ALAdView alloc] initBannerAd];
        }
        
        return adView;
    }
    
    /**
     * Initialize the AppLovin SDK manually
     */
    void _AppLovinInitializeSdk()
    {
        [[ALSdk shared] initializeSdk];
    }
    
    /**
     *  Show AppLovin Banner Ad
     */
    void _AppLovinShowAd()
    {
        [SharedAdview() setHidden:false];
        
        if (!adLoaded)
        {
            [SharedAdview() loadNextAd];
            [UnityGetGLView() addSubview:SharedAdview()];
            adLoaded = YES;
        }
    }
    
    /**
     *  Hide AppLovin Banner Ad
     */
    void _AppLovinHideAd()
    {
        [SharedAdview() setHidden:true];
    }
    
    
    /**
     *  Show AppLovin Interstitial Ad
     */
    void _AppLovinShowInterstitial()
    {
        [ALInterstitialAd showOver:[[UIApplication sharedApplication] keyWindow]];
    }
    
    
    /**
     *  For internal use only
     */
    CGFloat getAvailableScreenWidth()
    {
        CGRect screenBounds = [[UIScreen mainScreen] applicationFrame];
        
        UIInterfaceOrientation orientation = [[UIApplication sharedApplication] statusBarOrientation];
        
        CGFloat width = screenBounds.size.width;
        
        // Don't trust the system
        if ((UIInterfaceOrientationIsLandscape(orientation) && screenBounds.size.height > screenBounds.size.width) || (UIInterfaceOrientationIsPortrait(orientation) && screenBounds.size.width > screenBounds.size.height))
        {
            width = screenBounds.size.height;
        }
        
        return width;
    }
    
    /**
     *  For internal use only
     */
    CGFloat getAvailableScreenHeight()
    {
        CGRect screenBounds = [[UIScreen mainScreen] applicationFrame];
        
        UIInterfaceOrientation orientation = [[UIApplication sharedApplication] statusBarOrientation];
        
        CGFloat height = screenBounds.size.height;
        
        if ((UIInterfaceOrientationIsLandscape(orientation) && screenBounds.size.height > screenBounds.size.width) || (UIInterfaceOrientationIsPortrait(orientation) && screenBounds.size.width > screenBounds.size.height))
        {
            height = screenBounds.size.width;
        }
        
        return height;
    }
    
    /**
     *  For internal use only
     */
    void updateAdPosition()
    {
        CGRect newRect = SharedAdview().frame;
        
        if (adX == POSITION_CENTER)
        {
            newRect.origin.x = getAvailableScreenWidth() / 2 - newRect.size.width / 2;
        }
        else if (adX == POSITION_LEFT)
        {
            newRect.origin.x = 0;
        }
        else if (adX == POSITION_RIGHT)
        {
            newRect.origin.x = getAvailableScreenWidth() - newRect.size.width;
        }
        else
        {
            newRect.origin.x = adX;
        }
        
        if (adY == POSITION_TOP)
        {
            newRect.origin.y = 0;
        }
        else if (adY == POSITION_BOTTOM)
        {
            newRect.origin.y = getAvailableScreenHeight() - newRect.size.height;
        }
        else
        {
            newRect.origin.y = adY;
        }
        
        SharedAdview().frame = newRect;
    }
    
    /**
     * Set the position of the banner ad
     * 
     * @param float x   Horizontal position of the ad in dp or constant (POSITION_LEFT, POSITION_CENTER, POSITION_RIGHT)
     * @param float y   Veritcal position of the ad in dp or constant (POSITION_TOP, POSITION_BOTTOM)
     */
    void _AppLovinSetAdPosition(CGFloat x, CGFloat y)
    {
        adX = x;
        adY = y;
        
        updateAdPosition();
    }
    
    /**
     * Set the width of the banner ad
     *
     * @param float width   Width of the ad in dp
     */
    void _AppLovinSetAdWidth(CGFloat width)
    {
        CGRect newRect = [SharedAdview() frame];
        
        newRect.size.width = width;
        
        SharedAdview().frame = newRect;
        
        updateAdPosition();
    }
    
    /**
     * Set the gender for targeting
     *
     * @param string gender    Gender of the user. Accepted values: m, f
     */
    void _AppLovinSetGender(const char * gender)
    {
        if (!gender) { return; };
        
        NSString * genderStr = [NSString stringWithUTF8String:gender];
        
        char genderChar;
        if ([genderStr isEqualToString:@"m"])
        {
            genderChar = kALGenderMale;
        }
        else if ([genderStr isEqualToString:@"f"])
        {
            genderChar = kALGenderFemale;
        }
        
        if (genderChar)
        {
            [[[ALSdk shared] targetingData] setGender:genderChar];
        }
    }
    
    /**
     * Set the phone number for targeting
     *
     * @param string phoneNumber    The current user's phone number
     */
    void _AppLovinSetPhoneNumber(const char * phoneNumber)
    {
        if (!phoneNumber) { return; };
        
        NSString * nsPhone = [NSString stringWithUTF8String:phoneNumber];
        [[[ALSdk shared] targetingData] setPhoneNumber:nsPhone];
    }
    
    /**
     * Set the year of birth for targeting
     *
     * @param int birthYear    The current user's year of birth
     */
    void _AppLovinSetBirthYear(int birthYear)
    {
        [[[ALSdk shared] targetingData] setBirthYear:birthYear];
    }
    
    /**
     * Set the language for targeting
     *
     * @param string language    The current user's language
     */
    void _AppLovinSetLanguage(const char * language)
    {
        if (!language) { return; };
        
        NSString * languageStr = [NSString stringWithUTF8String:language];
        
        [[[ALSdk shared] targetingData] setLanguage:languageStr];
    }
    
    /**
     * Set the country for targeting
     *
     * @param string country    The current user's country
     */
    void _AppLovinSetCountry(const char * country)
    {
        if (!country) { return; };
        
        NSString * countryStr = [NSString stringWithUTF8String:country];
        
        [[[ALSdk shared] targetingData] setCountry:countryStr];
    }
    
    
    /**
     * Set the user's first name for targeting
     *
     * @param string firstName    The current user's first name
     */
    void _AppLovinSetFirstName(const char * firstName)
    {
        if (!firstName) { return; };
        
        NSString * firstNameStr = [NSString stringWithUTF8String:firstName];
        
        [[[ALSdk shared] targetingData] setFirstName:firstNameStr];
    }
    
    /**
     * Set the carrier for targeting
     *
     * @param string carrier    The current user's carrier
     */
    void _AppLovinSetCarrier(const char * carrier)
    {
        if (!carrier) { return; };
        
        NSString * carrierStr = [NSString stringWithUTF8String:carrier];
        
        [[[ALSdk shared] targetingData] setFirstName:carrierStr];
    }
    
    /**
     * Set the user's email for targeting
     *
     * @param string email    The current user's email
     */
    void _AppLovinSetEmail(const char * email)
    {
        if (!email) { return; };
        
        NSString * emailStr = [NSString stringWithUTF8String:email];
        
        [[[ALSdk shared] targetingData] setEmail:emailStr];
    }
    
    /**
     * Set the user's emails for targeting
     *
     * @param string[] emails    The current user's emails
     */
    void _AppLovinSetEmails(const char * emails[])
    {
        NSMutableArray * emailsArr = [NSMutableArray arrayWithCapacity:sizeof(*emails)];
        for (int i = 0; i < sizeof(*emails); i++)
        {
            if (emails[i])
            {
                [emailsArr addObject:[NSString stringWithUTF8String:emails[i]]];
            }
        }
        [[[ALSdk shared] targetingData] setEmails:emailsArr];
    }
    
    /**
     * Set the user's interests for targeting
     *
     * @param string[] interests    The current user's intersts
     */
    void _AppLovinSetInterests(const char * interests[])
    {
        NSMutableArray * interestsArr = [NSMutableArray arrayWithCapacity:sizeof(*interests)];
        for (int i = 0; i < sizeof(*interests); i++)
        {
            if (interests[i])
            {
                [interestsArr addObject:[NSString stringWithUTF8String:interests[i]]];
            }
        }
        
        [[[ALSdk shared] targetingData] setInterests:interestsArr];
    }
    
    /**
     * Set the app's keywords for targeting
     *
     * @param string[] keywords    The current app's keywords
     */
    void _AppLovinSetKeywords(const char * keywords[])
    {
        NSMutableArray * keywordsArr = [NSMutableArray arrayWithCapacity:sizeof(*keywords)];
        for (int i = 0; i < sizeof(*keywords); i++)
        {
            if (keywords[i])
            {
                [keywordsArr addObject:[NSString stringWithUTF8String:keywords[i]]];
            }
        }
        [[[ALSdk shared] targetingData] setKeywords:keywordsArr];
    }
    
    /**
     * Set the user's education level for targeting
     *
     * @param string education    The current user's education level
     */
    void _AppLovinSetEducation(const char * education)
    {
        if (!education) { return; };
        
        NSString * educationStr = [NSString stringWithUTF8String:education];
        
        [[[ALSdk shared] targetingData] setEducation:educationStr];
    }
    
    /**
     * Set the user's ethnicity for targeting
     *
     * @param string ethnicity    The current user's ethnicity
     */
    void _AppLovinSetEthnicity(const char * ethnicity)
    {
        if (!ethnicity) { return; };
        
        NSString * ethnicityStr = [NSString stringWithUTF8String:ethnicity];
        
        [[[ALSdk shared] targetingData] setEthnicity:ethnicityStr];
    }
    
    /**
     * Set the user's income for targeting
     *
     * @param string income    The current user's income
     */
    void _AppLovinSetIncome(const char * income)
    {
        if (!income) { return; };
        
        NSString * incomeStr = [NSString stringWithUTF8String:income];
        
        [[[ALSdk shared] targetingData] setIncome:incomeStr];
    }
    
    /**
     * Set the user's marital status for targeting
     *
     * @param string marital    The current user's marital status
     */
    void _AppLovinSetMaritalStatus(const char * marital)
    {
        if (!marital) { return; };
        
        NSString * maritalStr = [NSString stringWithUTF8String:marital];
        
        [[[ALSdk shared] targetingData] setMaritalStatus:maritalStr];
    }
    
    /**
     * Set the AppLovin SDK key for the application
     *
     * @param string sdkKey    The SDK key for the application
     */
    void _AppLovinSetSdkKey(const char * sdkKey)
    {
        if (!sdkKey) { return; };
        
        NSString * sdkKeyStr = [NSString stringWithUTF8String:sdkKey];
        
        NSDictionary* infoDict = [[NSBundle mainBundle] infoDictionary];
        [infoDict setValue:sdkKeyStr forKey:@"AppLovinSdkKey"];
    }
    
    /**
     * Set extra targeting parameters
     *
     * @param string key    The key for the parameter
     * @param string val    The value of the parameter
     */
    void _AppLovinPutExtra(const char * key, const char * val)
    {
        if (!key || !val) { return; };
        
        
        NSString * keyStr = [NSString stringWithUTF8String:key];
        NSString * valStr = [NSString stringWithUTF8String:val];
        
        [[[ALSdk shared] targetingData] setExtraValue:valStr forKey:keyStr];
    }
    
}
