//
//  ALAdSize.h
//  sdk
//
//  Created by Basil on 2/27/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>


/**
 * This class defines a size of an ad to be displayed. It is recommended to use default sizes that are
 * declared in this class (<code>BANNER</code>, <code>INTERSTITIAL</code>)
 *  
 * @author Basil Shikin
 * @version 1.0
 */
@interface ALAdSize : NSObject<NSCopying> {
    NSUInteger width;
    NSUInteger height;
    NSString * label;
}

-(NSUInteger) width;
-(NSUInteger) height;
-(NSString *) label;

-(id)initWith: (NSString *)label;
-(id)initWith: (NSUInteger)width by:(NSUInteger)height;


+(ALAdSize *) sizeBanner;

+(ALAdSize *) sizeInterstitial;

+(ALAdSize *) sizeMRec;

+(ALAdSize *) sizeLeader;

+(NSArray *) allSizes;

@end
