/* tslint:disable */
/* eslint-disable */
/**
 * CCubAPI
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
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
 * @interface User
 */
export interface User {
    /**
     * 
     * @type {number}
     * @memberof User
     */
    userId?: number;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    userName?: string | null;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    password?: string | null;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    location?: string | null;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    userSkillSet?: string | null;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    emailId?: string | null;
    /**
     * 
     * @type {string}
     * @memberof User
     */
    contact?: string | null;
    /**
     * 
     * @type {number}
     * @memberof User
     */
    tenantId?: number;
    /**
     * 
     * @type {number}
     * @memberof User
     */
    roleId?: number | null;
    /**
     * 
     * @type {Date}
     * @memberof User
     */
    createdDate?: Date;
    /**
     * 
     * @type {Date}
     * @memberof User
     */
    updatedDate?: Date | null;
}

/**
 * Check if a given object implements the User interface.
 */
export function instanceOfUser(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function UserFromJSON(json: any): User {
    return UserFromJSONTyped(json, false);
}

export function UserFromJSONTyped(json: any, ignoreDiscriminator: boolean): User {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'userId': !exists(json, 'userId') ? undefined : json['userId'],
        'userName': !exists(json, 'userName') ? undefined : json['userName'],
        'password': !exists(json, 'password') ? undefined : json['password'],
        'location': !exists(json, 'location') ? undefined : json['location'],
        'userSkillSet': !exists(json, 'userSkillSet') ? undefined : json['userSkillSet'],
        'emailId': !exists(json, 'emailId') ? undefined : json['emailId'],
        'contact': !exists(json, 'contact') ? undefined : json['contact'],
        'tenantId': !exists(json, 'tenantId') ? undefined : json['tenantId'],
        'roleId': !exists(json, 'roleId') ? undefined : json['roleId'],
        'createdDate': !exists(json, 'createdDate') ? undefined : (new Date(json['createdDate'])),
        'updatedDate': !exists(json, 'updatedDate') ? undefined : (json['updatedDate'] === null ? null : new Date(json['updatedDate'])),
    };
}

export function UserToJSON(value?: User | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'userId': value.userId,
        'userName': value.userName,
        'password': value.password,
        'location': value.location,
        'userSkillSet': value.userSkillSet,
        'emailId': value.emailId,
        'contact': value.contact,
        'tenantId': value.tenantId,
        'roleId': value.roleId,
        'createdDate': value.createdDate === undefined ? undefined : (value.createdDate.toISOString()),
        'updatedDate': value.updatedDate === undefined ? undefined : (value.updatedDate === null ? null : value.updatedDate.toISOString()),
    };
}
