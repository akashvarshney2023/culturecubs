/* tslint:disable */
/* eslint-disable */
/**
 * OpenAPI Document on Azure Functions
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
import type { ContestTab } from './ContestTab';
import {
    ContestTabFromJSON,
    ContestTabFromJSONTyped,
    ContestTabToJSON,
} from './ContestTab';

/**
 * 
 * @export
 * @interface Contest
 */
export interface Contest {
    /**
     * 
     * @type {number}
     * @memberof Contest
     */
    id?: number;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    tenantId?: string;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    title?: string;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    description?: string;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    category?: string;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    location?: string;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    experience?: string;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    image?: string;
    /**
     * 
     * @type {Date}
     * @memberof Contest
     */
    registrationEndDate?: Date;
    /**
     * 
     * @type {string}
     * @memberof Contest
     */
    summary?: string;
    /**
     * 
     * @type {Array<ContestTab>}
     * @memberof Contest
     */
    tabs?: Array<ContestTab>;
}

/**
 * Check if a given object implements the Contest interface.
 */
export function instanceOfContest(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function ContestFromJSON(json: any): Contest {
    return ContestFromJSONTyped(json, false);
}

export function ContestFromJSONTyped(json: any, ignoreDiscriminator: boolean): Contest {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'id': !exists(json, 'id') ? undefined : json['id'],
        'tenantId': !exists(json, 'tenantId') ? undefined : json['tenantId'],
        'title': !exists(json, 'title') ? undefined : json['title'],
        'description': !exists(json, 'description') ? undefined : json['description'],
        'category': !exists(json, 'category') ? undefined : json['category'],
        'location': !exists(json, 'location') ? undefined : json['location'],
        'experience': !exists(json, 'experience') ? undefined : json['experience'],
        'image': !exists(json, 'image') ? undefined : json['image'],
        'registrationEndDate': !exists(json, 'registrationEndDate') ? undefined : (new Date(json['registrationEndDate'])),
        'summary': !exists(json, 'summary') ? undefined : json['summary'],
        'tabs': !exists(json, 'tabs') ? undefined : ((json['tabs'] as Array<any>).map(ContestTabFromJSON)),
    };
}

export function ContestToJSON(value?: Contest | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'id': value.id,
        'tenantId': value.tenantId,
        'title': value.title,
        'description': value.description,
        'category': value.category,
        'location': value.location,
        'experience': value.experience,
        'image': value.image,
        'registrationEndDate': value.registrationEndDate === undefined ? undefined : (value.registrationEndDate.toISOString()),
        'summary': value.summary,
        'tabs': value.tabs === undefined ? undefined : ((value.tabs as Array<any>).map(ContestTabToJSON)),
    };
}

