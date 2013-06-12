//
//  VungleManager.m
//  VungleTest
//
//  Created by Mike Desaro on 6/5/12.
//  Copyright (c) 2012 prime31. All rights reserved.
//

#import "VungleManager.h"
#import "JSONKit.h"
#import <objc/runtime.h>


void UnityPause( bool pause );

UIViewController *UnityGetGLViewController();

void UnitySendMessage( const char * className, const char * methodName, const char * param );


@implementation VungleManager

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark Class Methods

+ (VungleManager*)sharedManager
{
	static VungleManager *sharedSingleton;
	
	if( !sharedSingleton )
		sharedSingleton = [[VungleManager alloc] init];
	
	return sharedSingleton;
}


+ (NSArray*)propertyNames:(Class)klass
{
	NSMutableArray *propertyNames = [[NSMutableArray alloc] init];
	unsigned int propertyCount = 0;
	objc_property_t *properties = class_copyPropertyList( klass, &propertyCount );
	
	for( unsigned int i = 0; i < propertyCount; ++i )
	{
		objc_property_t property = properties[i];
		const char * name = property_getName(property);
		
		[propertyNames addObject:[NSString stringWithUTF8String:name]];
	}
	free( properties );
	
	return [propertyNames autorelease];
}


+ (id)objectFromJson:(NSString*)json
{
	return [json objectFromJSONString];
}


+ (NSString*)jsonFromObject:(id)object
{
    return [object JSONString];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Public

- (void)startWithAppId:(NSString*)appId
{
	[VGVunglePub startWithPubAppID:appId];
	[VGVunglePub setDelegate:self];
}


- (void)startWithAppId:(NSString*)appId userDataString:(NSString*)userDataString
{
	[self startWithAppId:appId userData:[VungleManager objectFromJson:userDataString]];
}


- (void)startWithAppId:(NSString*)appId userData:(NSDictionary*)dict
{
	VGUserData *data = [[VGUserData alloc] init];

	NSArray *properties = [VungleManager propertyNames:[VGUserData class]];
	for( NSString *prop in properties )
	{
		if( [[dict allKeys] containsObject:prop] )
		{
			[data setValue:[dict objectForKey:prop] forKey:prop];
		}
	}

	[VGVunglePub startWithPubAppID:appId userData:data];
	[VGVunglePub setDelegate:self];
}


- (void)stop
{
	[VGVunglePub stop];
}


- (void)playModalAdShowingCloseButton:(BOOL)showCloseButton
{
	[VGVunglePub playModalAd:UnityGetGLViewController() animated:YES showClose:showCloseButton];
}


- (void)playIncentivizedAdWithUserTag:(NSString*)user showCloseButton:(BOOL)showCloseButton
{
	[VGVunglePub playIncentivizedAd:UnityGetGLViewController() animated:YES showClose:showCloseButton userTag:user];
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - VGVunglePubDelegate

- (void)vungleMoviePlayed:(VGPlayData*)playData
{
	UnitySendMessage( "VungleManager", "vungleMoviePlayed", [VungleManager jsonFromObject:[playData JSONData]].UTF8String );
}


- (void)vungleStatusUpdate:(VGStatusData*)statusData
{
	NSDictionary *dict = [NSDictionary dictionaryWithObjectsAndKeys:[statusData statusString], @"status",
						  [NSNumber numberWithInt:statusData.videoAdsCached], @"videoAdsCached",
						  [NSNumber numberWithInt:statusData.videoAdsUnviewed], @"videoAdsUnviewed", nil];
	
	UnitySendMessage( "VungleManager", "vungleStatusUpdate", [VungleManager jsonFromObject:dict].UTF8String );
}


- (void)vungleViewDidDisappear:(UIViewController*)viewController
{
	UnityPause( false );
	UnitySendMessage( "VungleManager", "vungleViewDidDisappear", "" );
}


- (void)vungleViewWillAppear:(UIViewController*)viewController
{
	UnityPause( true );
	UnitySendMessage( "VungleManager", "vungleViewWillAppear", "" );
}


@end
