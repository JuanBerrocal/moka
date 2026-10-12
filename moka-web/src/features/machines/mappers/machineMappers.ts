
import type {MachineFormData} from '../components/MachineFormData';
import type {RequestMachineDto} from '../types/machineDtos';

export const toRequestMachineDto = (machineFormData: MachineFormData): RequestMachineDto => {
    return {
        model: machineFormData.model,
        serial: machineFormData.serial || null,
        sapCode: machineFormData.sapCode || null,
        asset: machineFormData.asset || null,
        machineType: parseNumber(machineFormData.machineType),
        purchaseDate: machineFormData.purchaseDate || null,
        state: parseNumber(machineFormData.state),
        storeId: parseNumber(machineFormData.storeId),
        assignment: machineFormData.assignment || null,
        isExternal: Boolean(machineFormData.isExternal),
        notes: machineFormData.notes || null,
    };
}   

export const parseNumber = (value:string | number | null): number => {

    if (value === null || value === undefined) {
        throw new Error("Value cannot be null, undefined, or empty");
    }
    if (typeof value === "string" && value.trim() === "") {
        throw new Error("Value cannot be empty");
    }
    
    const parsed = Number(value);

    if (isNaN(parsed)) {
        throw new Error(`Value "${value}" is not a valid number`);
    }
    return parsed;
}