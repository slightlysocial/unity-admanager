//
//  ALTargetingData.h
//  sdk
//
//  Created by Basil on 9/18/12.
//
//

#import <Foundation/Foundation.h>

@interface ALTargetingData : NSObject

#define kALGenderMale        'm'
#define kALGenderFemale      'f'

#define kALEducationNone              @"none";
#define kALEducationHighSchool        @"high_school";
#define kALEducationCollege           @"college";
#define kALEducationInCollege         @"in_college";
#define kALEducationBachelors         @"bachelors";
#define kALEducationMasters           @"masters";
#define kALEducationDoctoral          @"doctoral";
#define kALEducationOther             @"other";

#define kALEthnicityNone             @"none";
#define kALEthnicityMixed            @"mixed";
#define kALEthnicityAsian            @"asian";
#define kALEthnicityBlack            @"black";
#define kALEthnicityHispanic         @"hispanic";
#define kALEthnicityNativeAmerican   @"native_american";
#define kALEthnicityWhite            @"white";
#define kALEthnicityOther            @"other";

#define kALMaritalStatusNone        @"none";
#define kALMaritalStatusSingle      @"single";
#define kALMaritalStatusMarried     @"married";
#define kALMaritalStatusDivorced    @"divorced";
#define kALMaritalStatusWidowed     @"widowed";


/**
 * Set carrier current device is on.
 */
@property(strong, nonatomic) NSString * carrier;

/**
 * Set a two-character ISO 3166-1 country code of the device.
 */
@property(strong, nonatomic) NSString * country;

/**
 * Phone number of the current user. Phone number should be in E.164 format. For
 * example: +15551234567.
 *
 * <p>
 *     <b>Please note:</b> Raw phone number will not be transferred over the
 *     network. AppLovin SDK will hash the value before using it.
 * </p>
 *
 * @param phoneNumber Raw phone number of current user.
 */
@property(strong, nonatomic) NSString * phoneNumber;

/**
 * Email of the current user.
 *
 * <p>
 *     <b>Please note:</b> Raw value will not be transferred over the network.
 *     AppLovin SDK will hash the value before using it.
 * </p>
 */
@property(strong, nonatomic) NSString * email;

/**
 * All known emails of the current user.
 *
 * <p>
 *     <b>Please note:</b> Raw values will not be transferred over the network.
 *     AppLovin SDK will hash all the values before using it.
 * </p>
 */
@property(strong, nonatomic) NSArray * emails;

/**
 * Hashed phone number of the current user.
 *
 * <p>
 * <b>Please note:</b>  Phone number should be normalized to E.164, hashed
 * with SHA-1 function and converted to HEX string. For example,
 * '555-123-3512' would be normalized to '+15551233512' and would result in
 * '0ffdf5e4d6bf07163edd87e94ead4ed7ee38ba21'.
 * </p>
 *
 * @param email Hashed email of the current user.
 */
@property(strong, nonatomic) NSString * hashedPhoneNumber;

/**
 * Hashed email of the current user.
 *
 * <p>
 * <b>Please note:</b> Emails should be lowercased, hashed with SHA-1 function and
 * converted to HEX string. For example, 'TestEmail@test3.nz' would result in
 * '07e775b970a44530a12ef3729a6f49da9e5f0229'
 * </p>
 *
 * @param email Hashed email of the current user.
 */
@property(strong, nonatomic) NSString * hashedEmail;

/**
 * Set all known hashed emails of the current user.
 *
 * <p>
 * <b>Please note:</b> Emails should be lowercased, hashed with SHA-1 function and
 * converted to HEX string. For example, 'TestEmail@test3.nz' would result in
 * '07e775b970a44530a12ef3729a6f49da9e5f0229'
 * </p>
 *
 * @param emails Raw emails of current user.
 */
@property(strong, nonatomic) NSArray * hashedEmails;

/**
 * First name of current user.
 */
@property(strong, nonatomic) NSString * firstName;

/**
 * Set the year of birth of current user.
 */
@property(assign, nonatomic) UInt16 birthYear;

/**
 * Gender of the  current user. 
 * <p>
 * Following constants contain supported values: <code>kALGenderMale</code> and
 * <code>kALGenderFemale</code>.
 */
@property(assign, nonatomic) char gender;

/**
 * The language of the current user. Language is expressed as two-character
 * ISO 639-1 language code.
 */
@property(strong, nonatomic) NSString * language;

/**
 * Income of the current user. Income format is could be as follows:
 * <ul>
 *      <li> An exact number like 100000 or 100K
 *      <li> A range like 10K-30K
 *      <li> A range like <100K
 * </ul>
 */
@property(strong, nonatomic) NSString * income;

/**
 * Highest level of education of the current user. 
 * <p>
 * Following constants contain supported values: <code>kALEducationNone, 
 * kALEducationHighSchool, kALEducationCollege, kALEducationInCollege, kALEducationBachelors,
 * kALEducationMasters, kALEducationDoctoral</code> and <code>kALEducationOther</code>.
 */
@property(strong, nonatomic) NSString * education;

/**
 * Highest level of education of the current user. 
 * <p>
 * Following constants contain supported values: kALEthnicityNone, kALEthnicityMixed,
 * kALEthnicityAsian, kALEthnicityBlack, kALEthnicityHispanic, kALEthnicityNativeAmerican, 
 * kALEthnicityWhite</code> and <code>kALEthnicityOther</code>.
 */
@property(strong, nonatomic) NSString * ethnicity;


/**
 * Martical status of the current user. 
 * <p>
 * Following constants contain supported values: <code>kALMaritalStatusNone,
 * kALMaritalStatusSingle, kALMaritalStatusMarried, kALMaritalStatusDivorced</code>
 * and <code>kALMaritalStatusWidowed</code>.
 */
@property(strong, nonatomic) NSString * maritalStatus;


/**
 * Keywords for the application.
 */
@property(strong, nonatomic) NSArray * keywords;

/**
 * Interests for the user.
 */
@property(strong, nonatomic) NSArray * interests;

/**
 * Set the location of current user. The location represented as
 * longiture and latitude
 */
-(void) setLocationWithLatitude: (double) latitude longitude: (double)longitude;

/**
 * Put an extra targeting parameter
 *
 * @param key Key of the parameter. Must not be null.
 * @param value Parameter value.
 */
-(void) setExtraValue: (NSString *) value forKey: (NSString *)key;

/**
 * Clear all saved targeting data
 */
-(void) clearAll;

@end
