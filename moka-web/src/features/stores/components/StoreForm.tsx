import {useState} from "react";
import type {StoreFormData} from "@/features/stores/components/StoreFormData";


interface StoreFormProps {
    initialValues: StoreFormData;
    onSubmit: (values: StoreFormData) => void;
}

export const StoreForm = ({initialValues, onSubmit,}: StoreFormProps) => {
     const [formData, setFormData] = useState<StoreFormData>(initialValues);

     const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const {name, value} = event.target;

        setFormData((previousData) => ( {...previousData, [name]: value} ));
     }

     const handleSubmit = (event: React.SubmitEvent<HTMLFormElement>) => {

        event.preventDefault();
        onSubmit(formData);
     }

     return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="name">Name:</label>
                <input id="name" name="name" value={formData.name} onChange = {handleChange} required/>
            </div>
            <div>
                <label htmlFor="sapCode">SAP code:</label>
                <input id="sapCode" name="sapCode" value={formData.sapCode} onChange = {handleChange} />
            </div>
            <div>
                <label htmlFor="tradeName">Trade Name:</label>
                <input id="tradeName" name="tradeName" value={formData.tradeName} onChange = {handleChange} />
            </div>
            <div>
                <label htmlFor="address">Address:</label>
                <input id="address" name="address" value={formData.address} onChange = {handleChange} />
            </div>
            <div>
                <label htmlFor="postalCode">Postal Code:</label>
                <input id="postalCode" name="postalCode" value={formData.postalCode} onChange = {handleChange} />
            </div>
            <div>
                <label htmlFor="city">City:</label>
                <input id="city" name="city" value={formData.city} onChange = {handleChange} />
            </div>
            <div>
                <label htmlFor="taxId">DNI/NIE/CIF:</label>
                <input id="taxId" name="taxId" value={formData.taxId} onChange = {handleChange} />
            </div>
            <div>
                <label htmlFor="taxId">Notes:</label>
                <textarea id="notes" name="notes" value={formData.notes} onChange = {handleChange} />
            </div>
            <button type="submit">Save</button>
        </form>
     );
}

