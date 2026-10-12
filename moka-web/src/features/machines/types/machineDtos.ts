export interface MachineDto {
    id: number;
    model: string;
    serial: string;
    sapCode: string | null;
    asset: string;
    machineType: number;
    purchaseDate: string | null;
    state: number;
    storeId: number;
    assignment: string;
    isExternal: boolean;
    notes: string | null;
}

export interface RequestMachineDto {
    model: string;
    serial: string | null;
    sapCode: string | null;
    asset: string | null;
    machineType: number;
    purchaseDate: string | null;
    state: number;
    storeId: number;
    assignment: string | null;
    isExternal: boolean;
    notes: string | null;
}
