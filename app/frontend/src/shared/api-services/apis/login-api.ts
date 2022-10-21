/* tslint:disable */
/* eslint-disable */
/**
 * 规范化接口演示
 * 让 .NET 开发更简单，更通用，更流行。
 *
 * OpenAPI spec version: 1.0.0
 * Contact: monksoul@outlook.com
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import globalAxios, { AxiosResponse, AxiosInstance, AxiosRequestConfig } from 'axios';
import { Configuration } from '../configuration';
// Some imports not used depending on template conditions
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError } from '../base';
import { LoginInput } from '../models';
import { RESTfulResultPublicUser } from '../models';
/**
 * LoginApi - axios parameter creator
 * @export
 */
export const LoginApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @summary 登录
         * @param {LoginInput} [body] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiLoginPost: async (body?: LoginInput, options: AxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/login`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, 'https://example.com');
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }
            const localVarRequestOptions :AxiosRequestConfig = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            // authentication Bearer required

            localVarHeaderParameter['Content-Type'] = 'application/json';

            const query = new URLSearchParams(localVarUrlObj.search);
            for (const key in localVarQueryParameter) {
                query.set(key, localVarQueryParameter[key]);
            }
            for (const key in options.params) {
                query.set(key, options.params[key]);
            }
            localVarUrlObj.search = (new URLSearchParams(query)).toString();
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            const needsSerialization = (typeof body !== "string") || localVarRequestOptions.headers['Content-Type'] === 'application/json';
            localVarRequestOptions.data =  needsSerialization ? JSON.stringify(body !== undefined ? body : {}) : (body || "");

            return {
                url: localVarUrlObj.pathname + localVarUrlObj.search + localVarUrlObj.hash,
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * LoginApi - functional programming interface
 * @export
 */
export const LoginApiFp = function(configuration?: Configuration) {
    return {
        /**
         * 
         * @summary 登录
         * @param {LoginInput} [body] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiLoginPost(body?: LoginInput, options?: AxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => Promise<AxiosResponse<RESTfulResultPublicUser>>> {
            const localVarAxiosArgs = await LoginApiAxiosParamCreator(configuration).apiLoginPost(body, options);
            return (axios: AxiosInstance = globalAxios, basePath: string = BASE_PATH) => {
                const axiosRequestArgs :AxiosRequestConfig = {...localVarAxiosArgs.options, url: basePath + localVarAxiosArgs.url};
                return axios.request(axiosRequestArgs);
            };
        },
    }
};

/**
 * LoginApi - factory interface
 * @export
 */
export const LoginApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    return {
        /**
         * 
         * @summary 登录
         * @param {LoginInput} [body] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiLoginPost(body?: LoginInput, options?: AxiosRequestConfig): Promise<AxiosResponse<RESTfulResultPublicUser>> {
            return LoginApiFp(configuration).apiLoginPost(body, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * LoginApi - object-oriented interface
 * @export
 * @class LoginApi
 * @extends {BaseAPI}
 */
export class LoginApi extends BaseAPI {
    /**
     * 
     * @summary 登录
     * @param {LoginInput} [body] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof LoginApi
     */
    public async apiLoginPost(body?: LoginInput, options?: AxiosRequestConfig) : Promise<AxiosResponse<RESTfulResultPublicUser>> {
        return LoginApiFp(this.configuration).apiLoginPost(body, options).then((request) => request(this.axios, this.basePath));
    }
}