//
//  AppLovinAd.h
//  sdk
//
//  Created by Basil on 2/27/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "ALAdSize.h"

#import "ALAdSize.h"

/**
 * This class represents an ad that has been served from AppLovin server and
 * should be displayed to the user.
 *
 * @author Basil Shikin
 * @version 1.0
 */
@interface ALAd : NSObject

@property (strong, nonatomic) NSString * html;
@property (strong, nonatomic) ALAdSize * size;
@property (strong, nonatomic) NSString * destinationUrl;
@property (strong, nonatomic) NSString * clickTrackerUrl;

@end
