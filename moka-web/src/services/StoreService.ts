import {api} from "../api/api";
import type {StoreApiResponse} from "../types/StoreDto";

export async function getStores(page: number, pageSize: number): Promise<StoreApiResponse> {

    const response = await api.get<StoreApiResponse>("/stores", {params: { page, pageSize}});
    return response.data; 
}