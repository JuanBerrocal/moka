import {api} from "@/core/api";
import type {MachineDto} from "../types/MachineDto";


export async function getMachines(): Promise<MachineDto[]> {

    const response = await api.get<MachineDto[]>("/machines");
    return response.data; 
}