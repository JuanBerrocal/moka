
import {useState, useEffect} from "react";
import type {StoreDto} from "./../types/StoreDto";
import {getStores} from "./../api/storesApi";

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
          {stores.map(store => (
            <tr key={store.id}>
              <td style={{ textAlign: "left" }}>{store.sapCode}</td>
              <td style={{ textAlign: "left" }}>{store.name}</td>
              <td style={{ textAlign: "left" }}>{store.tradeName}</td>
              <td>{store.city}</td>
            </tr>
          ))}
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