//
//  AdMobBinding.m
//  AdMobTest
//
//  Created by Mike on 1/27/11.
//  Copyright 2011 Prime31 Studios. All rights reserved.
//

#import "AdMobManager.h"


// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]


// Sets the publiser Id and prepares AdMob for action.  Must be called before any other methods!
void _adMobInit( const char * publisherId, BOOL isTesting )
{
	[AdMobManager sharedManager].publisherId = GetStringParam( publisherId );
	if( isTesting )
		[AdMobManager sharedManager].isTesting = isTesting;
}


void _adMobSetTestDevice( const char * deviceId )
{
	[[AdMobManager sharedManager].testDevices addObject:GetStringParam( deviceId )];
}


void _adMobCreateBanner( int bannerType, int bannerPosition )
{
	AdMobBannerType type = (AdMobBannerType)bannerType;
	AdMobAdPosition position = (AdMobAdPosition)bannerPosition;
	
	[[AdMobManager sharedManager] createBanner:type withPosition:position];
}


// Destroys the banner and removes it from view
void _adMobDestroyBanner()
{
	[[AdMobManager sharedManager] destroyBanner];
}


// Starts loading an interstitial ad
void _adMobRequestInterstitalAd( const char * interstitialUnitId )
{
	[[AdMobManager sharedManager] requestInterstitalAd:GetStringParam( interstitialUnitId )];
}


// Checks to see if the interstitial ad is loaded and ready to show
bool _adMobIsInterstitialAdReady()
{
	return [AdMobManager sharedManager].interstitialAd.isReady;
}


// If an interstitial ad is loaded this will take over the screen and show the ad
void _adMobShowInterstitialAd()
{
	[[AdMobManager sharedManager] showInterstitialAd];
}

