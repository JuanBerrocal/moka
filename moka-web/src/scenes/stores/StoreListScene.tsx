
import { useEffect, useState } from "react";
import type {StoreDto} from "@/types/StoreDto";

const StoreListScene: React.FC = () => {

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

    return ();
}

export {StoreListScene};