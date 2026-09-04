import {useState} from "react";
import type {StoreFormData} from "@/features/stores/components/StoreFormData";


interface StoreFormProps {
    initialValues: StoreFormData;
    onSubmit: (values: StoreFormData) => void;
}

export const StoreForm = ({initialValues, onSubmit,}: StoreFormProps) => {
     const [formData, setFormData] = useState<StoreFormData>(initialValues);

     const handleSubmit = (event: React.SubmitEvent<HTMLFormElement>) => {

        event.preventDefault();
        onSubmit(formData);
     }

}