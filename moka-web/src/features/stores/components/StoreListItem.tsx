
import {Link} from "react-router-dom";
import type {StoreDto} from "../types/StoreDto";

interface Props {
    store: StoreDto;
    onDelete: (id: number) => void;
}

export const StoreListItem = ({store, onDelete}: Props) => {
    
    return (
        <tr>
            <td style={{ textAlign: "left" }}>{store.sapCode}</td>
            <td style={{ textAlign: "left" }}>{store.name}</td>
            <td style={{ textAlign: "left" }}>{store.tradeName}</td>
            <td style={{ textAlign: "left" }}>{store.city}</td>
            <td><Link to={`/stores/${store.id}/edit`}>Edit</Link></td>
            <td><button onClick={() => onDelete(store.id)}>Delete</button></td>
        </tr>);
}