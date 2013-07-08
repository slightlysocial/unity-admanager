//
//  ALInterstitialAd.h
//  sdk
//
//  Created by Basil on 3/22/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//
#import <UIKit/UIKit.h>
#import "ALSdk.h"
#import "ALAdService.h"

@interface ALInterstitialAd : UIView

@property (strong, atomic) id<ALAdLoadDelegate> adLoadDelegate;
@property (strong, atomic) id<ALAdDisplayDelegate> adDisplayDelegate;


/**
 * Show current interstitial over a given window. This method will show an
 * interstitial and load the next into it.
 *
 * @param window An instance of window to show the interstitial over.
 */
-(void) showOver:(UIWindow *)window;


/**
 * Show current interstitial over a given window and render a specified ad.
 *
 * @param window An instance of window to show the interstitial over.
 * @param ad     The ALAd that you want to render into this interstitial.
 */
-(void) showOver:(UIWindow *)window andRender: (ALAd *) ad;

/**
 * Dismiss current dialog
 */
- (void)dismiss;

/**
 * Init this interstitial ad with a given AppLovin SDK
 *
 * @param sdk    Instance of AppLovin SDK to use.
 */
-(id)initInterstitialAdWithSdk: (ALSdk *)anSdk;

/**
 * Init this interstitial ad with given frame and AppLovin SDK
 *
 * @param frame  Initial frame of the interstitial
 * @param sdk    Instance of AppLovin SDK to use.
 */
- (id)initWithFrame:(CGRect)frame sdk: (ALSdk *) anSdk;

/**
 * Show a new interstitial ad. This method would display a dialog on top of current
 * view with an advertisement in it.
 *
 * @param window  A window to show the interstitial over
 */
+(ALInterstitialAd *) showOver:(UIWindow *)window;

/**
 * Get shared interstitial view
 */
+(ALInterstitialAd *) shared;

@end
