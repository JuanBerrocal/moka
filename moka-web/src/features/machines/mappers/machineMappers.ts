
import type {MachineFormData} from '../components/MachineFormData';
import type {RequestMachineDto} from '../types/machineDtos';

export const toRequestMachineDto = (machineFormData: MachineFormData): RequestMachineDto => {
    return {
        model: machineFormData.model,
        serial: machineFormData.serial || null,
        sapCode: machineFormData.sapCode || null,
        asset: machineFormData.asset || null,
        type: parseNumber(machineFormData.type, "machineType"),
        purchaseDate: machineFormData.purchaseDate || null,
        state: parseNumber(machineFormData.state, "state"),
        storeId: parseNumber(machineFormData.storeId, "storeId"),
        assignment: machineFormData.assignment || null,
        isExternal: Boolean(machineFormData.isExternal),
        notes: machineFormData.notes || null,
    };
}   

export const parseNumber = (value:string | number | null, fieldName: string): number => {

    if (value === null || value === undefined) {
        throw new Error(`Value for field "${fieldName}" cannot be null, undefined, or empty`);
    }
    if (typeof value === "string" && value.trim() === "") {
        throw new Error(`Value for field "${fieldName}" cannot be empty`);  
    }
    
    const parsed = Number(value);

    if (isNaN(parsed)) {
        throw new Error(`Value "${value}" for field "${fieldName}" is not a valid number`);
    }
    if (parsed < 0 ) {
        throw new Error(`Value "${value}" for field "${fieldName}" cannot be negative`);
    }
    return parsed;
}