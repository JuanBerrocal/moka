import {Link} from "react-router-dom";
import type {MachineDto} from "../types/machineDtos";

interface Props {
    machine: MachineDto;
    onDelete: (id: number) => void;
}

export const MachineListItem = ({machine, onDelete}: Props) => {
    
    return (
        <tr>
            <td style={{ textAlign: "left" }}>{machine.model}</td>
            <td style={{ textAlign: "left" }}>{machine.serial}</td>
            <td style={{ textAlign: "left" }}>{machine.asset}</td>
            <td style={{ textAlign: "left" }}>{machine.state}</td>
            <td style={{ textAlign: "left" }}>{machine.type}</td>
            <td><Link to={`/machines/${machine.id}/edit`}>Edit</Link></td>
            <td><button onClick={() => onDelete(machine.id)}>Delete</button></td>
        </tr>);
}