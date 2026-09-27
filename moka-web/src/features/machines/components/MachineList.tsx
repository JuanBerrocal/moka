
import {useState, useEffect} from "react";
import type {MachineDto} from "../types/MachineDto";
import {getMachines} from "./../api/machinesApi";
import {MachineListItem} from "./MachineListItem";

export const MachineList: React.FC = () => {

    const [machines, setMachines] = useState<MachineDto[]>([]);
    const [error, setError] = useState<string | null>(null);
    
    /* To be implemented later for pagination
      const [currentPage, setCurrentPage] = useState(1);
      const [totalItems, setTotalItems] = useState(0);
      const pageSize = 20;
      */
    
    useEffect(() => 
      {
        const loadMachines = async () => {
          try {
            const result = await getMachines();
            setMachines(result);
          } 
          catch (error) {
            console.error("Error loading machines:", error);
            setError("Failed to load machines. Please try again later.");
          }
        };

      loadMachines();    

      }, []);
    
    
    // const totalPages = Math.ceil(totalItems / pageSize);

    /* To be implemented later for delete functionality
    const handleDelete = async (id: number) => {
        try {
            // Here you would call your API to delete the store
            await deleteStore(id);
            setStores((prevStores) => prevStores.filter((store) => store.id !== id));
        } catch (error) {
            console.error("Error deleting store:", error);
        }
    }
    */

    return (
    <div>
      <h3>Machines</h3>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <table>
        <thead>
          <tr>
            <th>Model</th>
            <th>Serial Number</th>
            <th>Asset</th>
            <th>State</th>
          </tr>
        </thead>
        <tbody>
          {machines.map(machine => <MachineListItem key={machine.id } machine={machine} /> )}
        </tbody>
      </table>
       </div>
    );
}
