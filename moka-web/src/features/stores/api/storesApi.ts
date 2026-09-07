import {api} from "@/core/api";
import type {StoreApiResponse, StoreDto} from "./../types/StoreDto";
import type {StoreFormData} from "../components/StoreFormData";

export async function getStores(page: number, pageSize: number): Promise<StoreApiResponse> {

    const response = await api.get<StoreApiResponse>("/stores", {params: { page, pageSize}});
    return response.data; 
}

export async function createStore(store: StoreFormData): Promise<StoreDto> {

    const response = await api.post<StoreDto>("/stores", store);
    return response.data; 
}