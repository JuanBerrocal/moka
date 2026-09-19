
import axios from "axios";
import {useState, useEffect} from "react";
import {Link, useNavigate, useParams} from "react-router-dom";
import { StoreForm, type StoreFormData } from "@/features/stores";
import {createStore, getStoreById, updateStore} from "@/features/stores/api/storesApi";
import {type StoreDto} from "@/features/stores/types/StoreDto";

export const StoreDetailScene = () => {

    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [store, setStore] = useState<StoreFormData | null>(null);

    const navigate = useNavigate();
    const {id} = useParams<{id: string}>();
    console.log("Store id:", id);
    const isEditMode = Boolean(id);

    const emptyStoreValues: StoreFormData = {
            name: "",
            sapCode: "",
            tradeName: "",
            address: "",
            postalCode: "",
            city: "",
            taxId: "",
            notes: "",
        };

    const loadStore = async (id: number): Promise<StoreDto | null> => {
        try {
            const store = await getStoreById(id);
            return store;
        }
        catch (error) {
            console.error("Error loading store:", error);
            setError("Error loading the store");
            return null;
        }
    }

    useEffect(() => {
            const load = async () => {
                if (isEditMode && id) {
                    await loadStore(Number(id)).then((store) => {
                        if (store) {
                            setStore({...store});
                        }
                    });
                }
            };

        load();
    },[id, isEditMode]);

    const onSubmit = async (values: StoreFormData) => {
        try {
            setIsSubmitting(true);
            setError(null);

            if (!isEditMode) {
                const newStore = await createStore(values);
                console.log("Store created", newStore);
                navigate("/stores", {state: {message: "Store created successfully!"}});
            }
            else {
                await updateStore(Number(id), values);
                // Implement update store logic here
                console.log("Updating store with values:", values);
                // You would typically call an updateStore API function here
                navigate("/stores", {state: {message: "Store updated successfully!"}});
            }
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                console.error("Axios error:", error.response?.data);
                setError(error.response?.data?.message || "An error occurred while saving the store.");
            }
        }
        finally {
            setIsSubmitting(false);
        }
    }
    
    if (!isEditMode) {
        return (
        <div>
            <h3>Creating New Store</h3>
            <Link to="/stores">Back to Stores</Link>
            { error &&  <div>{error}</div> }
            <StoreForm initialValues = {emptyStoreValues} onSubmit = {onSubmit} isSubmitting = {isSubmitting}/>
        </div>
        )
    }

    const buildBody = () => {
        if (error) {
            return (<><div>{error}</div> </>);
        }
        else if (!store) {
            return (<div>Loading store...</div>);
            }
        else {
            return (<StoreForm initialValues = {store} onSubmit = {onSubmit} isSubmitting = {isSubmitting}/>);
        }
    }

    return (
        <div>
            <h3>Editing Store</h3>
            <Link to="/stores">Back to Stores</Link>
            {buildBody()}
        </div>
    )
}