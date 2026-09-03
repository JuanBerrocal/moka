

import type {StoreDto} from "../types/StoreDto";

interface Props {
    store: StoreDto;
}

export const StoreListItem = ({store}: Props) => {
    
    return (
        <tr>
            <td style={{ textAlign: "left" }}>{store.sapCode}</td>
            <td style={{ textAlign: "left" }}>{store.name}</td>
            <td style={{ textAlign: "left" }}>{store.tradeName}</td>
            <td>{store.city}</td>
        </tr>);
}