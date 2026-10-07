
import {useEffect, useState} from "react";
import {Link, useParams} from "react-router-dom";

import type {MachineDto} from "@/features/machines/types/MachineDto";
import type {MachineFormData} from "@/features/machines/components/MachineFormData";
import {getMachineById} from "@/features/machines/api/machinesApi";

export const MachineDetailScene = () => {

    const [machine, setMachine] = useState<MachineDto | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [isSubmitting, setIsSubmitting] = useState(false);

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
        storeId: null,
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

    const onSubmit = () => {
        try {
            setIsSubmitting(true);
        }
        catch (error) {
            console.error("Error submitting machine: ", error);
            setError("Error submitting the machine");
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
        return (<></>);
        // return (<MachineForm initialValues = {machine} onSubmit = {onSubmit} isSubmitting = {isSubmitting}/>);
    }

    if (!isEditting) {

        return (
            <>
            <div>Creating machine</div>
            <Link to ="/machines">Back to machines</Link>
            <MachineForm initialValues = {emptyMachineValues} onSubmit = {onSubmit} isSubmitting = {isSubmitting}/>
            </>
        );
    }

    return (
    <>
        <div>Machine Detail</div>
        <Link to="/machines">Back to machines</Link>
        {buildBody()}
    </>);
}