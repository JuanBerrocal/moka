
import {useState, useEffect} from "react";
import type {MachineDto} from "../types/machineDtos";
import {getMachines, deleteMachine} from "./../api/machinesApi";
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

    const handleDelete = async (id: number) => {
        try {
            await deleteMachine(id);
            setMachines((prevMachines) => prevMachines.filter((machine) => machine.id !== id));
        } catch (error) {
            console.error("Error deleting machine:", error);
        }
    }

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
          {machines.map(machine => <MachineListItem key={machine.id } machine={machine} onDelete={handleDelete}/> )}
        </tbody>
      </table>
       </div>
    );
}
