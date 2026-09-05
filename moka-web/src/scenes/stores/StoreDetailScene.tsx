

import {Link} from "react-router-dom";
import { StoreForm, type StoreFormData } from "@/features/stores";

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
        console.log("Form submitted with values:", values);
    }

    return (
        <div>
            <h3>Store Detail</h3>
            <Link to="/stores">Back to Stores</Link>
            <StoreForm initialValues = {initialValues} onSubmit = {onSubmit}/>
        </div>
    )
}