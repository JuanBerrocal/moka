
import {useState} from "react";

import type {MachineFormData} from '../';
import {MachineType, MachineState} from '../types/machineEnums';

interface MachineFormProps  {
    initialValues: MachineFormData;
    onSubmit: (values: MachineFormData) => void;
    isSubmitting: boolean;  
}

export const MachineForm = ({initialValues, onSubmit, isSubmitting} : MachineFormProps) => {

    const [formData, setFormData] = useState<MachineFormData>(initialValues);

    const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const {name, value} = event.target;
        setFormData((previousData) => ({...previousData, [name]: value}));
    }

    const handleSubmit = (event: React.SubmitEvent<HTMLFormElement>) => {
        event.preventDefault();
        onSubmit(formData);
    }

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="model">Model:</label>
                <input id="model" name="model" value={formData.model} onChange={handleChange} required />
            </div>
            <div>
                <label htmlFor="serial">Serial:</label>
                <input id="serial" name="serial" value={formData.serial} onChange={handleChange} />
            </div>
            <div>
                <label htmlFor="sapCode">SAP code:</label>
                <input id="sapCode" name="sapCode" value={formData.sapCode || ""} onChange={handleChange} />
            </div>
            <div>
                <label htmlFor="asset">Asset:</label>
                <input id="asset" name="asset" value={formData.asset} onChange={handleChange} />
            </div>
            <div>
                <label htmlFor="type">Machine Type:</label>
                <select id="type" name="type" value={formData.type} onChange={handleChange} required >
                    <option value={MachineType.Unknown}>Unknown</option>
                    <option value={MachineType.Machine}>Machine</option>
                    <option value={MachineType.Grinder}>Grinder</option>
                    <option value={MachineType.Vending}>Vending</option>
                    <option value={MachineType.Capsule}>Capsule</option>
                    <option value={MachineType.Juicer}>Juicer</option>
                    <option value={MachineType.Warmer}>Warmer</option>
                    <option value={MachineType.Refrigerator}>Refrigerator</option>
                    <option value={MachineType.Dispenser}>Dispenser</option>
                    <option value={MachineType.Cabinet}>Cabinet</option>
                    <option value={MachineType.Office}>Office</option>
                    <option value={MachineType.Others}>Others</option>
                </select>
            </div>
            <div>
                <label htmlFor="purchaseDate">Purchase Date:</label>
                <input id="purchaseDate" name="purchaseDate" type="date" value={formData.purchaseDate || ""} onChange={handleChange} />
            </div>
            <div>
                <label htmlFor="state">State:</label>
                <select id="state" name="state" value={formData.state} onChange={handleChange} required >
                    <option value={MachineState.Unknown}>Unknown</option>
                    <option value={MachineState.Ready}>Ready</option>
                    <option value={MachineState.NeedsReview}>NeedsReview</option>
                    <option value={MachineState.Useless}>Useless</option>
                </select>
            </div>
            <div>
                <label htmlFor="storeId">Store ID:</label>
                <input id="storeId" name="storeId" type="number" value={formData.storeId} onChange={handleChange} /> 
            </div>
            <div>
                <label htmlFor="assignment">Assignment:</label>
                <input id="assignment" name="assignment" value={formData.assignment} onChange={handleChange} />
            </div>
            <div>
                <label htmlFor="isExternal">Is External:</label>
                <input id="isExternal" name="isExternal" type="checkbox" checked={formData.isExternal} onChange={(e) => setFormData((previousData) => ({...previousData, isExternal: e.target.checked}))} />
            </div>
            <div>
                <textarea id="notes" name="notes" value={formData.notes || ""} onChange={handleChange} />
            </div>
            <div>
                <button type="submit" disabled={isSubmitting}>{(isSubmitting) ? "Saving..." : "Save"} </button>
            </div>
        </form>
    );
}