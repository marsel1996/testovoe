import { AxiosRequestConfig } from "axios";

export class TestApiBase {
    private baseApiUrl: string;

    constructor() {
        this.baseApiUrl = 'https://localhost:44347' ?? '';
    }

    protected async transformOptions(options: AxiosRequestConfig): Promise<AxiosRequestConfig> {
        return Promise.resolve(options);
    }
    
    getBaseUrl(defaultUrl: string, baseUrl?: string) : string {
        return this.baseApiUrl;
    }
}