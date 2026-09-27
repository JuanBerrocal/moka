//import {Link} from "react-router-dom";
import type {MachineDto} from "../types/MachineDto";

interface Props {
    machine: MachineDto;
    //onDelete: (id: number) => void;
}

export const MachineListItem = ({machine}: Props) => {
    
    return (
        <tr>
            <td style={{ textAlign: "left" }}>{machine.model}</td>
            <td style={{ textAlign: "left" }}>{machine.serial}</td>
            <td style={{ textAlign: "left" }}>{machine.asset}</td>
            <td style={{ textAlign: "left" }}>{machine.state}</td>
            
        </tr>);
}