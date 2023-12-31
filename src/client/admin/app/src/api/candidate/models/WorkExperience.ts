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
 * @interface WorkExperience
 */
export interface WorkExperience {
    /**
     * 
     * @type {string}
     * @memberof WorkExperience
     */
    companyName?: string;
    /**
     * 
     * @type {string}
     * @memberof WorkExperience
     */
    jobTitle?: string;
    /**
     * 
     * @type {string}
     * @memberof WorkExperience
     */
    employmentDates?: string;
    /**
     * 
     * @type {string}
     * @memberof WorkExperience
     */
    jobDescription?: string;
    /**
     * 
     * @type {string}
     * @memberof WorkExperience
     */
    accomplishments?: string;
    /**
     * 
     * @type {number}
     * @memberof WorkExperience
     */
    experienceFrom?: number;
    /**
     * 
     * @type {number}
     * @memberof WorkExperience
     */
    experienceTo?: number;
}

/**
 * Check if a given object implements the WorkExperience interface.
 */
export function instanceOfWorkExperience(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function WorkExperienceFromJSON(json: any): WorkExperience {
    return WorkExperienceFromJSONTyped(json, false);
}

export function WorkExperienceFromJSONTyped(json: any, ignoreDiscriminator: boolean): WorkExperience {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'companyName': !exists(json, 'companyName') ? undefined : json['companyName'],
        'jobTitle': !exists(json, 'jobTitle') ? undefined : json['jobTitle'],
        'employmentDates': !exists(json, 'employmentDates') ? undefined : json['employmentDates'],
        'jobDescription': !exists(json, 'jobDescription') ? undefined : json['jobDescription'],
        'accomplishments': !exists(json, 'accomplishments') ? undefined : json['accomplishments'],
        'experienceFrom': !exists(json, 'experienceFrom') ? undefined : json['experienceFrom'],
        'experienceTo': !exists(json, 'experienceTo') ? undefined : json['experienceTo'],
    };
}

export function WorkExperienceToJSON(value?: WorkExperience | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'companyName': value.companyName,
        'jobTitle': value.jobTitle,
        'employmentDates': value.employmentDates,
        'jobDescription': value.jobDescription,
        'accomplishments': value.accomplishments,
        'experienceFrom': value.experienceFrom,
        'experienceTo': value.experienceTo,
    };
}

