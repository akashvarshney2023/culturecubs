/* tslint:disable */
/* eslint-disable */
/**
 * OpenAPI definition
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface ContactInformation
 */
export interface ContactInformation {
    /**
     * 
     * @type {string}
     * @memberof ContactInformation
     */
    email?: string;
    /**
     * 
     * @type {string}
     * @memberof ContactInformation
     */
    phoneNumber?: string;
    /**
     * 
     * @type {string}
     * @memberof ContactInformation
     */
    address?: string;
    /**
     * 
     * @type {string}
     * @memberof ContactInformation
     */
    nationality?: string;
    /**
     * 
     * @type {string}
     * @memberof ContactInformation
     */
    workAuthorizationStatus?: string;
    /**
     * 
     * @type {string}
     * @memberof ContactInformation
     */
    gender?: string;
    /**
     * 
     * @type {string}
     * @memberof ContactInformation
     */
    dateOfBirth?: string;
}

/**
 * Check if a given object implements the ContactInformation interface.
 */
export function instanceOfContactInformation(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function ContactInformationFromJSON(json: any): ContactInformation {
    return ContactInformationFromJSONTyped(json, false);
}

export function ContactInformationFromJSONTyped(json: any, ignoreDiscriminator: boolean): ContactInformation {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'email': !exists(json, 'email') ? undefined : json['email'],
        'phoneNumber': !exists(json, 'phoneNumber') ? undefined : json['phoneNumber'],
        'address': !exists(json, 'address') ? undefined : json['address'],
        'nationality': !exists(json, 'nationality') ? undefined : json['nationality'],
        'workAuthorizationStatus': !exists(json, 'workAuthorizationStatus') ? undefined : json['workAuthorizationStatus'],
        'gender': !exists(json, 'gender') ? undefined : json['gender'],
        'dateOfBirth': !exists(json, 'dateOfBirth') ? undefined : json['dateOfBirth'],
    };
}

export function ContactInformationToJSON(value?: ContactInformation | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'email': value.email,
        'phoneNumber': value.phoneNumber,
        'address': value.address,
        'nationality': value.nationality,
        'workAuthorizationStatus': value.workAuthorizationStatus,
        'gender': value.gender,
        'dateOfBirth': value.dateOfBirth,
    };
}
