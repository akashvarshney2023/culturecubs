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


import * as runtime from '../runtime';
import type {
  Tenant,
} from '../models';
import {
    TenantFromJSON,
    TenantToJSON,
} from '../models';

export interface ApiTenantsAddtenantPostRequest {
    tenant?: Tenant;
}

export interface ApiTenantsTenantGetRequest {
    id?: number;
}

/**
 * TenantsApi - interface
 * 
 * @export
 * @interface TenantsApiInterface
 */
export interface TenantsApiInterface {
    /**
     * 
     * @param {Tenant} [tenant] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TenantsApiInterface
     */
    apiTenantsAddtenantPostRaw(requestParameters: ApiTenantsAddtenantPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<void>>;

    /**
     */
    apiTenantsAddtenantPost(requestParameters: ApiTenantsAddtenantPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<void>;

    /**
     * 
     * @param {number} [id] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof TenantsApiInterface
     */
    apiTenantsTenantGetRaw(requestParameters: ApiTenantsTenantGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Tenant>>;

    /**
     */
    apiTenantsTenantGet(requestParameters: ApiTenantsTenantGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Tenant>;

}

/**
 * 
 */
export class TenantsApi extends runtime.BaseAPI implements TenantsApiInterface {

    /**
     */
    async apiTenantsAddtenantPostRaw(requestParameters: ApiTenantsAddtenantPostRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<void>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        const response = await this.request({
            path: `/api/Tenants/addtenant`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: TenantToJSON(requestParameters.tenant),
        }, initOverrides);

        return new runtime.VoidApiResponse(response);
    }

    /**
     */
    async apiTenantsAddtenantPost(requestParameters: ApiTenantsAddtenantPostRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<void> {
        await this.apiTenantsAddtenantPostRaw(requestParameters, initOverrides);
    }

    /**
     */
    async apiTenantsTenantGetRaw(requestParameters: ApiTenantsTenantGetRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Tenant>> {
        const queryParameters: any = {};

        if (requestParameters.id !== undefined) {
            queryParameters['id'] = requestParameters.id;
        }

        const headerParameters: runtime.HTTPHeaders = {};

        const response = await this.request({
            path: `/api/Tenants/tenant`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => TenantFromJSON(jsonValue));
    }

    /**
     */
    async apiTenantsTenantGet(requestParameters: ApiTenantsTenantGetRequest = {}, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Tenant> {
        const response = await this.apiTenantsTenantGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

}
