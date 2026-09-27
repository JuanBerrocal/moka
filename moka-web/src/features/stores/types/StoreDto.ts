
export interface StoreDto {
    id: number;
    name: string;
    sapCode: string | null;
    tradeName: string | null;
    address: string | null;
    postalCode: string | null;
    city: string | null;
    taxId: string | null;
    notes: string | null;

}

export interface StoreApiResponse {
    totalItems: number;
    page: number;
    pageSize: number;
    items: StoreDto[];
}
 
export interface CreateStoreDto{
    name: string;
    sapCode: string | null;
    tradeName: string | null;
    address: string | null;
    postalCode: string | null;
    city: string | null;
    taxId: string | null;
    notes: string | null;
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