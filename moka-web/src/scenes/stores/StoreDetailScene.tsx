

import {Link} from "react-router-dom";
import { StoreForm, type StoreFormData } from "@/features/stores";
import {createStore} from "@/features/stores/api/storesApi";

export const StoreDetailScene = () => {

    const initialValues: StoreFormData = {
        name: "",
        sapCode: "",
        tradeName: "",
        address: "",
        postalCode: "",
        city: "",
        taxId: "",
        notes: "",
    };

    const onSubmit = (values: StoreFormData) => {
        try {
            createStore(values);
        }
        catch (error) {
            console.error("Error creating store:", error);
        }
    }

    return (
        <div>
            <h3>Store Detail</h3>
            <Link to="/stores">Back to Stores</Link>
            <StoreForm initialValues = {initialValues} onSubmit = {onSubmit}/>
        </div>
    )
}