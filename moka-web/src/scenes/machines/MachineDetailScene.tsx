
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

    const [machineST, setMachineST] = useState<MachineDto | null>(null);
    const [errorST, setErrorST] = useState<string | null>(null);
    const [isSubmittingST, setIsSubmittingST] = useState(false);


    const navigate = useNavigate();
    const {id} = useParams<{id: string}>();
    const isEditting = Boolean(id);

    const emptyMachineValues: MachineFormData = {
        model: "",
        serial: "",
        sapCode: null,
        asset: "",
        type: 0,
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
            setErrorST("Error loading the machine");
            return null;
        }
    }

    useEffect(() => {
        const load = async () => {
            if (isEditting && id) {
                await loadMachine(Number(id)).then((machine) => {
                    if (machine) {
                        setMachineST({...machine});
                    }
                });
            }
        };
        load();
    }, [id, isEditting]);

    const onSubmit = async (values: MachineFormData) => {
        try {
                setIsSubmittingST(true);
                setErrorST(null);
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
                setErrorST("Error of input value. " + error.message);
            }
            else if (axios.isAxiosError(error)) {
                console.error("Error submitting machine. Axios error:", error.response?.data);
                setErrorST(error.response?.data?.message || "An error occurred while saving the store.");
            }
        }
        finally {
            setIsSubmittingST(false);
        }
    }

    const buildBody = () => {
        if (!machineST) {
            return <div>Machine loading...</div>;
        }
        
        return (<MachineForm initialValues = {machineST} onSubmit = {onSubmit} isSubmitting = {isSubmittingST}/>);
    }

    if (!isEditting) {

        return (
            <>
            <div>Creating machine</div>
            <Link to ="/machines">Back to machines</Link>
            { errorST &&  <div className="error-message">{errorST}</div> }
            <MachineForm initialValues = {emptyMachineValues} onSubmit = {onSubmit} isSubmitting = {isSubmittingST}/>
            </>
        );
    }

    return (
    <>
        <div>Machine Detail</div>
        <Link to="/machines">Back to machines</Link>
        { errorST &&  <div className="error-message">{errorST}</div> }
        {buildBody()}
    </>);
}