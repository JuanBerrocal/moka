export interface MachineFormData {
    model: string;
    serial: string;
    sapCode: string | null;
    asset: string;
    type: number;
    purchaseDate: string | null;
    state: number;
    storeId: number;
    assignment: string;
    isExternal: boolean;
    notes: string | null;
}