
export interface StoreDto{
    id: number;
    name: string;
    sapCode?: string;
    tradeName?: string;
    address?: string;
    postalCode?: string;
    city?: string;
    taxId?: string;
    notes?: string;

}

export interface StoreApiResponse {
    totalItems: number;
    page: number;
    pageSize: number;
    items: StoreDto[];
}
 
export interface CreateStoreDto{
    name: string;
    sapCode?: string;
    tradeName?: string;
    address?: string;
    postalCode?: string;
    city?: string;
    taxId?: string;
    notes?: string;
}
 
export interface UpdateStoreDto{
    name: string;
    sapCode?: string;
    tradeName?: string;
    address?: string;
    postalCode?: string;
    city?: string;
    taxId?: string;
    notes?: string;
}