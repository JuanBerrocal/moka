import {api} from "@/core/api";
import type {MachineDto, RequestMachineDto} from "../types/machineDtos";


export async function getMachines(): Promise<MachineDto[]> {

    const response = await api.get<MachineDto[]>("/machines");
    return response.data; 
}

export async function getMachineById(id: number): Promise<MachineDto> {

    const response = await api.get<MachineDto>(`/machines/${id}`);
    return response.data; 
}

export async function createMachine(machine: RequestMachineDto): Promise<MachineDto> {

    const response = await api.post<MachineDto>("/machines", machine as MachineDto);
    return response.data; 
}

export async function updateMachine(id: number, machine: RequestMachineDto): Promise<MachineDto> {
    const response = await api.put<MachineDto>(`/machines/${id}`, machine);
    return response.data; 
}

export async function deleteMachine(id: number): Promise<void> {
    await api.delete(`/machines/${id}`);
}