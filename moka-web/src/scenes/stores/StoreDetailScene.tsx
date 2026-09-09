
import {useState} from "react";
import {Link, useNavigate} from "react-router-dom";
import { StoreForm, type StoreFormData } from "@/features/stores";
import {createStore} from "@/features/stores/api/storesApi";

export const StoreDetailScene = () => {

    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);
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
            setIsSubmitting(true);
            setError(null);
            const newStore = await createStore(values);
            console.log("Store created", newStore);
            navigate("/stores", {state: {message: "Store created successfully!"}});
        }
        catch (error) {
            console.error("Error creating store:", error);
            setError("Error creating the store");
        }
        finally {
            setIsSubmitting(false);
        }
    }

    return (
        <div>
            <h3>Store Detail</h3>
            <Link to="/stores">Back to Stores</Link>
            { error &&  <div>{error}</div> }
            <StoreForm initialValues = {initialValues} onSubmit = {onSubmit} isSubmitting = {isSubmitting}/>
        </div>
    )
}