export interface MachineDto {
    id: number;
    model: string;
    serial: string;
    sapCode: string | null;
    asset: string;
    machineType: number;
    purchaseDate: string | null;
    state: number;
    storeId: number | null;
    assignment: string;
    isExternal: boolean;
    notes: string | null;
}