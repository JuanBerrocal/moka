
import {useEffect, useState} from "react";
import {Link, useParams, useNavigate} from "react-router-dom";
import axios from "axios";

import {createMachine, updateMachine} from "@/features/machines";
import type {MachineDto} from "@/features/machines/types/machineDtos";
import type {MachineFormData} from "@/features/machines";
import {MachineForm} from "@/features/machines";
import {getMachineById} from "@/features/machines/api/machinesApi";
import {toRequestMachineDto} from "@/features/machines/mappers/machineMappers";

export const MachineDetailScene = () => {

    const [machine, setMachine] = useState<MachineDto | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);


    const navigate = useNavigate();
    const {id} = useParams<{id: string}>();
    const isEditting = Boolean(id);

    const emptyMachineValues: MachineFormData = {
        model: "",
        serial: "",
        sapCode: null,
        asset: "",
        machineType: 0,
        purchaseDate: null,
        state: 0,
        storeId: 0,
        assignment: "",
        isExternal: false,
        notes: null
    };

    const loadMachine = async (id: number): Promise<MachineDto | null> => {
        try {
            const machine = await getMachineById(id);
            return machine;
        }
        catch (error) {
            console.error("Error loading machine: ", error);
            setError("Error loading the machine");
            return null;
        }
    }

    useEffect(() => {
        const load = async () => {
            if (isEditting && id) {
                await loadMachine(Number(id)).then((machine) => {
                    if (machine) {
                        setMachine({...machine});
                    }
                });
            }
        };
        load();
    }, [id, isEditting]);

    const onSubmit = async (values: MachineFormData) => {
        try {
                setIsSubmitting(true);
                setError(null);
                if (isEditting) {
                    // Update machine logic here
                    const updatedMachine = await updateMachine(Number(id), toRequestMachineDto(values));
                    console.log("Machine updated successfully:", updatedMachine);
                    navigate("/machines", {state: {message: "Machine updated successfully"}});
                }
                else {
                    const newMachine = await createMachine(toRequestMachineDto(values));
                    console.log("Machine created successfully:", newMachine);
                    navigate("/machines", {state: {message: "Machine created successfully"}});
                }
            }
        catch (error) {
            if (error instanceof Error) {
                console.error("Error of input value. " + error.message);
                setError("Error of input value. " + error.message);
            }
            else if (axios.isAxiosError(error)) {
                console.error("Error submitting machine. Axios error:", error.response?.data);
                setError(error.response?.data?.message || "An error occurred while saving the store.");
            }
        }
        finally {
            setIsSubmitting(false);
        }
    }

    const buildBody = () => {
        if (error) {
            return <div>Error: {error}</div>;
        }
        if (!machine) {
            return <div>Machine loading...</div>;
        }
        
        return (<MachineForm initialValues = {machine} onSubmit = {onSubmit} isSubmitting = {isSubmitting}/>);
    }

    if (!isEditting) {

        return (
            <>
            <div>Creating machine</div>
            <Link to ="/machines">Back to machines</Link>
            { error &&  <div>{error}</div> }
            <MachineForm initialValues = {emptyMachineValues} onSubmit = {onSubmit} isSubmitting = {isSubmitting}/>
            </>
        );
    }

    return (
    <>
        <div>Machine Detail</div>
        <Link to="/machines">Back to machines</Link>
        { error &&  <div>{error}</div> }
        {buildBody()}
    </>);
}