
import {useState, useEffect} from "react";
import type {StoreDto} from "./../types/StoreDto";
import {getStores, deleteStore} from "./../api/storesApi";
import {StoreListItem} from "./StoreListItem";

export const StoreList: React.FC = () => {

    const [stores, setStores] = useState<StoreDto[]>([]);
      const [currentPage, setCurrentPage] = useState(1);
      const [totalItems, setTotalItems] = useState(0);
      const pageSize = 20;
    
      useEffect(() => {
        try {
          const loadStores = async () => {
            const storeResponse = await getStores(currentPage, pageSize);
            setStores(storeResponse.items);
            setTotalItems(storeResponse.totalItems);
          };
          loadStores();
        } 
        catch (error) {
          console.error("Error loading stores:", error);
        }
          
      }, [currentPage]);
    
    const totalPages = Math.ceil(totalItems / pageSize);

    const handleDelete = async (id: number) => {
        try {
            // Here you would call your API to delete the store
            await deleteStore(id);
            setStores((prevStores) => prevStores.filter((store) => store.id !== id));
        } catch (error) {
            console.error("Error deleting store:", error);
        }
    }
    
    return (
    <div>
      <h3>Stores</h3>
      <table>
        <thead>
          <tr>
            <th>SAP Code</th>
            <th>Name</th>
            <th>Trade Name</th>
            <th>City</th>
          </tr>
        </thead>
        <tbody>
          {stores.map(store => <StoreListItem key={store.id } store={store} onDelete={handleDelete}/> )}
        </tbody>
      </table>
      <div style={{ display: "flex", justifyContent: "space-between", marginTop: "20px" }}>
        <button onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1)) } disabled={currentPage === 1}>Previous</button>
        <span>Page {currentPage} of {totalPages}</span>
        <button onClick={() => setCurrentPage((prev) => (prev >= totalPages ) ? prev : prev + 1)} disabled={currentPage >= totalPages}>Next</button>
      </div>
    </div>
    );
}
