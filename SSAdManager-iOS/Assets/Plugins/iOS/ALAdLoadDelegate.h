//
//  ALAdLoadDelegate.h
//  sdk
//
//  Created by Basil on 3/23/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "ALAd.h"

@class ALAdService;

@protocol ALAdLoadDelegate <NSObject>

-(void)adService:(ALAdService *)adService didLoadAd:(ALAd *)ad;

-(void)adService:(ALAdService *)adService didFailToLoadAdWithError:(int)code;

@end
