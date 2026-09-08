

import {Link, useNavigate} from "react-router-dom";
import { StoreForm, type StoreFormData } from "@/features/stores";
import {createStore} from "@/features/stores/api/storesApi";

export const StoreDetailScene = () => {

    const navigate = useNavigate();
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

    const onSubmit = async (values: StoreFormData) => {
        try {
            const newStore = await createStore(values);
            console.log("Store created", newStore);
            navigate("/stores", {state: {message: "Store created successfully!"}});
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